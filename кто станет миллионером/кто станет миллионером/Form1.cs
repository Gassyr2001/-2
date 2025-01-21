using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace кто_станет_миллионером
{
    public partial class Form1 : Form
    {
        private Random random;
        public Form1()
        {
            InitializeComponent();
            this.Icon = new System.Drawing.Icon("bag.ico");
            random = new Random();
        }

        private int znach = 0;
        private string prav_znach="";
        private int levels = 0;
        private int levels_vop_count = 0;
        private string levels_vop_str;
        private int randomVop;
        private int pomosh_drug=0;
        private void button_check(Button butt)
        {
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;
            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;
            button5.BackColor = Color.White;
            button5.ForeColor = Color.Black;
            if (znach != 0)
            {
                if (butt.BackColor == Color.Black && butt.ForeColor == Color.White)
                {
                    butt.BackColor = Color.White;
                    butt.ForeColor = Color.Black;
                }
                else
                {
                    butt.BackColor = Color.Black;
                    butt.ForeColor = Color.White;
                }
                button6.Text = "Проверить";
            }
            else
            {
                button6.Text = "Выберите вариант";
            }
        }
        private void button_check__prav_otv_btn(Button butt, int prav)
        {
            if(prav == 1) {
                butt.BackColor = Color.Green;
                butt.ForeColor = Color.Black;
            }else if (prav == 0)
            {
                butt.BackColor = Color.Red;
                butt.ForeColor = Color.Black;
            }
        }
        private void button_check_prav_otv(String zn,int prav)
        {
                if (zn == "1")
                {
                    button_check__prav_otv_btn(button2, prav);
                }
                else if (zn == "2")
                {
                    button_check__prav_otv_btn(button3, prav);
                }
                else if (zn == "3")
                {
                    button_check__prav_otv_btn(button4, prav);
                }
                else if (zn == "4")
                {
                    button_check__prav_otv_btn(button5, prav);
                }
            button_check_btn_enab(button2, false);
            button_check_btn_enab(button3, false);
            button_check_btn_enab(button4, false);
            button_check_btn_enab(button5, false);
        }
        private void button_check_btn_enab(Button butt, bool enab)
        {
            butt.Enabled = enab;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            znach = 1;
            button_check(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            znach = 2;
            button_check(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            znach = 3;
            button_check(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            znach = 4;
            button_check(button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "Проверить")
            {
                if (prav_znach == znach.ToString())
                {
                    button_check_prav_otv(znach.ToString(), 1);
                    if (levels >= 10)
                    {
                        button6.Text = "Начать заново";
                        MessageBox.Show("Вы  выиграли " + label4.Text + "!!!");
                        levels = 0;
                    }
                    else
                    {
                        button6.Text = "Следующий вопрос";
                    }
                }
                else
                {
                    button_check_prav_otv(znach.ToString(), 0);
                    button_check_prav_otv(prav_znach, 1);
                    MessageBox.Show("Вы проиграли !!!");
                    button6.Text = "Начать заново";
                    levels = 0;
                }
            } else if (button6.Text == "Выберите вариант")
            {
                if (znach == 0)
                {
                    MessageBox.Show("Выберите варианта !!!");
                }
            }
            else
            {
                znach = 0;
                prav_znach = "";
                levels++;

                button_check_btn_enab(button2, true);
                button_check_btn_enab(button3, true);
                button_check_btn_enab(button4, true);
                button_check_btn_enab(button5, true);

                if (levels == 1)
                {
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                    checkBox3.Enabled = true;
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                }
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                panel1.Visible = false;

                label6.Text = levels.ToString();
                button_check(button6);

                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "millionaire_level.txt");
                string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "millionaire_questions.txt");

                if (File.Exists(filePath))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            string line = null;

                            for (int i = 0; i < levels - 1; i++)
                            {
                                reader.ReadLine();
                            }
                            line = reader.ReadLine();

                            if (line != null)
                            {
                                label4.Text = line;
                            }
                            else
                            {
                                MessageBox.Show("строки нет в файле.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Файл не найден.");
                }

                if (File.Exists(filePath2))
                {
                    try
                    {
                        using (StreamReader reader3 = new StreamReader(filePath2))
                        {
                            while ((levels_vop_str = reader3.ReadLine()) != null)
                            {
                                reader3.ReadLine();
                                reader3.ReadLine();
                                reader3.ReadLine();
                                reader3.ReadLine();
                                reader3.ReadLine();
                                reader3.ReadLine();
                                if (levels_vop_str == levels.ToString())
                                {
                                    levels_vop_count++;
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                    }

                    int[] stroki = new int[levels_vop_count];
                    try
                    {
                        using (StreamReader reader4 = new StreamReader(filePath2))
                        {
                            levels_vop_count = 0;
                            int levels_vop_num_str = 0;
                            while ((levels_vop_str = reader4.ReadLine()) != null)
                            {
                                reader4.ReadLine();
                                reader4.ReadLine();
                                reader4.ReadLine();
                                reader4.ReadLine();
                                reader4.ReadLine();
                                reader4.ReadLine();
                                if (levels_vop_str == levels.ToString())
                                {
                                    stroki[levels_vop_count] = levels_vop_num_str;
                                    levels_vop_count++;
                                }
                                levels_vop_num_str += 7;
                            }
                            randomVop = random.Next(0, levels_vop_count);
                            //MessageBox.Show("Количество вопросов: " + randomVop);
                            levels_vop_count = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                    }


                    try
                    {
                        using (StreamReader reader2 = new StreamReader(filePath2))
                        {
                            //if (levels != 1) {
                            for (int v = 0; v < stroki[randomVop]; v++)
                            {
                                reader2.ReadLine();
                            }
                            //}
                            reader2.ReadLine();
                            button1.Text = reader2.ReadLine();
                            button2.Text = reader2.ReadLine();
                            button3.Text = reader2.ReadLine();
                            button4.Text = reader2.ReadLine();
                            button5.Text = reader2.ReadLine();
                            prav_znach=reader2.ReadLine();
                        }
                        levels_vop_count = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Файл не найден.");
                }

            }
        }
        int randvar2 = 0;
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                randvar2 = int.Parse(prav_znach);
                while (randvar2 == int.Parse(prav_znach))
                {
                    randvar2 = random.Next(1, 5);
                }
                if (randvar2 != 1 && prav_znach != "1")
                {
                    button2.Visible = false;
                }
                if (randvar2 != 2 && prav_znach != "2")
                {
                    button3.Visible = false;
                }
                if (randvar2 != 3 && prav_znach != "3")
                {
                    button4.Visible = false;
                }
                if (randvar2 != 4 && prav_znach != "4")
                {
                    button5.Visible = false;
                }
                checkBox3.Enabled = false;
            }
        }

        private void checkBox2_proc_otv(int nom_poriad,int proc_poriad)
        {
            if (nom_poriad == 1)
            {
                button2.Text = button2.Text + " - " + proc_poriad + "%";
            } 
            else if (nom_poriad == 2)
            {
                button3.Text = button3.Text + " - " + proc_poriad + "%";
            }
            else if (nom_poriad == 3)
            {
                button4.Text = button4.Text + " - " + proc_poriad + "%";
            }
            else if (nom_poriad == 4)
            {
                button5.Text = button5.Text + " - " + proc_poriad + "%";
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                int randzal = int.Parse(prav_znach);
                int randzal2 = randzal;
                int randzal3 = randzal2;
                int randpro1, randpro2, randpro3, randpro4;
                while (randzal == int.Parse(prav_znach))
                {
                    randzal = random.Next(1, 5);
                }
                while (randzal2 == int.Parse(prav_znach) || randzal2 == randzal)
                {
                    randzal2 = random.Next(1, 5);
                }
                while (randzal3 == int.Parse(prav_znach) || randzal3 == randzal || randzal3 == randzal2)
                {
                    randzal3 = random.Next(1, 5);
                }
                randpro1= random.Next(1, 100);
                randpro2 = random.Next(1, 100 - randpro1);
                randpro3 = random.Next(1, 100 - randpro1 - randpro2);
                randpro4 = 100 - randpro1 - randpro2 - randpro3;
                checkBox2_proc_otv(int.Parse(prav_znach), randpro1);
                checkBox2_proc_otv(randzal, randpro2);
                checkBox2_proc_otv(randzal2, randpro3);
                checkBox2_proc_otv(randzal3, randpro4);
                checkBox2.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                panel1.Visible = true;
                checkBox1.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pomosh_drug = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pomosh_drug = 2;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pomosh_drug = 3;
        }

        private string Button_sel_text(string btnnum)
        {
            if (btnnum == "1")
            {
                return button2.Text;  
            }
            else if (btnnum == "2")
            {
                return button3.Text;  
            }
            else if (btnnum == "3")
            {
                return button4.Text;  
            }
            else if (btnnum == "4")
            {
                return button5.Text;  
            }
            else
            {
                return "Пусто";  
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (pomosh_drug == 0)
            {
                MessageBox.Show("Выберите друга !!!");
            }
            else
            {
                if (pomosh_drug == 1)
                {
                    MessageBox.Show("Ғасыр братан : " + Button_sel_text(prav_znach));
                }
                else if (pomosh_drug == 2) 
                {
                    if (randvar2 == 0)
                    {
                        randvar2 = int.Parse(prav_znach);
                        while (randvar2 == int.Parse(prav_znach))
                        {
                            randvar2 = random.Next(1, 5);
                        }
                        MessageBox.Show("Тема братишка : " + Button_sel_text(randvar2.ToString()));
                    }
                    else
                    {
                        MessageBox.Show("Тема братишка : " + Button_sel_text(randvar2.ToString()));
                    }
                }
                else if (pomosh_drug == 3)
                {
                    MessageBox.Show("Маке : Смотря как...");
                }
                panel1.Visible = false;
                pomosh_drug = 0;
            }
        }
    }
}

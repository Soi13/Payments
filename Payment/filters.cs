using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;

namespace Payment
{
    public partial class filters : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public static bool set_filter;
        public static string str_query;
      
        public filters()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                //checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                //checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;

            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox1.Checked = false;
                //checkBox2.Checked = false;
                //checkBox3.Checked = false;
                //checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;

            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                //checkBox4.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;

            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((checkBox1.Checked != true) && (checkBox2.Checked != true) && (checkBox3.Checked != true) && (checkBox4.Checked != true) && (checkBox5.Checked != true) && (checkBox6.Checked != true) && (checkBox7.Checked != true) && (checkBox8.Checked != true))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не выбраны параметры фильтрации!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            view_data view_data = (view_data)this.Owner;

            /////////////////////////////////////////////////
            if (checkBox1.Checked == true)
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {                    
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where POLUCHAT_PLATEJ='" + comboBox1.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where POLUCHAT_PLATEJ='"+comboBox1.Text+"'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text= "Режим фильтра включен";
                    
                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and POLUCHAT_PLATEJ='" + comboBox1.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and POLUCHAT_PLATEJ='"+comboBox1.Text+"'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    
                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if (checkBox2.Checked == true)
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where BRANCH='" + comboBox2.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where BRANCH='"+comboBox2.Text+"'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and BRANCH='" + comboBox2.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and BRANCH='"+comboBox2.Text+"'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if (checkBox3.Checked == true)
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where ISPOLNITEL='" + comboBox3.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where ISPOLNITEL='"+comboBox3.Text+"'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and ISPOLNITEL='" + comboBox3.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and ISPOLNITEL='"+comboBox3.Text+"'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if (checkBox4.Checked == true)
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103)", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103)", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if (checkBox5.Checked == true)
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where ARTICLE_BUDGET='" + comboBox5.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where ARTICLE_BUDGET='" + comboBox5.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and ARTICLE_BUDGET='" + comboBox5.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and ARTICLE_BUDGET='"+comboBox5.Text+"'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if (checkBox6.Checked == true)
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where DEPARTMENT_ZAYAVITEL='" + comboBox6.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where DEPARTMENT_ZAYAVITEL='" + comboBox6.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and DEPARTMENT_ZAYAVITEL='" + comboBox6.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and DEPARTMENT_ZAYAVITEL='" + comboBox6.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if (checkBox7.Checked == true)
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = false;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра отключен";
                    

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + "select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val, conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = false;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра отключен";
                    

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if ((checkBox4.Checked == true) && (checkBox5.Checked == true))
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and ARTICLE_BUDGET='" + comboBox5.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and ARTICLE_BUDGET='" + comboBox5.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and ARTICLE_BUDGET='" + comboBox5.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and ARTICLE_BUDGET='" + comboBox5.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if ((checkBox4.Checked == true) && (checkBox2.Checked == true))
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and BRANCH='" + comboBox2.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and BRANCH='" + comboBox2.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and BRANCH='" + comboBox2.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and BRANCH='" + comboBox2.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if ((checkBox4.Checked == true) && (checkBox3.Checked == true))
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and ISPOLNITEL='" + comboBox3.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and ISPOLNITEL='" + comboBox3.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";

                    this.Close();
                }
                else
                {
                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and ISPOLNITEL='" + comboBox3.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103) and ISPOLNITEL='" + comboBox3.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            /////////////////////////////////////////////////
            if (checkBox8.Checked == true)
            {
                if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                {
                    textBox1.Text = textBox1.Text.Replace(".", ",");
                    textBox1.Text = textBox1.Text.Replace(" ", string.Empty); //Обрезание пробелов с суммах.

                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where SUMM='" + textBox1.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where DEPARTMENT_ZAYAVITEL='" + textBox1.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
                else
                {
                    textBox1.Text = textBox1.Text.Replace(".", ",");
                    textBox1.Text = textBox1.Text.Replace(" ", string.Empty); //Обрезание пробелов с суммах.

                    SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + " and DEPARTMENT_ZAYAVITEL='" + textBox1.Text + "' select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val + "and DEPARTMENT_ZAYAVITEL='" + textBox1.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    view_data.dataGridView1.DataSource = ds.Tables[0];
                    view_data.statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    view_data.statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    //Установка фильтра в TRUE для возможности потом контролировать его состояние
                    set_filter = true;
                    str_query = command.CommandText;
                    view_data.label2.Text = "Режим фильтра включен";
                    

                    this.Close();
                }
            }
            /////////////////////////////////////////////////

            //Проверка статса заданий на платеж и пометка их красным цветом если они отвергнуты
            for (int s = 0; s <= view_data.dataGridView1.Rows.Count - 1; s++)
            {
                if (Convert.ToString(view_data.dataGridView1.Rows[s].Cells[27].Value) == "Отвергнуто")
                {
                    view_data.dataGridView1.Rows[s].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Проверка статса заданий на платеж и пометка их зеленым цветом если они одобрены
            for (int s1 = 0; s1 <= view_data.dataGridView1.Rows.Count - 1; s1++)
            {
                if (Convert.ToString(view_data.dataGridView1.Rows[s1].Cells[27].Value) == "Принято")
                {
                    view_data.dataGridView1.Rows[s1].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }

        private void filters_Load(object sender, EventArgs e)
        {
            //Заполнение поля Получатель, данными из БД
            SqlCommand command3 = conn.CreateCommand();
            command3.CommandText = "select NAIMENOVAN_KONTR from CATALOG_KONTRAGENT order by NAIMENOVAN_KONTR";
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataReader reader;
            reader = command3.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    string result = reader.GetString(0);
                    comboBox1.Items.Add(result);
                }
                catch { }

            }
            conn.Close();
            ////////////////////////

            //Заполнение поля Исполнитель, данными из БД
            SqlCommand command4 = conn.CreateCommand();
            command4.CommandText = "select distinct ISPOLNITEL from ZADANIE_PLAT order by ISPOLNITEL";
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataReader reader4;
            reader4 = command4.ExecuteReader();
            while (reader4.Read())
            {
                try
                {
                    string result4 = reader4.GetString(0);
                    comboBox3.Items.Add(result4);
                }
                catch { }

            }
            conn.Close();
            ////////////////////////

            //Заполнение поля Статья бюджета, данными из БД
            SqlCommand command2 = conn.CreateCommand();
            command2.CommandText = "select distinct CODE_BDDS, CODE_BDDS_SHORT, NAME_CODE_BDDS from BDDS_CODES";
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataReader reader1;
            reader1 = command2.ExecuteReader();
            while (reader1.Read())
            {
                try
                {
                    string result1 = reader1.GetString(0);
                    string result11 = reader1.GetString(1);
                    string result12 = reader1.GetString(2);
                    comboBox5.Items.Add(result1 + "     " + result11 + "     " + result12);
                }
                catch { }

            }
            conn.Close();
            ////////////////////////

            //Заполнение поля Подразделение, данными из БД
            SqlCommand command5 = conn.CreateCommand();
            command5.CommandText = "select PODR from PODR_ZAYAVIT order by PODR";
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataReader reader5;
            reader5 = command5.ExecuteReader();
            while (reader5.Read())
            {
                try
                {
                    string result5 = reader5.GetString(0);
                    comboBox6.Items.Add(result5);
                }
                catch { }

            }
            conn.Close();
            ////////////////////////
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox8.Checked = false;

            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox7.Checked = false;
                checkBox6.Checked = false;

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

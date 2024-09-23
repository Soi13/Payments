using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Data.SqlClient;

namespace Payment
{
    public partial class user_add : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public user_add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Проверка на существование контрагента в справочнике
            SqlCommand command = new SqlCommand("select * from USERS where USER_NAME='" + textBox1.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            da.Fill(ds, "USERS");
            if (ds.Tables[0].Rows.Count != 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Пользователь " + textBox1.Text + " уже существует в базе. Дублирование записей не возможно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (textBox1.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не заполнено поле \"Логин\".", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (textBox2.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не заполнено поле \"ФИО\".", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                
                return;
            }

            if (comboBox1.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не выбрано подразделение.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                                
                return;
            }

            if (comboBox2.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не выбран филиал.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string adm = "";
            if (checkBox1.Checked == true)
            {
                adm = "1";
            }
            else
            {
                adm = "0";
            }

            /////////Вставка данных в БД
            SqlCommand cm = conn.CreateCommand();
            cm.CommandText = "BEGIN TRANSACTION " +
                             "insert into USERS (USER_NAME,FULL_NAME,PASSW,DEPARTMENT,BRANCH,EMAIL,ADMINISTRATION) values ('" + textBox1.Text + "',  '" + textBox2.Text + "', '6ece4fd51bc113942692637d9d4b860e', '" + comboBox1.Text + "', '" + comboBox2.Text + "', '" + textBox3.Text + "', '" + adm + "')" +
                             " COMMIT TRANSACTION";
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataReader reader1;
            reader1 = cm.ExecuteReader();
            conn.Close();
            ///////////////////////////////

            SystemSounds.Beep.Play();
            MessageBox.Show("Пользователь добавлен успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            this.Close();

        }

        private void user_add_Load(object sender, EventArgs e)
        {
            //Заполнение поля Подразделение, данными из БД
            SqlCommand command4 = conn.CreateCommand();
            command4.CommandText = "select PODR from PODR_ZAYAVIT order by PODR";
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataReader reader2;
            reader2 = command4.ExecuteReader();
            while (reader2.Read())
            {
                try
                {
                    string result1 = reader2.GetString(0);
                    comboBox1.Items.Add(result1);
                }
                catch { }

            }
            conn.Close();
            ////////////////////////
        }
    }
}

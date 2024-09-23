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
    public partial class add_kontragent : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");
      
        public add_kontragent()
        {
            InitializeComponent();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Проверка на существование контрагента в справочнике
            SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT where INN='" + textBox2.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            da.Fill(ds, "CATALOG_KONTRAGENT");
            if (ds.Tables[0].Rows.Count != 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Контрагент "+ textBox1.Text +  " уже существует в справочнике. Дублирование записей не возможно! Если необходимо ввести такого же КА, но с другими реквизитами нажмите правой кнопкой мыши по наименованию КА и выберите пункт \"Добавить доп. реквизиты\"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
                      
            
            if (textBox1.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введено наименование контрагента!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (textBox2.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введен ИНН контрагента!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (textBox3.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введен КПП контрагента!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (textBox4.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введен счет контрагента!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (textBox5.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введено наименование банка контрагента!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (textBox6.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введен БИК банка контрагента!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (textBox7.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введен корр. счет банка!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (textBox8.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введен код R3!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            /////////Вставка данных в БД
            SqlCommand cm = conn.CreateCommand();
            cm.CommandText = "BEGIN TRANSACTION " +
                             "insert into CATALOG_KONTRAGENT (NAIMENOVAN_KONTR,INN,KPP,ACCOUNT,BANK_NAIMENOVAN,BIK,KORR_COUNT,USER_ID,DATETIME_CREATE,R3) values ('" + textBox1.Text + "',  '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + Form1.val + "', convert(datetime,'" + DateTime.Now.ToString() + "', 103), '"+ textBox8.Text +"')" +
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

            this.Close();
        }
    }
}

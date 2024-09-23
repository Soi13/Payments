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
    public partial class add_dop_rekvisit : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");
     
        public add_dop_rekvisit()
        {
            InitializeComponent();
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

        private void add_dop_rekvisit_Load(object sender, EventArgs e)
        {
            textBox1.Text = catalog_kontragent.naim;
            textBox2.Text = catalog_kontragent.inn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

            /////////Вставка данных в БД
            SqlCommand cm = conn.CreateCommand();
            cm.CommandText = "insert into CATALOG_KONTRAGENT_DOP_REKVISIT (ID_CATALOG_KONTRAGENT,INN,KPP,ACCOUNT,BANK_NAIMENOVAN,BIK,KORR_COUNT,USER_ID,DATETIME_CREATE) values (@ID_CATALOG_KONTRAGENT, @INN, @KPP, @ACCOUNT, @BANK_NAIMENOVAN, @BIK, @KORR_COUNT, @USER_ID, GETDATE())";
            cm.Parameters.AddWithValue("@ID_CATALOG_KONTRAGENT", catalog_kontragent.ka_id);
            cm.Parameters.AddWithValue("@INN", catalog_kontragent.inn);
            cm.Parameters.AddWithValue("@KPP", textBox3.Text);
            cm.Parameters.AddWithValue("@ACCOUNT", textBox4.Text);
            cm.Parameters.AddWithValue("@BANK_NAIMENOVAN", textBox5.Text);
            cm.Parameters.AddWithValue("@BIK", textBox6.Text);
            cm.Parameters.AddWithValue("@KORR_COUNT", textBox7.Text);
            cm.Parameters.AddWithValue("@USER_ID", Form1.val);
                                
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

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
    public partial class catalog_kontragent : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public static string ka_id;
        public static string ka_id_4_show;
        public static string naim;
        public static string naim_4_show;
        public static string inn;

        SqlDataAdapter da;

        public catalog_kontragent()
        {
            InitializeComponent();
        }

        //Заполнение DataGridView наименованиями полей 
        public void fill_gridview()
        {
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["ID"].Width = 40;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["NAIMENOVAN_KONTR"].HeaderText = "Наименование";
            dataGridView1.Columns["NAIMENOVAN_KONTR"].Width = 250;
            dataGridView1.Columns["INN"].HeaderText = "ИНН";
            dataGridView1.Columns["INN"].Width = 70;
            dataGridView1.Columns["KPP"].HeaderText = "КПП";
            dataGridView1.Columns["KPP"].Width = 70;
            dataGridView1.Columns["ACCOUNT"].HeaderText = "Счет";
            dataGridView1.Columns["ACCOUNT"].Width = 100;
            dataGridView1.Columns["BANK_NAIMENOVAN"].HeaderText = "Наименование банка";
            dataGridView1.Columns["BANK_NAIMENOVAN"].Width = 250;
            dataGridView1.Columns["BIK"].HeaderText = "БИК";
            dataGridView1.Columns["BIK"].Width = 70;
            dataGridView1.Columns["KORR_COUNT"].HeaderText = "Корр. счет";
            dataGridView1.Columns["KORR_COUNT"].Width = 100;
            dataGridView1.Columns["USER_ID"].HeaderText = "USER_ID";
            dataGridView1.Columns["USER_ID"].Width = 20;
            dataGridView1.Columns["USER_ID"].Visible = false;
            dataGridView1.Columns["DATETIME_CREATE"].HeaderText = "DATETIME_CREATE";
            dataGridView1.Columns["DATETIME_CREATE"].Width = 20;
            dataGridView1.Columns["DATETIME_CREATE"].Visible = false;
            
        }

        private void catalog_kontragent_Load(object sender, EventArgs e)
        {
            if (Form1.administration == "1")
            {
                button2.Enabled = true;
            }

            SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT", conn);
            da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "CATALOG_KONTRAGENT");
            dataGridView1.DataSource = ds.Tables[0];
            
            statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
            fill_gridview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add_kontragent add_kontragent = new add_kontragent();
            add_kontragent.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить контрагента \"" + dataGridView1.CurrentRow.Cells[1].Value + "\"?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                SqlCommand scmd5 = conn.CreateCommand();
                scmd5.CommandText = "delete from CATALOG_KONTRAGENT where id='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                try
                {
                    conn.Open();
                }
                catch { }
                SqlDataReader reader5;
                reader5 = scmd5.ExecuteReader();
                conn.Close();
                
                //Обновление данных в Гриде после удаления позиции
                SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT", conn);
                da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "CATALOG_KONTRAGENT");
                dataGridView1.DataSource = ds.Tables[0];

                fill_gridview();

                SystemSounds.Beep.Play();
                MessageBox.Show("Контрагент удален успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Text = "Включен режим редактирования";
            button4.Visible = true;
            dataGridView1.ReadOnly = false;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                da.Update((System.Data.DataTable)dataGridView1.DataSource);
                button3.Enabled = true;
                button3.Text = "Редактирование";
                button4.Visible = false;
                SystemSounds.Beep.Play();
                MessageBox.Show("Изменения в базе данных выполнены!", "Уведомление о результатах", MessageBoxButtons.OK);

            }
            catch (Exception)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Изменения в базе данных выполнить не удалось!", "Уведомление о результатах", MessageBoxButtons.OK);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((checkBox1.Checked == false) && (checkBox2.Checked == false))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не выбраны режимы поиска!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Clear();                
                return;
            }

            if (checkBox1.Checked == true)
            {
                SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT where NAIMENOVAN_KONTR like '%" + textBox1.Text + "%'", conn);
                da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "CATALOG_KONTRAGENT");
                dataGridView1.DataSource = ds.Tables[0];

                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);

                fill_gridview();
            }

            if (checkBox2.Checked == true)
            {
                SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT where INN like '%" + textBox1.Text + "%'", conn);
                da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "CATALOG_KONTRAGENT");
                dataGridView1.DataSource = ds.Tables[0];

                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);

                fill_gridview();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }

        private void добавитьДопРеквизитыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ka_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            naim = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            inn = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            add_dop_rekvisit add_dop_rekvisit = new add_dop_rekvisit();
            add_dop_rekvisit.ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ka_id_4_show = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            naim_4_show = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            catalog_kontragent_dop_rekvisit catalog_kontragent_dop_rekvisit = new catalog_kontragent_dop_rekvisit();
            catalog_kontragent_dop_rekvisit.ShowDialog();
        }

        private void просмотрДопРеквизитовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ka_id_4_show = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            naim_4_show = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            catalog_kontragent_dop_rekvisit catalog_kontragent_dop_rekvisit = new catalog_kontragent_dop_rekvisit();
            catalog_kontragent_dop_rekvisit.ShowDialog();
        }
        
    }
}

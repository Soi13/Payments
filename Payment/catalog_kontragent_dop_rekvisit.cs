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
    public partial class catalog_kontragent_dop_rekvisit : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public catalog_kontragent_dop_rekvisit()
        {
            InitializeComponent();
        }

        //Заполнение DataGridView наименованиями полей 
        public void fill_gridview()
        {
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["ID"].Width = 40;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["ID_CATALOG_KONTRAGENT"].HeaderText = "ID_CATALOG_KONTRAGENT";
            dataGridView1.Columns["ID_CATALOG_KONTRAGENT"].Width = 250;
            dataGridView1.Columns["ID_CATALOG_KONTRAGENT"].Visible=false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void catalog_kontragent_dop_rekvisit_Load(object sender, EventArgs e)
        {
            label2.Text = "Контрагент: " + catalog_kontragent.naim_4_show;

            SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT_DOP_REKVISIT where ID_CATALOG_KONTRAGENT="+catalog_kontragent.ka_id_4_show, conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "CATALOG_KONTRAGENT_DOP_REKVISIT");
            dataGridView1.DataSource = ds.Tables[0];

            statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
            fill_gridview();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Проверка на существование записей для удаления
            SqlCommand command1 = new SqlCommand("select * from CATALOG_KONTRAGENT_DOP_REKVISIT where ID_CATALOG_KONTRAGENT=" + catalog_kontragent.ka_id_4_show, conn);
            SqlDataAdapter da1 = new SqlDataAdapter(command1);//Переменная объявлена как глобальная
            SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
            DataSet ds1 = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da1.Fill(ds1, "CATALOG_KONTRAGENT_DOP_REKVISIT");

            if (ds1.Tables[0].Rows.Count == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Удалять нечего!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить данную запись?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlCommand scmd5 = conn.CreateCommand();
                scmd5.CommandText = "delete from CATALOG_KONTRAGENT_DOP_REKVISIT where id='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                try
                {
                    conn.Open();
                }
                catch { }
                SqlDataReader reader5;
                reader5 = scmd5.ExecuteReader();
                conn.Close();

                //Обновление данных в Гриде после удаления позиции
                SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT_DOP_REKVISIT where ID_CATALOG_KONTRAGENT="+catalog_kontragent.ka_id_4_show, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "CATALOG_KONTRAGENT_DOP_REKVISIT");
                dataGridView1.DataSource = ds.Tables[0];
                
                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                fill_gridview();

                SystemSounds.Beep.Play();
                MessageBox.Show("Запись удалена успешно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

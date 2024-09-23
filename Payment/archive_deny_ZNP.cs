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
    public partial class archive_deny_ZNP : Form
    {
          SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

          public static object value_4_scan1;

        public archive_deny_ZNP()
        {
            InitializeComponent();
        }

        //Заполнение DataGridView наименованиями полей 
        public void fill_gridview()
        {
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["ID"].Width = 20;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["USER_ID"].HeaderText = "ФИО";
            dataGridView1.Columns["USER_ID"].Width = 20;
            dataGridView1.Columns["USER_ID"].Visible = false;
            dataGridView1.Columns["PAYER"].HeaderText = "Плательщик";
            dataGridView1.Columns["PAYER"].Width = 20;
            dataGridView1.Columns["PAYER"].Visible = false;
            dataGridView1.Columns["BRANCH"].HeaderText = "Подразделение";
            dataGridView1.Columns["BRANCH"].Width = 105;
            dataGridView1.Columns["ISPOLNITEL"].HeaderText = "Исполнитель";
            dataGridView1.Columns["ISPOLNITEL"].Width = 290;
            dataGridView1.Columns["PLAN_DATE_PAYMENT"].HeaderText = "Дата платежа";
            dataGridView1.Columns["PLAN_DATE_PAYMENT"].Width = 80;
            dataGridView1.Columns["SUMM"].HeaderText = "Сумма платежа";
            dataGridView1.Columns["SUMM"].Width = 100;
            dataGridView1.Columns["NDS"].HeaderText = "НДС";
            dataGridView1.Columns["NDS"].Width = 60;
            dataGridView1.Columns["NAZNACHEN_PLATEJ"].HeaderText = "NAZNACHEN_PLATEJ";
            dataGridView1.Columns["NAZNACHEN_PLATEJ"].Width = 20;
            dataGridView1.Columns["NAZNACHEN_PLATEJ"].Visible = false;
            dataGridView1.Columns["OBOSNOVANIE"].HeaderText = "OBOSNOVANIE";
            dataGridView1.Columns["OBOSNOVANIE"].Width = 20;
            dataGridView1.Columns["OBOSNOVANIE"].Visible = false;
            dataGridView1.Columns["POLUCHAT_PLATEJ"].HeaderText = "Получатель платежа";
            dataGridView1.Columns["POLUCHAT_PLATEJ"].Width = 350;
            dataGridView1.Columns["INN_POLUCHATEL"].HeaderText = "INN_POLUCHATEL";
            dataGridView1.Columns["INN_POLUCHATEL"].Width = 20;
            dataGridView1.Columns["INN_POLUCHATEL"].Visible = false;
            dataGridView1.Columns["KPP_POLUCHATEL"].HeaderText = "KPP_POLUCHATEL";
            dataGridView1.Columns["KPP_POLUCHATEL"].Width = 20;
            dataGridView1.Columns["KPP_POLUCHATEL"].Visible = false;
            dataGridView1.Columns["ACCOUNT_POLUCHATEL"].HeaderText = "ACCOUNT_POLUCHATEL";
            dataGridView1.Columns["ACCOUNT_POLUCHATEL"].Width = 20;
            dataGridView1.Columns["ACCOUNT_POLUCHATEL"].Visible = false;
            dataGridView1.Columns["BANK_NAIMENOVAN"].HeaderText = "BANK_NAIMENOVAN";
            dataGridView1.Columns["BANK_NAIMENOVAN"].Width = 20;
            dataGridView1.Columns["BANK_NAIMENOVAN"].Visible = false;
            dataGridView1.Columns["BIK_BANK_POLUCHATEL"].HeaderText = "BIK_BANK_POLUCHATEL";
            dataGridView1.Columns["BIK_BANK_POLUCHATEL"].Width = 20;
            dataGridView1.Columns["BIK_BANK_POLUCHATEL"].Visible = false;
            dataGridView1.Columns["KOR_ACCOUNT_POLUCHATEL"].HeaderText = "KOR_ACCOUNT_POLUCHATEL";
            dataGridView1.Columns["KOR_ACCOUNT_POLUCHATEL"].Width = 20;
            dataGridView1.Columns["KOR_ACCOUNT_POLUCHATEL"].Visible = false;
            dataGridView1.Columns["ARTICLE_BUDGET"].HeaderText = "ARTICLE_BUDGET";
            dataGridView1.Columns["ARTICLE_BUDGET"].Width = 20;
            dataGridView1.Columns["ARTICLE_BUDGET"].Visible = false;
            dataGridView1.Columns["DEPARTMENT_ZAYAVITEL"].HeaderText = "DEPARTMENT_ZAYAVITEL";
            dataGridView1.Columns["DEPARTMENT_ZAYAVITEL"].Width = 20;
            dataGridView1.Columns["DEPARTMENT_ZAYAVITEL"].Visible = false;
            dataGridView1.Columns["BOSS_DEPARTMENT"].HeaderText = "BOSS_DEPARTMENT";
            dataGridView1.Columns["BOSS_DEPARTMENT"].Width = 20;
            dataGridView1.Columns["BOSS_DEPARTMENT"].Visible = false;
            dataGridView1.Columns["OTVETSTVENN_PFM_CFO"].HeaderText = "OTVETSTVENN_PFM_CFO";
            dataGridView1.Columns["OTVETSTVENN_PFM_CFO"].Width = 20;
            dataGridView1.Columns["OTVETSTVENN_PFM_CFO"].Visible = false;
            dataGridView1.Columns["GL_BUH"].HeaderText = "GL_BUH";
            dataGridView1.Columns["GL_BUH"].Width = 20;
            dataGridView1.Columns["GL_BUH"].Visible = false;
            dataGridView1.Columns["BOSS_RESURS_OBESPECHEN"].HeaderText = "BOSS_RESURS_OBESPECHEN";
            dataGridView1.Columns["BOSS_RESURS_OBESPECHEN"].Width = 20;
            dataGridView1.Columns["BOSS_RESURS_OBESPECHEN"].Visible = false;
            dataGridView1.Columns["KAZNACHEYSTVO"].HeaderText = "KAZNACHEYSTVO";
            dataGridView1.Columns["KAZNACHEYSTVO"].Width = 20;
            dataGridView1.Columns["KAZNACHEYSTVO"].Visible = false;
            dataGridView1.Columns["BOSS_KAZNACHEYSTVO"].HeaderText = "BOSS_KAZNACHEYSTVO";
            dataGridView1.Columns["BOSS_KAZNACHEYSTVO"].Width = 20;
            dataGridView1.Columns["BOSS_KAZNACHEYSTVO"].Visible = false;
            dataGridView1.Columns["DATETIME_CREATE"].HeaderText = "Дата создания";
            dataGridView1.Columns["DATETIME_CREATE"].Width = 100;
            //dataGridView1.Columns["DATETIME_CREATE"].Visible = false;
            dataGridView1.Columns["PERIOD"].HeaderText = "Период";
            dataGridView1.Columns["PERIOD"].Width = 20;
            dataGridView1.Columns["PERIOD"].Visible = false;
            dataGridView1.Columns["STATUS"].HeaderText = "Статус";
            dataGridView1.Columns["STATUS"].Width = 100;
            dataGridView1.Columns["TEXT_DENY"].HeaderText = "TEXT_DENY";
            dataGridView1.Columns["TEXT_DENY"].Width = 20;
            dataGridView1.Columns["TEXT_DENY"].Visible = false;
            dataGridView1.Columns["DATE_UPDATE"].HeaderText = "DATE_UPDATE";
            dataGridView1.Columns["DATE_UPDATE"].Width = 20;
            dataGridView1.Columns["DATE_UPDATE"].Visible = false;
            dataGridView1.Columns["NOTES"].HeaderText = "NOTES";
            dataGridView1.Columns["NOTES"].Width = 20;
            dataGridView1.Columns["NOTES"].Visible = false;
            dataGridView1.Columns["ZADANIE_PLAT_ID"].HeaderText = "ZADANIE_PLAT_ID";
            dataGridView1.Columns["ZADANIE_PLAT_ID"].Width = 20;
            dataGridView1.Columns["ZADANIE_PLAT_ID"].Visible = false;
            dataGridView1.Columns["SROK_OPLAT"].HeaderText = "Срок оплаты";
            dataGridView1.Columns["SROK_OPLAT"].Width = 60;
            dataGridView1.Columns["PAY_STATUS"].HeaderText = "Статус оплаты";
            dataGridView1.Columns["PAY_STATUS"].Width = 60;   
        }
        //////////////////////////////////////////////////////

        private void archive_deny_ZNP_Load(object sender, EventArgs e)
        {
            label12.Text = Form1.name_user;

            if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
            {
                SqlCommand command = new SqlCommand("select * from ARCHIVE_ZADANIE_PLAT_DENY", conn);
                SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "ARCHIVE_ZADANIE_PLAT_DENY");
                dataGridView1.DataSource = ds.Tables[0];

                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);

                fill_gridview();
            }
            else
            {
                SqlCommand command = new SqlCommand("select * from ARCHIVE_ZADANIE_PLAT_DENY where USER_ID=" + Form1.val, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "ARCHIVE_ZADANIE_PLAT_DENY");
                dataGridView1.DataSource = ds.Tables[0];

                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);

                fill_gridview();
            }

            //Проверка статса заданий на платеж и пометка их красным цветом если они отвергнуты
            for (int s = 0; s <= dataGridView1.Rows.Count - 1; s++)
            {
                if (Convert.ToString(dataGridView1.Rows[s].Cells[27].Value) == "Отвергнуто")
                {
                    dataGridView1.Rows[s].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Проверка статса заданий на платеж и пометка их зеленым цветом если они одобрены
            for (int s1 = 0; s1 <= dataGridView1.Rows.Count - 1; s1++)
            {
                if (Convert.ToString(dataGridView1.Rows[s1].Cells[27].Value) == "Принято")
                {
                    dataGridView1.Rows[s1].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ////////////Проверка, существует ли привязанные файлы у записи перед отображением
            try
            {
                conn.Open();
            }
            catch { }
            SqlCommand mycommand = new SqlCommand("select * from IMAGES where ZADANIE_PLAT_ID=" + dataGridView1.CurrentRow.Cells[31].Value, conn);
            SqlDataReader sqlDataReader = mycommand.ExecuteReader();
            if (!sqlDataReader.HasRows)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Отображать нечего! К данной записи файлы не привязаны.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                conn.Close();
                return;
            }
            ////////////////////////////////////////////////////////////////////////////
            conn.Close();

            value_4_scan1 = dataGridView1.CurrentRow.Cells[31].Value;

            list_of_scan_archive_deny list_of_scan_archive_deny = new list_of_scan_archive_deny();
            list_of_scan_archive_deny.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ///////////////////////
            if (dataGridView1.CurrentRow.Cells[27].Value.ToString() == "Отвергнуто")
            {
                richTextBox1.Visible = true;
                label3.Visible = true;
                richTextBox1.Text = dataGridView1.CurrentRow.Cells[28].Value.ToString();

            }
            else
            {
                richTextBox1.Visible = false;
                label3.Visible = false;
                richTextBox1.Clear();
            }
            ////////////////////


            richTextBox2.Text = dataGridView1.CurrentRow.Cells[30].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}

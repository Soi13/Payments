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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Payment
{
    public partial class view_data : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");
 
        public SqlDataAdapter da;
        public static object value;
        public static object value_4_scan;
        public static object value_4_deny;
        public static string value_4_deny_del_image;
        public static int st;
        public static string value_4_edit;
        public static int value_4_deny_return_position;   
        
        private Microsoft.Office.Interop.Excel.Application ObjExcel;
        private Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
        private Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
       
        public view_data()
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
            dataGridView1.Columns["ISPOLNITEL"].Width = 250;
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
            dataGridView1.Columns["POLUCHAT_PLATEJ"].Width = 290;
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
            dataGridView1.Columns["SROK_OPLAT"].HeaderText = "Срок оплаты";
            dataGridView1.Columns["SROK_OPLAT"].Width = 60;
            dataGridView1.Columns["PAY_STATUS"].HeaderText = "Статус оплаты";
            dataGridView1.Columns["PAY_STATUS"].Width = 60;         
            
        }
        //////////////////////////////////////////////////////

        
        //Обновление данных в гриде после ввода новой записи
        public void refill()
        {
            
                //Обновление данных на форме просмотра данных после ввода нового задания на платеж
                 if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
                 {
                     SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT", conn);
                    SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da.Fill(ds, "ZADANIE_PLAT");
                    dataGridView1.DataSource = ds.Tables[0];

                    statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                    statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                    fill_gridview();
                 }
                 else
                 {
                     SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + "select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val, conn);
                     da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                     SqlCommandBuilder cb = new SqlCommandBuilder(da);
                     DataSet ds = new DataSet();
                     conn.Close();
                     //Заполнение DataGridView наименованиями полей 
                     da.Fill(ds, "ZADANIE_PLAT");
                     dataGridView1.DataSource = ds.Tables[0];

                     statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                     statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

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
        /////////////////////



        //Обновление данных в гриде с установленным фильтром
        public void refill_with_filter()
        {

            //Обновление данных на форме просмотра данных после ввода нового задания на платеж
            if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
            {
                SqlCommand command = new SqlCommand(filters.str_query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "ZADANIE_PLAT");
                dataGridView1.DataSource = ds.Tables[0];

                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");
                
            }
            else
            {
                SqlCommand command = new SqlCommand(filters.str_query, conn);
                da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "ZADANIE_PLAT");
                dataGridView1.DataSource = ds.Tables[0];

                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");
                
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
        /////////////////////
        


        //Функция создания копии задания на платеж
        public void copy_zadanie()
        {
            if (MessageBox.Show("Создать копию выбранного задания на платеж", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (filters.set_filter == true)
                {
                    string USER_ID = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                    string PAYER = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                    string BRANCH = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
                    string ISPOLNITEL = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
                    string PLAN_DATE_PAYMENT = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
                    string SUMM = Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value);
                    string NDS = Convert.ToString(dataGridView1.CurrentRow.Cells[7].Value); ;
                    string NAZNACHEN_PLATEJ = Convert.ToString(dataGridView1.CurrentRow.Cells[8].Value);
                    string OBOSNOVANIE = Convert.ToString(dataGridView1.CurrentRow.Cells[9].Value);
                    string POLUCHAT_PLATEJ = Convert.ToString(dataGridView1.CurrentRow.Cells[10].Value);
                    string INN_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[11].Value);
                    string KPP_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[12].Value);
                    string ACCOUNT_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[13].Value);
                    string BANK_NAIMENOVAN = Convert.ToString(dataGridView1.CurrentRow.Cells[14].Value);
                    string BIK_BANK_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[15].Value);
                    string KOR_ACCOUNT_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[16].Value);
                    string ARTICLE_BUDGET = Convert.ToString(dataGridView1.CurrentRow.Cells[17].Value);
                    string DEPARTMENT_ZAYAVITEL = Convert.ToString(dataGridView1.CurrentRow.Cells[18].Value);
                    string BOSS_DEPARTMENT = Convert.ToString(dataGridView1.CurrentRow.Cells[19].Value);
                    string OTVETSTVENN_PFM_CFO = Convert.ToString(dataGridView1.CurrentRow.Cells[20].Value);
                    string GL_BUH = Convert.ToString(dataGridView1.CurrentRow.Cells[21].Value);
                    string BOSS_RESURS_OBESPECHEN = Convert.ToString(dataGridView1.CurrentRow.Cells[22].Value);
                    string KAZNACHEYSTVO = Convert.ToString(dataGridView1.CurrentRow.Cells[23].Value);
                    string BOSS_KAZNACHEYSTVO = Convert.ToString(dataGridView1.CurrentRow.Cells[24].Value);
                    //string DATETIME_CREATE = Convert.ToString(ds.Tables[0].Rows[i][25]);
                    string PERIOD = Convert.ToString(dataGridView1.CurrentRow.Cells[26].Value);
                    string STATUS = Convert.ToString(dataGridView1.CurrentRow.Cells[27].Value);
                    //string TEXT_DENY = Convert.ToString(dataGridView1.CurrentRow.Cells[28].Value);
                    //string DATE_UPDATE = Convert.ToString(dataGridView1.CurrentRow.Cells[29].Value);
                    string NOTES = Convert.ToString(dataGridView1.CurrentRow.Cells[30].Value);
                    string SROK_OPLAT = Convert.ToString(dataGridView1.CurrentRow.Cells[31].Value);
                    //string PAY_STATUS = Convert.ToString(dataGridView1.CurrentRow.Cells[32].Value);

                    /////////Вставка данных в БД
                    SqlCommand cm9 = conn.CreateCommand();
                    cm9.CommandText = "BEGIN TRANSACTION " +
                                     "insert into ZADANIE_PLAT (USER_ID,PAYER,BRANCH,ISPOLNITEL,PLAN_DATE_PAYMENT,SUMM,NDS,NAZNACHEN_PLATEJ,OBOSNOVANIE,POLUCHAT_PLATEJ,INN_POLUCHATEL,KPP_POLUCHATEL,ACCOUNT_POLUCHATEL,BANK_NAIMENOVAN,BIK_BANK_POLUCHATEL,KOR_ACCOUNT_POLUCHATEL,ARTICLE_BUDGET,DEPARTMENT_ZAYAVITEL,BOSS_DEPARTMENT,OTVETSTVENN_PFM_CFO,GL_BUH,BOSS_RESURS_OBESPECHEN,KAZNACHEYSTVO,BOSS_KAZNACHEYSTVO,DATETIME_CREATE,PERIOD,STATUS,NOTES, SROK_OPLAT, PAY_STATUS) values ('" + Form1.val + "',  '" + PAYER + "', '" + BRANCH + "', '" + ISPOLNITEL + "', convert(datetime,'" + PLAN_DATE_PAYMENT + "', 103), '" + SUMM + "', '" + NDS + "', '" + NAZNACHEN_PLATEJ + "', '" + OBOSNOVANIE + "', '" + POLUCHAT_PLATEJ + "', '" + INN_POLUCHATEL + "', '" + KPP_POLUCHATEL + "', '" + ACCOUNT_POLUCHATEL + "', '" + BANK_NAIMENOVAN + "', '" + BIK_BANK_POLUCHATEL + "', '" + KOR_ACCOUNT_POLUCHATEL + "', '" + ARTICLE_BUDGET + "', '" + DEPARTMENT_ZAYAVITEL + "', '" + BOSS_DEPARTMENT + "', '" + OTVETSTVENN_PFM_CFO + "', '" + GL_BUH + "', '" + BOSS_RESURS_OBESPECHEN + "', '" + KAZNACHEYSTVO + "', '" + BOSS_KAZNACHEYSTVO + "', GETDATE(), '" + view_data.value.ToString() + "', 'В ожидании', '" + NOTES + "', '" + SROK_OPLAT + "', 'Не оплачено')" +
                                     " COMMIT TRANSACTION";

                    try
                    {
                        conn.Open();
                    }
                    catch { }
                    SqlDataReader reader11;
                    reader11 = cm9.ExecuteReader();
                    conn.Close();
                    ///////////////////////////////

                    refill_with_filter();

                    SystemSounds.Beep.Play();
                    MessageBox.Show("Копия задания на платеж сделана удачно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string USER_ID = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                    string PAYER = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                    string BRANCH = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
                    string ISPOLNITEL = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
                    string PLAN_DATE_PAYMENT = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
                    string SUMM = Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value);
                    string NDS = Convert.ToString(dataGridView1.CurrentRow.Cells[7].Value); ;
                    string NAZNACHEN_PLATEJ = Convert.ToString(dataGridView1.CurrentRow.Cells[8].Value);
                    string OBOSNOVANIE = Convert.ToString(dataGridView1.CurrentRow.Cells[9].Value);
                    string POLUCHAT_PLATEJ = Convert.ToString(dataGridView1.CurrentRow.Cells[10].Value);
                    string INN_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[11].Value);
                    string KPP_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[12].Value);
                    string ACCOUNT_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[13].Value);
                    string BANK_NAIMENOVAN = Convert.ToString(dataGridView1.CurrentRow.Cells[14].Value);
                    string BIK_BANK_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[15].Value);
                    string KOR_ACCOUNT_POLUCHATEL = Convert.ToString(dataGridView1.CurrentRow.Cells[16].Value);
                    string ARTICLE_BUDGET = Convert.ToString(dataGridView1.CurrentRow.Cells[17].Value);
                    string DEPARTMENT_ZAYAVITEL = Convert.ToString(dataGridView1.CurrentRow.Cells[18].Value);
                    string BOSS_DEPARTMENT = Convert.ToString(dataGridView1.CurrentRow.Cells[19].Value);
                    string OTVETSTVENN_PFM_CFO = Convert.ToString(dataGridView1.CurrentRow.Cells[20].Value);
                    string GL_BUH = Convert.ToString(dataGridView1.CurrentRow.Cells[21].Value);
                    string BOSS_RESURS_OBESPECHEN = Convert.ToString(dataGridView1.CurrentRow.Cells[22].Value);
                    string KAZNACHEYSTVO = Convert.ToString(dataGridView1.CurrentRow.Cells[23].Value);
                    string BOSS_KAZNACHEYSTVO = Convert.ToString(dataGridView1.CurrentRow.Cells[24].Value);
                    //string DATETIME_CREATE = Convert.ToString(ds.Tables[0].Rows[i][25]);
                    string PERIOD = Convert.ToString(dataGridView1.CurrentRow.Cells[26].Value);
                    string STATUS = Convert.ToString(dataGridView1.CurrentRow.Cells[27].Value);
                    //string TEXT_DENY = Convert.ToString(dataGridView1.CurrentRow.Cells[28].Value);
                    //string DATE_UPDATE = Convert.ToString(dataGridView1.CurrentRow.Cells[29].Value);
                    string NOTES = Convert.ToString(dataGridView1.CurrentRow.Cells[30].Value);
                    string SROK_OPLAT = Convert.ToString(dataGridView1.CurrentRow.Cells[31].Value);
                    //string PAY_STATUS = Convert.ToString(dataGridView1.CurrentRow.Cells[32].Value);

                    /////////Вставка данных в БД
                    SqlCommand cm9 = conn.CreateCommand();
                    cm9.CommandText = "BEGIN TRANSACTION " +
                                     "insert into ZADANIE_PLAT (USER_ID,PAYER,BRANCH,ISPOLNITEL,PLAN_DATE_PAYMENT,SUMM,NDS,NAZNACHEN_PLATEJ,OBOSNOVANIE,POLUCHAT_PLATEJ,INN_POLUCHATEL,KPP_POLUCHATEL,ACCOUNT_POLUCHATEL,BANK_NAIMENOVAN,BIK_BANK_POLUCHATEL,KOR_ACCOUNT_POLUCHATEL,ARTICLE_BUDGET,DEPARTMENT_ZAYAVITEL,BOSS_DEPARTMENT,OTVETSTVENN_PFM_CFO,GL_BUH,BOSS_RESURS_OBESPECHEN,KAZNACHEYSTVO,BOSS_KAZNACHEYSTVO,DATETIME_CREATE,PERIOD,STATUS,NOTES, SROK_OPLAT, PAY_STATUS) values ('" + Form1.val + "',  '" + PAYER + "', '" + BRANCH + "', '" + ISPOLNITEL + "', convert(datetime,'" + PLAN_DATE_PAYMENT + "', 103), '" + SUMM + "', '" + NDS + "', '" + NAZNACHEN_PLATEJ + "', '" + OBOSNOVANIE + "', '" + POLUCHAT_PLATEJ + "', '" + INN_POLUCHATEL + "', '" + KPP_POLUCHATEL + "', '" + ACCOUNT_POLUCHATEL + "', '" + BANK_NAIMENOVAN + "', '" + BIK_BANK_POLUCHATEL + "', '" + KOR_ACCOUNT_POLUCHATEL + "', '" + ARTICLE_BUDGET + "', '" + DEPARTMENT_ZAYAVITEL + "', '" + BOSS_DEPARTMENT + "', '" + OTVETSTVENN_PFM_CFO + "', '" + GL_BUH + "', '" + BOSS_RESURS_OBESPECHEN + "', '" + KAZNACHEYSTVO + "', '" + BOSS_KAZNACHEYSTVO + "', GETDATE(), '" + view_data.value.ToString() + "', 'В ожидании', '" + NOTES + "', '" + SROK_OPLAT + "', 'Не оплачено')" +
                                     " COMMIT TRANSACTION";

                    try
                    {
                        conn.Open();
                    }
                    catch { }
                    SqlDataReader reader11;
                    reader11 = cm9.ExecuteReader();
                    conn.Close();
                    ///////////////////////////////

                    refill();

                    SystemSounds.Beep.Play();
                    MessageBox.Show("Копия задания на платеж сделана удачно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        ////////////////////////////////////////////
        
        
        private void контрагентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            catalog_kontragent catalog_kontragent = new catalog_kontragent();
            catalog_kontragent.ShowDialog();
        }

        private void view_data_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();        
        }

        private void view_data_Load(object sender, EventArgs e)
        {
            this.Text = "Список заданий на платеж - " + Assembly.GetExecutingAssembly().GetName().Version;
                        
            if ((Form1.val == 1) || (Form1.val == 2)) //ПОказ всех сотрудников если заходит Скворцов ОИ или Комосская
            {                
                отвергнутьЗаданиеНаПлатежToolStripMenuItem.Visible = true;
                button3.Visible = true;
                одобритьзаданиенаплатежToolStripMenuItem.Visible = true;
                отметитьКакОплаченноеToolStripMenuItem.Visible = true;
                сформироватьРеестрДляОплатыToolStripMenuItem.Visible = true;
                администрированиеToolStripMenuItem.Visible = true;
                отправитьВАрхивОтвергнутоеЗНПToolStripMenuItem.Visible = true;
                //отвергнутыеЗНПToolStripMenuItem.Visible = true;
                снятьСтатусПринятоToolStripMenuItem.Visible = true;
                реестрПлатежейДляСГКToolStripMenuItem.Visible = true;

                SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT", conn);
                da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "ZADANIE_PLAT");
                dataGridView1.DataSource = ds.Tables[0];

                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0]+" руб.");

                fill_gridview();
            }
            else
            {                
                отвергнутьЗаданиеНаПлатежToolStripMenuItem.Visible = false;
                button3.Visible = false;
                одобритьзаданиенаплатежToolStripMenuItem.Visible = false;
                отметитьКакОплаченноеToolStripMenuItem.Visible = false;
                сформироватьРеестрДляОплатыToolStripMenuItem.Visible = false;
                администрированиеToolStripMenuItem.Visible = false;
                отправитьВАрхивОтвергнутоеЗНПToolStripMenuItem.Visible = false;
                //отвергнутыеЗНПToolStripMenuItem.Visible = false;
                снятьСтатусПринятоToolStripMenuItem.Visible = false;
                реестрПлатежейДляСГКToolStripMenuItem.Visible = false;

                SqlCommand command = new SqlCommand("select * from ZADANIE_PLAT where USER_ID=" + Form1.val + "select sum(convert(float, replace(SUMM, ',','.'))) from ZADANIE_PLAT where USER_ID=" + Form1.val, conn);
                da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "ZADANIE_PLAT");
                dataGridView1.DataSource = ds.Tables[0];

                statusStrip1.Items[0].Text = "Всего записей: " + Convert.ToString(ds.Tables[0].Rows.Count);
                statusStrip1.Items[1].Text = "Сумма всех заданий на платеж: " + Convert.ToString(ds.Tables[1].Rows[0][0] + " руб.");

                fill_gridview();
            }

            value = "";
            /*//Чтение текущего периода из таблицы
            conn.Open();
            SqlCommand mycommand = new SqlCommand("select * from PERIOD", conn);
            value = mycommand.ExecuteScalar();
            conn.Close();
            //////////////////////////////////// */

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


                /////Вставка данных в таблицу журнала вход/выход
                SqlCommand scmd4 = conn.CreateCommand();
                scmd4.CommandText = "INSERT into JOURNAL (USER_ID,USER_FULL_NAME,EVENT_DATETIME,EVENT_STATUS,MACHINE_NAME,SYSTEM_NAME) VALUES (" + "'" + Form1.val + "', (select FULL_NAME from USERS where ID=" + Form1.val + "), GETDATE(), 'Вход','" + Environment.MachineName + "','" + Environment.UserName + "')";
                try
                {
                    conn.Open();
                }
                catch { }
                SqlDataReader reader4;
                reader4 = scmd4.ExecuteReader();
                conn.Close();
                //////////////////
              

            label12.Text = Form1.name_user;
            //label2.Text = "Текущий период: " + value.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main main = new main();
            main.Owner = this;
            main.Show();
        }

        private void привязатьСканЗаданияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Проверка на статус. Если Приянто или отвергнуто, то изменение задания на платеж не возможно
            if ((dataGridView1.CurrentRow.Cells[27].Value.ToString() == "Принято") || (dataGridView1.CurrentRow.Cells[27].Value.ToString() == "Отвергнуто"))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Казначейством, заданию на платеж присвоен статус \"" + dataGridView1.CurrentRow.Cells[27].Value.ToString() + "\". Прявязка сканов невозможна!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                byte[] fileByteArray = File.ReadAllBytes(openFileDialog1.FileName);
                string filename = Path.GetFileName(openFileDialog1.FileName);
                
                /////////Вставка данных в БД
                SqlCommand cm = conn.CreateCommand();
                cm.CommandText = "insert into IMAGES (IMAGE,USER_ID,ZADANIE_PLAT_ID,DATETIME_CREATE,FILE_NAME) values (@binaryData, " + Form1.val + ", '" + dataGridView1.CurrentRow.Cells[0].Value + "', GETDATE(), '"+ filename +"')";
                cm.Parameters.Add("@binaryData", SqlDbType.VarBinary).Value = fileByteArray;
                try
                {
                    conn.Open();
                }
                catch { }
                cm.ExecuteNonQuery();

                //SqlDataReader reader1;
                //reader1 = cm.ExecuteReader();
                conn.Close();
                ///////////////////////////////

                SystemSounds.Beep.Play();
                MessageBox.Show("Файл добавлен удачно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Удалять нечего! Список пуст!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Проверка на статус. Если Приянто или отвергнуто, то изменение задания на платеж не возможно
            if ((dataGridView1.CurrentRow.Cells[27].Value.ToString()=="Принято") || (dataGridView1.CurrentRow.Cells[27].Value.ToString()=="Отвергнуто"))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Казначейством, заданию на платеж присвоен статус \"" + dataGridView1.CurrentRow.Cells[27].Value.ToString()+"\". Удаление не возможно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                    

            if (MessageBox.Show("Вы уверены, что хотите удалить данное задание на платеж?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                   /////////Удаление выбранного задания на платеж, перед этим удаляются все привязанные картинки
                    SqlCommand scmd = conn.CreateCommand();
                    scmd.CommandText = "BEGIN TRANSACTION " +
                                       "delete from IMAGES where ZADANIE_PLAT_ID=" + dataGridView1.CurrentRow.Cells[0].Value +
                                       " delete from ZADANIE_PLAT where ID=" + dataGridView1.CurrentRow.Cells[0].Value +
                                       " COMMIT TRANSACTION";
                    try
                    {
                        conn.Open();
                    }
                    catch { }
                    SqlDataReader reader;
                    reader = scmd.ExecuteReader();
                    conn.Close();
                    //////////////////                               
                
                refill();//Обновление данных в гриде после ввода новой записи
              
                SystemSounds.Beep.Play();
                MessageBox.Show("Задание на платеж удалено удачно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private void сменаПароляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_passw change_passw = new change_passw();
            change_passw.ShowDialog();
        }

        private void задатьВопросРазработчикуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            question question = new question();
            question.ShowDialog();
        }

        private void отобразитьПривязанныеСканыЗаданийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////////////Проверка, существует ли привязанные файлы у записи, перед вставкой другого файла
            try
            {
                conn.Open();
            }
            catch { }
            SqlCommand mycommand = new SqlCommand("select * from IMAGES where ZADANIE_PLAT_ID=" + dataGridView1.CurrentRow.Cells[0].Value, conn);
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
                        
            value_4_scan = dataGridView1.CurrentRow.Cells[0].Value;
            value_4_deny_del_image = dataGridView1.CurrentRow.Cells[27].Value.ToString();

            list_of_scan list_of_scan = new list_of_scan();
            list_of_scan.ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ////////////Проверка, существует ли привязанные файлы у записи перед отображением
            try
            {
                conn.Open();
            }
            catch { }
            SqlCommand mycommand = new SqlCommand("select * from IMAGES where ZADANIE_PLAT_ID=" + dataGridView1.CurrentRow.Cells[0].Value, conn);
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

            value_4_scan = dataGridView1.CurrentRow.Cells[0].Value;
            value_4_deny_del_image = dataGridView1.CurrentRow.Cells[27].Value.ToString();

            list_of_scan list_of_scan = new list_of_scan();
            list_of_scan.ShowDialog();
        }

        private void причинаОтверженияToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

            if (dataGridView1.SelectedRows.Count > 1)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }

            if (dataGridView1.CurrentRow.Cells[29].Value == null)
            {
                statusStrip1.Items[2].Text = "Дата обновления записи: ---";
            }
            else
            {
                statusStrip1.Items[2].Text = "Дата обновления записи: " + dataGridView1.CurrentRow.Cells[29].Value.ToString();
            }

            richTextBox2.Text = dataGridView1.CurrentRow.Cells[30].Value.ToString();
            
        }

        private void отвергнутьЗаданиеНаПлатежToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value_4_deny = dataGridView1.CurrentRow.Cells[0].Value;
            value_4_deny_return_position = dataGridView1.CurrentRow.Index;

            deny_platej deny_platej = new deny_platej();
            deny_platej.Owner=this;
            deny_platej.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что необходимо одобрить данные задания на платеж?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Проверка на уже существующие записи с статусом ПРИНЯТО
                for (int a = 0; a <= dataGridView1.Rows.Count - 1; a++)
                {
                    if (dataGridView1.Rows[a].Selected)
                    {
                        string ch = dataGridView1.Rows[a].Cells[27].Value.ToString();
                        if (ch == "Принято")
                        {
                            SystemSounds.Beep.Play();
                            MessageBox.Show("Одно из заданий на платеж уже имеет статус \"Принято\". Повторно изменять статус запрещено!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                /////////////////////////
                              
                for (int s = 0; s <= dataGridView1.Rows.Count - 1; s++)
                {
                    if (dataGridView1.Rows[s].Selected)
                    {
                        /////////Обновление данных в БД
                        SqlCommand cm = conn.CreateCommand();
                        cm.CommandText = "BEGIN TRANSACTION " +
                                         "update ZADANIE_PLAT SET STATUS='Принято', TEXT_DENY='', DATE_UPDATE=GETDATE() where ID=" + dataGridView1.Rows[s].Cells[0].Value.ToString() +
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
                     
                    }
                }
                refill(); //Вызов функции обновления грида после ввода новой записи в БД
            }

        }

        private void одобритьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что необходимо одобрить данное задание на платеж?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Если фильтр включен, то испозьуем функцию заполнения с фильтром, если нет, то общую функцию заполнения dbgrid
                if (filters.set_filter == true) 
                {
                    int ind = dataGridView1.CurrentRow.Index;

                    /////////Обновление данных в БД
                    SqlCommand cm = conn.CreateCommand();
                    cm.CommandText = "BEGIN TRANSACTION " +
                                     "update ZADANIE_PLAT SET STATUS='Принято', DATE_UPDATE=GETDATE() where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() +
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

                    //Вызов функции обновления грида после ввода новой записи в БД
                    refill_with_filter();

                    dataGridView1.CurrentCell = dataGridView1[3, ind]; // Перемещение к той же записи, которую и одобряли после обновления статуса
                }
                else
                {
                    int ind = dataGridView1.CurrentRow.Index;

                    /////////Обновление данных в БД
                    SqlCommand cm = conn.CreateCommand();
                    cm.CommandText = "BEGIN TRANSACTION " +
                                     "update ZADANIE_PLAT SET STATUS='Принято', DATE_UPDATE=GETDATE() where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() +
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

                    //Вызов функции обновления грида после ввода новой записи в БД
                    refill();

                    dataGridView1.CurrentCell = dataGridView1[3, ind]; // Перемещение к той же записи, которую и одобряли после обновления статуса
                }
                
            }
        }

        
        private void открытьАрхивToolStripMenuItem_Click(object sender, EventArgs e)
        {
            archive archive = new archive();
            archive.Owner = this;
            archive.ShowDialog();
        }

        private void сформироавтьЗаданиеВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сформировать Excel-формат задания на платеж?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                /////Создание объекта задание на платеж
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(Environment.CurrentDirectory + @"\template\plat.xlsx", 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                /////////

                ObjWorkSheet.Cells[3, 1] ="Задание на платеж №"+ dataGridView1.CurrentRow.Cells[0].Value.ToString();
                ObjWorkSheet.Cells[6, 13] = DateTime.Today;
                ObjWorkSheet.Cells[5, 2] = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                ObjWorkSheet.Cells[6, 2] = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                ObjWorkSheet.Cells[7, 2] = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                ObjWorkSheet.Cells[8, 2] = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                ObjWorkSheet.Cells[10, 2] = dataGridView1.CurrentRow.Cells[6].Value.ToString() + " руб.";
                if (dataGridView1.CurrentRow.Cells[7].Value.ToString().Length > 0) { ObjWorkSheet.Cells[11, 2] = dataGridView1.CurrentRow.Cells[7].Value.ToString() + " руб."; } else { ObjWorkSheet.Cells[11, 2] = ""; }
                ObjWorkSheet.Cells[12, 2] = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                ObjWorkSheet.Cells[13, 2] = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                ObjWorkSheet.Cells[14, 2] = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                ObjWorkSheet.Cells[16, 2] = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                ObjWorkSheet.Cells[17, 2] = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                ObjWorkSheet.Cells[18, 2] = dataGridView1.CurrentRow.Cells[13].Value.ToString();
                ObjWorkSheet.Cells[19, 2] = dataGridView1.CurrentRow.Cells[14].Value.ToString();
                ObjWorkSheet.Cells[20, 2] = dataGridView1.CurrentRow.Cells[15].Value.ToString();
                ObjWorkSheet.Cells[21, 2] = dataGridView1.CurrentRow.Cells[16].Value.ToString();
                ObjWorkSheet.Cells[22, 2] = dataGridView1.CurrentRow.Cells[17].Value.ToString();
                ObjWorkSheet.Cells[23, 2] = dataGridView1.CurrentRow.Cells[31].Value.ToString();
                ObjWorkSheet.Cells[25, 2] = dataGridView1.CurrentRow.Cells[18].Value.ToString();
                ObjWorkSheet.Cells[26, 2] = dataGridView1.CurrentRow.Cells[30].Value.ToString();
                ObjWorkSheet.Cells[28, 3] = dataGridView1.CurrentRow.Cells[19].Value.ToString();
                ObjWorkSheet.Cells[29, 3] = dataGridView1.CurrentRow.Cells[20].Value.ToString();
                ObjWorkSheet.Cells[30, 3] = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                ObjWorkSheet.Cells[31, 3] = dataGridView1.CurrentRow.Cells[21].Value.ToString();
                ObjWorkSheet.Cells[32, 3] = dataGridView1.CurrentRow.Cells[24].Value.ToString();
                
                ObjExcel.Visible = true;

                GC.Collect();
            }
        }

        private void отметитьКакОплаченноеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Уверены, что необходимо изменить статус на \"Оплачено\"?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (filters.set_filter == true)
                {
                    string check_st = dataGridView1.CurrentRow.Cells[27].Value.ToString();

                    if (check_st == "В ожидании")
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Невозможно переместить в архив и присвоить статус \"Оплачено\" заданию на платеж со статусом \"В ожидании!\"", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    string IDD = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);

                    /////////Обновление поля Статус Оплаты на ОПЛАЧЕНО
                    SqlCommand cm = conn.CreateCommand();
                    cm.CommandText = "BEGIN TRANSACTION " +
                                     "update ZADANIE_PLAT SET PAY_STATUS='Оплачено' where ID=" + IDD +
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


                    //Запрос на обновленную строку
                    SqlCommand command1 = new SqlCommand("select * from ZADANIE_PLAT where ID=" + IDD, conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(command1);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da1.Fill(ds, "ZADANIE_PLAT");

                    string ID = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    string USER_ID = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    string PAYER = Convert.ToString(ds.Tables[0].Rows[0][2]);
                    string BRANCH = Convert.ToString(ds.Tables[0].Rows[0][3]);
                    string ISPOLNITEL = Convert.ToString(ds.Tables[0].Rows[0][4]);
                    string PLAN_DATE_PAYMENT = Convert.ToString(ds.Tables[0].Rows[0][5]);
                    string SUMM = Convert.ToString(ds.Tables[0].Rows[0][6]);
                    string NDS = Convert.ToString(ds.Tables[0].Rows[0][7]); ;
                    string NAZNACHEN_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][8]);
                    string OBOSNOVANIE = Convert.ToString(ds.Tables[0].Rows[0][9]);
                    string POLUCHAT_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][10]);
                    string INN_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][11]);
                    string KPP_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][12]);
                    string ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][13]);
                    string BANK_NAIMENOVAN = Convert.ToString(ds.Tables[0].Rows[0][14]);
                    string BIK_BANK_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][15]);
                    string KOR_ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][16]);
                    string ARTICLE_BUDGET = Convert.ToString(ds.Tables[0].Rows[0][17]);
                    string DEPARTMENT_ZAYAVITEL = Convert.ToString(ds.Tables[0].Rows[0][18]);
                    string BOSS_DEPARTMENT = Convert.ToString(ds.Tables[0].Rows[0][19]);
                    string OTVETSTVENN_PFM_CFO = Convert.ToString(ds.Tables[0].Rows[0][20]);
                    string GL_BUH = Convert.ToString(ds.Tables[0].Rows[0][21]);
                    string BOSS_RESURS_OBESPECHEN = Convert.ToString(ds.Tables[0].Rows[0][22]);
                    string KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][23]);
                    string BOSS_KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][24]);
                    string DATETIME_CREATE = Convert.ToString(ds.Tables[0].Rows[0][25]);
                    string PERIOD = Convert.ToString(ds.Tables[0].Rows[0][26]);
                    string STATUS = Convert.ToString(ds.Tables[0].Rows[0][27]);
                    string TEXT_DENY = Convert.ToString(ds.Tables[0].Rows[0][28]);
                    string DATE_UPDATE = Convert.ToString(ds.Tables[0].Rows[0][29]);
                    string NOTES = Convert.ToString(ds.Tables[0].Rows[0][30]);
                    string SROK_OPLAT = Convert.ToString(ds.Tables[0].Rows[0][31]);
                    string PAY_STATUS = Convert.ToString(ds.Tables[0].Rows[0][32]);



                    /////////Перенос данных из рабочей таблицы в архив
                    SqlCommand cm1 = conn.CreateCommand();
                    cm1.CommandText = "BEGIN TRANSACTION " +
                                     "insert into ARCHIVE_ZADANIE_PLAT (USER_ID, PAYER, BRANCH, ISPOLNITEL,	PLAN_DATE_PAYMENT, SUMM, NDS,NAZNACHEN_PLATEJ, OBOSNOVANIE, POLUCHAT_PLATEJ, INN_POLUCHATEL, KPP_POLUCHATEL, ACCOUNT_POLUCHATEL, BANK_NAIMENOVAN, BIK_BANK_POLUCHATEL, KOR_ACCOUNT_POLUCHATEL, ARTICLE_BUDGET, DEPARTMENT_ZAYAVITEL, BOSS_DEPARTMENT, OTVETSTVENN_PFM_CFO, GL_BUH, BOSS_RESURS_OBESPECHEN, KAZNACHEYSTVO, BOSS_KAZNACHEYSTVO, DATETIME_CREATE, PERIOD, STATUS, TEXT_DENY, DATE_UPDATE, NOTES, ZADANIE_PLAT_ID, SROK_OPLAT, PAY_STATUS) VALUES ('" + USER_ID + "', '" + PAYER + "', '" + BRANCH + "', '" + ISPOLNITEL + "', convert(datetime,'" + PLAN_DATE_PAYMENT + "', 103), '" + SUMM + "', '" + NDS + "', '" + NAZNACHEN_PLATEJ + "', '" + OBOSNOVANIE + "', '" + POLUCHAT_PLATEJ + "', '" + INN_POLUCHATEL + "', '" + KPP_POLUCHATEL + "', '" + ACCOUNT_POLUCHATEL + "', '" + BANK_NAIMENOVAN + "', '" + BIK_BANK_POLUCHATEL + "', '" + KOR_ACCOUNT_POLUCHATEL + "', '" + ARTICLE_BUDGET + "', '" + DEPARTMENT_ZAYAVITEL + "', '" + BOSS_DEPARTMENT + "', '" + OTVETSTVENN_PFM_CFO + "', '" + GL_BUH + "', '" + BOSS_RESURS_OBESPECHEN + "', '" + KAZNACHEYSTVO + "', '" + BOSS_KAZNACHEYSTVO + "', convert(datetime,'" + DATETIME_CREATE + "', 103), '" + PERIOD + "', '" + STATUS + "', '" + TEXT_DENY + "', convert(datetime,'" + DATE_UPDATE + "', 103), '" + NOTES + "', '" + ID + "', '" + SROK_OPLAT + "', '" + PAY_STATUS + "')" +
                                     " delete from ZADANIE_PLAT where ID=" + IDD +
                                     " COMMIT TRANSACTION";
                    try
                    {
                        conn.Open();
                    }
                    catch { }
                    SqlDataReader reader2;
                    reader2 = cm1.ExecuteReader();
                    conn.Close();
                    ///////////////////////////////  



                    //Вызов функции обновления грида после ввода новой записи в БД
                    refill_with_filter();

                    //Определение EMAIL, чью платежку одобрили
                    SqlCommand command2 = new SqlCommand("select email from USERS where ID=" + USER_ID, conn);
                    SqlDataAdapter da2 = new SqlDataAdapter(command2);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                    DataSet ds2 = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da2.Fill(ds2, "USERS");

                    if (ds2.Tables[0].Rows[0][0].ToString() != "")
                    {
                        send_status_opl send_status_opl = new send_status_opl();
                        send_status_opl.send_em(ds2.Tables[0].Rows[0][0].ToString(), POLUCHAT_PLATEJ, SUMM, NDS);
                    }
                }
                else
                {
                    string check_st = dataGridView1.CurrentRow.Cells[27].Value.ToString();

                    if (check_st == "В ожидании")
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Невозможно переместить в архив и присвоить статус \"Оплачено\" заданию на платеж со статусом \"В ожидании!\"", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    string IDD = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);

                    /////////Обновление поля Статус Оплаты на ОПЛАЧЕНО
                    SqlCommand cm = conn.CreateCommand();
                    cm.CommandText = "BEGIN TRANSACTION " +
                                     "update ZADANIE_PLAT SET PAY_STATUS='Оплачено' where ID=" + IDD +
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


                    //Запрос на обновленную строку
                    SqlCommand command1 = new SqlCommand("select * from ZADANIE_PLAT where ID=" + IDD, conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(command1);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da1.Fill(ds, "ZADANIE_PLAT");

                    string ID = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    string USER_ID = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    string PAYER = Convert.ToString(ds.Tables[0].Rows[0][2]);
                    string BRANCH = Convert.ToString(ds.Tables[0].Rows[0][3]);
                    string ISPOLNITEL = Convert.ToString(ds.Tables[0].Rows[0][4]);
                    string PLAN_DATE_PAYMENT = Convert.ToString(ds.Tables[0].Rows[0][5]);
                    string SUMM = Convert.ToString(ds.Tables[0].Rows[0][6]);
                    string NDS = Convert.ToString(ds.Tables[0].Rows[0][7]); ;
                    string NAZNACHEN_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][8]);
                    string OBOSNOVANIE = Convert.ToString(ds.Tables[0].Rows[0][9]);
                    string POLUCHAT_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][10]);
                    string INN_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][11]);
                    string KPP_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][12]);
                    string ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][13]);
                    string BANK_NAIMENOVAN = Convert.ToString(ds.Tables[0].Rows[0][14]);
                    string BIK_BANK_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][15]);
                    string KOR_ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][16]);
                    string ARTICLE_BUDGET = Convert.ToString(ds.Tables[0].Rows[0][17]);
                    string DEPARTMENT_ZAYAVITEL = Convert.ToString(ds.Tables[0].Rows[0][18]);
                    string BOSS_DEPARTMENT = Convert.ToString(ds.Tables[0].Rows[0][19]);
                    string OTVETSTVENN_PFM_CFO = Convert.ToString(ds.Tables[0].Rows[0][20]);
                    string GL_BUH = Convert.ToString(ds.Tables[0].Rows[0][21]);
                    string BOSS_RESURS_OBESPECHEN = Convert.ToString(ds.Tables[0].Rows[0][22]);
                    string KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][23]);
                    string BOSS_KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][24]);
                    string DATETIME_CREATE = Convert.ToString(ds.Tables[0].Rows[0][25]);
                    string PERIOD = Convert.ToString(ds.Tables[0].Rows[0][26]);
                    string STATUS = Convert.ToString(ds.Tables[0].Rows[0][27]);
                    string TEXT_DENY = Convert.ToString(ds.Tables[0].Rows[0][28]);
                    string DATE_UPDATE = Convert.ToString(ds.Tables[0].Rows[0][29]);
                    string NOTES = Convert.ToString(ds.Tables[0].Rows[0][30]);
                    string SROK_OPLAT = Convert.ToString(ds.Tables[0].Rows[0][31]);
                    string PAY_STATUS = Convert.ToString(ds.Tables[0].Rows[0][32]);



                    /////////Перенос данных из рабочей таблицы в архив
                    SqlCommand cm1 = conn.CreateCommand();
                    cm1.CommandText = "BEGIN TRANSACTION " +
                                     "insert into ARCHIVE_ZADANIE_PLAT (USER_ID, PAYER, BRANCH, ISPOLNITEL,	PLAN_DATE_PAYMENT, SUMM, NDS,NAZNACHEN_PLATEJ, OBOSNOVANIE, POLUCHAT_PLATEJ, INN_POLUCHATEL, KPP_POLUCHATEL, ACCOUNT_POLUCHATEL, BANK_NAIMENOVAN, BIK_BANK_POLUCHATEL, KOR_ACCOUNT_POLUCHATEL, ARTICLE_BUDGET, DEPARTMENT_ZAYAVITEL, BOSS_DEPARTMENT, OTVETSTVENN_PFM_CFO, GL_BUH, BOSS_RESURS_OBESPECHEN, KAZNACHEYSTVO, BOSS_KAZNACHEYSTVO, DATETIME_CREATE, PERIOD, STATUS, TEXT_DENY, DATE_UPDATE, NOTES, ZADANIE_PLAT_ID, SROK_OPLAT, PAY_STATUS) VALUES ('" + USER_ID + "', '" + PAYER + "', '" + BRANCH + "', '" + ISPOLNITEL + "', convert(datetime,'" + PLAN_DATE_PAYMENT + "', 103), '" + SUMM + "', '" + NDS + "', '" + NAZNACHEN_PLATEJ + "', '" + OBOSNOVANIE + "', '" + POLUCHAT_PLATEJ + "', '" + INN_POLUCHATEL + "', '" + KPP_POLUCHATEL + "', '" + ACCOUNT_POLUCHATEL + "', '" + BANK_NAIMENOVAN + "', '" + BIK_BANK_POLUCHATEL + "', '" + KOR_ACCOUNT_POLUCHATEL + "', '" + ARTICLE_BUDGET + "', '" + DEPARTMENT_ZAYAVITEL + "', '" + BOSS_DEPARTMENT + "', '" + OTVETSTVENN_PFM_CFO + "', '" + GL_BUH + "', '" + BOSS_RESURS_OBESPECHEN + "', '" + KAZNACHEYSTVO + "', '" + BOSS_KAZNACHEYSTVO + "', convert(datetime,'" + DATETIME_CREATE + "', 103), '" + PERIOD + "', '" + STATUS + "', '" + TEXT_DENY + "', convert(datetime,'" + DATE_UPDATE + "', 103), '" + NOTES + "', '" + ID + "', '" + SROK_OPLAT + "', '" + PAY_STATUS + "')" +
                                     " delete from ZADANIE_PLAT where ID=" + IDD +
                                     " COMMIT TRANSACTION";
                    try
                    {
                        conn.Open();
                    }
                    catch { }
                    SqlDataReader reader2;
                    reader2 = cm1.ExecuteReader();
                    conn.Close();
                    ///////////////////////////////  



                    //Вызов функции обновления грида после ввода новой записи в БД
                    refill();

                    //Определение EMAIL, чью платежку одобрили
                    SqlCommand command2 = new SqlCommand("select email from USERS where ID=" + USER_ID, conn);
                    SqlDataAdapter da2 = new SqlDataAdapter(command2);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                    DataSet ds2 = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da2.Fill(ds2, "USERS");

                    if (ds2.Tables[0].Rows[0][0].ToString() != "")
                    {
                        send_status_opl send_status_opl = new send_status_opl();
                        send_status_opl.send_em(ds2.Tables[0].Rows[0][0].ToString(), POLUCHAT_PLATEJ, SUMM, NDS);
                    }
                }
            }
            
        }

        private void сформироватьРеестрДляОплатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Уверены, что необходимо сформировать реестр?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SqlCommand command1 = new SqlCommand("select * from ZADANIE_PLAT where STATUS='Принято' order by BRANCH", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(command1);//Переменная объявлена как глобальная
                SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da1.Fill(ds, "ZADANIE_PLAT");

                SqlCommand command2 = new SqlCommand("select sum(Cast(replace(summ, ',','.') as float)) from ZADANIE_PLAT where status='Принято'", conn);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);//Переменная объявлена как глобальная
                SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                DataSet ds2 = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da2.Fill(ds2, "ZADANIE_PLAT");



                ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                ObjExcel.SheetsInNewWorkbook = 1;
                ObjExcel.Workbooks.Add(Type.Missing);
                ObjExcel.Rows[2].Font.Bold = true;
                ObjExcel.Rows[3].Font.Bold = true;
                ObjExcel.Rows[4].Font.Bold = true;
                ObjExcel.Rows[5].Font.Bold = true;
                ObjExcel.Rows[7].Font.Bold = true;

                ObjExcel.Columns[1].ColumnWidth = 10; ObjExcel.Columns[1].WrapText = true;
                ObjExcel.Columns[2].ColumnWidth = 55; ObjExcel.Columns[2].WrapText = true;
                ObjExcel.Columns[3].ColumnWidth = 30; ObjExcel.Columns[3].WrapText = true;
                ObjExcel.Columns[4].ColumnWidth = 44; ObjExcel.Columns[4].WrapText = true;
                ObjExcel.Columns[5].ColumnWidth = 53; ObjExcel.Columns[5].WrapText = true;
                ObjExcel.Columns[6].ColumnWidth = 34; ObjExcel.Columns[6].WrapText = true;
                ObjExcel.Columns[7].ColumnWidth = 72; ObjExcel.Columns[7].WrapText = true;
                ObjExcel.Columns[8].ColumnWidth = 44; ObjExcel.Columns[8].WrapText = true;
                ObjExcel.Columns[9].ColumnWidth = 30; ObjExcel.Columns[9].WrapText = true;
                ObjExcel.Columns[10].ColumnWidth = 31; ObjExcel.Columns[10].WrapText = true;
                ObjExcel.Columns[11].ColumnWidth = 25; ObjExcel.Columns[11].WrapText = true;
                ObjExcel.Columns[12].ColumnWidth = 29; ObjExcel.Columns[12].WrapText = true;
                ObjExcel.Columns[13].ColumnWidth = 23; ObjExcel.Columns[13].WrapText = true;
                ObjExcel.Columns[14].ColumnWidth = 122; ObjExcel.Columns[14].WrapText = true;
                

                ObjExcel.Cells[2, 2] = "Реестр \"МТР\"";
                ObjExcel.Cells[2, 5] = "ОАО \"СибЭР\"";
                ObjExcel.Cells[3, 2] = "на " + DateTime.Now.ToShortDateString();
                ObjExcel.Cells[4, 2] = "\"Утверждаю.\"";
                ObjExcel.Cells[5, 2] = "Директор по экономике и финансам";
                ObjExcel.Cells[5, 4] = "___________________________";
                ObjExcel.Cells[5, 5] = "/В. С. Полянская/";
                ObjExcel.Cells[7, 1] = "№ п/п";
                ObjExcel.Cells[7, 2] = "Контрагент";
                ObjExcel.Cells[7, 3] = "ИНН";
                ObjExcel.Cells[7, 4] = "Назначение платежа";
                ObjExcel.Cells[7, 5] = "Статья БДДС";
                ObjExcel.Cells[7, 6] = "Сумма";
                ObjExcel.Cells[7, 7] = "Основание платежа";
                ObjExcel.Cells[7, 8] = "Условие оплаты";
                ObjExcel.Cells[7, 9] = "Объект для работ на котором произв. закупки";
                ObjExcel.Cells[7, 10] = "Подразделение - куратор, ответственный";
                ObjExcel.Cells[7, 11] = "Задолженность по договору (ДЗ с \" + \", КЗ с \" - \")";
                ObjExcel.Cells[7, 12] = "Задолженность по КА  (ДЗ с \" + \", КЗ с \" - \"), руб.";
                ObjExcel.Cells[7, 13] = "Дата оплаты по договору.";
                ObjExcel.Cells[7, 14] = "Примечания";
                ObjExcel.Range["A7", "N7"].Borders.Weight = 3;

                int st = 8;
                int cnt = 1;
                for (int s = 0; s <= ds.Tables[0].Rows.Count - 1; s++)
                {
                    ObjExcel.Cells[st, 1] = cnt;//Номер поля
                    ObjExcel.Cells[st, 2] = ds.Tables[0].Rows[s][10].ToString();
                    ObjExcel.Cells[st, 3] = ds.Tables[0].Rows[s][11].ToString();
                    ObjExcel.Cells[st, 4] = ds.Tables[0].Rows[s][8].ToString();
                    ObjExcel.Cells[st, 5] = ds.Tables[0].Rows[s][17].ToString();
                    ObjExcel.Cells[st, 6] = ds.Tables[0].Rows[s][6].ToString();
                    ObjExcel.Cells[st, 7] = ds.Tables[0].Rows[s][9].ToString();
                    ObjExcel.Cells[st, 8] = ds.Tables[0].Rows[s][31].ToString();
                    ObjExcel.Cells[st, 9] = ds.Tables[0].Rows[s][3].ToString();
                    ObjExcel.Cells[st, 10] = ds.Tables[0].Rows[s][18].ToString() + " " + ds.Tables[0].Rows[s][20].ToString();
                    ObjExcel.Cells[st, 13] = ds.Tables[0].Rows[s][5].ToString();
                    ObjExcel.Cells[st, 14] = ds.Tables[0].Rows[s][30].ToString();
                    ObjExcel.Range["A"+st, "N"+st].Borders.Weight = 2;
                    st++;
                    cnt++;
                }



                ObjExcel.Rows[st + 5].Font.Bold = true;
                ObjExcel.Rows[st + 7].Font.Bold = true;
                ObjExcel.Rows[st + 9].Font.Bold = true;
                
                ObjExcel.Cells[st+5, 2] = "ВСЕГО: "+ds2.Tables[0].Rows[0][0].ToString()+" руб.";
                ObjExcel.Cells[st + 7, 2] = "Начальник казначейства";
                ObjExcel.Cells[st + 9, 2] = "Экономист по фин. работе";
                ObjExcel.Cells[st + 7, 3] = "________________________";
                ObjExcel.Cells[st + 9, 3] = "________________________";
                ObjExcel.Cells[st + 7, 4] = "Черявко Д. В.";
                ObjExcel.Cells[st + 9, 4] = "Комосская И. А.";

            }
            

            ObjExcel.Visible = true;

        }

        private void добавлениеПользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            user_add user_add = new user_add();
            user_add.ShowDialog();
        }

        private void view_data_FormClosing(object sender, FormClosingEventArgs e)
        {
            /////Вставка данных в таблицу журнала вход/выход
            SqlCommand scmd4 = conn.CreateCommand();
            scmd4.CommandText = "INSERT into JOURNAL (USER_ID,USER_FULL_NAME,EVENT_DATETIME,EVENT_STATUS,MACHINE_NAME,SYSTEM_NAME) VALUES (" + "'" + Form1.val + "', (select FULL_NAME from USERS where ID=" + Form1.val + "), GETDATE(), 'Выход','" + Environment.MachineName + "','" + Environment.UserName + "')";
            try
            {
                conn.Open();
            }
            catch { }
            SqlDataReader reader4;
            reader4 = scmd4.ExecuteReader();
            conn.Close();
            //////////////////
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Желаете выйти?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string check_station = dataGridView1.CurrentRow.Cells[27].Value.ToString();

            if (check_station != "В ожидании")
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Редактирование запрещено, т.к. данное задание уже обработано казначейством! (не в статусе \"В ожидании\"!)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            value_4_edit = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
            edit_zadan_plat edit_zadan_plat = new edit_zadan_plat();
            edit_zadan_plat.Owner = this;
            edit_zadan_plat.ShowDialog();
        }

        private void сделатьКопиюЗаданияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copy_zadanie();
        }

        private void сделатьКопиюЗаданияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy_zadanie();
        }

        private void фильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filters = new filters();
            filters.Owner = this;
            filters.ShowDialog();
        }

        private void рассылкаПоПользователямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            email email = new email();
            email.ShowDialog();

        }

        private void отправитьВАрхивОтвергнутоеЗНПToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Уверены, что необходимо отправить данное ЗНП в архив", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (filters.set_filter == true)
                {
                    string check_st1 = dataGridView1.CurrentRow.Cells[27].Value.ToString();

                    if (check_st1 != "Отвергнуто")
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Невозможно переместить в архив отвергнутых, ЗНП которое не имеет такого статуса!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    string IDD1 = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);


                    //Запрос на обновленную строку
                    SqlCommand command1 = new SqlCommand("select * from ZADANIE_PLAT where ID=" + IDD1, conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(command1);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da1.Fill(ds, "ZADANIE_PLAT");

                    string ID = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    string USER_ID = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    string PAYER = Convert.ToString(ds.Tables[0].Rows[0][2]);
                    string BRANCH = Convert.ToString(ds.Tables[0].Rows[0][3]);
                    string ISPOLNITEL = Convert.ToString(ds.Tables[0].Rows[0][4]);
                    string PLAN_DATE_PAYMENT = Convert.ToString(ds.Tables[0].Rows[0][5]);
                    string SUMM = Convert.ToString(ds.Tables[0].Rows[0][6]);
                    string NDS = Convert.ToString(ds.Tables[0].Rows[0][7]); ;
                    string NAZNACHEN_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][8]);
                    string OBOSNOVANIE = Convert.ToString(ds.Tables[0].Rows[0][9]);
                    string POLUCHAT_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][10]);
                    string INN_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][11]);
                    string KPP_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][12]);
                    string ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][13]);
                    string BANK_NAIMENOVAN = Convert.ToString(ds.Tables[0].Rows[0][14]);
                    string BIK_BANK_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][15]);
                    string KOR_ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][16]);
                    string ARTICLE_BUDGET = Convert.ToString(ds.Tables[0].Rows[0][17]);
                    string DEPARTMENT_ZAYAVITEL = Convert.ToString(ds.Tables[0].Rows[0][18]);
                    string BOSS_DEPARTMENT = Convert.ToString(ds.Tables[0].Rows[0][19]);
                    string OTVETSTVENN_PFM_CFO = Convert.ToString(ds.Tables[0].Rows[0][20]);
                    string GL_BUH = Convert.ToString(ds.Tables[0].Rows[0][21]);
                    string BOSS_RESURS_OBESPECHEN = Convert.ToString(ds.Tables[0].Rows[0][22]);
                    string KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][23]);
                    string BOSS_KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][24]);
                    string DATETIME_CREATE = Convert.ToString(ds.Tables[0].Rows[0][25]);
                    string PERIOD = Convert.ToString(ds.Tables[0].Rows[0][26]);
                    string STATUS = Convert.ToString(ds.Tables[0].Rows[0][27]);
                    string TEXT_DENY = Convert.ToString(ds.Tables[0].Rows[0][28]);
                    string DATE_UPDATE = Convert.ToString(ds.Tables[0].Rows[0][29]);
                    string NOTES = Convert.ToString(ds.Tables[0].Rows[0][30]);
                    string SROK_OPLAT = Convert.ToString(ds.Tables[0].Rows[0][31]);
                    string PAY_STATUS = Convert.ToString(ds.Tables[0].Rows[0][32]);



                    /////////Перенос данных из рабочей таблицы в архив
                    SqlCommand cm1 = conn.CreateCommand();
                    cm1.CommandText = "BEGIN TRANSACTION " +
                                     "insert into ARCHIVE_ZADANIE_PLAT_DENY (USER_ID, PAYER, BRANCH, ISPOLNITEL,	PLAN_DATE_PAYMENT, SUMM, NDS,NAZNACHEN_PLATEJ, OBOSNOVANIE, POLUCHAT_PLATEJ, INN_POLUCHATEL, KPP_POLUCHATEL, ACCOUNT_POLUCHATEL, BANK_NAIMENOVAN, BIK_BANK_POLUCHATEL, KOR_ACCOUNT_POLUCHATEL, ARTICLE_BUDGET, DEPARTMENT_ZAYAVITEL, BOSS_DEPARTMENT, OTVETSTVENN_PFM_CFO, GL_BUH, BOSS_RESURS_OBESPECHEN, KAZNACHEYSTVO, BOSS_KAZNACHEYSTVO, DATETIME_CREATE, PERIOD, STATUS, TEXT_DENY, DATE_UPDATE, NOTES, ZADANIE_PLAT_ID, SROK_OPLAT, PAY_STATUS) VALUES ('" + USER_ID + "', '" + PAYER + "', '" + BRANCH + "', '" + ISPOLNITEL + "', convert(datetime,'" + PLAN_DATE_PAYMENT + "', 103), '" + SUMM + "', '" + NDS + "', '" + NAZNACHEN_PLATEJ + "', '" + OBOSNOVANIE + "', '" + POLUCHAT_PLATEJ + "', '" + INN_POLUCHATEL + "', '" + KPP_POLUCHATEL + "', '" + ACCOUNT_POLUCHATEL + "', '" + BANK_NAIMENOVAN + "', '" + BIK_BANK_POLUCHATEL + "', '" + KOR_ACCOUNT_POLUCHATEL + "', '" + ARTICLE_BUDGET + "', '" + DEPARTMENT_ZAYAVITEL + "', '" + BOSS_DEPARTMENT + "', '" + OTVETSTVENN_PFM_CFO + "', '" + GL_BUH + "', '" + BOSS_RESURS_OBESPECHEN + "', '" + KAZNACHEYSTVO + "', '" + BOSS_KAZNACHEYSTVO + "', convert(datetime,'" + DATETIME_CREATE + "', 103), '" + PERIOD + "', '" + STATUS + "', '" + TEXT_DENY + "', convert(datetime,'" + DATE_UPDATE + "', 103), '" + NOTES + "', '" + ID + "', '" + SROK_OPLAT + "', '" + PAY_STATUS + "')" +
                                     " delete from ZADANIE_PLAT where ID=" + IDD1 +
                                     " COMMIT TRANSACTION";
                    try
                    {
                        conn.Open();
                    }
                    catch { }
                    SqlDataReader reader2;
                    reader2 = cm1.ExecuteReader();
                    conn.Close();
                    ///////////////////////////////  
                    
                    //Вызов функции обновления грида после ввода новой записи в БД
                    refill_with_filter();
                }
                else
                {
                    string check_st1 = dataGridView1.CurrentRow.Cells[27].Value.ToString();

                    if (check_st1 != "Отвергнуто")
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Невозможно переместить в архив отвергнутых, ЗНП которое не имеет такого статуса!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    string IDD1 = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);


                    //Запрос на обновленную строку
                    SqlCommand command1 = new SqlCommand("select * from ZADANIE_PLAT where ID=" + IDD1, conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(command1);//Переменная объявлена как глобальная
                    SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
                    DataSet ds = new DataSet();
                    conn.Close();
                    //Заполнение DataGridView наименованиями полей 
                    da1.Fill(ds, "ZADANIE_PLAT");

                    string ID = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    string USER_ID = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    string PAYER = Convert.ToString(ds.Tables[0].Rows[0][2]);
                    string BRANCH = Convert.ToString(ds.Tables[0].Rows[0][3]);
                    string ISPOLNITEL = Convert.ToString(ds.Tables[0].Rows[0][4]);
                    string PLAN_DATE_PAYMENT = Convert.ToString(ds.Tables[0].Rows[0][5]);
                    string SUMM = Convert.ToString(ds.Tables[0].Rows[0][6]);
                    string NDS = Convert.ToString(ds.Tables[0].Rows[0][7]); ;
                    string NAZNACHEN_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][8]);
                    string OBOSNOVANIE = Convert.ToString(ds.Tables[0].Rows[0][9]);
                    string POLUCHAT_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][10]);
                    string INN_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][11]);
                    string KPP_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][12]);
                    string ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][13]);
                    string BANK_NAIMENOVAN = Convert.ToString(ds.Tables[0].Rows[0][14]);
                    string BIK_BANK_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][15]);
                    string KOR_ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][16]);
                    string ARTICLE_BUDGET = Convert.ToString(ds.Tables[0].Rows[0][17]);
                    string DEPARTMENT_ZAYAVITEL = Convert.ToString(ds.Tables[0].Rows[0][18]);
                    string BOSS_DEPARTMENT = Convert.ToString(ds.Tables[0].Rows[0][19]);
                    string OTVETSTVENN_PFM_CFO = Convert.ToString(ds.Tables[0].Rows[0][20]);
                    string GL_BUH = Convert.ToString(ds.Tables[0].Rows[0][21]);
                    string BOSS_RESURS_OBESPECHEN = Convert.ToString(ds.Tables[0].Rows[0][22]);
                    string KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][23]);
                    string BOSS_KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][24]);
                    string DATETIME_CREATE = Convert.ToString(ds.Tables[0].Rows[0][25]);
                    string PERIOD = Convert.ToString(ds.Tables[0].Rows[0][26]);
                    string STATUS = Convert.ToString(ds.Tables[0].Rows[0][27]);
                    string TEXT_DENY = Convert.ToString(ds.Tables[0].Rows[0][28]);
                    string DATE_UPDATE = Convert.ToString(ds.Tables[0].Rows[0][29]);
                    string NOTES = Convert.ToString(ds.Tables[0].Rows[0][30]);
                    string SROK_OPLAT = Convert.ToString(ds.Tables[0].Rows[0][31]);
                    string PAY_STATUS = Convert.ToString(ds.Tables[0].Rows[0][32]);



                    /////////Перенос данных из рабочей таблицы в архив
                    SqlCommand cm1 = conn.CreateCommand();
                    cm1.CommandText = "BEGIN TRANSACTION " +
                                     "insert into ARCHIVE_ZADANIE_PLAT_DENY (USER_ID, PAYER, BRANCH, ISPOLNITEL,	PLAN_DATE_PAYMENT, SUMM, NDS,NAZNACHEN_PLATEJ, OBOSNOVANIE, POLUCHAT_PLATEJ, INN_POLUCHATEL, KPP_POLUCHATEL, ACCOUNT_POLUCHATEL, BANK_NAIMENOVAN, BIK_BANK_POLUCHATEL, KOR_ACCOUNT_POLUCHATEL, ARTICLE_BUDGET, DEPARTMENT_ZAYAVITEL, BOSS_DEPARTMENT, OTVETSTVENN_PFM_CFO, GL_BUH, BOSS_RESURS_OBESPECHEN, KAZNACHEYSTVO, BOSS_KAZNACHEYSTVO, DATETIME_CREATE, PERIOD, STATUS, TEXT_DENY, DATE_UPDATE, NOTES, ZADANIE_PLAT_ID, SROK_OPLAT, PAY_STATUS) VALUES ('" + USER_ID + "', '" + PAYER + "', '" + BRANCH + "', '" + ISPOLNITEL + "', convert(datetime,'" + PLAN_DATE_PAYMENT + "', 103), '" + SUMM + "', '" + NDS + "', '" + NAZNACHEN_PLATEJ + "', '" + OBOSNOVANIE + "', '" + POLUCHAT_PLATEJ + "', '" + INN_POLUCHATEL + "', '" + KPP_POLUCHATEL + "', '" + ACCOUNT_POLUCHATEL + "', '" + BANK_NAIMENOVAN + "', '" + BIK_BANK_POLUCHATEL + "', '" + KOR_ACCOUNT_POLUCHATEL + "', '" + ARTICLE_BUDGET + "', '" + DEPARTMENT_ZAYAVITEL + "', '" + BOSS_DEPARTMENT + "', '" + OTVETSTVENN_PFM_CFO + "', '" + GL_BUH + "', '" + BOSS_RESURS_OBESPECHEN + "', '" + KAZNACHEYSTVO + "', '" + BOSS_KAZNACHEYSTVO + "', convert(datetime,'" + DATETIME_CREATE + "', 103), '" + PERIOD + "', '" + STATUS + "', '" + TEXT_DENY + "', convert(datetime,'" + DATE_UPDATE + "', 103), '" + NOTES + "', '" + ID + "', '" + SROK_OPLAT + "', '" + PAY_STATUS + "')" +
                                     " delete from ZADANIE_PLAT where ID=" + IDD1 +
                                     " COMMIT TRANSACTION";
                    try
                    {
                        conn.Open();
                    }
                    catch { }
                    SqlDataReader reader2;
                    reader2 = cm1.ExecuteReader();
                    conn.Close();
                    ///////////////////////////////  

                    //Вызов функции обновления грида после ввода новой записи в БД
                    refill();
                }
            }
        }

        private void отвергнутыеЗНПToolStripMenuItem_Click(object sender, EventArgs e)
        {
            archive_deny_ZNP archive_deny_ZNP = new archive_deny_ZNP();
            archive_deny_ZNP.Owner = this;
            archive_deny_ZNP.ShowDialog();

        }

        private void снятьСтатусПринятоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что необходимо снять статус \"Принято\" с данного ЗНП?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (filters.set_filter == true)
                {
                    int ind = dataGridView1.CurrentRow.Index;

                    /////////Обновление данных в БД
                    SqlCommand cm = conn.CreateCommand();
                    cm.CommandText = "BEGIN TRANSACTION " +
                                     "update ZADANIE_PLAT SET STATUS='В ожидании', DATE_UPDATE=GETDATE() where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() +
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

                    //Вызов функции обновления грида после ввода новой записи в БД
                    refill_with_filter();

                    dataGridView1.CurrentCell = dataGridView1[3, ind]; // Перемещение к той же записи, которой и изменяли статус
                }
                else
                {
                    int ind = dataGridView1.CurrentRow.Index;

                    /////////Обновление данных в БД
                    SqlCommand cm = conn.CreateCommand();
                    cm.CommandText = "BEGIN TRANSACTION " +
                                     "update ZADANIE_PLAT SET STATUS='В ожидании', DATE_UPDATE=GETDATE() where ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() +
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

                    //Вызов функции обновления грида после ввода новой записи в БД
                    refill();

                    dataGridView1.CurrentCell = dataGridView1[3, ind]; // Перемещение к той же записи, которой и изменяли статус
                }

            }
        }

        private void реестрПлатежейДляСГКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сформировать Реестр платежей для СГК?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                /////Создание объекта задание на платеж
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(Environment.CurrentDirectory + @"\template\report.xlsm", 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                /////////

                SqlCommand command2 = new SqlCommand("select ZADANIE_PLAT.ID, ZADANIE_PLAT.ARTICLE_BUDGET, ZADANIE_PLAT.SUMM, ZADANIE_PLAT.NDS, ZADANIE_PLAT.POLUCHAT_PLATEJ, ZADANIE_PLAT.INN_POLUCHATEL, ZADANIE_PLAT.ACCOUNT_POLUCHATEL, ZADANIE_PLAT.BANK_NAIMENOVAN, ZADANIE_PLAT.BIK_BANK_POLUCHATEL, ZADANIE_PLAT.KOR_ACCOUNT_POLUCHATEL, ZADANIE_PLAT.NAZNACHEN_PLATEJ, ZADANIE_PLAT.KPP_POLUCHATEL, CATALOG_KONTRAGENT.R3 from ZADANIE_PLAT, CATALOG_KONTRAGENT where ZADANIE_PLAT.POLUCHAT_PLATEJ=CATALOG_KONTRAGENT.NAIMENOVAN_KONTR and ZADANIE_PLAT.STATUS='Принято' or ZADANIE_PLAT.STATUS is null", conn);                                                      
                SqlDataAdapter da2 = new SqlDataAdapter(command2);//Переменная объявлена как глобальная
                SqlCommandBuilder cb2 = new SqlCommandBuilder(da2);
                DataSet ds2 = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da2.Fill(ds2, "ZADANIE_PLAT");

                SqlCommand command3 = new SqlCommand("select sum(ZADANIE_PLAT.SUMM) from ZADANIE_PLAT, CATALOG_KONTRAGENT where ZADANIE_PLAT.POLUCHAT_PLATEJ=CATALOG_KONTRAGENT.NAIMENOVAN_KONTR and ZADANIE_PLAT.STATUS='Принято' or ZADANIE_PLAT.STATUS is null", conn);
                SqlDataAdapter da3 = new SqlDataAdapter(command3);//Переменная объявлена как глобальная
                SqlCommandBuilder cb3 = new SqlCommandBuilder(da3);
                DataSet ds3 = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da3.Fill(ds3, "ZADANIE_PLAT");
                

                ObjWorkSheet.Cells[3, 6] = ds3.Tables[1].Rows[0][0].ToString();
                /*
                for (int t = 6; t <= dataGridView1.Rows.Count; t++)
                {
                    if (Convert.ToString(dataGridView1.Rows[t - 6].Cells[27].Value) == "Принято")
                    {
                        ObjWorkSheet.Cells[t, 2] = t - 5;
                        ObjWorkSheet.Cells[t, 3] = dataGridView1.Rows[t - 6].Cells[17].Value.ToString();
                        ObjWorkSheet.Cells[t, 8] = dataGridView1.Rows[t - 6].Cells[6].Value.ToString();
                        ObjWorkSheet.Cells[t, 9] = dataGridView1.Rows[t - 6].Cells[7].Value.ToString();
                        ObjWorkSheet.Cells[t, 10] = dataGridView1.Rows[t - 6].Cells[10].Value.ToString();
                        ObjWorkSheet.Cells[t, 11] = dataGridView1.Rows[t - 6].Cells[11].Value.ToString();
                        ObjWorkSheet.Cells[t, 12] = dataGridView1.Rows[t - 6].Cells[13].Value.ToString();
                        ObjWorkSheet.Cells[t, 13] = dataGridView1.Rows[t - 6].Cells[14].Value.ToString();
                        ObjWorkSheet.Cells[t, 14] = dataGridView1.Rows[t - 6].Cells[15].Value.ToString();
                        ObjWorkSheet.Cells[t, 15] = dataGridView1.Rows[t - 6].Cells[16].Value.ToString();
                        ObjWorkSheet.Cells[t, 17] = dataGridView1.Rows[t - 6].Cells[8].Value.ToString();
                        ObjWorkSheet.Cells[t, 18] = dataGridView1.Rows[t - 6].Cells[0].Value.ToString();
                        ObjWorkSheet.Cells[t, 20] = "246201001";
                        ObjWorkSheet.Cells[t, 21] = dataGridView1.Rows[t - 6].Cells[12].Value.ToString();
                        ObjWorkSheet.Cells[t, 38] = dataGridView1.Rows[t - 6].Cells[10].Value.ToString();
                    }
                }*/
                ObjExcel.Visible = true;

                GC.Collect();
            }
        }
            

    
    }
}

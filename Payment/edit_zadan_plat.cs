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
    public partial class edit_zadan_plat : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public static string rekv;

        public static string ID;
        public static string USER_ID;
        public static string PAYER;
        public static string BRANCH;
        public static string ISPOLNITEL;
        public static string PLAN_DATE_PAYMENT;
        public static string SUMM;
        public static string NDS;
        public static string NAZNACHEN_PLATEJ;
        public static string OBOSNOVANIE;
        public static string POLUCHAT_PLATEJ;
        public static string INN_POLUCHATEL;
        public static string KPP_POLUCHATEL;
        public static string ACCOUNT_POLUCHATEL;
        public static string BANK_NAIMENOVAN;
        public static string BIK_BANK_POLUCHATEL;
        public static string KOR_ACCOUNT_POLUCHATEL;
        public static string ARTICLE_BUDGET;
        public static string DEPARTMENT_ZAYAVITEL;
        public static string BOSS_DEPARTMENT;
        public static string OTVETSTVENN_PFM_CFO;
        public static string GL_BUH;
        public static string BOSS_RESURS_OBESPECHEN;
        public static string KAZNACHEYSTVO;
        public static string BOSS_KAZNACHEYSTVO;
        public static string DATETIME_CREATE;
        public static string PERIOD;
        public static string STATUS;
        public static string TEXT_DENY;
        public static string DATE_UPDATE;
        public static string NOTES;
        public static string SROK_OPLAT;
        public static string PAY_STATUS;

        public edit_zadan_plat()
        {
            InitializeComponent();
        }

        private void edit_zadan_plat_Load(object sender, EventArgs e)
        {
            //Запрос на обновленную строку
            SqlCommand command1 = new SqlCommand("select * from ZADANIE_PLAT where ID=" + view_data.value_4_edit, conn);
            SqlDataAdapter da1 = new SqlDataAdapter(command1);//Переменная объявлена как глобальная
            SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da1.Fill(ds, "ZADANIE_PLAT");

            ID = Convert.ToString(ds.Tables[0].Rows[0][0]);
            USER_ID = Convert.ToString(ds.Tables[0].Rows[0][1]);
            PAYER = Convert.ToString(ds.Tables[0].Rows[0][2]);
            BRANCH = Convert.ToString(ds.Tables[0].Rows[0][3]);
            ISPOLNITEL = Convert.ToString(ds.Tables[0].Rows[0][4]);
            PLAN_DATE_PAYMENT = Convert.ToString(ds.Tables[0].Rows[0][5]);
            SUMM = Convert.ToString(ds.Tables[0].Rows[0][6]);
            NDS = Convert.ToString(ds.Tables[0].Rows[0][7]); ;
            NAZNACHEN_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][8]);
            OBOSNOVANIE = Convert.ToString(ds.Tables[0].Rows[0][9]);
            POLUCHAT_PLATEJ = Convert.ToString(ds.Tables[0].Rows[0][10]);
            INN_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][11]);
            KPP_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][12]);
            ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][13]);
            BANK_NAIMENOVAN = Convert.ToString(ds.Tables[0].Rows[0][14]);
            BIK_BANK_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][15]);
            KOR_ACCOUNT_POLUCHATEL = Convert.ToString(ds.Tables[0].Rows[0][16]);
            ARTICLE_BUDGET = Convert.ToString(ds.Tables[0].Rows[0][17]);
            DEPARTMENT_ZAYAVITEL = Convert.ToString(ds.Tables[0].Rows[0][18]);
            BOSS_DEPARTMENT = Convert.ToString(ds.Tables[0].Rows[0][19]);
            OTVETSTVENN_PFM_CFO = Convert.ToString(ds.Tables[0].Rows[0][20]);
            GL_BUH = Convert.ToString(ds.Tables[0].Rows[0][21]);
            BOSS_RESURS_OBESPECHEN = Convert.ToString(ds.Tables[0].Rows[0][22]);
            KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][23]);
            BOSS_KAZNACHEYSTVO = Convert.ToString(ds.Tables[0].Rows[0][24]);
            DATETIME_CREATE = Convert.ToString(ds.Tables[0].Rows[0][25]);
            PERIOD = Convert.ToString(ds.Tables[0].Rows[0][26]);
            STATUS = Convert.ToString(ds.Tables[0].Rows[0][27]);
            TEXT_DENY = Convert.ToString(ds.Tables[0].Rows[0][28]);
            DATE_UPDATE = Convert.ToString(ds.Tables[0].Rows[0][29]);
            NOTES = Convert.ToString(ds.Tables[0].Rows[0][30]);
            SROK_OPLAT = Convert.ToString(ds.Tables[0].Rows[0][31]);
            PAY_STATUS = Convert.ToString(ds.Tables[0].Rows[0][32]);

            comboBox1.Text = BRANCH;
            textBox2.Text = ISPOLNITEL;
            dateTimePicker1.Value = Convert.ToDateTime(PLAN_DATE_PAYMENT);
            textBox3.Text = SUMM;
            textBox4.Text = NDS;
            textBox5.Text = NAZNACHEN_PLATEJ;
            textBox6.Text = OBOSNOVANIE;
            comboBox2.Text = POLUCHAT_PLATEJ;
            comboBox7.Text = INN_POLUCHATEL;
            textBox8.Text = KPP_POLUCHATEL;
            textBox9.Text = ACCOUNT_POLUCHATEL;
            textBox10.Text = BANK_NAIMENOVAN;
            textBox11.Text = BIK_BANK_POLUCHATEL;
            textBox12.Text = KOR_ACCOUNT_POLUCHATEL;
            comboBox3.Text = ARTICLE_BUDGET;
            comboBox4.Text = DEPARTMENT_ZAYAVITEL;
            comboBox5.Text = BOSS_DEPARTMENT;
            comboBox6.Text = OTVETSTVENN_PFM_CFO;
            textBox13.Text = GL_BUH;
            textBox14.Text = BOSS_RESURS_OBESPECHEN;
            textBox15.Text = KAZNACHEYSTVO;
            textBox16.Text = BOSS_KAZNACHEYSTVO;
            comboBox8.Text = SROK_OPLAT;
            richTextBox1.Text = NOTES;


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
                    comboBox3.Items.Add(result1 + "     " + result11 + "     " + result12);
                }
                catch { }

            }
            conn.Close();
            ////////////////////////

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
                    comboBox2.Items.Add(result);
                }
                catch { }

            }
            conn.Close();
            ////////////////////////

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
                    comboBox4.Items.Add(result1);
                }
                catch { }

            }
            conn.Close();
            ////////////////////////


            //Заполнение поля ИНН, данными из БД
            SqlCommand command5 = conn.CreateCommand();
            command5.CommandText = "select INN from CATALOG_KONTRAGENT";
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
                    comboBox7.Items.Add(result5);
                }
                catch { }

            }
            conn.Close();
            //////////////////////// 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Подтвердить?", "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if ((comboBox1.Text != "ИА") && (comboBox1.Text != "КрФ") && (comboBox1.Text != "КФ") && (comboBox1.Text != "АФ") && (comboBox1.Text != "БФ"))
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Подразделение необходимо выбирать только из списка! Произвольные названия недопустимы!!!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (comboBox1.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Подразделение\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (textBox2.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Исполнитель\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (textBox3.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Сумма платежа\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (textBox5.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Назначение платежа\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (textBox6.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Обоснование платежа\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (comboBox7.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"ИНН\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (comboBox2.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не выбран \"Получатель\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (comboBox3.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не выбрана \"Статья бюджета\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (comboBox4.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не выбрано \"Подразделение заявитель платежа\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (comboBox5.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не выбран \"Руководитель подразделения\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (comboBox6.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не выбран \"ответственный по ПФМ/ЦФО\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (comboBox8.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Срок оплаты\"!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                /*
                if (textBox13.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Главный бухгалтер филиала/ИА!\"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }*/

                if (textBox14.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Начальник управления ресурсного обеспечения!\"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                /*
                if (textBox15.Text.Length == 0)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Не заполнено поле \"Казначейство\"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                } */             
                
                
                view_data view_data = (view_data)this.Owner;

                /////////Обновление данных в БД
                SqlCommand cm9 = conn.CreateCommand();
                cm9.CommandText = "BEGIN TRANSACTION " +
                                 "update ZADANIE_PLAT SET USER_ID='" + USER_ID + "', PAYER='" + PAYER + "', BRANCH='" + comboBox1.Text + "', ISPOLNITEL='" + textBox2.Text + "', PLAN_DATE_PAYMENT=convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103), SUMM='" + textBox3.Text + "', NDS='" + textBox4.Text + "', NAZNACHEN_PLATEJ='" + textBox5.Text + "', OBOSNOVANIE='" + textBox6.Text + "', POLUCHAT_PLATEJ='" + comboBox2.Text + "', INN_POLUCHATEL='" + comboBox7.Text + "', KPP_POLUCHATEL='" + textBox8.Text + "', ACCOUNT_POLUCHATEL='" + textBox9.Text + "', BANK_NAIMENOVAN='" + textBox10.Text + "', BIK_BANK_POLUCHATEL='" + textBox11.Text + "', KOR_ACCOUNT_POLUCHATEL='" + textBox12.Text + "', ARTICLE_BUDGET='" + comboBox3.Text + "', DEPARTMENT_ZAYAVITEL='" + comboBox4.Text + "', BOSS_DEPARTMENT='" + comboBox5.Text + "', OTVETSTVENN_PFM_CFO='" + comboBox6.Text + "', GL_BUH='" + textBox13.Text + "', BOSS_RESURS_OBESPECHEN='" + textBox14.Text + "', KAZNACHEYSTVO='" + textBox15.Text + "', BOSS_KAZNACHEYSTVO='" + textBox16.Text + "', DATETIME_CREATE=convert(datetime,'" + DATETIME_CREATE + "',103) , PERIOD='" + PERIOD + "', STATUS='" + STATUS + "', TEXT_DENY='" + TEXT_DENY + "', DATE_UPDATE=GETDATE(), NOTES='" + richTextBox1.Text + "', SROK_OPLAT='" + comboBox8.Text + "', PAY_STATUS='" + PAY_STATUS + "' where ID='" + view_data.value_4_edit + "'" +
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

                SystemSounds.Beep.Play();
                MessageBox.Show("Задание обновлено удачно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                view_data.refill();

                this.Close();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                //вычисление НДС 18%
                if (checkBox2.Checked == true)
                {
                    double nds = Convert.ToDouble(textBox3.Text) - (Convert.ToDouble(textBox3.Text) * 100 / 118);
                    textBox4.Text = Convert.ToString(Math.Round(nds, 2));
                }

                //вычисление НДС 10%
                if (checkBox1.Checked == true)
                {
                    double nds = Convert.ToDouble(textBox3.Text) - (Convert.ToDouble(textBox3.Text) * 100 / 110);
                    textBox4.Text = Convert.ToString(Math.Round(nds, 2));
                }

            }
            else textBox4.Clear();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;

                if (textBox3.Text.Length != 0)
                {
                    //Подсчет НДС 18%
                    double nds = Convert.ToDouble(textBox3.Text) - (Convert.ToDouble(textBox3.Text) * 100 / 118);
                    textBox4.Text = Convert.ToString(Math.Round(nds, 2));
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;

                if (textBox3.Text.Length != 0)
                {
                    //Подсчет НДС 10%
                    double nds = Convert.ToDouble(textBox3.Text) - (Convert.ToDouble(textBox3.Text) * 100 / 110);
                    textBox4.Text = Convert.ToString(Math.Round(nds, 2));
                }

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox1.Checked = false;
                checkBox4.Checked = false;

                textBox4.Clear();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox4.Clear();
                textBox4.ReadOnly = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox1.Checked = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen p = new Pen(Color.Black, 1);// цвет линии и ширина
            Point p1 = new Point(500, 1);// первая точка
            Point p2 = new Point(500, 499);// вторая точка
            gr.DrawLine(p, p1, p2);// рисуем линию
            gr.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT where NAIMENOVAN_KONTR='" + comboBox2.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "CATALOG_KONTRAGENT");

            if (ds.Tables[0].Rows.Count != 0)
            {
                //textBox7.Text = ds.Tables[0].Rows[0][2].ToString();
                comboBox7.Text = ds.Tables[0].Rows[0][2].ToString();
                textBox8.Text = ds.Tables[0].Rows[0][3].ToString();
                textBox9.Text = ds.Tables[0].Rows[0][4].ToString();
                textBox10.Text = ds.Tables[0].Rows[0][5].ToString();
                textBox11.Text = ds.Tables[0].Rows[0][6].ToString();
                textBox12.Text = ds.Tables[0].Rows[0][7].ToString();

            }

            //Проверка на существование нескольких реквизитов по одному КА
            SqlCommand command1 = new SqlCommand("select CATALOG_KONTRAGENT_DOP_REKVISIT.INN, CATALOG_KONTRAGENT_DOP_REKVISIT.KPP, CATALOG_KONTRAGENT_DOP_REKVISIT.ACCOUNT, CATALOG_KONTRAGENT_DOP_REKVISIT.BANK_NAIMENOVAN,CATALOG_KONTRAGENT_DOP_REKVISIT.BIK, CATALOG_KONTRAGENT_DOP_REKVISIT.KORR_COUNT from CATALOG_KONTRAGENT_DOP_REKVISIT, CATALOG_KONTRAGENT where [CATALOG_KONTRAGENT].ID=CATALOG_KONTRAGENT_DOP_REKVISIT.ID_CATALOG_KONTRAGENT and CATALOG_KONTRAGENT.NAIMENOVAN_KONTR='" + comboBox2.Text + "'", conn);
            SqlDataAdapter da1 = new SqlDataAdapter(command1);//Переменная объявлена как глобальная
            SqlCommandBuilder cb1 = new SqlCommandBuilder(da1);
            DataSet ds1 = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da1.Fill(ds1, "CATALOG_KONTRAGENT");

            if (ds1.Tables[0].Rows.Count == 0)
            {
                button3.Visible = false;
            }
            else
            {
                button3.Visible = true;
            }
           
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            //Проверка на изменение поля ПОЛУЧАТЕЛЬ, если получатель введен руками и такого нет в БД, то обнуляем все поля ИНН, КПП и т.д.
            SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT where NAIMENOVAN_KONTR='" + comboBox2.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "CATALOG_KONTRAGENT");

            if (ds.Tables[0].Rows.Count == 0)
            {
                comboBox7.Text = "";
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox11.Clear();
                textBox12.Clear();

            }
            else
            {
                comboBox7.Text = ds.Tables[0].Rows[0][2].ToString();
                textBox8.Text = ds.Tables[0].Rows[0][3].ToString();
                textBox9.Text = ds.Tables[0].Rows[0][4].ToString();
                textBox10.Text = ds.Tables[0].Rows[0][5].ToString();
                textBox11.Text = ds.Tables[0].Rows[0][6].ToString();
                textBox12.Text = ds.Tables[0].Rows[0][7].ToString();
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT where INN='" + comboBox7.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "CATALOG_KONTRAGENT");

            if (ds.Tables[0].Rows.Count != 0)
            {
                //textBox7.Text = ds.Tables[0].Rows[0][2].ToString();
                comboBox2.Text = ds.Tables[0].Rows[0][1].ToString();
                //comboBox7.Text = ds.Tables[0].Rows[0][2].ToString();
                textBox8.Text = ds.Tables[0].Rows[0][3].ToString();
                textBox9.Text = ds.Tables[0].Rows[0][4].ToString();
                textBox10.Text = ds.Tables[0].Rows[0][5].ToString();
                textBox11.Text = ds.Tables[0].Rows[0][6].ToString();
                textBox12.Text = ds.Tables[0].Rows[0][7].ToString();

            }   */
        }

        private void comboBox7_TextChanged(object sender, EventArgs e)
        {
          /*  //Проверка на изменение поля ПОЛУЧАТЕЛЬ, если получатель введен руками и такого нет в БД, то обнуляем все поля ИНН, КПП и т.д.
            SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT where INN='" + comboBox7.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "CATALOG_KONTRAGENT");

            if (ds.Tables[0].Rows.Count == 0)
            {
                comboBox2.Text = "";
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox11.Clear();
                textBox12.Clear();

            }
            else
            {
                comboBox2.Text = ds.Tables[0].Rows[0][1].ToString();
                textBox8.Text = ds.Tables[0].Rows[0][3].ToString();
                textBox9.Text = ds.Tables[0].Rows[0][4].ToString();
                textBox10.Text = ds.Tables[0].Rows[0][5].ToString();
                textBox11.Text = ds.Tables[0].Rows[0][6].ToString();
                textBox12.Text = ds.Tables[0].Rows[0][7].ToString();
            }    */   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rekv = comboBox7.Text;

            select_dop_rekvisit_edit select_dop_rekvisit_edit = new select_dop_rekvisit_edit();
            select_dop_rekvisit_edit.Owner = this;
            select_dop_rekvisit_edit.ShowDialog();
        }
    }
}

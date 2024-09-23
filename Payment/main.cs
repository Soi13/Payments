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
    public partial class main : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public static string rekv;

        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
           
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen p = new Pen(Color.Black, 1);// цвет линии и ширина
            Point p1 = new Point(500,1);// первая точка
            Point p2 = new Point(500,499);// вторая точка
            gr.DrawLine(p, p1, p2);// рисуем линию
            gr.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
        }

        private void button1_Click(object sender, EventArgs e)
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

            /*if (textBox13.Text.Length == 0)
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

            /*if (textBox15.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не заполнено поле \"Казначейство\"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }*/

            textBox3.Text=textBox3.Text.Replace(" ", string.Empty); //Обрезание пробелов с суммах.

            view_data view_data = (view_data)this.Owner;
            
            /////////Вставка данных в БД
            SqlCommand cm9 = conn.CreateCommand();
            cm9.CommandText = "BEGIN TRANSACTION " +
                             "insert into ZADANIE_PLAT (USER_ID,PAYER,BRANCH,ISPOLNITEL,PLAN_DATE_PAYMENT,SUMM,NDS,NAZNACHEN_PLATEJ,OBOSNOVANIE,POLUCHAT_PLATEJ,INN_POLUCHATEL,KPP_POLUCHATEL,ACCOUNT_POLUCHATEL,BANK_NAIMENOVAN,BIK_BANK_POLUCHATEL,KOR_ACCOUNT_POLUCHATEL,ARTICLE_BUDGET,DEPARTMENT_ZAYAVITEL,BOSS_DEPARTMENT,OTVETSTVENN_PFM_CFO,GL_BUH,BOSS_RESURS_OBESPECHEN,KAZNACHEYSTVO,BOSS_KAZNACHEYSTVO,DATETIME_CREATE,PERIOD,STATUS,NOTES, SROK_OPLAT, PAY_STATUS) values ('" + Form1.val + "',  '" + textBox1.Text + "', '" + comboBox1.Text + "', '" + textBox2.Text + "', convert(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "', 103), '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + comboBox2.Text + "', '" + comboBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "', '" + textBox10.Text+ "', '"+ textBox11.Text + "', '" + textBox12.Text + "', '" + comboBox3.Text + "', '" + comboBox4.Text + "', '" + comboBox5.Text + "', '" + textBox2.Text + "', '" + textBox13.Text + "', '" + textBox14.Text + "', '" + textBox15.Text + "', '" + textBox16.Text + "', GETDATE(), '"+ view_data.value.ToString() +"', 'В ожидании', '"+richTextBox1.Text+"', '"+comboBox8.Text+"', 'Не оплачено')" +
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

            //Вызов функции обновления грида после ввода новой записи в БД
          
            view_data.refill();

            this.Close();
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
                button2.Visible = false;            
            }
            else
            {
                button2.Visible = true;                
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
                comboBox7.Text="";
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

        private void main_Activated(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox7.Items.Clear();

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

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from CATALOG_KONTRAGENT where INN='" + comboBox7.Text + "'", conn);
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
                textBox8.Text = ds.Tables[0].Rows[0][3].ToString();
                textBox9.Text = ds.Tables[0].Rows[0][4].ToString();
                textBox10.Text = ds.Tables[0].Rows[0][5].ToString();
                textBox11.Text = ds.Tables[0].Rows[0][6].ToString();
                textBox12.Text = ds.Tables[0].Rows[0][7].ToString();

            }
        }

        private void comboBox7_TextChanged(object sender, EventArgs e)
        {
            //Проверка на изменение поля ПОЛУЧАТЕЛЬ, если получатель введен руками и такого нет в БД, то обнуляем все поля ИНН, КПП и т.д.
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
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rekv = comboBox7.Text;
            //Открытие формы с доп.реквизитами
            select_dop_rekvisit select_dop_rekvisit = new select_dop_rekvisit(this);
            select_dop_rekvisit.ShowDialog();
        }

        
    }
}

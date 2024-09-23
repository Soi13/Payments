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
using System.Security.Cryptography;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Payment
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public static int val;
        public static string name_user;
        public static string administration;
   
        public Form1()
        {
            InitializeComponent();
            this.AcceptButton = button1; //Задает кнопку, которая нажимается при нажатии на ENTER
        }

        //Функция шифрования с помощью алгоритма MD5
        string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length == 0) || (maskedTextBox1.Text.Length == 0))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Заполнены не все поля!", "Внимание", MessageBoxButtons.OK);
                return;
            }

            //Удаляем файл существующий, перед записью в него введенного логина
            if (File.Exists("login.txt")) //Создание файла для хранения последнего введеного логина, чтобы при входе он его показывал
            {
                File.Delete("login.txt");                
            }
            //Запись введенного логина в файл
            StreamWriter ff = new StreamWriter(Environment.CurrentDirectory+ @"\login.txt",true);
            ff.Write(textBox1.Text);
            ff.Close();
            //

            string pass = GetHashString(maskedTextBox1.Text);

            conn.Open();
            SqlCommand mycommand = new SqlCommand("select * from users where user_name=" + "'" + textBox1.Text + "' and passw='" + pass + "'", conn);

            SqlDataAdapter da = new SqlDataAdapter(mycommand);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            da.Fill(ds, "USERS");
            if (ds.Tables[0].Rows.Count != 0)
            {
                object value = ds.Tables[0].Rows[0][0].ToString();
                if (value != null)
                {

                    val = Convert.ToInt16(value);
                    name_user = "Пользователь: " + ds.Tables[0].Rows[0][2].ToString();
                    administration = ds.Tables[0].Rows[0][7].ToString();


                    view_data view_data = new view_data();
                    view_data.ShowDialog();
                    this.Visible = false;

                    ////////////////////////////////////        

                }

            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не верно введено либо имя либо пароль!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-US")); //Переключение раскладки клавы
            //InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ru-RU")); //Переключение раскладки клавы

            if (!File.Exists("login.txt")) //Создание файла для хранения последнего введеного логина, чтобы при входе он его показывал
            {
                FileStream fs = File.Create("login.txt");
                fs.Close();
            }
            else
            {
                StreamReader rr = File.OpenText("login.txt");
                textBox1.Text = rr.ReadLine();
                rr.Close();
            }

            //Запуска обновляльщика
            Process p = new Process();
            p.StartInfo.FileName = Environment.CurrentDirectory + @"\Update_Payment.exe";
            p.Start(); 
        }
    }
}

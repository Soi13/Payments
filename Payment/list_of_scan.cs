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
using System.IO;
using System.Diagnostics;

namespace Payment
{
    public partial class list_of_scan : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public list_of_scan()
        {
            InitializeComponent();
        }

        //Заполнение DataGridView наименованиями полей 
        public void fill_gridview()
        {
            dataGridView1.Columns["ID"].HeaderText = "ID";
            dataGridView1.Columns["ID"].Width = 20;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["IMAGE"].HeaderText = "IMAGE";
            dataGridView1.Columns["IMAGE"].Width = 20;
            dataGridView1.Columns["IMAGE"].Visible = false;
            dataGridView1.Columns["USER_ID"].HeaderText = "USER_ID";
            dataGridView1.Columns["USER_ID"].Width = 20;
            dataGridView1.Columns["USER_ID"].Visible = false;
            dataGridView1.Columns["ZADANIE_PLAT_ID"].HeaderText = "ZADANIE_PLAT_ID";
            dataGridView1.Columns["ZADANIE_PLAT_ID"].Width = 110;
            dataGridView1.Columns["ZADANIE_PLAT_ID"].Visible=false;
            dataGridView1.Columns["DATETIME_CREATE"].HeaderText = "DATETIME_CREATE";
            dataGridView1.Columns["DATETIME_CREATE"].Width = 150;
            dataGridView1.Columns["DATETIME_CREATE"].Visible = false;
            dataGridView1.Columns["FILE_NAME"].HeaderText = "Имя файла скана";
            dataGridView1.Columns["FILE_NAME"].Width = 500;       

        }
        //////////////////////////////////////////////////////

        private void list_of_scan_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from IMAGES where ZADANIE_PLAT_ID="+view_data.value_4_scan, conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "IMAGES");
            dataGridView1.DataSource = ds.Tables[0];

            statusStrip1.Items[0].Text = "Всего файлов: " + Convert.ToString(ds.Tables[0].Rows.Count);

            fill_gridview();

            //Создание папки в папке МОИ ДОКУМЕНТЫ пользователя для хранения временных файлов открываемых  из БД
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal); //путь к Документам
            if (Directory.Exists(md + @"\zadan_plat") == false)
            {
                Directory.CreateDirectory(md + @"\zadan_plat");
            }
            ////////////////////////
        }

        private void удалитьФайлсканToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Проверка на статус. Если Приянто или отвергнуто, то изменение задания на платеж не возможно
            if ((view_data.value_4_deny_del_image == "Принято") || (view_data.value_4_deny_del_image == "Отвергнуто"))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Запрещено удалять привязанные файлы у заданий на платеж с присвоеным статусом \"" + view_data.value_4_deny_del_image + "\".", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить данный файл?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                /////////Удаление у текущей записи привязанного файла
                SqlCommand scmd = conn.CreateCommand();
                scmd.CommandText = "delete from IMAGES where ID=" + dataGridView1.CurrentRow.Cells[0].Value;
                try
                {
                    conn.Open();
                }
                catch { }
                SqlDataReader reader;
                reader = scmd.ExecuteReader();
                conn.Close();
                //////////////////

                //Обновление данных в гриде после удаления файла
                SqlCommand command = new SqlCommand("select * from IMAGES where ZADANIE_PLAT_ID=" + view_data.value_4_scan, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "IMAGES");
                dataGridView1.DataSource = ds.Tables[0];
                /////////////////

                statusStrip1.Items[0].Text = "Всего файлов: " + Convert.ToString(ds.Tables[0].Rows.Count);

                fill_gridview();

                SystemSounds.Beep.Play();
                MessageBox.Show("Файл удален удачно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {      
            SqlCommand command = new SqlCommand("select * from IMAGES where ID=" + dataGridView1.CurrentRow.Cells[0].Value, conn);
            SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            conn.Close();
            //Заполнение DataGridView наименованиями полей 
            da.Fill(ds, "IMAGES");

            string file = ds.Tables[0].Rows[0][5].ToString();
            byte[] fileByteArray = (byte[])ds.Tables[0].Rows[0][1];

            FileStream fileStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\zadan_plat\" + file, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binWriter = new BinaryWriter(fileStream);
            binWriter.Write(fileByteArray);
            binWriter.Close();

            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\zadan_plat\" + file);


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void сохранитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            { 
                SqlCommand command = new SqlCommand("select * from IMAGES where ID=" + dataGridView1.CurrentRow.Cells[0].Value, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "IMAGES");

                string file = ds.Tables[0].Rows[0][5].ToString();
                byte[] fileByteArray = (byte[])ds.Tables[0].Rows[0][1];

                FileStream fileStream = new FileStream(folderBrowserDialog1.SelectedPath+@"\" + file, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binWriter = new BinaryWriter(fileStream);
                binWriter.Write(fileByteArray);
                binWriter.Close();

                SystemSounds.Beep.Play();
                MessageBox.Show("Файл сохранен удачно!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }         

        }
    }
}

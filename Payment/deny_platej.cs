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
    public partial class deny_platej : Form
    {
         SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        public deny_platej()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if (richTextBox1.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не заполнена Причина отказа!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (filters.set_filter == true)
            {
                view_data view_data = (view_data)this.Owner;
                /////////Обновление данных в БД
                SqlCommand cm = conn.CreateCommand();
                cm.CommandText = "BEGIN TRANSACTION " +
                                 "update ZADANIE_PLAT SET TEXT_DENY='" + richTextBox1.Text + "', STATUS='Отвергнуто', DATE_UPDATE=GETDATE() where ID=" + view_data.value_4_deny +
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

                view_data.refill_with_filter();

                view_data.dataGridView1.CurrentCell = view_data.dataGridView1[3, view_data.value_4_deny_return_position]; // Перемещение к той же записи, которую и одобряли после обновления статуса

                this.Close();
            }
            else
            {
                view_data view_data = (view_data)this.Owner;
                /////////Обновление данных в БД
                SqlCommand cm = conn.CreateCommand();
                cm.CommandText = "BEGIN TRANSACTION " +
                                 "update ZADANIE_PLAT SET TEXT_DENY='" + richTextBox1.Text + "', STATUS='Отвергнуто', DATE_UPDATE=GETDATE() where ID=" + view_data.value_4_deny +
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

                view_data.refill();

                view_data.dataGridView1.CurrentCell = view_data.dataGridView1[3, view_data.value_4_deny_return_position]; // Перемещение к той же записи, которую и одобряли после обновления статуса

                this.Close();
            }
        }
    }
}

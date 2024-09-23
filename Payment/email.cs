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
using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO;

namespace Payment
{
    public partial class email : Form
    {
        SqlConnection conn = new SqlConnection(@"Password=000;Persist Security Info=True;User ID=sa;Initial Catalog=PAYMENT;Data Source=T1212-W00079\MSSQLSERVER2012");

        DataSet ds;

        public email()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((richTextBox1.Text.Length == 0) || (textBox1.Text.Length == 0))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Не введена тема сообщения либо текст сообщения!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите произвести рассылку?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
            {
                SqlCommand command = new SqlCommand("select email from users", conn);
                SqlDataAdapter da = new SqlDataAdapter(command);//Переменная объявлена как глобальная
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                conn.Close();
                //Заполнение DataGridView наименованиями полей 
                da.Fill(ds, "USERS");
            }
            

            //Рассылка без вложения
            if (openFileDialog1.FileName == "")
            {

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    //Add a recipient.
                    // TODO: Change the following recipient where appropriate.
                    if (ds.Tables[0].Rows[i][0].ToString().Length > 0)
                    {
                        // Create the Outlook application by using inline initialization.
                        Outlook.Application oApp = new Outlook.Application();

                        //Create the new message by using the simplest approach.
                        Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

                        Outlook.Recipient oRecip = (Outlook.Recipient)oMsg.Recipients.Add(ds.Tables[0].Rows[i][0].ToString());
                        oRecip.Resolve();

                        //Set the basic properties.
                        oMsg.Subject = textBox1.Text;
                        oMsg.Body = richTextBox1.Text;

                        //Add an attachment.
                        // TODO: change file path where appropriate
                        /*String sSource = "C:\\setupxlg.txt";
                        String sDisplayName = "MyFirstAttachment";
                        int iPosition = (int)oMsg.Body.Length + 1;
                        int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                        Outlook.Attachment oAttach = oMsg.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);
                        */
                        // If you want to, display the message.
                        // oMsg.Display(true);  //modal

                        //Send the message.
                        oMsg.Save();
                        oMsg.Send();
                        //Explicitly release objects.
                        oRecip = null;

                        //oAttach = null;
                        oMsg = null;
                        oApp = null;

                    }
                }
            }

                //Рассылка c вложением
                if (openFileDialog1.FileName != "")
                {

                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        //Add a recipient.
                        // TODO: Change the following recipient where appropriate.
                        if (ds.Tables[0].Rows[i][0].ToString().Length > 0)
                        {
                            // Create the Outlook application by using inline initialization.
                            Outlook.Application oApp = new Outlook.Application();

                            //Create the new message by using the simplest approach.
                            Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

                            Outlook.Recipient oRecip = (Outlook.Recipient)oMsg.Recipients.Add(ds.Tables[0].Rows[i][0].ToString());
                            oRecip.Resolve();

                            //Set the basic properties.
                            oMsg.Subject = textBox1.Text;
                            oMsg.Body = richTextBox1.Text;

                            //Add an attachment.
                            // TODO: change file path where appropriate
                            String sSource = openFileDialog1.FileName;
                            String sDisplayName = Path.GetFileName(openFileDialog1.FileName);
                            int iPosition = (int)oMsg.Body.Length + 1;
                            int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                            Outlook.Attachment oAttach = oMsg.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);

                            // If you want to, display the message.
                            // oMsg.Display(true);  //modal

                            //Send the message.
                            oMsg.Save();
                            oMsg.Send();
                            //Explicitly release objects.
                            oRecip = null;

                            //oAttach = null;
                            oMsg = null;
                            oApp = null;

                        }
                    }

                                      
                }

                SystemSounds.Beep.Play();
                MessageBox.Show("Рассылка произведена удачно.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox1.Clear();
                this.Close();            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label5.Visible = true;
                label5.Text = openFileDialog1.FileName;
            }
        }
    }
}

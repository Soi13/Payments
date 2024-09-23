using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Media;

namespace Payment
{
    class send_status_opl
    {
        public void send_em(string addr, string ka, string sum, string nds)
        {

            try
            {
                // Create the Outlook application by using inline initialization.
                Outlook.Application oApp = new Outlook.Application();

                //Create the new message by using the simplest approach.
                Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

                //Add a recipient.
                // TODO: Change the following recipient where appropriate.
                Outlook.Recipient oRecip = (Outlook.Recipient)oMsg.Recipients.Add(addr);
                oRecip.Resolve();

                //Set the basic properties.
                oMsg.Subject = "Уведомление";
                oMsg.Body = "ЗНП для контрагента: \""+ka+"\" на сумму "+sum+ ", НДС "+nds+" оплачено!";

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

                       // Simple error handler.
            catch (Exception)
            {
                
            }
        }
    }
}

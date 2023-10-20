using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SendMessageLibrary
{
    //Ref: https://sendgrid.com/docs/api-reference/
    // using SendGrid's C# Library
    // https://github.com/sendgrid/sendgrid-csharp
    //PM> Install-Package SendGrid
    //private string AppName = "Sample-Send-Grip-Api";
    //private string SendGridKey = ConfigurationManager.AppSettings["SendGridKey"].ToString(); 

    #region :Usage
    /*
    //Tested Good - 4/4/19
    Email em = new Email()
    {
        sender_email = "donot-reply@email.com",
        sender_name = "Company Name",
        sender_phone = "213-776-0999",
        sender_message = "This is a test message from SendGrid Console...",
        email_subject = "Test SendGrid Email",
        recipient_email = "services@email.com",
        recipient_name = "Company Name"
    };

    SendEmail(em).Wait();

            //Test Good - 4/4/19
            //string sender_email = "donot-reply@email.com";
            //string sender_name = "Company Name";
            //string sender_phone = "213-776-0999";
            //string sender_message = "This is a test message from SendGrid Console...";
            //string email_subject = "Test SendGrid Email";
            //string recipient_email = "services@email.com";
            //string recipient_name = "Tester Co";

            //SendEmail(sender_email, sender_name, sender_phone, sender_message, email_subject, recipient_email, recipient_name).Wait();
    */
    #endregion

    public class SendGridEmailer
    {       
        private string _SendGridKey { get; set; }

        public SendGridEmailer(string sendgridkey)
        {
            _SendGridKey = sendgridkey;
        }

        public async Task SendEmail(Email em)
        {
            try
            {
                var client = new SendGridClient(_SendGridKey);
                var from = new EmailAddress(em.sender_email, em.sender_name);
                var subject = em.email_subject;
                var to = new EmailAddress(em.recipient_email, em.recipient_name);
                var plainTextContent = em.sender_phone + "\n" + em.sender_message;
                var htmlContent = "<div>" + em.sender_phone + "</div><p>" + em.sender_message + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (System.Exception ex)
            {
                string error = ex.ToString();
            }
            
        }

        public async Task SendEmail(Email em, string attachment_path = "")
        {
            try
            {
                var client = new SendGridClient(_SendGridKey);
                var from = new EmailAddress(em.sender_email, em.sender_name);
                var subject = em.email_subject;
                var to = new EmailAddress(em.recipient_email, em.recipient_name);
                var plainTextContent = em.sender_phone + "\n" + em.sender_message;
                var htmlContent = "<div>" + em.sender_phone + "</div><p>" + em.sender_message + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                if (attachment_path != "")
                {
                    FileInfo f = new FileInfo(attachment_path);
                    using (var fileStream = File.OpenRead(attachment_path))
                    {
                        //string fname = File.
                        await msg.AddAttachmentAsync(f.Name, fileStream);
                    }
                }

                var response = await client.SendEmailAsync(msg);
            }
            catch (System.Exception ex)
            {
                string error = ex.ToString();
            }

        }

        public async Task SendEmail(
            string sender_email, string sender_name,
            string sender_phone, string sender_message,
            string email_subject, string recipient_email, string recipient_name, string attachment_path = "")
        {
            try
            {
                var client = new SendGridClient(_SendGridKey);
                var from = new EmailAddress(sender_email, sender_name);
                var subject = email_subject;
                var to = new EmailAddress(recipient_email, recipient_name);
                var plainTextContent = sender_phone + "\n" + sender_message;
                var htmlContent = "<div>" + sender_phone + "</div><p>" + sender_message + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                if (attachment_path != "")
                {
                    FileInfo f = new FileInfo(attachment_path);
                    using (var fileStream = File.OpenRead(attachment_path))
                    {
                        //string fname = File.
                        await msg.AddAttachmentAsync(f.Name, fileStream);
                    }
                }
                var response = await client.SendEmailAsync(msg);
            }
            catch (System.Exception ex)
            {
                string error = ex.ToString();
            }
            
        }
    }
}

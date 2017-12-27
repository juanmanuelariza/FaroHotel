using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace FaroHotel.Models
{
    public class SendMail
    {
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }


        public void Send()
        {
            //Envio Mail
            var body = "<p>{0}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(this.Destinatario));  // replace with valid value 
            message.From = new MailAddress("sistemaelfarohotel@gmail.com");  // replace with valid value
            message.ReplyToList.Add("sistemaelfarohotel@gmail.com");
            //message.From = new MailAddress("virginiaharanda@gmail.com");  // replace with valid value
            //message.ReplyToList.Add("virginiaharanda@gmail.com");
            message.Subject = this.Asunto;
            message.Body = string.Format(body, this.Mensaje);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "sistemaelfarohotel@gmail.com",  // replace with valid value
                    Password = "C3pmm6hRUr7q07XqfrVS"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            };

        }
    }

}
using GoAndSee_API.Data;
using GoAndSee_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace GoAndSee_API.Business
{
    public class Mail
    {
        ISubobjectDataAccess isobject = new SubobjectDataAccess();
        public void sendMail(Rating rating, string question, string email)
        {
            string subobjects = processContact(rating.Rsoid);

            MailMessage message = new MailMessage();
            message.IsBodyHtml = false;
            message.Sender = new MailAddress("no-reply@austrian.com");
            message.IsBodyHtml = false;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.From = message.Sender;
            message.To.Add(email);
            message.Subject = "Unzureichende Objektbewertung";
            message.Body = String.Format("Objekt/e: {0} ist/sind in einem inakzeptablen Zustand!\n {1} - Unzureichend\n", subobjects, question);
            // Global
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-relay.gate01.skylines.global";
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("AP51046", "EyT|aaVLSe^_(f(j-VA.];X6>", "gate01");
            smtp.Send(message);
        }

        public string processContact(string contactjson)
        {
            string subobjects = null;

            List<string> rids = JsonConvert.DeserializeObject<List<string>>(isobject.readSubobjectsBySId(contactjson));
            for (int i = 0; i < rids.Count; ++i)
            {
                if (i > 0 && rids.Count > 1)
                {
                    subobjects += ",";
                }
                subobjects += rids[i];
            }
            return subobjects;
        }
    }
}
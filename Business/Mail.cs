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
        IObjectDataAccess @object = new ObjectDataAccess();
        ISubobjectDataAccess sobject = new SubobjectDataAccess();
        public void sendMail(Rating rating, string question, string email)
        {
            string objectname = @object.readObject(rating.Roid).Oname;
            string subobject = sobject.readSubobjectsBySId(rating.Rsoid);

            MailMessage message = new MailMessage();
            message.IsBodyHtml = false;
            message.Sender = new MailAddress("no-reply@austrian.com");
            message.IsBodyHtml = false;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.From = message.Sender;
            message.To.Add(email);
            message.Subject = "Unzureichende Objektbewertung";
            message.Body = String.Format("Objekt: {0} - {1}: {2} ist in einem inakzeptablen Zustand!\n {3} - Unzureichend\n", objectname, rating.Rsoid,subobject, question);
            // Global
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-relay.gate01.skylines.global";
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("AP51046", "EyT|aaVLSe^_(f(j-VA.];X6>", "gate01");
            smtp.Send(message);
        }
    }
}
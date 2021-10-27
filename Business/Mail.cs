using GoAndSee_API.Controllers;
using GoAndSee_API.Data;
using GoAndSee_API.Data.Access;
using GoAndSee_API.Data.Interface;
using GoAndSee_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using static GoAndSee_API.Controllers.UserModel;

namespace GoAndSee_API.Business
{
    public class Mail
    {
        IObjectDataAccess @object = new ObjectDataAccess();
        ISubobjectDataAccess sobject = new SubobjectDataAccess();
        // Ramiz 
        IRatingAnswerDataAccess ra = new RatingAnswerDataAccess(); 
        UserAuth user = new UserAuth(); 
        

        // Ramiz: hab einen weiteren Parameter "answer" hinzugefügt um den Kommentar für die Frage per Mail schicken zu können
        public void sendMail(Rating rating, string question, string email, string answer, string image, string contenttype)
        {
            // Ramiz
            string userId = user.activeUser();
            string answerText = (answer == "") ? "" : answer;
           

            



            string objectname = @object.readObject(rating.Roid).Oname;
            string subobject = sobject.readSubobjectsBySId(rating.Rsoid);

            MailMessage message = new MailMessage();
            message.IsBodyHtml = false;
            message.Sender = new MailAddress("no-reply@austrian.com");
            message.IsBodyHtml = false;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.From = message.Sender;
            message.To.Add(email);
            // Ramiz Attachment
            if (!String.IsNullOrEmpty(image))
            {
                var imageBytes = Convert.FromBase64String(image);
                var stream = new MemoryStream(imageBytes);
                message.Attachments.Add(new Attachment(stream, "image."+contenttype));// you may want to provide a name here


            }

            message.Subject = String.Format("{0}", question);
            message.Body = String.Format("Objekt: '{0}' / '{1}': \nBetreff: '{2}'\nWho: '{3}'\nProblem: '{4}'", objectname, subobject, question, userId, answerText);
            // Global
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-relay.gate01.skylines.global";
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("AP51046", "EyT|aaVLSe^_(f(j-VA.];X6>", "gate01");
            smtp.Send(message);
        }
    }
}
using GoAndSee_API.Data;
using GoAndSee_API.Data.Access;
using GoAndSee_API.Data.Interface;
using GoAndSee_API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoAndSee_API.Business
{
    public class RatingAnswerProcessing
    {
        IRatingAnswerDataAccess irating = new RatingAnswerDataAccess();
        IQuestionDataAccess iquestion = new QuestionDataAccess();
        Mail mail = new Mail();

        public void processRating(Rating rating)
        {
            Question question = iquestion.readQuestion(rating.Roid);
            List<string> questionlist = JsonConvert.DeserializeObject<List<string>>(question.QName);
            List<string> contactlist = JsonConvert.DeserializeObject<List<string>>(question.QContact);
            IList<RatingAnswer> ratinganswers = JsonConvert.DeserializeObject<List<RatingAnswer>>(rating.Rrating);

            for (int i = 0; i < ratinganswers.Count; ++i)
            {
                ratinganswers[i].Rarid = rating.Rid;
                ratinganswers[i].Raquestion = questionlist[i];

                if (ratinganswers[i].Rarating == 0 && contactlist[i] !="")
                {
                    mail.sendMail(rating, questionlist[i], contactlist[i]);               
                }
                irating.createRatingAnswer(ratinganswers[i]);
            }
        }

        public List<RatingAnswerDTO> prepareRatingAnswer(string id)
        {
            List<RatingAnswerDTO> ratinganswer = irating.readAnswersbyId(id);
            return ratinganswer;
        }
    }
}

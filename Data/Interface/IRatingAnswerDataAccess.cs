using GoAndSee_API.Models;
using System.Collections.Generic;

namespace GoAndSee_API.Data.Interface
{
    public interface IRatingAnswerDataAccess
    {
        void createRatingAnswer(RatingAnswer ratinganswer);
        List<RatingAnswerDTO> readAnswersbyId(string id);
        RatingAnswer readAnswer(string id);
    }
}

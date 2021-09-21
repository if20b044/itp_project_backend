using GoAndSee_API.Models;
using System.Collections.Generic;

namespace GoAndSee_API.Data
{
    public interface IQuestionDataAccess
    {
        Question readQuestion(string id);
        List<Question> readAllQuestion();
        void deleteQuestion(string id);
        void deleteObjectQuestion(string id);
        void createQuestion(Question question);
        void updateQuestion(Question question);
        string readQuestionId(string id);
    }
}

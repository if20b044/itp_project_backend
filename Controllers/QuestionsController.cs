using GoAndSee_API.Models;
using GoAndSee_API.Data;
using System.Web.Http;

namespace GoAndSee_API.Controllers
{
    public class QuestionsController : ApiController
    {
        private IQuestionDataAccess iquestion = new QuestionDataAccess();
        private IObjectDataAccess iobject = new ObjectDataAccess();

        public Question Get(string id)
        {
            Question question = iquestion.readQuestion(id);
            return question;
        }

        public IHttpActionResult Post(Question question)
        {
            iquestion.createQuestion(question);
            return Ok();
        }

        public IHttpActionResult Put(Question question)
        {
            question.QId = iquestion.readQuestionId(question.Objectid);
            iquestion.updateQuestion(question);
            iobject.updateLastModified(question.Objectid);
            return Ok();
        }

        public IHttpActionResult Delete(string id)
        {
            iquestion.deleteQuestion(id);
            return Ok();
        }
    }
}

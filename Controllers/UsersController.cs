using GoAndSee_API.Business;
using System.Web.Http;

namespace GoAndSee_API.Controllers
{
    public class UsersController : ApiController
    {
        private UserAuth user = new UserAuth();

        public string Get()
        {
            return user.getUsername();
        }

    }
}
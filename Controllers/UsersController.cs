using GoAndSee_API.Business;
using System.Web.Http;

namespace GoAndSee_API.Controllers
{
    public class UserModel
    {
        public string userId { get; set; }
        public bool isAdmin { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    public class UsersController : ApiController
    {
        private UserAuth user = new UserAuth();
        private UserModel model = new UserModel(); 
        

        public UserModel Get()
        {
            model.userId = user.activeUser();
            model.isAdmin = user.isAdmin();
            model.firstName = user.getUsername();
            model.lastName = user.getSurname(); 
            return model;
        }
    }
}
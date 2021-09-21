 using System;
using System.Web;

namespace GoAndSee_API.Business
{
    public class UserAuth
    {
        public string activeUser()
        {
            string currentUser = "";
            try {
                currentUser = HttpContext.Current.User.Identity.Name.ToString();
                if (!String.IsNullOrEmpty(currentUser) && currentUser.IndexOf("\\") != -1)
                {
                    string user = currentUser.Split('\\')[1];
                    
                    if (!String.IsNullOrEmpty(user))
                    {
                        return user;
                    }
                }
            } catch (Exception e)
            {

            }
            return currentUser;
        }
    }
}
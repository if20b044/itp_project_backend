using System;
using System.DirectoryServices.AccountManagement;
using System.Web;

namespace GoAndSee_API.Business
{
    public class UserAuth
    {
        public string activeUser()
        {
            string currentUser = "";
            try
            {
                currentUser = HttpContext.Current.User.Identity.Name.ToString();
                if (!String.IsNullOrEmpty(currentUser) && currentUser.IndexOf("\\") != -1)
                {
                    string user = currentUser.Split('\\')[1];

                    if (!String.IsNullOrEmpty(user))
                    {
                        return user;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return currentUser;
        }

        public string getUsername()
        {
            string name = "";
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                var usr = UserPrincipal.FindByIdentity(context, activeUser());
                if (usr != null)
                    name = usr.GivenName;
                if (name == "")
                    throw new Exception("The UserId is not present in Active Directory");
            }
            return name;
        }
    }
}
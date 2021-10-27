using System;
using System.DirectoryServices.AccountManagement;
using System.Web;

namespace GoAndSee_API.Business
{
    // Zeigt die UserID 
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
                throw new ArgumentException("Error: ", e);
            }
            return currentUser;
        }
        // Hier der Name zur UserID 
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
        // Ist der User in der Gruppe die Admin sein dürfen
        public bool isAdmin()
        {
            var ad = new AD.ActiveDirectoryLayer();
            return ad.IsUserInGroup("GATE01", activeUser(), "papp.gate01.GoAndSeeAdmin.allow"); 
        }

        public string getSurname()
        {
            string name = "";
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                var usr = UserPrincipal.FindByIdentity(context, activeUser());
                if (usr != null)
                    name = usr.Surname;

                if (name == "")
                    throw new Exception("The UserId is not present in Active Directory");
            }
            return name;
        }
    }
}
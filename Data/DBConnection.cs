using System;
using System.Configuration;

namespace GoAndSee_API.Data
{
    public class DBConnection
    {
        public string getDBConfiguration(string db)
        {
            if (String.IsNullOrEmpty(db))
            {
                db = "default";
            }
            string strcon = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            return strcon;
        }
    }
}

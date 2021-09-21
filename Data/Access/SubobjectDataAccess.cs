using GoAndSee_API.Business;
using GoAndSee_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GoAndSee_API.Data
{
    public class SubobjectDataAccess : ISubobjectDataAccess
    {
        DBConnection dbcon = new DBConnection();
        UserAuth user = new UserAuth();
        public void createSubobject(Subobject subobject)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Insert into Subobjects (sname,soid,ouserid) values(@Sname,@Soid,@Suserid )", con);
                    cmd.Parameters.AddWithValue("Sname", subobject.Stitle);
                    cmd.Parameters.AddWithValue("Soid", subobject.Sobjectid);
                    cmd.Parameters.AddWithValue("Suserid", user.activeUser());

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public void deleteSubobject(string id)
        {
            throw new NotImplementedException();
        }

        public List<string> readAllSubobjectsOId(string id)
        {
            List<string> slist = new List<string>();
            List<string> namelist = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("SELECT sname from Subobjects where soid=@id", con);
                    cmd.Parameters.AddWithValue("id", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string subobject = "null";

                            subobject = dr["sname"].ToString();

                            slist.Add(subobject);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    dr.Close();
                    con.Close();

                    for(int i = 0; i < slist.Count; ++i)
                    {
                        List<string> processList = JsonConvert.DeserializeObject<List<string>>(slist[i]);
                        for (int m = 0; m < processList.Count;++m)
                        {
                            namelist.Add(processList[m]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return namelist;
        }

        public List<Subobject> readAllSubobject()
        {
            List<Subobject> slist = new List<Subobject>();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("SELECT soid, _sid, sname from Subobjects", con);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Subobject subobject = new Subobject();
                            subobject.Sid = dr["_sid"].ToString();
                            subobject.Sobjectid = dr["soid"].ToString();
                            subobject.Stitle = dr["sname"].ToString();

                            slist.Add(subobject);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return slist;
        }

        public string readSubobjectsBySId(string id)
        {
            string rids = null;

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("SELECT sname from Subobjects where _sid= @id  ", con);
                    cmd.Parameters.AddWithValue("id", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rids = dr["sname"].ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return rids;
        }

        public List<Subobject> readSubobjectbyOId(string id)
        {
            List<Subobject> slist = new List<Subobject>();
            
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("SELECT soid, _sid, sname, ouserid from Subobjects where soid= @id  ", con);
                    cmd.Parameters.AddWithValue("id", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Subobject subobject = new Subobject();
                            subobject.Sobjectid = dr["soid"].ToString();
                            subobject.Sid = dr["_sid"].ToString();
                            subobject.Stitle = dr["sname"].ToString();
                            subobject.Suserid = dr["ouserid"].ToString();

                            slist.Add(subobject);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return slist;
        }
    }
}

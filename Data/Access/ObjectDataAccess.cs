using GoAndSee_API.Business;
using GoAndSee_API.Models;
using GoAndSee_API.Models.DTO.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Object = GoAndSee_API.Models.Object;

namespace GoAndSee_API.Data
{
    public class ObjectDataAccess : IObjectDataAccess
    {
        DBConnection dbcon = new DBConnection();
        IQuestionDataAccess iquestion = new QuestionDataAccess();
        ISubobjectDataAccess isubobject = new SubobjectDataAccess();
        UserAuth user = new UserAuth();

        public void createObject(Object @object)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Insert into _Objects (oname,odescription,ointerval,ouserid) values(@Oname,@Odescription,@Ointerval,@Ouserid )", con);
                    cmd.Parameters.AddWithValue("Oname", @object.Oname);
                    cmd.Parameters.AddWithValue("Odescription", @object.Odescription);
                    cmd.Parameters.AddWithValue("Ointerval", @object.Ointerval);
                    cmd.Parameters.AddWithValue("Ouserid", user.activeUser());

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Close();
                    con.Close();
                }
                readObjectId(@object);
                Question question = CreateQuestion(@object);
                iquestion.createQuestion(question);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                System.Diagnostics.EventLog.WriteEntry("GoandSee", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        public string updateObject(Object @object)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Update  _Objects set oname = @objname, olastmod = GETDATE(), odescription = @objdescription, ointerval = @objinterval  where oid = @id", con);
                    cmd.Parameters.AddWithValue("id", @object.Oid);
                    cmd.Parameters.AddWithValue("objname", @object.Oname);
                    cmd.Parameters.AddWithValue("objdescription", @object.Odescription);
                    cmd.Parameters.AddWithValue("objinterval", @object.Ointerval);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Close();
                    con.Close();

                    Question question = CreateQuestion(@object);
                    iquestion.updateQuestion(question);
                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            return @object.Oid;
        }

        public void updateLastModified(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Update  _Objects set olastmod = GETDATE() where oid = @id", con);
                    cmd.Parameters.AddWithValue("id", id);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
        }

        private void readObjectId(Object @object)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Select * from _Objects where oname=@Oname and odescription=@Odescription and ointerval=@Ointerval", con);
                    cmd.Parameters.AddWithValue("Oname", @object.Oname);
                    cmd.Parameters.AddWithValue("Odescription", @object.Odescription);
                    cmd.Parameters.AddWithValue("Ointerval", @object.Ointerval);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            @object.Oid = dr["oid"].ToString();
                        }
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
        }

        public void deleteObject(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Delete from _Objects where lower(oid)= @id  ", con);
                    cmd.Parameters.AddWithValue("id", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Close();
                    con.Close();

                    iquestion.deleteObjectQuestion(id);
                    isubobject.deleteSubobject(id);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
        }

        public List<ObjectListDTO> readAllObject()
        {
            List<ObjectListDTO> olist = new List<ObjectListDTO>();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("SELECT o.oid,o.olastmod, o.oname, o.odescription, o.ointerval,o.ouserid, o.otimestamp, q.qname FROM _Objects as o inner join Questions as q on o.oid = q.qoid", con);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ObjectListDTO objectDTO = new ObjectListDTO();
                            objectDTO.Oid = dr["oid"].ToString();
                            objectDTO.Oname = dr["oname"].ToString();
                            objectDTO.Ouserid = dr["ouserid"].ToString();
                            string questions = dr["qname"].ToString();
                            objectDTO.OnumOfquestion = countQuestions(questions);

                            if (dr["otimestamp"] != DBNull.Value)
                                objectDTO.Ocreated = (DateTime)dr["otimestamp"];
                            if (dr["olastmod"] != DBNull.Value)
                                objectDTO.OlastModified = (DateTime)dr["olastmod"];

                            olist.Add(objectDTO);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No data found.");
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            return olist;
        }

        public ObjectDTO readObject(string id)
        {
            ObjectDTO objectDTO = new ObjectDTO();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("SELECT o.oid,o.olastmod, o.oname, o.odescription, o.ointerval,o.ouserid, o.otimestamp, q.qname FROM _Objects as o inner join Questions as q on o.oid = q.qoid where oid= @id  ", con);
                    cmd.Parameters.AddWithValue("id", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            objectDTO.Oid = dr["oid"].ToString();
                            objectDTO.Oname = dr["oname"].ToString();
                            if (dr["otimestamp"] != DBNull.Value)
                                objectDTO.Ocreated = (DateTime)dr["otimestamp"];
                            objectDTO.Odescription = dr["odescription"].ToString();
                            objectDTO.Ointerval = (int)dr["ointerval"];
                            objectDTO.Oquestions = processQuestions(objectDTO.Oid);
                            objectDTO.Osubobjects = processSubobjects(objectDTO.Oid);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No data found.");
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            return objectDTO;
        }

        public string processQuestions(string id)
        {
            Question question = iquestion.readQuestion(id);
            List<ObjInfo> composition = new List<ObjInfo>();
            List<string> questionlist = JsonConvert.DeserializeObject<List<string>>(question.QName);
            List<string> contactlist = JsonConvert.DeserializeObject<List<string>>(question.QContact);

            for (int i = 0; i < questionlist.Count && i < contactlist.Count; ++i)
            {
                ObjInfo objinfo = new ObjInfo();
                objinfo.title = questionlist[i];
                objinfo.contact = contactlist[i];
                composition.Add(objinfo);
            }
            return JsonConvert.SerializeObject(composition);
        }

        public string processSubobjects(string id)
        {
            List<string> subs = isubobject.readAllSubobjectsOId(id);

            return JsonConvert.SerializeObject(subs);
        }

        public Question CreateQuestion(Object @object)
        {
            Question question = new Question();
            question.Objectid = @object.Oid;
            question.QName = @object.Oquestions;
            question.QUserid = @object.Ouserid;
            question.QContact = @object.Ocontact;
         
            return question;
        }

        public int countQuestions (string questions)
        {
            string[] questionarray = JsonConvert.DeserializeObject<string[]>(questions);
            return questionarray.Length;
        }
    }
}

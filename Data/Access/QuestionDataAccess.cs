using GoAndSee_API.Business;
using GoAndSee_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GoAndSee_API.Data
{
    public class QuestionDataAccess : IQuestionDataAccess
    {
        DBConnection dbcon = new DBConnection();
        UserAuth user = new UserAuth();
        public void createQuestion(Question question)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Insert into Questions (qname,qoid,quserid,qcontact) values(@Qname,@Qoid,@Quserid, @Qcontact )", con);
                    cmd.Parameters.AddWithValue("Qname", question.QName);
                    cmd.Parameters.AddWithValue("Qoid", question.Objectid);
                    cmd.Parameters.AddWithValue("Quserid", user.activeUser());
                    cmd.Parameters.AddWithValue("Qcontact", question.QContact);
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

        public void deleteObjectQuestion(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Questions where lower(qoid)= @id  ", con);
                    cmd.Parameters.AddWithValue("id", id);
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

        public string readQuestionId(string id)
        {
            string qid = null;
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from Questions where qoid=@id  ", con);
                    cmd.Parameters.AddWithValue("id", id);

                    SqlDataReader dr = cmd.ExecuteReader();


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            qid = dr["qid"].ToString();
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
            return qid;
        }

        public void updateQuestion(Question question)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Questions set qname = @name, qcontact = @contact where qoid=@id  ", con);
                    cmd.Parameters.AddWithValue("id",question.Objectid);
                    cmd.Parameters.AddWithValue("name", question.QName);
                    cmd.Parameters.AddWithValue("contact", question.QContact);
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
        public void deleteQuestion(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Questions where lower(qid)= @id  ", con);
                    cmd.Parameters.AddWithValue("id", id);
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

        public List<Question> readAllQuestion()
        {
            throw new NotImplementedException();
        }

        public Question readQuestion(string id)
        {
            Question question = new Question();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select* from Questions where lower(qoid)= @id  ", con);
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            question.Objectid = dr["qoid"].ToString();
                            question.QId = dr["qid"].ToString();
                            question.QName = dr["qname"].ToString();
                            question.QContact = dr["qcontact"].ToString();
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

            return question;
        }
    }
}

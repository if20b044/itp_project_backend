using GoAndSee_API.Business;
using GoAndSee_API.Data.Interface;
using GoAndSee_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GoAndSee_API.Data.Access
{
    public class RatingAnswerDataAccess : IRatingAnswerDataAccess
    {
        DBConnection dbcon = new DBConnection();
        UserAuth user = new UserAuth();
        public void createRatingAnswer(RatingAnswer ratinganswer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                        SqlCommand cmd = new SqlCommand("Insert into RatingAnswers (rarid,racomment,rarating,raquestion,raattachment, racontenttype) values(@Rarid,@Racomment,@Rarating,@Raquestion, @Raattachment, @Racontenttype)", con);
                        cmd.Parameters.AddWithValue("Rarid", ratinganswer.Rarid);
         
                        cmd.Parameters.AddWithValue("Rarating", ratinganswer.Rarating);
                        cmd.Parameters.AddWithValue("Raquestion", ratinganswer.Raquestion);
                    
                    if (ratinganswer.Raattachment == null)
                    {
                        cmd.Parameters.AddWithValue("Raattachment", DBNull.Value);
                        cmd.Parameters.AddWithValue("Racontenttype", DBNull.Value);
                   
                    } else
                    {
                        cmd.Parameters.AddWithValue("Raattachment", ratinganswer.Raattachment);
                        cmd.Parameters.AddWithValue("Racontenttype", ratinganswer.Raimagetype);
                    }

                    if (ratinganswer.Racomment == null)
                    {
                        cmd.Parameters.AddWithValue("Racomment", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("Racomment", ratinganswer.Racomment);
                    }
                    

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Create Exception: " + ex.Message);
            }
        }

        public RatingAnswer readAnswer(string id)
        {
            RatingAnswer ra = new RatingAnswer();
            
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Select * from RatingAnswers where raid=@Raid", con);
                    cmd.Parameters.AddWithValue("Raid", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ra.Raquestion = dr["raquestion"].ToString();
                            ra.Rarating = (int)dr["rarating"];
                            //ra.Racomment = dr["racomment"].ToString();
                        }
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Read Exception: " + ex.Message);
            }
            return ra;
        }

        public List<RatingAnswerDTO> readAnswersbyId(string id)
        {
            List<RatingAnswerDTO> ralist = new List<RatingAnswerDTO>();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Select raquestion, rarating, raattachment, racontenttype from RatingAnswers where rarid=@Rid", con);
                    cmd.Parameters.AddWithValue("Rid", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            RatingAnswerDTO ranswer = new RatingAnswerDTO();
                            ranswer.Title = dr["raquestion"].ToString();
                            ranswer.Rating =(int)dr["rarating"];
                            if(ranswer.Rating ==0)
                            {
                                if (dr["raattachment"] != DBNull.Value)
                                {
                                    ranswer.Attachment = dr["raattachment"].ToString();
                                    ranswer.Contenttype = (dr["racontenttype"].ToString());
                                }
                                
                   
                            }
                            ralist.Add(ranswer);
                        }
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadId Exception: " + ex.Message);
            }
            return ralist;
        }
    }
}

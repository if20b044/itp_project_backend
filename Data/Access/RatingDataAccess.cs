using GoAndSee_API.Business;
using GoAndSee_API.Data.Interface;
using GoAndSee_API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GoAndSee_API.Data.Access
{
    public class RatingDataAccess : IRatingDataAccess
    {
        DBConnection dbcon = new DBConnection();
        UserAuth user = new UserAuth();

        public void createRating(Rating rating)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Insert into Ratings (roid,ruserid,rsoid,roname,rsname) values(@Roid,@Ruserid,@Rsoid,@Roname,@Rsname)", con);
                    cmd.Parameters.AddWithValue("Roid", rating.Roid);
                    cmd.Parameters.AddWithValue("Ruserid", user.activeUser());
                    cmd.Parameters.AddWithValue("Rsoid", rating.Rsoid);
                    cmd.Parameters.AddWithValue("Rsname", rating.Rsname);
                    cmd.Parameters.AddWithValue("Roname", rating.Roname);

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

        public RatingDTO readRating(string id)
        {
            RatingDTO rating = new RatingDTO();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Select rid,roid,rsoid, rtimestamp, roname, rsname  from Ratings where rid=@Rid Order By rtimestamp Desc", con);
                    cmd.Parameters.AddWithValue("Rid", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rating.Rid = dr["rid"].ToString();
                            rating.Roname = dr["roname"].ToString();
                            rating.Roid = dr["roid"].ToString();
                            rating.Rsid = dr["rsoid"].ToString();
                            rating.Rsname = dr["rsname"].ToString();
                            if (dr["rtimestamp"] != DBNull.Value)
                                rating.Rtimestamp = (DateTime)dr["rtimestamp"];
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
            return rating;
        }

        public List<RatingDTO> readAllRating()
        {
            List<RatingDTO> rlist = new List<RatingDTO>();

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Select rid, roid,rsoid, rtimestamp, roname, rsname  from Ratings where ruserid=@user Order By rtimestamp Desc", con);
                    cmd.Parameters.AddWithValue("user", user.activeUser());

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            RatingDTO rating = new RatingDTO();
                            rating.Roid = dr["roid"].ToString();
                            rating.Roname = dr["roname"].ToString();
                            rating.Rsid = dr["rsoid"].ToString();
                            rating.Rsname = dr["rsname"].ToString();
                            rating.Rid = dr["rid"].ToString();
                            if (dr["rtimestamp"] != DBNull.Value)
                                rating.Rtimestamp = (DateTime)dr["rtimestamp"];
                            rlist.Add(rating);
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

            return rlist;
        }

        public DateTime readRatingDate(string id)
        {
            DateTime lastModified = new DateTime() ;
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {

                    SqlCommand cmd = new SqlCommand("Select Top 1 * From Ratings where roid=@id Order By rtimestamp Desc;", con);
                    cmd.Parameters.AddWithValue("id", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr["otimestamp"] != DBNull.Value)
                                lastModified = (DateTime)dr["rtimestamp"];
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
            return lastModified;
        }

        public void setRatingId(Rating rating)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.getDBConfiguration("default")))
                {
                    SqlCommand cmd = new SqlCommand("Select * from Ratings where roid=@Roid and ruserid=@Ruserid", con);
                    cmd.Parameters.AddWithValue("Roid", rating.Roid);
                    cmd.Parameters.AddWithValue("Ruserid", user.activeUser());

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rating.Rid = dr["rid"].ToString();
                        }
                    } 
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

    }
}

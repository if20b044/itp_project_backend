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
                    SqlCommand cmd = new SqlCommand("Insert into Ratings (roid,ruserid,rsoid) values(@Roid,@Ruserid,@Rsoid)", con);
                    cmd.Parameters.AddWithValue("Roid", rating.Roid);
                    cmd.Parameters.AddWithValue("Ruserid", user.activeUser());
                    cmd.Parameters.AddWithValue("Rsoid", rating.Rsoid);

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

                    SqlCommand cmd = new SqlCommand("Select r.rid,r.roid,r.rsoid, r.rtimestamp, o.oname, s.sname  from Ratings as r  inner join _Objects as o on o.oid = r.roid inner join Subobjects as s on r.rsoid = s._sid where r.rid=@Rid Order By r.rtimestamp Desc", con);
                    cmd.Parameters.AddWithValue("Rid", id);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rating.Rid = dr["rid"].ToString();
                            rating.Roname = dr["oname"].ToString();
                            rating.Roid = dr["roid"].ToString();
                            rating.Rsid = dr["rsoid"].ToString();
                            rating.Rsname = dr["sname"].ToString();
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

                    SqlCommand cmd = new SqlCommand("Select r.rid, r.roid,r.rsoid, r.rtimestamp, o.oname, s.sname  from Ratings as r  inner join _Objects as o on o.oid = r.roid inner join Subobjects as s on r.rsoid = s._sid where r.ruserid=@user Order By r.rtimestamp Desc", con);
                    cmd.Parameters.AddWithValue("user", user.activeUser());

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            RatingDTO rating = new RatingDTO();
                            rating.Roid = dr["roid"].ToString();
                            rating.Roname = dr["oname"].ToString();
                            rating.Rsid = dr["rsoid"].ToString();
                            rating.Rsname = dr["sname"].ToString();
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

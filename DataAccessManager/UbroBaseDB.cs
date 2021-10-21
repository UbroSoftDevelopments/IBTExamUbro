using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace IBTExamUbroAPI.DataAccessManager
{
    public class UbroBaseDB
    {
        DataTable dt;
        #region members
        readonly MySqlConnection con;
        MySqlDataAdapter adp;
        
       
        #endregion

        #region PrivateMethods
        public UbroBaseDB()
        {
            con = new MySqlConnection("Data Source=sg2nlmysql53plsk.secureserver.net;Persist Security Info=True;User ID=mrs;Password=Ubro@123;sslmode=None;database=ubrobase");//persistsecurityinfo=True;user id=mrs;server=sg2nlmysql53plsk.secureserver.net;sslmode=None;database=ubrobase
        }
        void OpenConnection()
        {

            if (con.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    con.Open();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "\n" + ex.InnerException);
                }

            }
            else { }


        }
        void CloseConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }

        #endregion
        public DataTable GetDataFromTable(string tablename)
        {
            OpenConnection();
            adp = new MySqlDataAdapter("SELECT * FROM ubrobase." + tablename, con);
            dt = new DataTable();
            adp.Fill(dt);
            adp.Dispose();
            CloseConnection();
            return dt;

        }
        public DataTable GetDataFromTableByEnrollment(string enrollment)
        {
            OpenConnection();
            adp = new MySqlDataAdapter("SELECT * FROM ubrobase.recimg where enroll='" + enrollment+"'", con);
            dt = new DataTable();
            adp.Fill(dt);
            adp.Dispose();
            CloseConnection();
            return dt;

        }
        public void InsertImageWithEnrollment(Models.RecordedImages recorded)
        {
            string InsterQuery = "insert into ubrobase.recimg(enroll,recimg) values(@enroll,@recimg)";
            con.Open();
            MySqlCommand insertCommand = new MySqlCommand(InsterQuery, con);
            insertCommand.Parameters.AddWithValue("@enroll", recorded.EnrollmentNumber);
            insertCommand.Parameters.AddWithValue("@recimg", recorded.RecordedImage);
            insertCommand.ExecuteNonQuery();
            insertCommand.Dispose();
            con.Close();
        }

        public DataTable GetVideoByEnrollment(string Enrollment)
        {
            OpenConnection();
            adp = new MySqlDataAdapter("SELECT * FROM ubrobase.VideoLog where enroll='" + Enrollment + "'", con);
            dt = new DataTable();
            adp.Fill(dt);
            adp.Dispose();
            CloseConnection();
            return dt;

        }
        public void InsertVideoWithEnrollment(Models.RecordedVideo recorded)
        {
            string InsterQuery = "insert into ubrobase.VideoLog(Enrollment,VideoLog) values(@Enrollment,@VideoLog)";
            con.Open();
            MySqlCommand insertCommand = new MySqlCommand(InsterQuery, con);
            insertCommand.Parameters.AddWithValue("@Enrollment", recorded.Enrollment);
            insertCommand.Parameters.AddWithValue("@VideoLog", recorded.Videolog);
            insertCommand.ExecuteNonQuery();
            insertCommand.Dispose();
            con.Close();
        }

    }
}
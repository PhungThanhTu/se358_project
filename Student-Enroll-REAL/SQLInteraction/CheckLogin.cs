using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Student_Enroll_REAL.SQLInteraction
{
   
    class CheckLogin
    {
        static private SqlConnection connection;
        static private SqlCommand cmd = new SqlCommand();
        static private SqlDataReader reader;


        static public void ConnectToSQL()
        {

            connection = new SqlConnection(SQLConnector.ConnectionString);
        }
        // //
        // 0 = user
        // 1 = admin
        // -1 = invalid username or pass
        // -2 = sql connection failed
        // //
        /// <summary>
        /// Check Login Status
        /// return -2 sql error
        /// return -1 invalid authentication
        /// return 0 user login
        /// return 1 admin login
        /// </summary>
        /// <param name="username"> Username</param>
        /// <param name="password"> Password</param>
        /// <returns></returns>
        public static string checkLogin(string username,string password)
        {

            try
            {
                connection.Open();
            }
            catch
            {
                return "error";
            }

            cmd.CommandText = "Select * from TAI_KHOAN_DANG_NHAP "
                            + "where ten_tai_khoan = '" + username + "' "
                            + "and mat_khau = '" + password + "'";
           

            cmd.Connection = connection;
            reader = cmd.ExecuteReader();

            int i = 0;
            bool isAdmin = false;

            while(reader.Read())
            {
                i++;
                isAdmin = (bool)reader["admin"];
            }
            cmd.Connection.Close();
            if(i != 0 && !isAdmin)
                return "user";
            if (i != 0 && isAdmin)
                return "admin";
            else return "failed";

        }

        public static SqlConnection Connection { get { return connection; } }
    }
}

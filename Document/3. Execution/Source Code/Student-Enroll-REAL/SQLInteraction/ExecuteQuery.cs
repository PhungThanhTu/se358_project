using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Student_Enroll_REAL.SQLInteraction
{
    class ExecuteQuery
    {
        public static void executeQuery(string query)
        {
            string commandText = query;
            using (SqlConnection conn = new SqlConnection(SQLConnector.ConnectionString))

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static DataTable SqlDataTableFromQuery(string query, string table_name = "underfined")
        {

            CheckLogin.ConnectToSQL();
            CheckLogin.Connection.Open();
            string CmdString = query;

            SqlCommand cmd = new SqlCommand(CmdString, CheckLogin.Connection);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);


            DataTable dt = new DataTable(table_name);


            sda.Fill(dt);





            CheckLogin.Connection.Close();
            return dt;


        }
        /// <summary>
        /// get single string-formated value with SQL query, return data is the
        /// value in first row and first column of the datatable got by query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string GetStringFromQuery(string query)
        { 
            

            CheckLogin.ConnectToSQL();
            CheckLogin.Connection.Open();
            string CmdString = query;
            SqlCommand cmd = new SqlCommand(CmdString, CheckLogin.Connection);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CheckLogin.Connection.Close();
            string result;
            if (dt.Rows.Count != 0)
                result = dt.Rows[0][0].ToString();
            else
                result = "underfined";

            return result;
        }
    }
}

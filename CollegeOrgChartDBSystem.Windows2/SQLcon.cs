using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace CollegeOrgChartDBSystem.Windows2
{
    class SQLcon
    {
        private string sqlConString;
        public int rowAffacted = 0;
        //  public string action;
        public SQLcon(string server_address, string database, string username, string password)
        {
            sqlConString = "Server =" + server_address + "; Database = " + database + ";UId = " + username + "; Pwd = " + password + ";";
            //data source = server name
        }

        //select statement
        public DataTable GetData(string sql)
        {

            MySqlConnection Sqlcon = new MySqlConnection(sqlConString);
            if (Sqlcon.State == ConnectionState.Closed) Sqlcon.Open();
            MySqlCommand SQLcom = new MySqlCommand(sql, Sqlcon);
            MySqlDataAdapter SQLadap = new MySqlDataAdapter(SQLcom);
            DataSet ds = new DataSet();
            SQLadap.Fill(ds);
            return ds.Tables[0];

        }
        //insert update delete
        public void executeSQL(string sql)
        {
            //connection
            MySqlConnection Sqlcon = new MySqlConnection(sqlConString);
            //open the connection
            if (Sqlcon.State == ConnectionState.Closed) Sqlcon.Open();
            //build the sql command
            MySqlCommand SQLcom = new MySqlCommand(sql, Sqlcon);
            rowAffacted = SQLcom.ExecuteNonQuery();
        }
        public string SqlConString { get; set; }
        //end of class
    }
}

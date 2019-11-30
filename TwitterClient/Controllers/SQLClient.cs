using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace TwitterClient.Controllers
{
    public class SQLClient
    {
        public SqlConnection Conecction { get; }

        public SQLClient(string connectionString)
        {
            Conecction = new SqlConnection(connectionString);
        }

        public bool Open()
        {
            try
            {
                Conecction.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                if (Conecction.State == System.Data.ConnectionState.Open)
                    Conecction.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
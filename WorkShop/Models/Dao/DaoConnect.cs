using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WorkShop.Models.Dao
{
    public class DaoConnect
    {
        public SqlConnection SqlConnect() {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            return conn;
        }
    }
}
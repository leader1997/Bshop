using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace WebApplication1.Models
{
    public class DAO
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader dr;
        public void connect()
        {
            con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\BUBU\\Documents\\bshop.mdf;Integrated Security=True;Connect Timeout=30");
            
        }
        public void deconnecter()
        {
            dr.Close();
            con.Close();
        }
        public void deconnectermaj()
        {
            con.Close();
        }
        public SqlDataReader select(String rec)
        {
            com = new SqlCommand(rec, con);
            con.Open();
            dr = com.ExecuteReader();
            return dr;
        }
        public void maj(String rec)
        {
            com = new SqlCommand(rec, con);
            con.Open();
            com.ExecuteNonQuery();
        }
    }
}
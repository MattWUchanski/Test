using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DataWizPro.AuthorizationOld
{
    public class L2_Menedzer
    {
        private string pi = null;
        private string imie = null;
        private string nazwisko = null;
        private string alias = null;
        private string L2 = null;
        private string L3 = null;
        private string L4 = null;

        public L2_Menedzer(string _pi)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT [pi],[imie],[nazwisko],[alias],[L2],[L3],[L4] FROM [net_app].[L2_Menedzer] WHERE pi = @pi", conn);
                cmd.Parameters.AddWithValue("@pi", _pi);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pi = reader["pi"].ToString();
                    imie = reader["imie"].ToString();
                    nazwisko = reader["nazwisko"].ToString();
                    alias = reader["alias"].ToString();
                    L2 = reader["L2"].ToString();
                    L3 = reader["L3"].ToString();
                    L4 = reader["L4"].ToString();
                }
                reader.Close();
                conn.Close();
            }
        }

        public string getPi() { return pi; }
        public string getImie() { return imie; }
        public string getNazwisko() { return nazwisko; }
        public string getAlias() { return alias; }
        public string getL2() { return L2; }
        public string getL3() { return L3; }
        public string getL4() { return L4; }

    }
}

using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Threading.Tasks.Dataflow;

namespace Complete.Models
{
    public class Implement___interface_Export_To_Excell : IExport_To_Excell
    {
        private readonly string str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
        public DataTable Exportcustomer()
        {
            DataTable dataTable = Exportfromdatabase().Tables[0];
            return dataTable;
        }

        public DataSet Exportfromdatabase()
        {

            DataSet ds = new DataSet();
            using(MySqlConnection conn=new MySqlConnection(str))
            {
                using(MySqlCommand cmd=new MySqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "select * from exporttoexcell";
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                       
                      da.Fill(ds);
                    }
                  
                }
            }
            return ds;
        }
    }
}

using Complete.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Complete.DAL_Services
{
    public class Customer_implement : ICustomer
    {
        public bool Delete(int? id)
        {
            using(MySqlConnection con=new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
            {
                con.Open();
                using(MySqlCommand cmd=new MySqlCommand())
                {
                    cmd.Connection=con;
                    cmd.CommandType= CommandType.Text;
                    cmd.CommandText = "delete from jquerygridtable where customerid=@id ";
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        public  List<Customer_model> GetCustomer()
        {
            List<Customer_model> custmer = new List<Customer_model>();
            MySqlDataAdapter da;
            DataTable tbl=new DataTable();
            using (MySqlConnection conn=new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
            {
                conn.Open();

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select * from jquerygridtable";
                        da = new MySqlDataAdapter(cmd);
                        da.Fill(tbl);

                        foreach (DataRow dr in tbl.Rows)
                        {
                            Customer_model obj=   new Customer_model()
                            {
                                CustomerID = (int)dr["customerid"],
                                Name = dr["name"].ToString(),
                                Address = dr["address"].ToString(),
                                Country = dr["country"].ToString(),
                                City = dr["city"].ToString(),
                                PhoneNo = dr["phoneno"].ToString()
                            };
                            custmer.Add(obj);

                        }
                        return custmer;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}

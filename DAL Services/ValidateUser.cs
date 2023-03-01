
using Complete.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Complete.DAL_Services
{
    public class ValidateUser
    {
        private readonly string str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
        public static bool Verfify(string email, string password)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
            {
                conn.Open();

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select * from tbluser where email =@email AND password=@password";
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        MySqlDataReader dr = cmd.ExecuteReader();
                        DataTable tbl = new DataTable();
                        tbl.Load(dr);
                        List<User_model_login> lst = new List<User_model_login>();
                        foreach (DataRow dar in tbl.Rows)
                        {
                            lst.Add(new User_model_login()
                            {
                                FullName = dar["fullname"].ToString(),
                                Email = dar["email"].ToString(),
                                Password = dar["password"].ToString(),

                            });
                        }


                        if (lst.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        //int status = cmd.ExecuteNonQuery();
                        //if (status > 0)
                        //{
                        //    return true;
                        //}
                        //else
                        //{
                        //    return false;
                        //}
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

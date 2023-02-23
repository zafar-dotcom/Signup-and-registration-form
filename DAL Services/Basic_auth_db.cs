using Complete.Models;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;

namespace Complete.DAL_Services
{
    public class Basic_auth_db
    {
        // string str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
        public static bool User(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
            {
                string query = "select count(*) from basicauth where username=@uname AND password=@pwd";
                //string qr = "insert into basicauth (username,password) values (@uname,@pwd)";
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uname", username);
                    cmd.Parameters.AddWithValue("@pwd", password);
                    //int count=cmd.ExecuteNonQuery();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (count >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }
        //hardcodded list 
        public static bool Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public static List<Basic_auth_tbluser_model> GetAlluser()
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
            {
                con.Open();
                string query = "select * from user";
                MySqlCommand cmd = new MySqlCommand(query, con);
                List<Basic_auth_tbluser_model> userlist = new List<Basic_auth_tbluser_model>();
                Basic_auth_tbluser_model mdl = new Basic_auth_tbluser_model();

                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userlist.Add(new Basic_auth_tbluser_model
                            {
                                Id = reader["id"] == DBNull.Value ? int.MinValue : (int)reader["id"],
                                Name = reader["name"] == DBNull.Value ? string.Empty : (string)reader["name"],
                                Email = reader["email"] == DBNull.Value ? string.Empty : (string)reader["email"],
                                Age = reader["age"] == DBNull.Value ? int.MinValue : (int)reader["age"],
                                Join_date = reader["join_date"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["join_date"],
                                End_date = reader["end_date"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["end_date"]
                                // reader["fullname"].ToString(),

                            });


                        }
                        return userlist;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public static Basic_auth_tbluser_model Get_user_at(int id)
        {
            string str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
                string query = "SELECT * FROM user WHERE id=@id";
            // List<Basic_auth_tbluser_model> userlist = new List<Basic_auth_tbluser_model>();
            Basic_auth_tbluser_model model = new Basic_auth_tbluser_model();
            using (MySqlConnection con = new MySqlConnection(str))
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            new Basic_auth_tbluser_model();
                            model.Id = reader["id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id"]);
                            model.Name = reader["name"] == DBNull.Value ? "" : reader["name"].ToString();
                            model.Email = reader["email"] == DBNull.Value ? "" : reader["email"].ToString();
                            model.Age = reader["age"] == DBNull.Value ? 0 : Convert.ToInt32(reader["age"]);
                            model.Join_date = reader["join_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["join_date"]);
                            model.End_date = reader["end_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["end_date"]);

                           // userlist.Add(model);
                        }
                    return model;
                }
                }

             
            }
        public static bool Insert_User(Basic_auth_tbluser_model model)
        {
            try
            {


                using (MySqlConnection con = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into user (name,email,age,join_date,end_date) values (@name,@email,@age,@join_date,@end_date)";
                        cmd.Parameters.AddWithValue("@name", model.Name);
                        cmd.Parameters.AddWithValue("@email", model.Email);
                        cmd.Parameters.AddWithValue("@age", model.Age);
                        cmd.Parameters.AddWithValue("@join_date", model.Join_date);
                        cmd.Parameters.AddWithValue("@end_date", model.End_date);
                        int state= cmd.ExecuteNonQuery();
                        con.Close();

                        if (state > 0)
                            return true;
                        else
                            return false;
                     
                    }
                }
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
        public static bool Update(Basic_auth_tbluser_model user)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update user set name=@name ,email=@email,age=@age,join_date=@join_date,end_date=@end_date where id=@id";
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@name", user.Name);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@age", user.Age);
                        cmd.Parameters.AddWithValue("@join_date", user.Join_date);
                        cmd.Parameters.AddWithValue("@end_date", user.End_date);
                        int state = cmd.ExecuteNonQuery();
                        if (state > 0)
                            return true;
                        else
                            return false;

                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();  
            }

        }
        public static bool Delete(int id)
        {
            using(MySqlConnection conn=new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud"))
            {
                conn.Open();
               
                using(MySqlCommand cmd=new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType=CommandType.Text;
                    cmd.CommandText= "delete from user where id=@id";
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        int state = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (state > 0)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
        }
    }
        
}

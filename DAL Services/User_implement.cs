using Complete.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace Complete.DAL_Services
{
    public class User_implement : IUser_Login_Registraion
    {
        private readonly string str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
        public bool FindDuplicate(string email)
        {
            using(MySqlConnection conn=new MySqlConnection(str))
            {
                conn.Open();

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select * from tbluser where email =@email";
                        cmd.Parameters.AddWithValue("@email", email);                        
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
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                        //if (status > 0)
                        //{
                        //    return false;
                        //}
                        //else
                        //{
                        //    return true;
                        //}
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
           
        }

        
    

        public bool Registration(User_model_login user)
        {
            using (MySqlConnection conn = new MySqlConnection(str))
            {
                conn.Open();

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "insert into tbluser (fullname,email,password) values (@fullname,@email,@password) ";
                        cmd.Parameters.AddWithValue("@fullname", user.FullName);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@password", user.Password);

                        int status = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (status > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public ValidationError Validate(User_model_login u)
        {
            ValidationError ve = new ValidationError();
            if (u.FullName == null || u.Email == null || u.Password == null || u.ConfirmPassword == null)
            {
                ve.retval = false;
                ve.retmsg = "Input can no be blank.";

                return ve;
            }
            else if (FindDuplicate(u.Email) == false)
            {
                ve.retval = false;
                ve.retmsg = "You have already registered with this email.";

                return ve;
            }
            else if (u.Password.Length < 4 || u.Password.Length > 8)
            {
                ve.retval = false;
                ve.retmsg = "Password must be more than 4 and less than 8 characters.";

                return ve;
            }
            else if (!u.Password.Equals(u.ConfirmPassword))
            {
                ve.retval = false;
                ve.retmsg = "Password and Confirm Password are not equal.";

                return ve;
            }
            else
            {
                ve.retval = true;
                ve.retmsg = null;

                return ve;
            }
        }


        public bool Verfify(string email,string password)
        {
            using (MySqlConnection conn = new MySqlConnection(str))
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
                       MySqlDataReader dr= cmd.ExecuteReader();
                        DataTable tbl = new DataTable();
                        tbl.Load(dr);
                        List<User_model_login> lst = new List<User_model_login>();
                        foreach (DataRow dar in tbl.Rows )
                        {
                            lst.Add(new User_model_login()
                            {
                                FullName = dar["fullname"].ToString(),
                                Email = dar["email"].ToString(),
                                Password = dar["password"].ToString(),

                            } );
                        }


                        if(lst.Count > 0)
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

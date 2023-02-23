using Complete.Models;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MySql.Data.MySqlClient;
using System.Data;

namespace Complete.Models
{
    public class User_implement_interface : IUser_sign_up
    {
         private readonly string str1 = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";
        private string str;
        MySqlDataReader dr;
        public User_implement_interface(string str)
        {
            this.str= str;
        }
        //private MySqlConnection conn=new MySqlConnection("server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud");
       // private MySqlCommand cmd = new MySqlCommand();
       // private MySqlDataReader dr;

        public bool FindDuplicate(string email)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(str1))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        DataTable tbl = new DataTable();

                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.CommandText = "sp_findduplicate_user_signup";
                        cmd.Parameters.AddWithValue("_email", email);
                        dr = cmd.ExecuteReader();
                        tbl.Load(dr);

                        List<User_signup_model> lst = new List<User_signup_model>();
                        foreach (DataRow row in tbl.Rows)
                        {
                            lst.Add(new User_signup_model
                            {
                                Fullname = row["fullname"].ToString(),
                                Email = row["email"].ToString(),
                                Password = row["password"].ToString(),

                            });

                        }
                        conn.Close();
                        if (lst.Count >= 1)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                return true;
            }

            catch (Exception)
            {

                return false;
            }
        }

        public bool Register(User_signup_model user_signup)
         {
            try
            { using (MySqlConnection conn = new MySqlConnection(str1)) 
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_register_user_signup";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("_fullname", user_signup.Fullname);
                        cmd.Parameters.AddWithValue("_email", user_signup.Email);
                        cmd.Parameters.AddWithValue("_password", user_signup.Password);
                        int i = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        conn.Close();
                        if (i == 1)
                            return true;
                        else
                            return false; 
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Verify(string email, string password)
        {
            DataTable tbl = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(str1))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        List<User_signup_model> lst = new List<User_signup_model>();
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_Verify_user_signup";
                        cmd.Parameters.AddWithValue("_email", email);
                        cmd.Parameters.AddWithValue("_password", password);
                        dr = cmd.ExecuteReader();
                        tbl.Load(dr);

                        foreach (DataRow row in tbl.Rows)
                        {
                            lst.Add(
                                new User_signup_model
                                {
                                    Fullname = row["fullname"].ToString(),
                                    Email = row["email"].ToString(),
                                    Password = row["password"].ToString(),
                                    Confirmpassword = null

                                });

                        }
                        conn.Close();
                        if (lst.Count == 1)
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
            catch (Exception)
            {

                throw;
            }
        }

        public ValidationError Validation(User_signup_model usr)
        {
            ValidationError obj = new ValidationError();
            if (usr.Fullname == null || usr.Email == null || usr.Password == null || usr.Confirmpassword == null)
            {
                obj.retval = false;
                obj.msg = "Field cant be empty";
                return obj;

            }
            else if (FindDuplicate(usr.Email) == false)
            {
                obj.retval = false;
                obj.msg = "you are already registered";
                return obj;
            }
            else if (usr.Password.Length <= 4 || usr.Password.Length > 8)
            {
                obj.retval = false;
                obj.msg = "Password must be between 4 to 8 character";
                return obj;
            }
            else if (!usr.Password.Equals(usr.Confirmpassword))
            {
                obj.retval = false;
                obj.msg = "Password and confirm password are not same";
                return obj;
            }
            else
            {
                obj.retval = true;
                obj.msg = null;
                return obj;
            }

        }
    }
    }

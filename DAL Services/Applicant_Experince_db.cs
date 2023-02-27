using Complete.Models;
using MySql.Data.MySqlClient;

namespace Complete.DAL_Services
{
    public class Applicant_Experince_db
    {
        private readonly string str = "server=localhost;port=3306;uid=root;pwd=sobiazafar@2023;database=mvc_crud";

        public static void Add(Applicant app)
        {
            try
            {

                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.Open();
                    string query = "insert into Applicant (name,gender,age,qualificaion,total_experience) values (@name,@gender,@age,@qualificaion,@total_experience);SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", app.Name);
                        cmd.Parameters.AddWithValue("@gender", app.Gender);
                        cmd.Parameters.AddWithValue("@age", app.Age);
                        cmd.Parameters.AddWithValue("@qualificaion", app.Qualification);
                        cmd.Parameters.AddWithValue("@total_experience", app.Total_experience);
                        int appid = (int)cmd.ExecuteScalar();

                        string quer = "insert into experience (company_name,designation,years_worked,app_id) values (@company_name,@designation,@years_worked,@app_id)";
                        using (MySqlCommand cmd1 = new MySqlCommand(quer, conn))
                        {
                            
                        // cmd1.Parameters.AddWithValue("@company_name", );
                        //cmd1.Parameters.AddWithValue("@designation",);
                        //cmd1.Parameters.AddWithValue("@years_worked", );
                        //cmd1.Parameters.AddWithValue("@app_id", appid);
                        //cmd1.ExecuteNonQuery();

                    }
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

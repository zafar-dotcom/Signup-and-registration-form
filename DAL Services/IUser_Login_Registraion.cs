using Complete.Models;

namespace Complete.DAL_Services
{
    public interface IUser_Login_Registraion
    {
        bool Verfify(string email,string password);
        bool Registration(User_model_login user);
        bool FindDuplicate(string email);
        bool Authenticate(User_model_jwt user);
        ValidationError Validate(User_model_login user);
    }
}

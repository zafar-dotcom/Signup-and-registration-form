using Complete.Models;

namespace Complete.DAL_Services
{
    public interface IUser_Login_Registraion
    {
        bool Verfify(string email,string password);
        bool Registration(User_model_login user);
        bool FindDuplicate(string email);
        ValidationError Validate(User_model_login user);
    }
}

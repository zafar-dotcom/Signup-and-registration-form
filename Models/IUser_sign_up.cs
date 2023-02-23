namespace Complete.Models
{
    public interface IUser_sign_up
    {
        bool FindDuplicate(string email);

        bool Register(User_signup_model user_signup);
        bool Verify(string email, string password);
        ValidationError Validation(User_signup_model user_);
    }
}

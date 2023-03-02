namespace Complete.Models
{
    public class User_model_RBAC
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string DateOfBirth { get; set; }
        public IEnumerable<User_model_RBAC> GetUsers()
        {
            return new List<User_model_RBAC>() { new User_model_RBAC { Id = 101, UserName = "anet", Name = "Anet", EmailId = "anet@test.com", Password = "anet123", Role = "Admin", DateOfBirth = "01/01/2012" } };
        }
    }

}


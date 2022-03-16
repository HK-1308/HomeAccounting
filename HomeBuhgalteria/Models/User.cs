namespace WinFormsApp1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public User()
        {}

        public User(string userName,string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
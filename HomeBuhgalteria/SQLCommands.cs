namespace WinFormsApp1
{
    public class SQLCommands
    {
        public static string GetAccountsByUserIdCommand(int userId)
        {
            return $"SELECT * FROM [Accounts] WHERE [UserId] = '{userId}'";
        }
        
        public static string GetUserByUserNameAndPasswordCommand(string userName, string password)
        {
            return $"SELECT * FROM [Users] WHERE [UserName] = '{userName}' AND [Password] = '{password}' ";
        }
        
        public static string AddNewUserByUserNameAndPasswordCommand(string userName, string password)
        {
            return $"INSERT INTO [Users] (Username,Password) VALUES ('{userName}','{password}')";
        }

        public static string AddDefaultAccountsByUserIdCommand(int userId)
        {
            return$"INSERT INTO [Accounts] (AccountName,UserId) VALUES ('Cash',{userId})";
        }
        
        public static string AddDefaultSettingsToAccountByAccountId(int accountId)
        {
            return $"INSERT INTO [AccountSettings] ([CurrencyId],[AccountId]) VALUES (1,{accountId})";
        }
        public static string AddDefaultSettingsToUserByUserId(int userId)
        {
            return $"INSERT INTO [UserSettings] (Theme,Language,UserId) VALUES ('White','ENG',{userId})";
        }


    }
}
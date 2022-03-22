using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models;
using WinFormsApp1.Repositories;

namespace WinFormsApp1.Controllers
{
    public class UserController
    {
        private UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }

        public async Task<bool> IsRegistered(string userName, string password)
        {
            User searchedUser = await userRepository.GetUserByNameAndPassword(userName, password);
            if (searchedUser != null)
            {
                SetCurrentUserId(searchedUser.UserId);
                return true;
            }
            return false;
        }

        public async Task<User> AddNewUser(string userName, string password)
        {
            User newUser = await userRepository.AddNewUserByNameAndPassword(userName,password);
            await userRepository.AddDefaultUserSettings(newUser.UserId);
            await userRepository.AddDefaultAccountForUser(newUser.UserId);
            return newUser;
        }

        private void SetCurrentUserId(int userId)
        {
            CurrentUser.UserId = userId;
        }
        
        public async Task<List<Account>> GetUserAccounts(int userId)
        {
            List<Account> accounts = await userRepository.GetAccountsByUserId(userId);
            return accounts;
        }
    }
}
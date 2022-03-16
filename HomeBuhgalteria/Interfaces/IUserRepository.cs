using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByNameAndPassword(string userName, string password);

        public Task<User> AddNewUserByNameAndPassword(string userName, string password);
    }
}
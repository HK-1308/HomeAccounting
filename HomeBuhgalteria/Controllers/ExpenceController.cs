using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    public class ExpenceController
    {
        private ExpenceRepository expenceRepository;

        public ExpenceController()
        {
            expenceRepository = new ExpenceRepository();
        }

        public async Task<List<Account>> GetUserAccounts(int userId)
        {
            List<Account> accounts = await expenceRepository.GetAccountsByUserId(userId);
            return accounts;
        }
    }
}
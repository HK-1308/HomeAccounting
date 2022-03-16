using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1.Interfaces
{
    public interface IExpenceRepository
    {
        public Task<List<Account>> GetAccountsByUserId(int userId);
    }
}
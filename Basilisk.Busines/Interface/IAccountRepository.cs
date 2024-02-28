using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Busines.Interface
{
    public interface IAccountRepository
    {
        public Account Get(string username);
        public void RegisterAccount(Account newAccount);
        public void UpdatePassword(Account account);
    }
}

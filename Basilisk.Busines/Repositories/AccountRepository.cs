using Basilisk.Busines.Interface;
using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Busines.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BasiliskTfContext _context;

        public AccountRepository(BasiliskTfContext context)
        {
            _context = context;
        }

        public Account Get(string username) {
            return _context.Accounts.FirstOrDefault(u=> u.Username == username) ?? 
                        throw new KeyNotFoundException("Username tidak ditemukan");
        }

        public void RegisterAccount(Account newAccount)
        {
            if(_context.Accounts.Any(a=>a.Username == newAccount.Username))
            {
                throw new InvalidOperationException("Username already exist");
            }
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
        }

        public void UpdatePassword(Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM2._0
{
    public class Bank
    {
        public int IdBank { get; set; }

        public string Name { get; set; }

        public bool Owner { get; set; }

        public List<Account> Accounts { get; set; }

        public List<User> Users { get; set; }   

        public bool ValidateUser(int idUser, int password)
        {
            var found = Users.FirstOrDefault(u => u.IdUser == idUser && u.Password == password && u.Status == true);
            if (found == null)
                return false;
            else
                return true;
        }

        public bool ValidateAccount(int idAccount, int idUser)
        {
            var found = Accounts.FirstOrDefault(a => a.IdAccount == idAccount && a.IdUser == idUser && a.Status == true);
            if (found == null)
                return false;
            else
                return true;
        }

        public User GetUser(int idUser)
        {
            return Users.FirstOrDefault(u => u.IdUser == idUser);
        }

        public Account GetAccount (int idAccount)
        {
            return Accounts.FirstOrDefault(a => a.IdAccount == idAccount);  
        }

        public void AddMoney(int idAccount, decimal money)
        {
            var account = Accounts.FirstOrDefault(a => a.IdAccount == idAccount);
            account.Balance += money;
        }

        public bool WithdrawalMoney(int idAccount, decimal money)
        {
            var account = Accounts.FirstOrDefault(a => a.IdAccount == idAccount);
            if(account.Balance - money < 10000)
                return false;
            else
                account.Balance -= money;
                return true;
        }
    }
}

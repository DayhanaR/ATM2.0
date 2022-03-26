using System;
using System.Collections.Generic;

namespace ATM2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User()
            {
                IdUser = 1,
                Name = "Dayhana Ramirez",
                Password = 0000,
                Status = true,
                TransactionsRecord = new List<Transaction>(),
            };

            Account account1 = new Account()
            {
                IdAccount = 1,
                Type = "Cuenta de ahorros",
                Status = true,
                Balance = 1500000,
                IdUser = 1,
            };

            Account account2 = new Account()
            {
                IdAccount = 2,
                Type = "Cuenta corriente",
                Status = true,
                Balance = 3500000,
                IdUser = 1,
            };

            Bank bancolombia = new Bank()
            {
                IdBank = 1,
                Name = "Bancolombia",
                Owner = true,
                Accounts = new List<Account>() { account1, account2 },
                Users = new List<User>() { user1 },   
            };

            ATM atm1 = new ATM()
            {
                IdATM = 1,
                Description = "Cajero Bancolombia",
                Status = true,
                IdBank = 1,
            };

            atm1.Login(bancolombia);

        }
    }
}

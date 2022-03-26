using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM2._0
{
    public class User
    {
        public int IdUser { get; set; }

        public string Name { get; set; }

        public int Password { get; set; }

        public bool Status{ get; set; }

        public List<Transaction> TransactionsRecord { get; set; } 

        public void ChangePassword(int newPassword)
        {
            Password = newPassword;
        }

        public void ShowTransactionsRecord()
        {
            if(TransactionsRecord.Count == 0)
            {
                Console.WriteLine("\nNo hay registros para mostrar");
            }
            else
            {
                foreach (var item in TransactionsRecord)
                {
                    Console.WriteLine($"\nId Transacción: {item.IdTransaction}" +
                                      $"\nId Usuario: {item.IdUser} " +
                                      $"\nId Cuenta: {item.IdAccount}" +
                                      $"\nDescripción: " +
                                      $"\n{item.Description}" +
                                      $"\nFecha: {item.Date}");
                }
            }
        }
    }
}

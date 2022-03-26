using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM2._0
{
    public class ATM
    {
        public int IdATM { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public int IdBank { get; set; }

        public void Login(Bank bank)
        {
            Console.WriteLine($"╔════════════════════════════════════════════╗" +
                              $"\n║ Bienvenid@ al cajero del banco {bank.Name} ║" +
                              $"\n╚════════════════════════════════════════════╝");

            string idUser = string.Empty;
            string password = string.Empty;
            int idUserNumber = 0;
            int passwordNumber = 0;

            do
            {
                Console.WriteLine("\nIngrese su Id de usuario: ");
                idUser = Console.ReadLine();
                Console.WriteLine("\nIngrese su contraseña de usuario: ");
                password = Console.ReadLine();

                if (ValidateNumber(idUser) && ValidateNumber(password))
                {
                    idUserNumber = ConvertNumber(idUser);
                    passwordNumber = ConvertNumber(password);   

                    if (bank.ValidateUser(idUserNumber, passwordNumber))
                        Menu(bank, idUserNumber);
                    else
                        Console.WriteLine($"\n************************* ALERTA *************************" +
                                          $"\n* No se ha encontrado un usuario con ese Id y contraseña *" +
                                          $"\n**********************************************************");
                }
                else
                    Console.WriteLine($"\n********************* ALERTA *********************" +
                                      $"\n* Por favor ingrese un Id y/o contraseña válidos *" +
                                      $"\n**************************************************");

            } while (!ValidateNumber(idUser) || !ValidateNumber(password) || !bank.ValidateUser(idUserNumber, passwordNumber));

        }

        public void Menu(Bank bank, int idUser)
        {
            int answer = 0;

            do
            {
                try
                {
                    int transactionCounter = 1;

                    Console.WriteLine($"\nBienvenid@ {bank.GetUser(idUser).Name}" +
                                      $"\n" +
                                      $"\n================================" +
                                      $"\n============= Menu =============" +
                                      $"\n================================" +
                                      $"\n                                " +
                                      $"\n       1. Ingresar dinero       " +
                                      $"\n       2. Retirar dinero        " +
                                      $"\n       3. Consulta de saldo     " +
                                      $"\n       4. Cambio de clave       " +
                                      $"\n       5. Transacciones         " +
                                      $"\n       6. Salir                 " +
                                      $"\n                                " +
                                      $"\n================================" +
                                      $"\n                                " +
                                      $"\nSeleccione una opción:");

                    answer = ConvertNumber(Console.ReadLine());

                    switch (answer)
                    {
                        case 1:
                            Console.WriteLine("\n¿Cuánto dinero desea ingresar? :");
                            int addMoney = ConvertNumber(Console.ReadLine());
                            ShowAccounts(FindAccounts(idUser, bank));
                            int accountOption1 = ConvertNumber(Console.ReadLine());

                            if (bank.ValidateAccount(accountOption1, idUser))
                            {
                                bank.AddMoney(accountOption1, addMoney);
                                Console.WriteLine($"\n|-------------------------------|" +
                                                  $"\n| Dinero ingresado correctamente|" +
                                                  $"\n|-------------------------------|" +
                                                  $"\n\tNuevo saldo: {bank.GetAccount(accountOption1).Balance} ");

                                string transaction = ($"Ingreso de dinero" +
                                                      $"\nCuenta: {accountOption1}" +
                                                      $"\nCantidad: {addMoney}" +
                                                      $"\nNuevo saldo: {bank.GetAccount(accountOption1).Balance}");

                                AddTransaction(bank, transactionCounter, idUser, accountOption1, transaction);
                                transactionCounter++;
                            }
                            else
                            {
                                ValidAccount();
                            }
                            break;

                        case 2:
                            Console.WriteLine("\n¿Cuánto dinero desea retirar? :");
                            int withdrawalMoney = ConvertNumber(Console.ReadLine());
                            ShowAccounts(FindAccounts(idUser, bank));
                            int accountOption2 = ConvertNumber(Console.ReadLine());

                            if(bank.ValidateAccount(accountOption2, idUser))
                            {
                                if (bank.WithdrawalMoney(accountOption2, withdrawalMoney))
                                {
                                    Console.WriteLine($"\n|------------------------------|" +
                                                      $"\n| Dinero retirado correctamente|" +
                                                      $"\n|------------------------------|" +
                                                      $"\n\tNuevo saldo: {bank.GetAccount(accountOption2).Balance} ");

                                    string transaction = ($"Retiro de dinero" +
                                                          $"\nCuenta: {accountOption2}" +
                                                          $"\nCantidad: {withdrawalMoney}" +
                                                          $"\nNuevo saldo: {bank.GetAccount(accountOption2).Balance}");

                                    AddTransaction(bank, transactionCounter, idUser, accountOption2, transaction);
                                    transactionCounter++;

                                }
                                else 
                                {
                                    Console.WriteLine($"\n******** ALERTA *******" +
                                                      $"\n* Dinero insuficiente *" +
                                                      $"\n***********************");
                                }
                            }
                            else
                            {
                                ValidAccount();
                            }
                            break;

                        case 3:
                            ShowAccounts(FindAccounts(idUser, bank));
                            int accountOption3 = ConvertNumber(Console.ReadLine());
                            if (bank.ValidateAccount(accountOption3, idUser))
                            {
                                Console.WriteLine($"\nSaldo de la cuenta número {accountOption3}: {bank.GetAccount(accountOption3).Balance}");
                            }
                            else
                            {
                                ValidAccount();
                            }
                            break;

                        case 4:
                            Console.WriteLine("Ingrese su contraseña actual: ");
                            int actualPassword = ConvertNumber(Console.ReadLine());

                            if (bank.GetUser(idUser).Password == actualPassword)
                            {
                                Console.WriteLine("Ingrese su la nueva contraseña: ");
                                int newPassword = ConvertNumber(Console.ReadLine());
                                bank.GetUser(idUser).ChangePassword(newPassword);
                                Console.WriteLine($"\n|----------------------------------|" +
                                                  $"\n| Contraseña cambiada correctamente|" +
                                                  $"\n|----------------------------------|" +
                                                  $"\n\tAntigua contraseña: {actualPassword}" +
                                                  $"\n\tNueva contraseña: {newPassword}");

                                string transaction = ($"Cambio de contraseña" +
                                                      $"\nContraseña antigua: {actualPassword}" +
                                                      $"\nContraseña nueva: {newPassword}");

                                AddTransaction(bank, transactionCounter, idUser, transaction);
                                transactionCounter++;
                                return;
                            }
                            else
                            {
                                Console.WriteLine($"\n************ ALERTA ************ " +
                                                  $"\n* Las contraseñas no coinciden *" +
                                                  $"\n********************************");
                            }
                            break;

                        case 5:
                            bank.GetUser(idUser).ShowTransactionsRecord();
                            break;

                        case 6:
                            Console.WriteLine($"╔═══════════════════════════════════════╗" +
                                              $"\n║ ¡Gracias por usar nuestros servicios! ║" +
                                              $"\n╚═══════════════════════════════════════╝");
                            break;

                        default:
                            Console.WriteLine($"\n*********************** ALERTA **********************" +
                                              $"\n* Por favor seleccione una de las opciones del menu *" +
                                              $"\n*****************************************************");
                            break;
                    }
                }

                catch (FormatException)
                {
                    Console.WriteLine($"\n*************** ALERTA ****************" +
                                      $"\n* Por favor ingrese una opción válida *" +
                                      $"\n***************************************");
                }

            } while (answer != 6);
        }

        public bool ValidateNumber(string number)
        {
            try
            {
                int convertNumber = Convert.ToInt32(number);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public int ConvertNumber(string number)
        {
            return Convert.ToInt32(number);
        }

        public List<Account> FindAccounts(int idUSer, Bank bank)
        {
            List<Account> accounts = bank.Accounts.Where(a => a.IdUser == idUSer && a.Status == true).ToList();
            return accounts;    
        }

        public void ShowAccounts(List<Account> accounts)
        {
            Console.WriteLine("\nSeleccione el número de la cuenta");

            foreach (var item in accounts)
            {
                Console.WriteLine($"Número de cuenta: {item.IdAccount} - Tipo: {item.Type}");   
            }
        }

        public void ValidAccount()
        {
            Console.WriteLine($"\n***************** ALERTA *****************" +
                              $"\n* Por favor seleccione una cuenta válida *" +
                              $"\n******************************************");
        }

        public void AddTransaction(Bank bank,int transactionCounter, int idUser, int idAccount, string description)
        {
            Transaction transaction = new Transaction()
            {
                IdTransaction = transactionCounter,
                IdUser = idUser,
                IdAccount = idAccount,
                Date = DateTime.Now,
                Description = description
            };

            bank.GetUser(idUser).TransactionsRecord.Add(transaction);
        }

        public void AddTransaction(Bank bank, int transactionCounter, int idUser, string description)
        {
            Transaction transaction = new Transaction()
            {
                IdTransaction = transactionCounter,
                IdUser = idUser,
                Date = DateTime.Now,
                Description = description
            };

            bank.GetUser(idUser).TransactionsRecord.Add(transaction);
        }
    }
}

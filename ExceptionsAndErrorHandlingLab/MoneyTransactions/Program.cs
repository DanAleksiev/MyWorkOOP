using System.Security.Principal;

namespace MoneyTransactions
    {
    internal class Program
        {
        static void Main(string[] args)
            {
            string[] bank = Console.ReadLine().Split(",");

            Dictionary<int,double>accounts = new Dictionary<int,double>();
            foreach (string person in bank)
                {
                string[]splitNumberFromBalance = person.Split("-",StringSplitOptions.RemoveEmptyEntries);
                int number = int.Parse(splitNumberFromBalance[0]);
                double balance = double.Parse(splitNumberFromBalance[1]);
                accounts.Add(number, balance);
                }

            string input = string.Empty;
            while((input = Console.ReadLine()) != "End")
                {
                string[] commandSpliter = input.Split();
                string action = commandSpliter[0];
                int account= int.Parse(commandSpliter[1]); 
                double amount = double.Parse(commandSpliter[2]); 

                try
                    {
                    if (action == "Deposit")
                        {
                        accounts[account]+= amount;
                        SuccesfulTransaction(account, accounts[account]);
                        
                        }
                    else if (action == "Withdraw")
                        {
                        if (accounts[account] < amount)
                            {
                            throw new ArgumentException("Insufficient balance!");
                            }
                        accounts[account] -= amount;
                        double balance = accounts[account];
                        SuccesfulTransaction(account, accounts[account]);
                        }
                    else
                        {
                        throw new ArgumentException("Invalid command!");
                        }
                    }
                catch (ArgumentException ex)
                    {
                    Console.WriteLine(ex.Message);                   
                    }
                catch (KeyNotFoundException)
                    {
                    Console.WriteLine("Invalid account!");
                    }
                finally { Console.WriteLine($"Enter another command"); }
                }

            }

        private static void SuccesfulTransaction(int account,double balance)
        {
            Console.WriteLine($"Account {account} has new balance: {balance:F2}");
        }
    }
    }
using CoreWCF;
using System.Collections.Concurrent;


namespace SoapServiceProject.Services.Banking {
    public class BankingService : IBankingService {

        private static ConcurrentDictionary<string, decimal> Accounts = new ConcurrentDictionary<string, decimal>();

        static BankingService() {
            Accounts["123"] = 1000.00m;
        }

        
        public string GetAccountBalance(string accountId) {
            if(Accounts.TryGetValue(accountId, out decimal balance)) {
                return balance.ToString("F2");
            }
            else {
                return "Account not found";
            }
        }

        public bool Deposit(string accountId, decimal amount) {
            if (amount <= 0) return false;

            Accounts.AddOrUpdate(accountId, amount, (id, oldBalance) => oldBalance + amount);
            return true;
        }

        public bool Withdraw(string accountId, decimal amount)
        {
            if (amount <= 0) return false;  // Invalid withdrawal amount.

            // Try to update the balance if funds are sufficient.
            return Accounts.AddOrUpdate(accountId,
                key => 0, // If account doesn't exist, indicate failure.
                (key, oldBalance) =>
                {
                    if (oldBalance >= amount)
                    {
                        return oldBalance - amount;
                    }
                    else
                    {
                        // If insufficient funds, do not update.
                        // In a real scenario, you might throw an exception or return a fault.
                        return oldBalance;
                    }
                }) != -1 && Accounts[accountId] >= 0;
        }

    }
}

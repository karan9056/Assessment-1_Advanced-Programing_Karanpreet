namespace GreenBankApp
{
    public class EverydayAccount : Account
    {
        public EverydayAccount(string accountId, string accountName, decimal openingBalance)
            : base(accountId, accountName, openingBalance, 0m, 0m, 0m)
        {
        }

        public override string Withdraw(decimal amount, Customer customer)
        {
            if (amount <= 0)
            {
                lastTransactionStatus = "Withdrawal failed. Enter a valid amount.";
                return lastTransactionStatus;
            }

            if (amount > balance)
            {
                lastTransactionStatus = "Insufficient funds";
                return lastTransactionStatus;
            }

            balance -= amount;
            lastTransactionStatus = "Withdrawal successful";
            return lastTransactionStatus;
        }

        public override decimal CalculateInterest()
        {
            lastTransactionStatus = "No interest for Everyday Account";
            return 0m;
        }

        public override string GetAccountInfo()
        {
            return "Account Type: Everyday Account" +
                   " | Account ID: " + accountId +
                   " | Balance: " + balance.ToString("$0.00") +
                   " | Interest: None" +
                   " | Overdraft: None" +
                   " | Transaction Fee: None" +
                   " | Last Transaction: " + lastTransactionStatus;
        }
    }
}
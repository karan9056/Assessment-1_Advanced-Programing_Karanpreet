namespace GreenBankApp
{
    public class InvestmentAccount : Account
    {
        public InvestmentAccount(string accountId, string accountName, decimal openingBalance, decimal interestRate, decimal failedFee)
            : base(accountId, accountName, openingBalance, interestRate, failedFee, 0m)
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
                decimal adjustedFee = customer.GetAdjustedFailedFee(failedFee);

                if (balance >= adjustedFee)
                {
                    balance -= adjustedFee;
                    lastTransactionStatus = "Insufficient funds. Failed transaction fee applied: " + adjustedFee.ToString("$0.00");
                }
                else
                {
                    lastTransactionStatus = "Insufficient funds. Failed transaction fee could not be applied.";
                }

                return lastTransactionStatus;
            }

            balance -= amount;
            lastTransactionStatus = "Withdrawal successful";
            return lastTransactionStatus;
        }

        public override decimal CalculateInterest()
        {
            if (balance <= 0)
            {
                lastTransactionStatus = "Interest not added";
                return 0m;
            }

            decimal interestAmount = balance * interestRate;
            balance += interestAmount;
            lastTransactionStatus = "Interest added: " + interestAmount.ToString("$0.00");
            return interestAmount;
        }

        public override string GetAccountInfo()
        {
            return "Account Type: Investment Account" +
                   " | Account ID: " + accountId +
                   " | Balance: " + balance.ToString("$0.00") +
                   " | Interest Rate: " + interestRate.ToString("P2") +
                   " | Failed Fee: " + failedFee.ToString("$0.00") +
                   " | Overdraft: Not allowed" +
                   " | Last Transaction: " + lastTransactionStatus;
        }
    }
}
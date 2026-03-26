namespace GreenBankApp
{
    public class OmniAccount : Account
    {
        private decimal interestThreshold;

        public decimal InterestThreshold
        {
            get { return interestThreshold; }
        }

        public OmniAccount(string accountId, string accountName, decimal openingBalance, decimal interestRate, decimal failedFee, decimal overdraftLimit, decimal interestThreshold)
            : base(accountId, accountName, openingBalance, interestRate, failedFee, overdraftLimit)
        {
            this.interestThreshold = interestThreshold;
        }

        public override string Withdraw(decimal amount, Customer customer)
        {
            if (amount <= 0)
            {
                lastTransactionStatus = "Withdrawal failed. Enter a valid amount.";
                return lastTransactionStatus;
            }

            if (amount > balance + overdraftLimit)
            {
                decimal adjustedFee = customer.GetAdjustedFailedFee(failedFee);

                if (balance >= adjustedFee)
                {
                    balance -= adjustedFee;
                    lastTransactionStatus = "Withdrawal declined. Failed transaction fee applied: " + adjustedFee.ToString("$0.00");
                }
                else
                {
                    lastTransactionStatus = "Withdrawal declined. Failed transaction fee could not be applied.";
                }

                return lastTransactionStatus;
            }

            balance -= amount;
            lastTransactionStatus = "Withdrawal successful";
            return lastTransactionStatus;
        }

        public override decimal CalculateInterest()
        {
            if (balance <= interestThreshold)
            {
                lastTransactionStatus = "Interest not added. Balance must be above " + interestThreshold.ToString("$0.00");
                return 0m;
            }

            decimal interestAmount = balance * interestRate;
            balance += interestAmount;
            lastTransactionStatus = "Interest added: " + interestAmount.ToString("$0.00");
            return interestAmount;
        }

        public override string GetAccountInfo()
        {
            return "Account Type: Omni Account" +
                   " | Account ID: " + accountId +
                   " | Balance: " + balance.ToString("$0.00") +
                   " | Interest Rate: " + interestRate.ToString("P2") +
                   " | Interest Threshold: " + interestThreshold.ToString("$0.00") +
                   " | Failed Fee: " + failedFee.ToString("$0.00") +
                   " | Overdraft Limit: " + overdraftLimit.ToString("$0.00") +
                   " | Last Transaction: " + lastTransactionStatus;
        }
    }
}
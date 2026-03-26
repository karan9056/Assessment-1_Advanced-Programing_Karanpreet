using System;

namespace GreenBankApp
{
    public abstract class Account
    {
        protected string accountId;
        protected string accountName;
        protected decimal balance;
        protected decimal interestRate;
        protected decimal failedFee;
        protected decimal overdraftLimit;
        protected string lastTransactionStatus;

        public string AccountId
        {
            get { return accountId; }
        }

        public string AccountName
        {
            get { return accountName; }
        }

        public decimal Balance
        {
            get { return balance; }
        }

        public decimal InterestRate
        {
            get { return interestRate; }
        }

        public decimal FailedFee
        {
            get { return failedFee; }
        }

        public decimal OverdraftLimit
        {
            get { return overdraftLimit; }
        }

        public string LastTransactionStatus
        {
            get { return lastTransactionStatus; }
        }

        protected Account(string accountId, string accountName, decimal openingBalance, decimal interestRate, decimal failedFee, decimal overdraftLimit)
        {
            this.accountId = accountId;
            this.accountName = accountName;
            this.balance = openingBalance;
            this.interestRate = interestRate;
            this.failedFee = failedFee;
            this.overdraftLimit = overdraftLimit;
            lastTransactionStatus = "No transactions yet";
        }

        public virtual string Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                lastTransactionStatus = "Deposit failed. Enter a valid amount.";
                return lastTransactionStatus;
            }

            balance += amount;
            lastTransactionStatus = "Deposit successful";
            return lastTransactionStatus;
        }

        public abstract string Withdraw(decimal amount, Customer customer);

        public abstract decimal CalculateInterest();

        public virtual string GetAccountInfo()
        {
            return "Account ID: " + accountId +
                   " | Account Name: " + accountName +
                   " | Balance: " + balance.ToString("$0.00") +
                   " | Interest Rate: " + interestRate.ToString("P2") +
                   " | Failed Fee: " + failedFee.ToString("$0.00") +
                   " | Overdraft Limit: " + overdraftLimit.ToString("$0.00") +
                   " | Last Transaction: " + lastTransactionStatus;
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace GreenBankApp
{
    public abstract class Customer
    {
        private string customerNumber;
        private string name;
        private string contactDetails;
        protected List<Account> accounts;

        public string CustomerNumber
        {
            get { return customerNumber; }
        }

        public string Name
        {
            get { return name; }
        }

        public string ContactDetails
        {
            get { return contactDetails; }
        }

        public List<Account> Accounts
        {
            get { return accounts; }
        }

        protected Customer(string customerNumber, string name, string contactDetails)
        {
            this.customerNumber = customerNumber;
            this.name = name;
            this.contactDetails = contactDetails;
            accounts = new List<Account>();
        }

        protected void AddDefaultAccounts()
        {
            accounts.Add(new EverydayAccount("ED001", "Everyday Account", 500m));
            accounts.Add(new InvestmentAccount("IN001", "Investment Account", 1200m, 0.05m, 20m));
            accounts.Add(new OmniAccount("OM001", "Omni Account", 1500m, 0.03m, 25m, 400m, 1000m));
        }

        public Account GetAccountByName(string accountName)
        {
            return accounts.FirstOrDefault(a => a.AccountName == accountName);
        }

        public virtual decimal GetAdjustedFailedFee(decimal feeAmount)
        {
            return feeAmount;
        }

        public virtual string GetCustomerSummary()
        {
            return "Customer Number: " + customerNumber +
                   " | Name: " + name +
                   " | Contact Details: " + contactDetails +
                   " | Customer Type: " + (IsStaff() ? "Bank Staff" : "Regular Customer");
        }

        public abstract bool IsStaff();
    }

    public class RegularCustomer : Customer
    {
        public RegularCustomer(string customerNumber, string name, string contactDetails)
            : base(customerNumber, name, contactDetails)
        {
            AddDefaultAccounts();
        }

        public override bool IsStaff()
        {
            return false;
        }
    }

    public class BankStaff : Customer
    {
        private decimal feeDiscountRate;

        public decimal FeeDiscountRate
        {
            get { return feeDiscountRate; }
        }

        public BankStaff(string customerNumber, string name, string contactDetails)
            : base(customerNumber, name, contactDetails)
        {
            feeDiscountRate = 0.50m;
            AddDefaultAccounts();
        }

        public override bool IsStaff()
        {
            return true;
        }

        public override decimal GetAdjustedFailedFee(decimal feeAmount)
        {
            return feeAmount * (1 - feeDiscountRate);
        }

        public override string GetCustomerSummary()
        {
            return base.GetCustomerSummary() +
                   " | Fee Discount: " + feeDiscountRate.ToString("P0");
        }
    }
}
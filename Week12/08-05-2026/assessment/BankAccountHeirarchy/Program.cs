using System;

namespace BankAccountHierarchy
{
    // Base Class
    public class BankAccount
    {
        // Properties
        public string AccountNumber { get; }
        protected double Balance { get; private set; }

        // Constructor
        public BankAccount(string accountNumber, double initialDeposit)
        {
            AccountNumber = accountNumber;
            Balance = initialDeposit;
        }

        // Deposit Method
        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }

            return false;
        }

        // Withdraw Method
        public virtual bool Withdraw(double amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                return true;
            }

            return false;
        }

        // Get Balance
        public double GetBalance()
        {
            return Balance;
        }

        // Protected helper for derived classes
        protected void UpdateBalance(double amount)
        {
            Balance += amount;
        }
    }

    // SavingsAccount Class
    public class SavingsAccount : BankAccount
    {
        // Additional Properties
        private double interestRate;
        private double minimumBalance;

        // Constructor
        public SavingsAccount(string accountNumber, double initialDeposit)
            : base(accountNumber, initialDeposit)
        {
            minimumBalance = 1000;
        }

        // Override Withdraw
        public override bool Withdraw(double amount)
        {
            if (GetBalance() - amount < minimumBalance)
            {
                Console.WriteLine(
                    $"Withdrawal Failed: Minimum balance requirement {minimumBalance}"
                );
                return false;
            }

            return base.Withdraw(amount);
        }

        // Apply Interest Method
        public void ApplyInterest(double rate)
        {
            interestRate = rate;

            double interest = GetBalance() * (interestRate / 100);
            UpdateBalance(interest);

            Console.WriteLine(
                $"Interest Applied,Rate:{interestRate},New Balance:{GetBalance()}"
            );
        }
    }

    // CurrentAccount Class
    public class CurrentAccount : BankAccount
    {
        // Additional Properties
        private double overdraftLimit;
        private double transactionFee;

        // Constructor
        public CurrentAccount(
            string accountNumber,
            double initialDeposit,
            double overdraftLimit = 2000,
            double transactionFee = 50
        )
            : base(accountNumber, initialDeposit)
        {
            this.overdraftLimit = overdraftLimit;
            this.transactionFee = transactionFee;
        }

        // Override Withdraw
        public override bool Withdraw(double amount)
        {
            if (GetBalance() - amount >= -overdraftLimit)
            {
                UpdateBalance(-amount);
                return true;
            }

            Console.WriteLine("Withdrawal Failed: Overdraft limit exceeded");
            return false;
        }

        // Deduct Transaction Fee
        public void DeductTransactionFee()
        {
            UpdateBalance(-transactionFee);

            Console.WriteLine(
                $"Fee Deducted,Amount:{transactionFee},Remaining:{GetBalance()}"
            );
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // Input
            string accountType = Console.ReadLine().Trim();
            string accountNumber = Console.ReadLine().Trim();
            double initialDeposit = double.Parse(Console.ReadLine());

            BankAccount account;

            // Create Account Object
            if (accountType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                account = new SavingsAccount(accountNumber, initialDeposit);
            }
            else
            {
                account = new CurrentAccount(accountNumber, initialDeposit);
            }

            // Process Operations
            string operationInput;

            while (!string.IsNullOrEmpty(operationInput = Console.ReadLine()))
            {
                string[] parts = operationInput.Split(' ');
                string operation = parts[0];

                switch (operation)
                {
                    case "Deposit":
                        double depositAmount = double.Parse(parts[1]);
                        account.Deposit(depositAmount);
                        break;

                    case "Withdraw":
                        double withdrawAmount = double.Parse(parts[1]);
                        account.Withdraw(withdrawAmount);
                        break;

                    case "GetBalance":
                        Console.WriteLine(
                            $"Current Balance: {account.GetBalance()}"
                        );
                        break;

                    case "ApplyInterest":
                        if (account is SavingsAccount savings)
                        {
                            double rate = double.Parse(parts[1]);
                            savings.ApplyInterest(rate);
                        }
                        break;

                    case "DeductTransactionFee":
                        if (account is CurrentAccount current)
                        {
                            current.DeductTransactionFee();
                        }
                        break;
                }
            }
        }
    }
}
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BankBlazor.Api.Data;

public static class SeedData
{
    public static void SeedIfEmpty(BankBlazorContext db, ILogger logger)
    {
        if (db.Customers.Any())
        {
            logger.LogInformation("Seed skipped: Customers already exist.");
            return;
        }

        logger.LogInformation("Seeding initial demo data (empty database)...");

        var customer = new Customer
        {
            Gender = "Female",
            Givenname = "Alex",
            Surname = "Andersson",
            Streetaddress = "Storgatan 1",
            City = "Stockholm",
            Zipcode = "111 22",
            Country = "Sweden",
            CountryCode = "SE",
            Birthday = new DateOnly(1990, 5, 12),
            Emailaddress = "alex.andersson@example.com",
            Telephonenumber = "+46 70 123 45 67",
            Telephonecountrycode = "+46"
        };

        var account1 = new Account
        {
            Frequency = "Monthly",
            Created = DateOnly.FromDateTime(DateTime.Today),
            Balance = 0m
        };

        var account2 = new Account
        {
            Frequency = "Monthly",
            Created = DateOnly.FromDateTime(DateTime.Today),
            Balance = 0m
        };

        var disposition1 = new Disposition
        {
            Customer = customer,
            Account = account1,
            Type = "OWNER"
        };

        var disposition2 = new Disposition
        {
            Customer = customer,
            Account = account2,
            Type = "OWNER"
        };

        var balance1 = 1000m;
        var balance2 = 2000m;

        var tx1 = new Transaction
        {
            AccountNavigation = account1,
            Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
            Type = "Credit",
            Operation = "Deposit",
            Amount = 500m,
            Balance = balance1 + 500m,
            Bank = "SEB",
            Symbol = "SALARY",
            Account = "1234567890"
        };
        balance1 += 500m;

        var tx2 = new Transaction
        {
            AccountNavigation = account1,
            Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            Type = "Debit",
            Operation = "Withdrawal",
            Amount = 200m,
            Balance = balance1 - 200m,
            Bank = "ATM",
            Symbol = "CASH",
            Account = "-"
        };
        balance1 -= 200m;

        var tx3 = new Transaction
        {
            AccountNavigation = account2,
            Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            Type = "Credit",
            Operation = "Deposit",
            Amount = 100m,
            Balance = balance2 + 100m,
            Bank = "SWEDBANK",
            Symbol = "REFUND",
            Account = "9876543210"
        };
        balance2 += 100m;

        account1.Balance = balance1;
        account2.Balance = balance2;

        db.Customers.Add(customer);
        db.Accounts.AddRange(account1, account2);
        db.Dispositions.AddRange(disposition1, disposition2);
        db.Transactions.AddRange(tx1, tx2, tx3);

        db.SaveChanges();

        logger.LogInformation(
            "Seed complete: CustomerId={CustomerId}, AccountIds={Account1Id},{Account2Id}.",
            customer.CustomerId,
            account1.AccountId,
            account2.AccountId);
    }
}

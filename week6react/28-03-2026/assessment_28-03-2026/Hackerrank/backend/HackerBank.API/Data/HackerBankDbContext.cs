using HackerBank.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerBank.API.Data
{
    public class HackerBankDbContext : DbContext
    {
        public HackerBankDbContext(DbContextOptions<HackerBankDbContext> options)
            : base(options) { }

        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1,  Date = "2026-03-28", Description = "HACKERBANK INC. DES:CCD+ ID: 33375894749", Type = 0, Amount = 1985.4f,  Balance = "$12,234.45" },
                new Transaction { Id = 2,  Date = "2026-03-27", Description = "ACH TRANSFER - PAYROLL DEPOSIT",            Type = 0, Amount = 3200.0f,  Balance = "$10,249.05" },
                new Transaction { Id = 3,  Date = "2026-03-27", Description = "AMAZON.COM PURCHASE",                       Type = 1, Amount = 59.99f,   Balance = "$7,049.05"  },
                new Transaction { Id = 4,  Date = "2026-03-26", Description = "STARBUCKS #4823 COFFEE",                    Type = 1, Amount = 6.75f,    Balance = "$7,109.04"  },
                new Transaction { Id = 5,  Date = "2026-03-26", Description = "ONLINE TRANSFER FROM SAVINGS",              Type = 0, Amount = 500.0f,   Balance = "$7,115.79"  },
                new Transaction { Id = 6,  Date = "2026-03-25", Description = "NETFLIX SUBSCRIPTION",                      Type = 1, Amount = 13.99f,   Balance = "$6,615.79"  },
                new Transaction { Id = 7,  Date = "2026-03-25", Description = "WHOLE FOODS MARKET",                        Type = 1, Amount = 87.34f,   Balance = "$6,629.78"  },
                new Transaction { Id = 8,  Date = "2026-03-24", Description = "UBER TECHNOLOGIES",                         Type = 1, Amount = 22.5f,    Balance = "$6,717.12"  },
                new Transaction { Id = 9,  Date = "2026-03-24", Description = "INTEREST PAYMENT",                          Type = 0, Amount = 12.48f,   Balance = "$6,739.62"  },
                new Transaction { Id = 10, Date = "2026-03-23", Description = "ATM WITHDRAWAL - 5TH AVE",                 Type = 1, Amount = 200.0f,   Balance = "$6,727.14"  },
                new Transaction { Id = 11, Date = "2026-03-22", Description = "SPOTIFY PREMIUM",                           Type = 1, Amount = 9.99f,    Balance = "$6,927.14"  },
                new Transaction { Id = 12, Date = "2026-03-22", Description = "DIRECT DEPOSIT - FREELANCE",                Type = 0, Amount = 750.0f,   Balance = "$6,937.13"  }
            );
        }
    }
}
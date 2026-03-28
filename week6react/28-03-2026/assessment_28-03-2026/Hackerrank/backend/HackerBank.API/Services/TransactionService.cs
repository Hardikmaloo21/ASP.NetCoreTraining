using HackerBank.API.Data;
using HackerBank.API.Models;

namespace HackerBank.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HackerBankDbContext _context;

        public TransactionService(HackerBankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetAll() =>
            _context.Transactions.ToList();

        public IEnumerable<Transaction> FilterByDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return _context.Transactions.ToList();

            return _context.Transactions
                .Where(t => t.Date == date.Trim())
                .ToList();
        }

        public IEnumerable<Transaction> GetSortedByAmount() =>
            _context.Transactions.OrderBy(t => t.Amount).ToList();

        public Transaction Add(Transaction transaction)
        {
            // Ensure date is in YYYY-MM-DD format
            if (DateTime.TryParse(transaction.Date, out DateTime parsed))
                transaction.Date = parsed.ToString("yyyy-MM-dd");

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }
    }
}
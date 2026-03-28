using HackerBank.API.Models;

namespace HackerBank.API.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll();
        IEnumerable<Transaction> FilterByDate(string date);
        IEnumerable<Transaction> GetSortedByAmount();
        Transaction Add(Transaction transaction);
    }
}
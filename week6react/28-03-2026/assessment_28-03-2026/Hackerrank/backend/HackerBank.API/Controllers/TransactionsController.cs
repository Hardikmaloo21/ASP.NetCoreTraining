using HackerBank.API.Models;
using HackerBank.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerBank.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        // GET /api/transactions
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        // GET /api/transactions/filter?date=2026-03-27
        [HttpGet("filter")]
        public IActionResult FilterByDate([FromQuery] string? date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return BadRequest(new { message = "A 'date' query parameter in YYYY-MM-DD format is required." });

            return Ok(_service.FilterByDate(date));
        }

        // GET /api/transactions/sorted
        [HttpGet("sorted")]
        public IActionResult GetSortedByAmount() => Ok(_service.GetSortedByAmount());

        // POST /api/transactions
        [HttpPost]
        public IActionResult AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
                return BadRequest(new { message = "Invalid transaction data." });

            if (string.IsNullOrWhiteSpace(transaction.Date))
                return BadRequest(new { message = "Date is required in YYYY-MM-DD format." });

            if (string.IsNullOrWhiteSpace(transaction.Description))
                return BadRequest(new { message = "Description is required." });

            if (transaction.Amount <= 0)
                return BadRequest(new { message = "Amount must be greater than 0." });

            var saved = _service.Add(transaction);
            return CreatedAtAction(nameof(GetAll), new { id = saved.Id }, saved);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SalesManagementAPI.Data;
using SalesManagementAPI.Models;
using SalesManagementAPI.DTOs;

namespace SalesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // ── US-3.1: Add new customer ──────────────────────────────────────────
        // POST api/customers
        [HttpPost]
        public IActionResult CreateCustomer(CreateCustomerDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            // Validate required fields
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return BadRequest(new { message = "Full name is required." });

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest(new { message = "Email is required." });

            if (string.IsNullOrWhiteSpace(dto.Phone))
                return BadRequest(new { message = "Phone number is required." });

            // Check duplicate email
            var existing = _context.Customers
                .FirstOrDefault(c => c.Email == dto.Email);

            if (existing != null)
                return Conflict(new { message = "A customer with this email already exists." });

            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                CompanyName = dto.CompanyName,
                Region = dto.Region,
                RevenueTier = "Low",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedByUserId = int.Parse(userIdClaim)
            };

            _context.Customers.Add(customer);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "CUSTOMER_CREATED",
                Details = $"Customer '{customer.FullName}' ({customer.Email}) created.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(MapToDto(customer));
        }

        // ── US-3.1: Edit customer ─────────────────────────────────────────────
        // PUT api/customers/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, UpdateCustomerDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            // Check duplicate email (ignore current customer)
            var emailExists = _context.Customers
                .Any(c => c.Email == dto.Email && c.Id != id);

            if (emailExists)
                return Conflict(new { message = "Another customer with this email already exists." });

            // Validate revenue tier
            var validTiers = new[] { "Low", "Medium", "High" };
            if (!string.IsNullOrWhiteSpace(dto.RevenueTier) && !validTiers.Contains(dto.RevenueTier))
                return BadRequest(new { message = "Invalid revenue tier. Valid values: Low, Medium, High." });

            customer.FullName = dto.FullName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.CompanyName = dto.CompanyName;
            customer.Region = dto.Region;
            customer.RevenueTier = dto.RevenueTier;
            customer.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "CUSTOMER_UPDATED",
                Details = $"Customer '{customer.FullName}' ({customer.Email}) updated.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(MapToDto(customer));
        }

        // ── US-3.1: Get all customers with filters ────────────────────────────
        // GET api/customers?name=&region=&revenueTier=&page=1&pageSize=20
        [HttpGet]
        public IActionResult GetCustomers([FromQuery] CustomerFilterDto filter)
        {
            var query = _context.Customers
                .Where(c => c.IsActive)
                .AsQueryable();

            // Filter by name
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(c => c.FullName.Contains(filter.Name));

            // Filter by email
            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(c => c.Email.Contains(filter.Email));

            // Filter by region
            if (!string.IsNullOrWhiteSpace(filter.Region))
                query = query.Where(c => c.Region == filter.Region);

            // Filter by revenue tier
            if (!string.IsNullOrWhiteSpace(filter.RevenueTier))
                query = query.Where(c => c.RevenueTier == filter.RevenueTier);

            var totalCount = query.Count();

            var customers = query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(c => MapToDto(c))
                .ToList();

            return Ok(new PagedCustomerDto
            {
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize),
                Customers = customers
            });
        }

        // ── US-3.2: Get single customer + notes ───────────────────────────────
        // GET api/customers/{id}
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            // Get customer notes
            var notes = _context.CustomerNotes
                .Where(n => n.CustomerId == id)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new CustomerNoteDto
                {
                    Id = n.Id,
                    Note = n.Note,
                    ReminderDate = n.ReminderDate,
                    ReminderSent = n.ReminderSent,
                    CreatedByName = n.CreatedByName,
                    CreatedAt = n.CreatedAt
                })
                .ToList();

            return Ok(new CustomerDetailDto
            {
                Customer = MapToDto(customer),
                Notes = notes,
                TotalOrders = 0,        // Will be updated in Sprint 4
                TotalRevenue = 0        // Will be updated in Sprint 4
            });
        }

        // ── US-3.3: Segment customers ─────────────────────────────────────────
        // GET api/customers/segment
        [HttpGet("segment")]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult SegmentCustomers([FromQuery] CustomerSegmentDto filter)
        {
            var query = _context.Customers
                .Where(c => c.IsActive)
                .AsQueryable();

            if (filter.Logic == "OR")
            {
                // OR logic — match any condition
                query = query.Where(c =>
                    (filter.Region == null || c.Region == filter.Region) ||
                    (filter.RevenueTier == null || c.RevenueTier == filter.RevenueTier)
                );
            }
            else
            {
                // AND logic (default) — match all conditions
                if (!string.IsNullOrWhiteSpace(filter.Region))
                    query = query.Where(c => c.Region == filter.Region);

                if (!string.IsNullOrWhiteSpace(filter.RevenueTier))
                    query = query.Where(c => c.RevenueTier == filter.RevenueTier);
            }

            var customers = query
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => MapToDto(c))
                .ToList();

            return Ok(new
            {
                TotalCount = customers.Count,
                Logic = filter.Logic,
                Filters = new
                {
                    filter.Region,
                    filter.RevenueTier,
                    filter.MinOrders
                },
                Customers = customers
            });
        }

        // ── US-3.4: Add note to customer ──────────────────────────────────────
        // POST api/customers/{id}/notes
        [HttpPost("{id}/notes")]
        public IActionResult AddNote(int id, AddNoteDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            if (string.IsNullOrWhiteSpace(dto.Note))
                return BadRequest(new { message = "Note content is required." });

            if (dto.Note.Length > 2000)
                return BadRequest(new { message = "Note cannot exceed 2000 characters." });

            var note = new CustomerNote
            {
                CustomerId = id,
                Note = dto.Note,
                ReminderDate = dto.ReminderDate,
                ReminderSent = false,
                CreatedByUserId = int.Parse(userIdClaim),
                CreatedByName = userName ?? "Unknown",
                CreatedAt = DateTime.UtcNow
            };

            _context.CustomerNotes.Add(note);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "CUSTOMER_NOTE_ADDED",
                Details = $"Note added to customer '{customer.FullName}'.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new CustomerNoteDto
            {
                Id = note.Id,
                Note = note.Note,
                ReminderDate = note.ReminderDate,
                ReminderSent = note.ReminderSent,
                CreatedByName = note.CreatedByName,
                CreatedAt = note.CreatedAt
            });
        }

        // ── US-3.4: Get all notes for a customer ──────────────────────────────
        // GET api/customers/{id}/notes
        [HttpGet("{id}/notes")]
        public IActionResult GetNotes(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound(new { message = "Customer not found." });

            var notes = _context.CustomerNotes
                .Where(n => n.CustomerId == id)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new CustomerNoteDto
                {
                    Id = n.Id,
                    Note = n.Note,
                    ReminderDate = n.ReminderDate,
                    ReminderSent = n.ReminderSent,
                    CreatedByName = n.CreatedByName,
                    CreatedAt = n.CreatedAt
                })
                .ToList();

            return Ok(notes);
        }

        // ── US-3.5: Merge duplicate customers ────────────────────────────────
        // PUT api/customers/merge
        [HttpPut("merge")]
        [Authorize(Roles = "Admin")]
        public IActionResult MergeCustomers(MergeCustomerDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            if (dto.PrimaryCustomerId == dto.SecondaryCustomerId)
                return BadRequest(new { message = "Primary and secondary customers must be different." });

            var primary = _context.Customers.Find(dto.PrimaryCustomerId);
            var secondary = _context.Customers.Find(dto.SecondaryCustomerId);

            if (primary == null)
                return NotFound(new { message = "Primary customer not found." });

            if (secondary == null)
                return NotFound(new { message = "Secondary customer not found." });

            // Move all notes from secondary to primary
            var secondaryNotes = _context.CustomerNotes
                .Where(n => n.CustomerId == dto.SecondaryCustomerId)
                .ToList();

            secondaryNotes.ForEach(n => n.CustomerId = dto.PrimaryCustomerId);

            // Archive the secondary customer (soft delete)
            secondary.IsActive = false;
            secondary.UpdatedAt = DateTime.UtcNow;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "CUSTOMER_MERGED",
                Details = $"Customer '{secondary.FullName}' (ID: {secondary.Id}) merged into '{primary.FullName}' (ID: {primary.Id}). {secondaryNotes.Count} notes transferred.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new
            {
                message = $"Customer '{secondary.FullName}' has been merged into '{primary.FullName}' successfully.",
                primaryCustomer = MapToDto(primary),
                notesTransferred = secondaryNotes.Count
            });
        }

        // ── Helper: Map Customer to CustomerDto ───────────────────────────────
        private static CustomerDto MapToDto(Customer c) => new CustomerDto
        {
            Id = c.Id,
            FullName = c.FullName,
            Email = c.Email,
            Phone = c.Phone,
            CompanyName = c.CompanyName,
            Region = c.Region,
            RevenueTier = c.RevenueTier,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            CreatedByUserId = c.CreatedByUserId
        };
    }
}

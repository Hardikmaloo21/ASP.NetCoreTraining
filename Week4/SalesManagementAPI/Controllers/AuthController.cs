using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SalesManagementAPI.Data;
using SalesManagementAPI.Models;
using SalesManagementAPI.Helpers;
using SalesManagementAPI.DTOs;

namespace SalesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;
        private readonly PasswordService _passwordService;

        public AuthController(AppDbContext context, JwtService jwt, PasswordService passwordService)
        {
            _context = context;
            _jwt = jwt;
            _passwordService = passwordService;
        }

        // ── US-1.1: Register ──────────────────────────────────────────────────
        // POST api/auth/register
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            // Validate password strength
            if (!_passwordService.IsStrongPassword(dto.Password))
                return BadRequest(new { message = "Password must be at least 8 characters with one uppercase letter and one number." });

            // Check for duplicate email
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (existingUser != null)
                return Conflict(new { message = "An account with this email already exists." });

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = _passwordService.HashPassword(dto.Password),
                Role = "SalesRep",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = null,
                Action = "USER_REGISTERED",
                Details = $"New user registered: {user.Email}",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = "Registration successful. Please log in." });
        }

        // ── US-1.2: Login ─────────────────────────────────────────────────────
        // POST api/auth/login
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            // Generic error — prevents user enumeration
            if (user == null || !_passwordService.VerifyPassword(dto.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid email or password." });

            if (!user.IsActive)
                return Unauthorized(new { message = "Your account has been deactivated. Contact an administrator." });

            var token = _jwt.GenerateToken(user);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = user.Id,
                Action = "USER_LOGIN",
                Details = $"{user.Email} logged in successfully.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new AuthResponseDto
            {
                Token = token,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            });
        }

        // ── US-1.3: Logout ────────────────────────────────────────────────────
        // POST api/auth/logout
        // JWT is stateless — client discards the token.
        // This endpoint logs the action server-side for audit trail.
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = int.Parse(userIdClaim),
                Action = "USER_LOGOUT",
                Details = $"User {userIdClaim} logged out.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = "Logged out successfully. Please discard your token on the client." });
        }

        // ── US-1.4: Forgot Password ───────────────────────────────────────────
        // POST api/auth/forgot-password
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            // Always return OK — prevents email enumeration
            if (user == null)
                return Ok(new { message = "If that email is registered, a reset link has been sent." });

            // Invalidate any existing active tokens for this user
            var existingTokens = _context.PasswordResetTokens
                .Where(t => t.UserId == user.Id && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow)
                .ToList();

            existingTokens.ForEach(t => t.IsUsed = true);

            // Generate new reset token (expires in 24 hours)
            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString("N"),
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.PasswordResetTokens.Add(resetToken);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = user.Id,
                Action = "PASSWORD_RESET_REQUESTED",
                Details = $"Password reset requested for {user.Email}.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            // NOTE: In production send resetToken.Token via email (SendGrid / SMTP)
            // Returned here for development / Swagger testing only
            return Ok(new
            {
                message = "If that email is registered, a reset link has been sent.",
                devResetToken = resetToken.Token    // ⚠️ Remove this line in production
            });
        }

        // ── US-1.4: Reset Password ────────────────────────────────────────────
        // POST api/auth/reset-password
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordDto dto)
        {
            var resetToken = _context.PasswordResetTokens
                .FirstOrDefault(t => t.Token == dto.Token && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow);

            if (resetToken == null)
                return BadRequest(new { message = "Invalid or expired reset token." });

            if (!_passwordService.IsStrongPassword(dto.NewPassword))
                return BadRequest(new { message = "Password must be at least 8 characters with one uppercase letter and one number." });

            var user = _context.Users.Find(resetToken.UserId);

            if (user == null)
                return NotFound(new { message = "User not found." });

            user.PasswordHash = _passwordService.HashPassword(dto.NewPassword);
            resetToken.IsUsed = true;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = user.Id,
                Action = "PASSWORD_RESET_COMPLETED",
                Details = $"Password reset completed for {user.Email}.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = "Password reset successfully. Please log in with your new password." });
        }

        // ── Change Password (logged-in user) ──────────────────────────────────
        // PUT api/auth/change-password
        [Authorize]
        [HttpPut("change-password")]
        public IActionResult ChangePassword(ChangePasswordDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var user = _context.Users.Find(int.Parse(userIdClaim));

            if (user == null)
                return NotFound(new { message = "User not found." });

            if (!_passwordService.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
                return BadRequest(new { message = "Current password is incorrect." });

            if (!_passwordService.IsStrongPassword(dto.NewPassword))
                return BadRequest(new { message = "New password must be at least 8 characters with one uppercase letter and one number." });

            user.PasswordHash = _passwordService.HashPassword(dto.NewPassword);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = user.Id,
                Action = "PASSWORD_CHANGED",
                Details = $"{user.Email} changed their password.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = "Password changed successfully." });
        }

        // ── Get My Profile ────────────────────────────────────────────────────
        // GET api/auth/me
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var user = _context.Users.Find(int.Parse(userIdClaim));

            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            });
        }
    }
}
namespace SalesManagementAPI.Helpers
{
    public class PasswordService
    {
        // Hash a plain-text password using BCrypt
        public string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword, workFactor: 12);
        }

        // Verify plain password against stored BCrypt hash
        public bool VerifyPassword(string plainPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, passwordHash);
        }

        // Min 8 chars, at least 1 uppercase, at least 1 digit
        public bool IsStrongPassword(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsDigit)) return false;
            return true;
        }
    }
}
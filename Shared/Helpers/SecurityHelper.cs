namespace Shared.Helpers
{
    public class SecurityHelper
    {
        public static string GenerateHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool ValidateHash(string password, string actualPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, actualPassword);
        }
        public static string GenerateRandomPassword()
        {
            var length = 10;
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var result = new char[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = validChars[random.Next(validChars.Length)];
            }
            return new string(result);
        }
    }
}

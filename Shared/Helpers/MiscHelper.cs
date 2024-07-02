using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class MiscHelper
    {
        private static readonly Random _random = new Random();
        public static string GenerateRefferalToken(string name)
        {
            name = name.Replace(" ", "").ToLower();
            var token = $"{name}-{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6)}";
            return token;
        }

        public static string GenerateVerificationToken() =>
            Guid.NewGuid().ToString().Replace("-", "");

        public static string GenerateVerificationCode()
        {
            const int codeLength = 4;
            const int minDigit = 0;
            const int maxDigit = 9;

            char[] code = new char[codeLength];

            for (int i = 0; i < codeLength; i++)
            {
                code[i] = (char)(_random.Next(minDigit, maxDigit + 1) + '0');
            }

            return new string(code);
        }
    }
}

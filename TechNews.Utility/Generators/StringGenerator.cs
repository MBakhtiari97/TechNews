using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.Utility.Generators
{
    public static class StringGenerator
    {
        public static string GenerateUniqueString()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }

        public static string GenerateRandomPassword()
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        }
    }
}

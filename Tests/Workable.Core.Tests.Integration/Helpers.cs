using System;
using System.IO;

namespace Workable.Core.Tests.Integration
{
    public class Helpers
    {
        public static string Subdomain()
        {
            return GetLine(0);
        }

        public static string AuthToken()
        {
            return GetLine(1);
        }

        private static string GetLine(int index)
        {
            return File.ReadAllText("..\\..\\Credentials.txt").Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries)[index];
        }
    }
}

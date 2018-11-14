using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading.Tasks;

namespace JsonPrettyPrint
{
    class Program
    {
        // Return codes
        public const int EXCEPTION = 2;
        public const int ERROR = 1;
        public const int OK = 0;

        public static async Task<int> Main(string[] args)
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Unexpected error: {ex}");
                Console.ResetColor();
                return EXCEPTION;
            }
        }
    }
}

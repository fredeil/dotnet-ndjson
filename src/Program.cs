using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ndsjonrettyPrint
{
    [Command(ThrowOnUnexpectedArgument = false)]
    class Program
    {
        [Option(Description = "Input is separated by newlines")]
        public bool Newline { get; set; }

        public string[] RemainingArguments { get; }

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            try
            {
                if (RemainingArguments == null)
                    return;

                foreach (var file in RemainingArguments)
                {
                    using (var sr = new StreamReader(file))
                    {
                        if (Newline)
                        {
                            while (!sr.EndOfStream)
                            {
                                Console.WriteLine(sr.ReadLine());
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            String line = sr.ReadToEnd();
                            Console.WriteLine(line);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("The file could not be read.");
            }
        }
    }
}

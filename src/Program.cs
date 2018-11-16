using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Threading.Tasks;
using Console = Colorful.Console;
using System.Drawing;
using Newtonsoft.Json;

namespace ndjson
{
    [Command(ThrowOnUnexpectedArgument = false)]
    class Program
    {
        public string[] RemainingArguments { get; }

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            try
            {
                if (RemainingArguments == null || RemainingArguments.Length == 0)
                    return;

                using (var sr = new StreamReader(RemainingArguments[0]))
                {
                    var count = 0;

                    while (!sr.EndOfStream)
                    {
                        var json = sr.ReadLine();
                        var jobject = JsonConvert.DeserializeObject(json);
                        Console.WriteLine(jobject.ToString());

                        if (++count >= 5)
                        {
                            if (!sr.EndOfStream)
                                Console.WriteLine("--More--", Color.Yellow);

                            Console.ReadKey();
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Could not read file.");
            }
            catch (Newtonsoft.Json.JsonException)
            {
                try
                {
                    using (var sr = new StreamReader(RemainingArguments[0]))
                    {
                        var json = sr.ReadToEnd();
                        var lenght = json.Length;
                        var jsonPretty = JsonConvert.DeserializeObject(json).ToString();
                        Console.WriteLine(jsonPretty);
                    }
                }
                catch
                {
                    Console.WriteLine("Could not read file.");
                }
            }
        }
    }
}

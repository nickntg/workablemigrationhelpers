using System;
using Workable.Core.Migration.Gmail;
using Workable.Gmail.Preprocessor.Etravel;

namespace Workable.Gmail.Preprocessor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: <Mbox file> <Storage directory>");
                return;
            }

            var reader = new MboxReader();
            reader.ProcessMbox(new Classifier(), args[1], args[0]);
        }
    }
}
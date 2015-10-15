using System;
using System.IO;
using RestSharp;
using Workable.Core;
using Workable.Core.Migration.Gmail;

namespace Workable.Gmail.Postprocessor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Job:<Job id> Source:<Directory with preprocessed data> FromDate:[Starting viable date] ToDate:[Ending viable date] AuthToken:<Authentication token> Subdomain:<Account subdomain> <dir1> [dir2] [dir3] .....");
                return;
            }

            var parameters = ParameterParser.Parse(args);
            if (string.IsNullOrEmpty(parameters.JobId))
            {
                Console.WriteLine("No Job id was specified");
                return;
            }

            if (string.IsNullOrEmpty(parameters.SourceDir))
            {
                Console.WriteLine("No source directory was specified");
                return;
            }

            if (parameters.Subdirs.Count == 0)
            {
                Console.WriteLine("At least one subdir must be specified");
                return;
            }

            var fromDate = string.IsNullOrEmpty(parameters.FromDate)
                ? DateTime.MinValue
                : Convert.ToDateTime(parameters.FromDate);
            var toDate = string.IsNullOrEmpty(parameters.ToDate)
                ? DateTime.MaxValue
                : Convert.ToDateTime(parameters.ToDate);

            var client = new WorkableClient {AuthToken = parameters.AuthToken, AccountSubdomain = parameters.Subdomain};

            var count = 0;
            var failed = 0;

            foreach (var dir in parameters.Subdirs)
            {
                var files = Directory.GetFiles(Path.Combine(parameters.SourceDir, dir), "*.json");
                foreach (var file in files)
                {
                    var classification = SimpleJson.DeserializeObject<Classification>(File.ReadAllText(file, System.Text.Encoding.Default));

                    if (classification.Submitted.CompareTo(fromDate) < 0 && toDate.CompareTo(classification.Submitted) < 0)
                    {
                        continue;
                    }

                    var candidate = new WorkableCandidate {candidate = new CandidateInner()};
                    candidate.candidate.cover_letter = classification.CoverLetter;
                    candidate.candidate.email = classification.Mail;
                    candidate.candidate.name = classification.Name;
                    candidate.candidate.phone = classification.Phone;
                    if (!string.IsNullOrEmpty(classification.AttachmentName))
                    {
                        candidate.candidate.resume = new Resume
                        {
                            data = classification.Attachment,
                            name = classification.AttachmentName
                        };
                    }
                    candidate.candidate.disqualified = false;

                    try
                    {
                        client.AddCandidate(candidate, "sourced", parameters.JobId);
                        count++;
                    }
                    catch (Exception)
                    {
                        failed++;
                    }
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Uploaded {0}, failed {1}", count, failed);
                }
            }

            Console.Write("Press key to continue");
            Console.ReadKey();
        }
    }
}

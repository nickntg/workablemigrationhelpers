using System.Collections.Generic;

namespace Workable.Gmail.Postprocessor
{
    public class ParameterParser
    {
        public static Parameters Parse(string[] args)
        {
            var parms = new Parameters {Subdirs = new List<string>()};
            foreach (var arg in args)
            {
                if (arg.StartsWith("Job:"))
                    parms.JobId = arg.Replace("Job:", string.Empty);
                else if (arg.StartsWith("Source:"))
                    parms.SourceDir = arg.Replace("Source:", string.Empty);
                else if (arg.StartsWith("FromDate:"))
                    parms.FromDate = arg.Replace("FromDate:", string.Empty);
                else if (arg.StartsWith("EndDate:"))
                    parms.ToDate = arg.Replace("EndDate:", string.Empty);
                else if (arg.StartsWith("Subdomain:"))
                    parms.Subdomain = arg.Replace("Subdomain:", string.Empty);
                else if (arg.StartsWith("AuthToken:"))
                    parms.AuthToken = arg.Replace("AuthToken:", string.Empty);                
                else
                    parms.Subdirs.Add(arg);
            }

            return parms;
        }
    }

    public class Parameters
    {
        public string JobId { get; set; }

        public string SourceDir { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public List<string> Subdirs { get; set; }

        public string Subdomain { get; set; }

        public string AuthToken { get; set; }
    }
}

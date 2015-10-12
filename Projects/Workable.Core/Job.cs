namespace Workable.Core
{
    public class Job
    {
        public string id { get; set; }

        public string title { get; set; }

        public string full_title { get; set; }

        public string short_code { get; set; }

        public string code { get; set; }

        public string state { get; set; }

        public string department { get; set; }

        public string url { get; set; }

        public string application_url { get; set; }

        public string shortlink { get; set; }
        
        public Location location { get; set; }

        public string created_at { get; set; }
    }
}

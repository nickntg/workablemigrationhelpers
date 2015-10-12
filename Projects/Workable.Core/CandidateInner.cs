using System.Collections.Generic;

namespace Workable.Core
{
    public class CandidateInner
    {
        public string id { get; set; }

        public string name { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string headline { get; set; }

        public string summary { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public string cover_letter { get; set; }

        public List<EducationEntry> education_entries { get; set; }

        public List<ExperienceEntry> experience_entries { get; set; }

        public List<Answer> answers { get; set; }

        public List<string> skills { get; set; }

        public List<SocialProfile> social_profiles { get; set; }

        public string resume_url { get; set; }

        public string image_url { get; set; }

        public Resume resume { get; set; }

        public bool? disqualified { get; set; }

        public List<string> tags { get; set; } 
    }
}
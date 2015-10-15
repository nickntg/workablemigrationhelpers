using System;

namespace Workable.Core.Migration.Gmail
{
    public class Classification
    {
        public string Job { get; set; }

        public DateTime Submitted { get; set; }

        public string Mail { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string AdUrl { get; set; }

        public string CoverLetter { get; set; }

        public string Attachment { get; set; }

        public string AttachmentName { get; set; }
    }
}
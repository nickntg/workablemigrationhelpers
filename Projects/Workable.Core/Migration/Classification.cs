using System;
using System.Collections.Generic;

namespace Workable.Core.Migration
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

        public List<Comment> Comments { get; set; }

        public string Stage { get; set; }

        public bool IsDisqualified { get; set; }
    }
}
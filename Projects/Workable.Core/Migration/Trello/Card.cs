using System.Collections.Generic;

namespace Workable.Core.Migration.Trello
{
    public class Card
    {
        public string id { get; set; }

        public string dateLastActivity { get; set; }

        public string desc { get; set; }

        public string idBoard { get; set; }

        public string idList { get; set; }
        
        public List<string> idLabels { get; set; }

        public string name { get; set; }
        
        public List<Label> labels { get; set; }
        
        public string url { get; set; }

        public List<Attachment> attachments { get; set; }
    }
}

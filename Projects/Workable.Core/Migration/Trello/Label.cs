using System.Collections.Generic;

namespace Workable.Core.Migration.Trello
{
    public class Label
    {
        public string id { get; set; }

        public string idBoard { get; set; }

        public string name { get; set; }

        public string color { get; set; }

        public int uses { get; set; }
    }
}

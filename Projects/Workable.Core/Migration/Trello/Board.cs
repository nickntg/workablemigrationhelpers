using System.Collections.Generic;

namespace Workable.Core.Migration.Trello
{
    public class Board
    {
        public string id { get; set; }

        public string name { get; set; }

        public string desc { get; set; }

        public string descData { get; set; }

        public string url { get; set; }

        public string shortLink { get; set; }

        public Dictionary<string, string> labelNames { get; set; }

        public string dateLastActivity { get; set; }

        public string dateLastView { get; set; }

        public string shortUrl { get; set; }

        public List<Label> labels { get; set; }

        public List<Card> cards { get; set; }

        public List<Member> members { get; set; }

        public List<List> lists { get; set; } 

    }
}

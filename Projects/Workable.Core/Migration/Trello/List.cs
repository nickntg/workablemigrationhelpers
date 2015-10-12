namespace Workable.Core.Migration.Trello
{
    public class List
    {
        public string id { get; set; }
        public string name { get; set; }

        public bool closed { get; set; }

        public string idBoard { get; set; }
    }
}

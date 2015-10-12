using System.Collections.Generic;

namespace Workable.Core
{
    public class Answer
    {
        public string question_key { get; set; }

        public string body { get; set; }

        public bool? Checked { get; set; }

        public List<string> choices { get; set; } 
    }
}

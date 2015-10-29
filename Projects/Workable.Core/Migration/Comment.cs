using System;

namespace Workable.Core.Migration
{
    public class Comment
    {
        public string Commenter { get; set; }

        public string AttachedFile { get; set; }

        public string CommentContents { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

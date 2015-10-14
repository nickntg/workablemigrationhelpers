using System.IO;
using System.Text;
using MsgReader.Mime;
using Workable.Core.Migration.Gmail.Interfaces;

namespace Workable.Core.Migration.Gmail
{
    public class MboxReader
    {
        public void ProcessMbox (IMailClassifier classifier, string saveDirectory, string mboxFile)
        {
            using (var sr = new StreamReader(mboxFile, Encoding.Default))
            {
                var buffer = new StringBuilder();
                var blank = true;

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (blank && line.StartsWith("From ") && buffer.Length != 0)
                    {
                        Classify(classifier, saveDirectory, buffer);
                        buffer = new StringBuilder();
                    }
                    else
                    {
                        blank = string.IsNullOrEmpty(line);
                        buffer.Append(line + "\r\n");
                    }
                }

                if (buffer.Length != 0)
                {
                    Classify(classifier, saveDirectory, buffer);
                }
            }
        }

        private void Classify(IMailClassifier classifier, string saveDirectory, StringBuilder buffer)
        {
            var classification = classifier.ClassifyMail(new Message(Encoding.Default.GetBytes(buffer.ToString())));
        }
    }
}
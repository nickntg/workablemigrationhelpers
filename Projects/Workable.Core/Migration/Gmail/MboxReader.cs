using System;
using System.IO;
using System.Text;
using MsgReader.Mime;
using RestSharp.Serializers;
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
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

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

            if (classification == null)
            {
                return;
            }

            var dir = Path.Combine(saveDirectory, classification.Job);
            Directory.CreateDirectory(dir);
            dir = Path.Combine(dir,
                string.Format("{0}.{1}.json", classification.Submitted.ToString("yyyyMMdd_HHmmss"),
                    Guid.NewGuid().ToString().Replace("-", "")));
            var contents = new JsonSerializer().Serialize(classification);
            File.WriteAllText(dir, contents);
        }
    }
}
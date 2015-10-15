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
                var classified = 0;
                var skipped = 0;

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    if (blank && line.StartsWith("From ") && buffer.Length != 0)
                    {
                        try
                        {
                            if (Classify(classifier, saveDirectory, buffer))
                            {
                                classified++;
                            }
                            else
                            {
                                skipped++;
                            }
                            buffer = new StringBuilder();

                            if ((classified + skipped) % 10 == 0)
                            {
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine("Skipped: {0}", skipped);
                                Console.WriteLine("Classified: {0}", classified);
                                Console.WriteLine("Read percentage: {0}", sr.BaseStream.Position * 100 / sr.BaseStream.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
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

        private bool Classify(IMailClassifier classifier, string saveDirectory, StringBuilder buffer)
        {
            try
            {
                var classification = classifier.ClassifyMail(new Message(Encoding.Default.GetBytes(buffer.ToString())));

                if (classification == null)
                {
                    return false;
                }

                var dir = Path.Combine(saveDirectory, classification.Job.Replace("/", "_").Replace(":", "_").Replace("\\", "_").Replace("<", "_").Replace(">", "_").Replace("\"", "_").Replace("EOF", "E_O_F_"));
                Directory.CreateDirectory(dir);
                dir = Path.Combine(dir,
                    string.Format("{0}.{1}.json", classification.Submitted.ToString("yyyyMMdd_HHmmss"),
                        Guid.NewGuid().ToString().Replace("-", "")));
                var contents = new JsonSerializer().Serialize(classification);
                File.WriteAllText(dir, contents);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
                Console.Clear();
                return false;
            }
        }
    }
}
using System.IO;
using MsgReader.Mime;
using NUnit.Framework;
using Workable.Core.Migration.Gmail;
using Workable.Core.Migration.Gmail.Interfaces;

namespace Workable.Core.Tests.Integration
{
    [TestFixture]
    public class MBoxReaderTests
    {
        [Test]
        public void TestReader()
        {
            var reader = new MboxReader();
            var classifier = new DummyClassifier();
            reader.ProcessMbox(classifier, Directory.GetCurrentDirectory(), "..\\..\\..\\..\\Data\\Gmail\\sampleFile.txt");
            Assert.AreEqual(7, classifier.NumMessages);
        }
    }

    public class DummyClassifier : IMailClassifier
    {
        public int NumMessages { get; set; }

        public Classification ClassifyMail(Message mail)
        {
            NumMessages++;
            return null;
        }
    }
}
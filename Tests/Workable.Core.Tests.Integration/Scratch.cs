using System.IO;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using Workable.Core.Migration.Trello;

namespace Workable.Core.Tests.Integration
{
    /// <summary>
    /// Scratch tests. Nothing to see here except a test workbench.
    /// </summary>
    [TestFixture]
    public class Scratch
    {
        [Test]
        public void TestReadTrelloExport()
        {
            var contents = File.ReadAllText("..\\..\\..\\..\\Data\\Trello\\testExport.json");

            var x = new JsonDeserializer();
            var dato = SimpleJson.DeserializeObject<Board>(contents);
        }
    }
}

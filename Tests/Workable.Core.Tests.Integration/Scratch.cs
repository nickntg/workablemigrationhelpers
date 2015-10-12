using System;
using System.Collections.Generic;
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
        [Test]
        public void TestSomething()
        {
            var client = new WorkableClient { AccountSubdomain = Helpers.Subdomain(), AuthToken = Helpers.AuthToken(), RequestUrl = "jobs/97DE64CE38/candidates" };
            var req = new RestRequest(Method.POST);
            req.AddQueryParameter("stage", "trial");

            var candidate = new WorkableCandidate
            {
                candidate =
                    new CandidateInner
                    {
                        name = "Test name",
                        firstname = "test",
                        lastname = "name",
                        headline = "a headline",
                        summary = "some summary",
                        address = "some address",
                        phone = "21078987812",
                        email = "nickntg@gmail.com",
                        cover_letter = null,
                        skills = new List<string>() {"Chef", "Pianist"},
                        //disqualified = true,
                        tags = new List<string> { "t1", "t2"}
                    }
            };
            candidate.candidate.resume = new Resume
            {
                name = "cv1.pdf",
                data = Convert.ToBase64String(File.ReadAllBytes("d:\\cv1.pdf"))
            };

            req.RequestFormat = DataFormat.Json;
            req.AddBody(candidate);
            var result = client.Execute<WorkableCandidate>(req);
        }
    }
}

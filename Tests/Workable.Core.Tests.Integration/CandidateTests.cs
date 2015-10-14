using System.Collections.Generic;
using NUnit.Framework;

namespace Workable.Core.Tests.Integration
{
    [TestFixture]
    public class CandidateTests
    {
        [Test]
        public void TestSomething()
        {
            var client = new WorkableClient { AccountSubdomain = Helpers.Subdomain(), AuthToken = Helpers.AuthToken() };

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
                        email = "mymail@gmail.com",
                        cover_letter = null,
                        skills = new List<string> { "Chef", "Pianist" }
                    }
            };

            candidate = client.AddCandidate(candidate, "trial", "97DE64CE38");

            Assert.IsNotNull(candidate);
        }
    }
}
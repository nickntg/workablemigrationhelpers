using NUnit.Framework;

namespace Workable.Core.Tests.Integration
{
    [TestFixture]
    public class JobTests
    {
        [Test]
        public void TestJobsRetrieval()
        {
            var client = new WorkableClient
            {
                AccountSubdomain = Helpers.Subdomain(),
                AuthToken = Helpers.AuthToken()
            };

            var jobs = client.GetJobs();

            Assert.IsNotNull(jobs);
            Assert.IsNotNull(jobs.jobs);
            Assert.IsTrue(jobs.jobs.Count > 0);
        }
    }
}
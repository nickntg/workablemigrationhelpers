using RestSharp;

namespace Workable.Core.Interfaces
{
    public interface IWorkableClient
    {
        string AccountSubdomain { get; set; }

        string AuthToken { get; set; }

        string RequestUrl { get; set; }

        T Execute<T>(RestRequest request) where T : new();

        JobArray GetJobs();
    }
}

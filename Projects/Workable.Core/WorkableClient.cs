using System;
using RestSharp;
using Workable.Core.Interfaces;

namespace Workable.Core
{
    public class WorkableClient : IWorkableClient
    {
        private const string BaseUrl = "https://www.workable.com/spi/v3/accounts";

        public string AccountSubdomain { get; set; }

        public string AuthToken { get; set; }

        public string RequestUrl { get; set; }

        public virtual T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(string.Format("{0}/{1}/{2}", BaseUrl, AccountSubdomain, RequestUrl))
            };

            client.AddDefaultHeader("Authorization", "Bearer " + AuthToken);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return response.Data;
        }

        public virtual JobArray GetJobs()
        {
            RequestUrl = "jobs";
            return Execute<JobArray>(new RestRequest(Method.GET));
        }
    }
}
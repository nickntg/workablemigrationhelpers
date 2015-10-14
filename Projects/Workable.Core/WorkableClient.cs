using System;
using System.Net;
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

        public virtual T Execute<T>(RestRequest request, HttpStatusCode? expectedCode) where T : new()
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

            if (expectedCode.HasValue && response.StatusCode != expectedCode.Value)
            {
                throw new InvalidOperationException(string.Format("Status code: {0}", response.StatusCode));
            }

            return response.Data;
        }

        public virtual JobArray GetJobs()
        {
            RequestUrl = "jobs";
            return Execute<JobArray>(new RestRequest(Method.GET), null);
        }

        public virtual WorkableCandidate AddCandidate(WorkableCandidate candidate, string stage, string jobId)
        {
            RequestUrl = string.Format("jobs/{0}/candidates", jobId);

            var req = new RestRequest(Method.POST);
            if (!string.IsNullOrEmpty(stage))
            {
                req.AddQueryParameter("stage", stage);
            }

            req.RequestFormat = DataFormat.Json;
            req.AddBody(candidate);

            return Execute<WorkableCandidate>(req, HttpStatusCode.Created);
        }
    }
}
using Microsoft.Extensions.Configuration;
using NasaNeo.Business.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NasaNeo.Business.NasaApi
{
    public class NasaApiLiveRepo : INasaNeoRepo
    {
        private string nasaApiKey = String.Empty;
        private string nasaNeoUrl = String.Empty;

        public NasaApiLiveRepo(IConfiguration config)
        {
            nasaNeoUrl = config["NasaNeoApi:Url"];

            nasaApiKey = config["appSettings:nasa:apikey"];
            if( String.IsNullOrWhiteSpace(nasaApiKey) )
            {
                nasaApiKey = config["NasaNeoApi:DevApiKey"];
            }
        }

        public async Task<NasaNeoSet> GetNeoForDateAsync(DateTime neoDate)
        {
            var result = new NasaNeoSet();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Clear();
            var stringTask = httpClient.GetStringAsync(
                nasaNeoUrl
                .Replace("{start-date}", neoDate.ToString("yyyy-MM-dd"))
                .Replace("{end-date}", neoDate.ToString("yyyy-MM-dd"))
                .Replace("{key}", nasaApiKey));

            var json = await stringTask;

            return NasaApiHelper.ProcessNeoFromJson(json, neoDate);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services;
using Newtonsoft.Json;
using System.Linq;
using static HackerNewsAPI.Domain.Util.RequestsHelper;
using System.Net;
using HackerNewsAPI.Domain.Exceptions;

namespace Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private const string baseUrl =" https://hacker-news.firebaseio.com/v0/";
        private readonly HttpClient client;

        public HackerNewsService()
        {
            client = new HttpClient();
        }

        public async Task<IEnumerable<Story>> BestStories(int totalStories = 20)
        {
            var url = baseUrl+"/"+ Methods[APIKeys.BestStories]+".json";
            
            var responseIds = await HttpCall(url);
                
            if(responseIds == null) 
                throw new HttpResponseException(HttpStatusCode.NotFound,
                                    "Error getting List of Stories Ids");

            var idsList 
                = JsonConvert.DeserializeObject<List<string>>(responseIds);

            var orderedList =  await GetStoriesAsync(idsList);
            
            return orderedList.OrderByDescending(x=>x.Score).Take(totalStories);
        }

        public async Task<Story> StoryDetail(string id)
        {
            var url = baseUrl+"/"+ Methods[APIKeys.Detail]+"/"+id+".json";

            try
            {
                var responseIds = await HttpCall(url);
                
                return JsonConvert.DeserializeObject<Story>(responseIds);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task<IEnumerable<Story>> GetStoriesAsync(List<string> idsList)
        {
            return await Task.WhenAll(from id in idsList select StoryDetail(id));
        }

        private async Task<string> HttpCall(string url)
        {
            using(var response = await client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
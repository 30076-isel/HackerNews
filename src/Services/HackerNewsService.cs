using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;
using Newtonsoft.Json;
using System.Linq;
using static HackerNewsAPI.Domain.Util.RequestsHelper;
using System.Net;
using HackerNewsAPI.Domain.Exceptions;
using Domain.Interfaces;

namespace Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private const string baseUrl =" https://hacker-news.firebaseio.com/v0/";

        private readonly IHttpClientFactory clientFactory;
        private readonly HttpClient client;

        public HackerNewsService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;

            client = clientFactory.CreateClient();
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
/*
Write a function:

class Solution { public int solution(int[] A); }

that, given an array A of N integers, returns the smallest positive integer (greater than 0) that does not occur in A.

For example, given A = [1, 3, 6, 4, 1, 2], the function should return 5.

Given A = [1, 2, 3], the function should return 4.

Given A = [−1, −3], the function should return 1.

Write an efficient algorithm for the following assumptions:

N is an integer within the range [1..100,000];
each element of array A is an integer within the range [−1,000,000..1,000,000].

*/
            public int solution(int[] A) {
        // write your code in C# 6.0 with .NET 4.5 (Mono)
                var min = 1;
        if(A.Length == 0) return min;

        Array.Sort(A);

        if(A[0] > 1 || A [A.Length - 1 ] <= 0)  return min;
        
        for (int i = 0; i < A.Length; i++){
            if(A[i] > min) break;
            if(A[i] == min) min = min + 1;
        }

        return min;
    }
    
    public int solution2(string A, string B) {
        // write your code in C# 6.0 with .NET 4.5 (Mono)
        char [] values = {
            '2','3','4','5','6','7','8','9','T','J','Q','K','A'
        };
        
        var N = A.Length;
        int total = 0;
        for(int i = 0;i< N; ++i)
        {
            var ACard = Array.IndexOf(values,A[i]);
            var BCard = Array.IndexOf(values,B[i]);
            if(ACard > BCard) ++total;
        }
        return total;
    }
    }
}
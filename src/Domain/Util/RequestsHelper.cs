
using System.Collections.Generic;

namespace HackerNewsAPI.Domain.Util
{
    public static class RequestsHelper
    {
        public enum APIKeys
        {
            BestStories = 1,
            Detail  = 2
        }
        public static Dictionary<APIKeys,string> Methods = new Dictionary<APIKeys, string>{
        {APIKeys.BestStories,"beststories"},
        {APIKeys.Detail,"item"}
        };
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<Story>> BestStories(int totalStories = 20);

        Task<Story> StoryDetail(string id);
    }
}
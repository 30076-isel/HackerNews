using System.Threading.Tasks;
using Controllers;
using Domain.Interfaces;
using FakeItEasy;
using Xunit;

namespace HackerNewsTests.Controllers
{
    public class HackerNewsControllerTest
    {
        private readonly IHackerNewsService service;
        private readonly HackerNewsController controller;
        public HackerNewsControllerTest()
        {
            service = A.Fake<IHackerNewsService>();

            controller = new HackerNewsController(service);
        }

        [Fact]
        public async Task TestControllerCall_BestStories()
        {
            var totalStories = 20;
            await controller.BestStories(totalStories);
            A.CallTo(() => service.BestStories(totalStories)).MustHaveHappened();
        }
    }
}

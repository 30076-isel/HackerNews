using System;
using System.Threading.Tasks;
using Domain.Services;
using HackerNewsAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HackerNewsController: ControllerBase
    {
        private readonly IHackerNewsService _iIHackerNewsService;

        public HackerNewsController(IHackerNewsService iIHackerNewsService)
        {
            this._iIHackerNewsService = iIHackerNewsService;
        }

        [HttpGet("beststories")]
        public async Task<IActionResult> BestStories(int totalStories = 20)
        {
            try
            {
                var stories = await _iIHackerNewsService.BestStories(totalStories);

                return Ok(stories);
            }
            catch(HttpResponseException ex)
            {
                return StatusCode((int) ex.Status,ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet("Story/{id}")]     
        public async Task<IActionResult> StoryDetail(string id)
        {
            var storyDetail 
                = await _iIHackerNewsService.StoryDetail(id);

            return Ok(storyDetail);
        }
    }
}
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchControllers : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchControllers(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        public IActionResult SearchRequest()
        {
            return Ok("You have found me!");
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await searchService.SearchAsync(term.CustomerId);
            if(result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }

            return NotFound();
        }
        
    }
}
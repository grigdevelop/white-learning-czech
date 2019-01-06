using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhileLearningCzech.Domain.Services.Articles;
using WhileLearningCzech.Domain.Services.Articles.Dto;
using WhileLearningCzech.Web.Helpers;

namespace WhileLearningCzech.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Search()
        {
            return new JsonResult(await _articleService.Search());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody]ArticleDto input)
        {
            return new JsonResult(await _articleService.Create(input));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]ArticleDto input)
        {
            return new JsonResult(await _articleService.Update(input));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ArticleDto input)
        {
            return new JsonResult(await _articleService.Delete(input));
        }
    }
}
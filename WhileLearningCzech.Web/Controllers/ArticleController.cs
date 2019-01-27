using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Web.Helpers;
using WhileLearningCzech.Domain.Services.Articles;
using WhileLearningCzech.Domain.Services.Articles.Dto;
using WhileLearningCzech.Domain.Services.Media;
using WhileLearningCzech.Web.Helpers;

namespace PersonalWebsite.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IImagesService _imagesService;

        public ArticleController(IArticleService articleService, IImagesService imagesService)
        {
            _articleService = articleService;
            _imagesService = imagesService;
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

        [HttpGet("[action]/{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> Image(string name)
        {
            // Get image path  
            var id = int.Parse(name.Substring(0, name.LastIndexOf('.')));
            var image = await _imagesService.GetImageById(id);            
            return File(image.Data, image.DataType.Replace("data:", string.Empty).Replace("base64", string.Empty));
        }
    }
}
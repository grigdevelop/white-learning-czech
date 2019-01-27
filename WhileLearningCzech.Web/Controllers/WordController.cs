using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhileLearningCzech.Domain.Services.Words;
using WhileLearningCzech.Domain.Services.Words.Dto;

namespace PersonalWebsite.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;

        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromBody]SearchWordDto input)
        {
            return new JsonResult(await _wordService.Search(input));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody]WordDto input)
        {
            return new JsonResult(await _wordService.CreateWord(input));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]WordDto input)
        {
            return new JsonResult(await _wordService.UpdateWord(input));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]WordDto input)
        {
            return new JsonResult(await _wordService.DeleteWord(input));
        }
    }
}
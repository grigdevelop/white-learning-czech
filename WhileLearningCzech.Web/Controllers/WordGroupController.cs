using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Web.Helpers;
using WhileLearningCzech.Domain.Services.WordGroups;
using WhileLearningCzech.Domain.Services.WordGroups.Dto;
using WhileLearningCzech.Web.Helpers;

namespace PersonalWebsite.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorize]
    public class WordGroupController : ControllerBase
    {
        private readonly IWordGroupService _wordGroupService;

        public WordGroupController(IWordGroupService wordGroupService)
        {
            _wordGroupService = wordGroupService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Search()
        {
            return new JsonResult(await _wordGroupService.GetWordGroups());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody]WordGroupDto input)
        {
            return new JsonResult(await _wordGroupService.CreateWordGroup(input));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]WordGroupDto input)
        {
            return new JsonResult(await _wordGroupService.UpdateWordGroup(input));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]WordGroupDto input)
        {
            return new JsonResult(await _wordGroupService.DeleteWordGroup(input));
        }
    }
}
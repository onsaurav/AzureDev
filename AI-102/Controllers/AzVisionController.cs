using AI_102.Helper;
using AI_102.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_102.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzVisionController : ControllerBase
    {
        private readonly AzVisionServiceHelper _AzVisionServiceHelper;

        public AzVisionController(AzVisionServiceHelper azVisionServiceHelper)
        {
            _AzVisionServiceHelper = azVisionServiceHelper;
        }

        [HttpGet("analyze-image")]
        public async Task<Result> AnalyzeImage(string imageUrl)
        {
            return await _AzVisionServiceHelper.AnalyzeImage(imageUrl);
        }
    }
}

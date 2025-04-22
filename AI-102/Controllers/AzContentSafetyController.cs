using AI_102.Helper;
using AI_102.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_102.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzContentSafetyController : ControllerBase
    {
        private readonly AzContentSafetyServiceHelper _AzContentSafetyServiceHelper;

        public AzContentSafetyController(AzContentSafetyServiceHelper azContentSafetyServiceHelper)
        {
            _AzContentSafetyServiceHelper = azContentSafetyServiceHelper
                ?? throw new ArgumentNullException(nameof(azContentSafetyServiceHelper));
        }

        [HttpGet("analyze-text-content-safety")]
        public async Task<Result> AnalyzeTextContentSafety(string text)
        {
            return await _AzContentSafetyServiceHelper.AnalyzeTextContentSafety(text);
        }

        [HttpGet("analyze-image-content-safety")]
        public async Task<Result> AnalyzeImageContentSafety(string imageName)
        {
            return await _AzContentSafetyServiceHelper.AnalyzeImageContentSafety(imageName);
        }
    }
}

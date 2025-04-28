using AI_102.Helper;
using AI_102.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_102.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzImmersiveReaderController : ControllerBase
    {
        private readonly AzImmersiveReaderServiceHelper _AzImmersiveReaderServiceHelper;

        public AzImmersiveReaderController(AzImmersiveReaderServiceHelper azImmersiveReaderServiceHelper)
        {
            _AzImmersiveReaderServiceHelper = azImmersiveReaderServiceHelper;
        }

        [HttpGet("analyze-text-content-safety")]
        public async Task<Result> AnalyzeTextContentSafety(string text)
        {
            return await _AzImmersiveReaderServiceHelper.AnalyzeTextContentSafety(text);
        }

        [HttpGet("analyze-image-content-safety")]
        public async Task<Result> AnalyzeImageContentSafety(string imageName)
        {
            return await _AzImmersiveReaderServiceHelper.AnalyzeImageContentSafety(imageName);
        }
    }
}

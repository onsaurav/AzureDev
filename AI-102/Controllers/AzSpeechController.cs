using AI_102.Helper;
using AI_102.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_102.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzSpeechController : ControllerBase
    {
        private readonly AzSpeechServiceHelper _AzSpeechServiceHelper;

        public AzSpeechController(AzSpeechServiceHelper azSpeechServiceHelper)
        {
            _AzSpeechServiceHelper = azSpeechServiceHelper;
        }

        [HttpGet("recogniz-speech-from-file")]
        public async Task<Result> RecognizeSpeechFromFile([FromQuery] string fileName)
        {
            return await _AzSpeechServiceHelper.RecognizSpeechFromFile(fileName);
        }

        [HttpGet("speak-this-text")]
        public async Task<Result> SpeakThisText([FromQuery] string text)
        {
            return await _AzSpeechServiceHelper.SpeakThisText(text);
        }
    }
}

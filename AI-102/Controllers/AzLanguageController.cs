using AI_102.Helper;
using AI_102.Model;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AI_102.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzLanguageController : ControllerBase
    {
        private readonly AzLanguageServiceHelper _AzLanguageServiceHelper;

        public AzLanguageController(AzLanguageServiceHelper azLanguageServiceHelper)
        {
            _AzLanguageServiceHelper = azLanguageServiceHelper;
        }

        [HttpGet("get-language")]
        public async Task<Result> GetLanguage(string text)
        {
            return await _AzLanguageServiceHelper.GetLanguage(text);
        }

        [HttpPost("identify-complaints")]
        public async Task<Result> IdentifyComplaints(List<string> batchedDocuments)
        {
            return await _AzLanguageServiceHelper.IdentifyComplaints(batchedDocuments);
        }
    }
}

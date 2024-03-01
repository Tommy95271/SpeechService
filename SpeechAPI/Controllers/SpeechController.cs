using Microsoft.AspNetCore.Mvc;
using SpeechAPI.Services;
using SpeechLibrary.Enums;
using SpeechLibrary.Models;

namespace SpeechAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SpeechController : ControllerBase
    {
        private readonly ILogger<SpeechController> _logger;
        private readonly SpeechAPIService _speechAPIService;

        public SpeechController(ILogger<SpeechController> logger, SpeechAPIService speechAPIService)
        {
            _logger = logger;
            _speechAPIService = speechAPIService;
        }

        [HttpGet]
        public async Task<SpeechModel> TranslateFromMicrophoneAndPlay()
        {
            var result = await _speechAPIService.TranslateFromMicrophoneAsync();
            return result;
        }

        [HttpGet]
        public List<LanguageEnum> GetLanguageEnums()
        {
            return Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>().ToList();
        }
    }
}

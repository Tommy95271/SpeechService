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

        [HttpPost]
        public async Task<SpeechResponse> TranslateFromMicrophoneAndPlay(SpeechRequest request)
        {
            var result = await _speechAPIService.TranslateFromMicrophoneAsync(request);
            return result;
        }

        [HttpGet]
        public async Task<SpeechResponse> StopPlayingAudio()
        {
            return await _speechAPIService.StopAudioAsync();
        }


        [HttpGet]
        public async Task<List<SpeechEnumModel>> GetLanguageEnums()
        {
            var result = _speechAPIService.GetLanguageEnumsAsync();
            return result;
        }
    }
}

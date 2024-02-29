using Microsoft.Extensions.DependencyInjection.Extensions;
using SpeechLibrary.Services;

namespace SpeechBlazor.Client.Services
{
    public static class CommonServices
    {
        public static void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddScoped<SpeechService>();
        }
    }
}

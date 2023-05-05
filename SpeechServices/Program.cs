using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpeechServices.Services;

try
{
    var configuration = new ConfigurationBuilder()
         .AddJsonFile($"appsettings.json");

    //var config = configuration.Build();

    var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddScoped<SpeechService>();

            // Register a named HttpClient with custom settings
            //services.AddHttpClient("SpeechServices", client =>
            //{
            //    client.BaseAddress = new Uri(config["Url:ApiBaseUrl"]);
            //    client.Timeout = TimeSpan.FromSeconds(30);
            //});
        }).Build();


    var speechService = host.Services.GetRequiredService<SpeechService>();
    await speechService.Execute();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}
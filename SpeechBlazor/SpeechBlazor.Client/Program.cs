using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpeechBlazor.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

CommonServices.ConfigureCommonServices(builder.Services);

await builder.Build().RunAsync();

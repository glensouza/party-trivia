using PartyTriviaWeb.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PartyTriviaWeb;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

string? licenseKey = "Ngo9BigBOggjHTQxAR8/V1NGaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUX1ccXBRRmRVUE1zVg==";

WebAssemblyHostBuilder? builder = WebAssemblyHostBuilder.CreateDefault(args);
SyncfusionLicenseProvider.RegisterLicense(licenseKey);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton<PdfService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

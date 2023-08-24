using PartyTriviaWeb.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PartyTriviaWeb;
using Syncfusion.Blazor;

WebAssemblyHostBuilder? builder = WebAssemblyHostBuilder.CreateDefault(args);

//Register Syncfusion license 
string? licenseKey = "MjY2MTcyNkAzMjMyMmUzMDJlMzBmMHdxTjREUDJxUXQ3aEF2M0pmWWFMS25SMjRGaHNGWXhpZHdEYjdLbWdRPQ==;MjY2MTcyN0AzMjMyMmUzMDJlMzBPUlZPSVBvdzZCbjJQanF1cTIvSEh4S2VXV2pkRW1zRGp1ZlVnVnI2alBFPQ==";
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton<PdfService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

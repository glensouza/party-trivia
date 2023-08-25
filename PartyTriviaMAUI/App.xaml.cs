namespace PartyTriviaMAUI;

public partial class App : Application
{
    public App()
    {
        //Register Syncfusion license
        string? licenseKey = "MjY2MTcyNkAzMjMyMmUzMDJlMzBmMHdxTjREUDJxUXQ3aEF2M0pmWWFMS25SMjRGaHNGWXhpZHdEYjdLbWdRPQ==;MjY2MTcyN0AzMjMyMmUzMDJlMzBPUlZPSVBvdzZCbjJQanF1cTIvSEh4S2VXV2pkRW1zRGp1ZlVnVnI2alBFPQ==";
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);

        InitializeComponent();

        MainPage = new MainPage();
    }
}
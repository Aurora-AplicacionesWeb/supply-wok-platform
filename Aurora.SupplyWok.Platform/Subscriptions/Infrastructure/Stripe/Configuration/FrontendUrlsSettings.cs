namespace Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Stripe.Configuration;

public class FrontendUrlsSettings
{
    public string BaseUrl { get; set; } = "http://localhost:5173";
    public string RegisterPath { get; set; } = "/register";
    public string RegisterCompletePath { get; set; } = "/register/complete";
}

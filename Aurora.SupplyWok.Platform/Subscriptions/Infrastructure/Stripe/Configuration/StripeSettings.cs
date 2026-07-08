namespace Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Stripe.Configuration;

public class StripeSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;
    public StripePriceSettings Prices { get; set; } = new();
}

public class StripePriceSettings
{
    public string PremiumMonthly { get; set; } = string.Empty;
    public string EnterpriseMonthly { get; set; } = string.Empty;
}

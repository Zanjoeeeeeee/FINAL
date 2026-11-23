using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DriveMate;

public record CheckoutSessionResult(bool Ok, string SessionId, string CheckoutUrl, string Error);
public record CheckoutVerifyResult(bool Paid, string PaymentId, string Method, string Message);

public interface IPaymentGateway
{
    Task<CheckoutSessionResult> CreateHostedCheckoutAsync(Booking b, User renter);
    Task<CheckoutVerifyResult> VerifyCheckoutSessionAsync(string sessionId);
    string Refund(string method, string originalReference, decimal amount);
}

/// <summary>
/// Xendit Hosted Invoice integration (Test Mode).
/// Set XENDIT_TEST_SECRET environment variable or replace TestSecretKey with your xnd_development_... key.
/// </summary>
public class XenditHostedGateway : IPaymentGateway
{
    public static readonly string TestSecretKey =
        Environment.GetEnvironmentVariable("XENDIT_TEST_SECRET") ?? "xnd_development_DzhUlBM4hdAuvVPt2a9jIFSSAtTxdThKmhWCaByVonwYLsQ2HbEwD2t6DzfAA";

    public static bool HasRealKey =>
        TestSecretKey.StartsWith("xnd_development_") && !TestSecretKey.Contains("replace_with");

    static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(15) };
    static readonly Random Rng = new();

    static AuthenticationHeaderValue AuthHeader() =>
        new("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{TestSecretKey}:")));

    public async Task<CheckoutSessionResult> CreateHostedCheckoutAsync(Booking b, User renter)
    {
        if (!HasRealKey)
        {
            var fakeId = $"inv_test_local_{Rng.NextInt64(1_000_000_000, 9_999_999_999)}";
            return new CheckoutSessionResult(true, fakeId, "local://sandbox-checkout", "");
        }

        try
        {
            var items = new List<object>
            {
                new { name = $"{b.CarName} ({b.Days} day(s))", quantity = 1, price = b.Total },
                new { name = "Refundable Security Deposit", quantity = 1, price = b.Deposit }
            };
            if (b.InsuranceFee > 0)
                items.Add(new { name = $"Insurance ({b.InsuranceTier})", quantity = 1, price = b.InsuranceFee });

            var payload = JsonSerializer.Serialize(new
            {
                external_id = $"cartogo-{b.Id:N}-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                amount = b.AmountDue,
                currency = "PHP",
                description = $"car-to-go Booking #{b.Id.ToString()[..8].ToUpper()} – {b.CarName}",
                payer_email = renter.Email,
                customer = new
                {
                    given_names = renter.FullName,
                    email = renter.Email,
                    mobile_number = string.IsNullOrWhiteSpace(renter.Phone) ? "+639170000000" : renter.Phone
                },
                items,
                payment_methods = new[] { "GCASH", "PAYMAYA", "GRABPAY", "QRPH", "CREDIT_CARD", "BPI", "UNIONBANK" }
            });

            using var req = new HttpRequestMessage(HttpMethod.Post, "https://api.xendit.co/v2/invoices");
            req.Headers.Authorization = AuthHeader();
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            req.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            using var res = await Http.SendAsync(req);
            var json = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                return new CheckoutSessionResult(false, "", "", $"Xendit API Error ({(int)res.StatusCode}):\n{json}");

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var invId = root.GetProperty("id").GetString() ?? "";
            var invoiceUrl = root.GetProperty("invoice_url").GetString() ?? "";

            return new CheckoutSessionResult(true, invId, invoiceUrl, "");
        }
        catch (Exception ex)
        {
            return new CheckoutSessionResult(false, "", "", $"Could not reach Xendit: {ex.Message}");
        }
    }

    public async Task<CheckoutVerifyResult> VerifyCheckoutSessionAsync(string sessionId)
    {
        if (sessionId.StartsWith("inv_test_local_"))
            return new CheckoutVerifyResult(true, $"xnd_pay_{Rng.NextInt64(100000, 999999)}", "Xendit Hosted (Sandbox)", "Paid");

        try
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.xendit.co/v2/invoices/{sessionId}");
            req.Headers.Authorization = AuthHeader();
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var res = await Http.SendAsync(req);
            var json = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                return new CheckoutVerifyResult(false, "", "", $"HTTP {(int)res.StatusCode}");

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var status = root.GetProperty("status").GetString();

            if (status is "PAID" or "SETTLED")
            {
                var payId = root.TryGetProperty("payment_id", out var pid) && pid.ValueKind == JsonValueKind.String
                    ? pid.GetString()!
                    : sessionId;
                var channel = root.TryGetProperty("payment_channel", out var ch) && ch.ValueKind == JsonValueKind.String
                    ? ch.GetString()!
                    : root.TryGetProperty("payment_method", out var pm) && pm.ValueKind == JsonValueKind.String
                        ? pm.GetString()!
                        : "Hosted";

                return new CheckoutVerifyResult(true, payId, $"Xendit ({channel})", "Payment verified");
            }

            return new CheckoutVerifyResult(false, "", "", "Awaiting customer payment on Xendit page…");
        }
        catch (Exception ex)
        {
            return new CheckoutVerifyResult(false, "", "", ex.Message);
        }
    }

    public string Refund(string method, string originalReference, decimal amount)
        => $"xnd_ref_{Rng.NextInt64(1_000_000_000, 9_999_999_999)}";
}

public static class Pay
{
    public static IPaymentGateway Gateway { get; set; } = new XenditHostedGateway();
}
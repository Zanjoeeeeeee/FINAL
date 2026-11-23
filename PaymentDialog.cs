using System.Diagnostics;

namespace DriveMate;

public class PaymentDialog : ThemedForm
{
    public string Method { get; private set; } = "";
    public string Reference { get; private set; } = "";

    readonly Booking booking;
    readonly User renter;
    readonly System.Windows.Forms.Timer pollTimer = new() { Interval = 2500 };
    readonly Label lblAmount = new();
    readonly Label lblStatus = new();
    readonly Button btnOpenBrowser = new();
    readonly Button btnConfirm = new();
    readonly Button btnCancel = new();

    string sessionId = "";
    string checkoutUrl = "";
    bool checking;

    public PaymentDialog(Booking booking, User renter)
    {
        this.booking = booking;
        this.renter = renter;

        Text = "Xendit Hosted Checkout (Test Mode)";
        ClientSize = new Size(440, 270);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        lblAmount.Location = new Point(22, 18);
        lblAmount.Size = new Size(396, 54);
        lblAmount.Font = Theme.Bold(11F);
        lblAmount.Text = $"Total Due: ₱{booking.AmountDue:N2}\n" +
                         $"Fare ₱{booking.Total:N0} + Insurance ₱{booking.InsuranceFee:N0} + Deposit ₱{booking.Deposit:N0}";

        lblStatus.Location = new Point(22, 82);
        lblStatus.Size = new Size(396, 68);
        lblStatus.ForeColor = Theme.Muted;
        lblStatus.Text = "Creating Xendit Hosted Checkout session…";

        btnOpenBrowser.Location = new Point(22, 162);
        btnOpenBrowser.Size = new Size(195, 36);
        btnOpenBrowser.Text = "Open Xendit page";
        btnOpenBrowser.Tag = "secondary";
        btnOpenBrowser.Enabled = false;
        btnOpenBrowser.Click += (s, e) => LaunchUrl();

        btnConfirm.Location = new Point(225, 162);
        btnConfirm.Size = new Size(193, 36);
        btnConfirm.Text = "I've completed payment";
        btnConfirm.Tag = "accent";
        btnConfirm.Enabled = false;
        btnConfirm.Click += async (s, e) => await CheckNowAsync(manual: true);

        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(318, 214);
        btnCancel.Size = new Size(100, 34);
        btnCancel.Text = "Cancel";

        Controls.AddRange(new Control[] { lblAmount, lblStatus, btnOpenBrowser, btnConfirm, btnCancel });

        pollTimer.Tick += async (s, e) => await CheckNowAsync(manual: false);
        Shown += async (s, e) => await StartCheckoutAsync();
        FormClosed += (s, e) => pollTimer.Stop();
    }

    async Task StartCheckoutAsync()
    {
        var res = await Pay.Gateway.CreateHostedCheckoutAsync(booking, renter);
        if (!res.Ok)
        {
            lblStatus.ForeColor = Theme.Red;
            lblStatus.Text = res.Error;
            return;
        }

        sessionId = res.SessionId;
        checkoutUrl = res.CheckoutUrl;
        btnOpenBrowser.Enabled = true;
        btnConfirm.Enabled = true;

        if (checkoutUrl == "local://sandbox-checkout")
        {
            lblStatus.ForeColor = Theme.Navy;
            lblStatus.Text = "Sandbox mode (no sk_test_ key set).\nClick 'I've completed payment' to simulate Hosted Checkout approval.";
            return;
        }

        lblStatus.ForeColor = Theme.Navy;
        lblStatus.Text = "Xendit Hosted Checkout opened in your browser.\nComplete GCash, Maya, or Card payment there — this window updates automatically.";
        LaunchUrl();
        pollTimer.Start();
    }

    void LaunchUrl()
    {
        if (string.IsNullOrWhiteSpace(checkoutUrl) || checkoutUrl.StartsWith("local://")) return;
        Process.Start(new ProcessStartInfo(checkoutUrl) { UseShellExecute = true });
    }

    async Task CheckNowAsync(bool manual)
    {
        if (checking || string.IsNullOrEmpty(sessionId)) return;
        checking = true;
        if (manual) btnConfirm.Text = "Checking…";

        var status = await Pay.Gateway.VerifyCheckoutSessionAsync(sessionId);
        checking = false;
        if (manual) btnConfirm.Text = "I've completed payment";

        if (status.Paid)
        {
            pollTimer.Stop();
            Method = status.Method;
            Reference = status.PaymentId;
            DialogResult = DialogResult.OK;
        }
        else if (manual)
        {
            Ui.Info("Payment is not marked as paid yet.\nFinish the checkout in your browser, then click again.");
        }
    }
}
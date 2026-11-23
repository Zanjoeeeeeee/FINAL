namespace DriveMate;

public class DisputeDialog : ThemedForm
{
    public string Reason => txtReason.Text.Trim();
    public List<PhotoItem> Photos { get; } = new();
    public decimal FinalDeduction => nudDeduction.Value;

    readonly TextBox txtReason = new();
    readonly Label lblCount = new();
    readonly NumericUpDown nudDeduction = new();

    public DisputeDialog(Booking b, bool isAdminResolve)
    {
        Text = isAdminResolve ? $"Resolve Damage Dispute – {b.CarName}" : $"Dispute Deposit Deductions – {b.CarName}";
        ClientSize = new Size(440, isAdminResolve ? 340 : 310);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        var lblInfo = new Label
        {
            Location = new Point(20, 16),
            Size = new Size(400, 48),
            Text = isAdminResolve
                ? $"Dispute reason: {b.DisputeReason}\nProposed deductions: ₱{b.Deductions:N0} out of ₱{b.Deposit:N0} deposit."
                : $"Proposed deductions: ₱{b.Deductions:N0} from your ₱{b.Deposit:N0} deposit.\nExplain your dispute and attach supporting photo evidence for admin review:"
        };

        txtReason.Location = new Point(20, 72); txtReason.Multiline = true; txtReason.Size = new Size(400, 90);

        var btnAdd = new Button { Location = new Point(20, 174), Size = new Size(140, 32), Text = "Attach photos…" };
        btnAdd.Click += (s, e) =>
        {
            var picked = Ui.PickPhotos(this, 6 - Photos.Count);
            Photos.AddRange(picked);
            lblCount.Text = $"{Photos.Count} evidence photo(s) attached";
        };
        lblCount.Location = new Point(170, 180); lblCount.AutoSize = true; lblCount.Text = "0 evidence photo(s) attached";

        var lblDed = new Label { Location = new Point(20, 218), AutoSize = true, Text = "Approved deduction (₱):" };
        nudDeduction.Location = new Point(190, 214); nudDeduction.Size = new Size(230, 28);
        nudDeduction.Maximum = b.Deposit; nudDeduction.Value = Math.Min(b.Deductions, b.Deposit);

        var btnOk = new Button { Location = new Point(200, isAdminResolve ? 270 : 235), Size = new Size(125, 36), Text = isAdminResolve ? "Settle dispute" : "Submit dispute", Tag = "accent" };
        btnOk.Click += (s, e) =>
        {
            if (Reason.Length < 5) { Ui.Info("Please enter clear details (at least 5 characters)."); return; }
            DialogResult = DialogResult.OK;
        };

        var btnCancel = new Button { DialogResult = DialogResult.Cancel, Location = new Point(335, isAdminResolve ? 270 : 235), Size = new Size(85, 36), Text = "Cancel" };

        Controls.AddRange(new Control[] { lblInfo, txtReason, btnOk, btnCancel });
        if (isAdminResolve)
            Controls.AddRange(new Control[] { lblDed, nudDeduction });
        else
            Controls.AddRange(new Control[] { btnAdd, lblCount });
    }
}
namespace DriveMate;

public partial class ReasonDialog : ThemedForm
{
    public string Reason => txtReason.Text.Trim();

    public ReasonDialog() => InitializeComponent();

    private void btnOk_Click(object? sender, EventArgs e)
    {
        if (Reason.Length < 3) { Ui.Info("Please enter a short reason so the user knows what to fix."); return; }
        DialogResult = DialogResult.OK;
    }
}

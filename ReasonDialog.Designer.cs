#nullable enable
namespace DriveMate;

partial class ReasonDialog
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblPrompt = new Label(); txtReason = new TextBox(); btnOk = new Button(); btnCancel = new Button();
        SuspendLayout();
        lblPrompt.Location = new Point(20, 16); lblPrompt.Size = new Size(360, 40);
        lblPrompt.Text = "Reason shown to the user (for example: ID photo is blurry or doesn't match the selfie):";
        txtReason.Location = new Point(20, 62); txtReason.Multiline = true; txtReason.Size = new Size(360, 76);
        btnOk.Tag = "danger"; btnOk.Location = new Point(150, 156); btnOk.Size = new Size(110, 36); btnOk.Text = "Reject"; btnOk.Click += btnOk_Click;
        btnCancel.DialogResult = DialogResult.Cancel; btnCancel.Location = new Point(270, 156); btnCancel.Size = new Size(110, 36); btnCancel.Text = "Cancel";
        AcceptButton = btnOk; CancelButton = btnCancel;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 210);
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent; Text = "Reject verification";
        Controls.AddRange(new Control[] { lblPrompt, txtReason, btnOk, btnCancel });
        ResumeLayout(false); PerformLayout();
    }

    private Label lblPrompt;
    private TextBox txtReason;
    private Button btnOk, btnCancel;
}

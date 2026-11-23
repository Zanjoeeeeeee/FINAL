#nullable enable
namespace DriveMate;

partial class ReviewDialog
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblRating = new Label(); lblComment = new Label(); cboStars = new ComboBox(); txtComment = new TextBox();
        btnSubmit = new Button(); btnCancel = new Button();
        SuspendLayout();
        lblRating.AutoSize = true; lblRating.Location = new Point(20, 24); lblRating.Text = "Rating";
        cboStars.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStars.Items.AddRange(new object[] { "5 ★★★★★", "4 ★★★★", "3 ★★★", "2 ★★", "1 ★" });
        cboStars.Location = new Point(110, 20); cboStars.Size = new Size(230, 28);
        lblComment.AutoSize = true; lblComment.Location = new Point(20, 66); lblComment.Text = "Comment";
        txtComment.Location = new Point(110, 62); txtComment.Multiline = true; txtComment.Size = new Size(230, 80);
        btnSubmit.Location = new Point(150, 165); btnSubmit.Size = new Size(95, 34); btnSubmit.Text = "Submit";
        btnSubmit.Click += btnSubmit_Click;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(255, 165); btnCancel.Size = new Size(85, 34); btnCancel.Text = "Cancel";
        AcceptButton = btnSubmit; CancelButton = btnCancel;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(365, 215);
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent; Text = "Rate your trip";
        Controls.AddRange(new Control[] { lblRating, cboStars, lblComment, txtComment, btnSubmit, btnCancel });
        ResumeLayout(false); PerformLayout();
    }

    private Label lblRating, lblComment;
    private ComboBox cboStars;
    private TextBox txtComment;
    private Button btnSubmit, btnCancel;
}

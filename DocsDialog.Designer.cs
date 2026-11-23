#nullable enable
namespace DriveMate;

partial class DocsDialog
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblType = new Label(); cboIdType = new ComboBox(); lblHint = new Label();
        lblId = new Label(); lblSelfie = new Label();
        picId = new PictureBox(); picSelfie = new PictureBox();
        btnId = new Button(); btnSelfie = new Button();
        btnSubmit = new Button(); btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)picId).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picSelfie).BeginInit();
        SuspendLayout();
        lblType.AutoSize = true; lblType.Location = new Point(20, 20); lblType.Text = "ID type";
        cboIdType.DropDownStyle = ComboBoxStyle.DropDownList; cboIdType.Location = new Point(110, 16); cboIdType.Size = new Size(330, 28);
        lblHint.Location = new Point(20, 54); lblHint.Size = new Size(420, 50); lblHint.Text = "";
        lblId.AutoSize = true; lblId.Location = new Point(20, 112); lblId.Text = "ID photo";
        lblSelfie.AutoSize = true; lblSelfie.Location = new Point(240, 112); lblSelfie.Text = "Selfie holding ID";
        picId.BorderStyle = BorderStyle.FixedSingle; picId.SizeMode = PictureBoxSizeMode.Zoom;
        picId.Location = new Point(20, 136); picId.Size = new Size(200, 140);
        picSelfie.BorderStyle = BorderStyle.FixedSingle; picSelfie.SizeMode = PictureBoxSizeMode.Zoom;
        picSelfie.Location = new Point(240, 136); picSelfie.Size = new Size(200, 140);
        btnId.Tag = "secondary"; btnId.Location = new Point(20, 284); btnId.Size = new Size(200, 32); btnId.Text = "Choose ID photo…"; btnId.Click += btnId_Click;
        btnSelfie.Tag = "secondary"; btnSelfie.Location = new Point(240, 284); btnSelfie.Size = new Size(200, 32); btnSelfie.Text = "Choose selfie…"; btnSelfie.Click += btnSelfie_Click;
        btnSubmit.Location = new Point(210, 336); btnSubmit.Size = new Size(110, 36); btnSubmit.Text = "Submit"; btnSubmit.Click += btnSubmit_Click;
        btnCancel.DialogResult = DialogResult.Cancel; btnCancel.Location = new Point(330, 336); btnCancel.Size = new Size(110, 36); btnCancel.Text = "Cancel";
        AcceptButton = btnSubmit; CancelButton = btnCancel;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(460, 392);
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent; Text = "Verify your identity";
        Controls.AddRange(new Control[] { lblType, cboIdType, lblHint, lblId, lblSelfie, picId, picSelfie, btnId, btnSelfie, btnSubmit, btnCancel });
        ((System.ComponentModel.ISupportInitialize)picId).EndInit();
        ((System.ComponentModel.ISupportInitialize)picSelfie).EndInit();
        ResumeLayout(false); PerformLayout();
    }

    private Label lblType, lblHint, lblId, lblSelfie;
    private ComboBox cboIdType;
    private PictureBox picId, picSelfie;
    private Button btnId, btnSelfie, btnSubmit, btnCancel;
}

#nullable enable
namespace DriveMate;

partial class BookDialog
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblCar = new Label(); picCar = new PictureBox(); btnPrev = new Button(); btnNext = new Button(); lblPhotoIdx = new Label();
        lblStart = new Label(); lblEnd = new Label(); lblInsurance = new Label(); lblSummaryCaption = new Label(); lblSummary = new Label(); lblPolicy = new Label();
        dtpStart = new DateTimePicker(); dtpEnd = new DateTimePicker(); cboInsurance = new ComboBox(); btnSend = new Button(); btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)picCar).BeginInit();
        SuspendLayout();

        lblCar.Font = new Font("Segoe UI", 11F, FontStyle.Bold); lblCar.Location = new Point(20, 14); lblCar.Size = new Size(370, 26);

        picCar.BackColor = Theme.Navy; picCar.Location = new Point(20, 44); picCar.Size = new Size(370, 198); picCar.SizeMode = PictureBoxSizeMode.Zoom;
        btnPrev.Location = new Point(20, 248); btnPrev.Size = new Size(38, 28); btnPrev.Text = "◀"; btnPrev.Tag = "secondary"; btnPrev.Click += btnPrev_Click;
        btnNext.Location = new Point(352, 248); btnNext.Size = new Size(38, 28); btnNext.Text = "▶"; btnNext.Tag = "secondary"; btnNext.Click += btnNext_Click;
        lblPhotoIdx.ForeColor = Theme.Muted; lblPhotoIdx.Location = new Point(66, 248); lblPhotoIdx.Size = new Size(278, 28); lblPhotoIdx.TextAlign = ContentAlignment.MiddleCenter;

        lblStart.AutoSize = true; lblStart.Location = new Point(20, 294); lblStart.Text = "Pick-up date";
        dtpStart.Format = DateTimePickerFormat.Short; dtpStart.Location = new Point(150, 290); dtpStart.Size = new Size(240, 27);
        dtpStart.ValueChanged += dtp_ValueChanged;

        lblEnd.AutoSize = true; lblEnd.Location = new Point(20, 332); lblEnd.Text = "Return date";
        dtpEnd.Format = DateTimePickerFormat.Short; dtpEnd.Location = new Point(150, 328); dtpEnd.Size = new Size(240, 27);
        dtpEnd.ValueChanged += dtp_ValueChanged;

        lblInsurance.AutoSize = true; lblInsurance.Location = new Point(20, 372); lblInsurance.Text = "Insurance";
        cboInsurance.DropDownStyle = ComboBoxStyle.DropDownList; cboInsurance.Location = new Point(150, 368); cboInsurance.Size = new Size(240, 28);
        cboInsurance.SelectedIndexChanged += dtp_ValueChanged;

        lblSummaryCaption.AutoSize = true; lblSummaryCaption.Location = new Point(20, 410); lblSummaryCaption.Text = "Price breakdown";
        lblSummary.Location = new Point(150, 410); lblSummary.Size = new Size(245, 76);

        lblPolicy.ForeColor = Theme.Muted; lblPolicy.Font = new Font("Segoe UI", 8F);
        lblPolicy.Location = new Point(20, 490); lblPolicy.Size = new Size(370, 32);
        lblPolicy.Text = "Cancellation policy: Free 100% refund up to 48h before pick-up; 50% fare refund 24–48h prior.";

        btnSend.Location = new Point(150, 530); btnSend.Size = new Size(130, 36); btnSend.Text = "Send request"; btnSend.Click += btnSend_Click;
        btnCancel.DialogResult = DialogResult.Cancel; btnCancel.Location = new Point(290, 530); btnCancel.Size = new Size(100, 36); btnCancel.Text = "Cancel";

        AcceptButton = btnSend; CancelButton = btnCancel;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(410, 582);
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent; Text = "Request booking";
        Controls.AddRange(new Control[] { lblCar, picCar, btnPrev, btnNext, lblPhotoIdx, lblStart, dtpStart, lblEnd, dtpEnd,
            lblInsurance, cboInsurance, lblSummaryCaption, lblSummary, lblPolicy, btnSend, btnCancel });
        ((System.ComponentModel.ISupportInitialize)picCar).EndInit();
        ResumeLayout(false); PerformLayout();
    }

    private Label lblCar, lblPhotoIdx, lblStart, lblEnd, lblInsurance, lblSummaryCaption, lblSummary, lblPolicy;
    private PictureBox picCar;
    private Button btnPrev, btnNext, btnSend, btnCancel;
    private DateTimePicker dtpStart, dtpEnd;
    private ComboBox cboInsurance;
}
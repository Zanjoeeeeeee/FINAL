#nullable enable
namespace DriveMate;

partial class InspectionDialog
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblHint = new Label(); lblOdo = new Label(); lblFuel = new Label(); lblNotes = new Label(); lblPhotos = new Label(); lblPhotoCount = new Label();
        nudOdo = new NumericUpDown(); nudFuel = new NumericUpDown();
        chkScratch = new CheckBox(); chkDirty = new CheckBox(); chkTire = new CheckBox();
        txtNotes = new TextBox(); btnAddPhotos = new Button(); btnClearPhotos = new Button();
        btnSave = new Button(); btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)nudOdo).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudFuel).BeginInit();
        SuspendLayout();
        lblHint.Location = new Point(20, 15); lblHint.Size = new Size(380, 45);
        lblOdo.AutoSize = true; lblOdo.Location = new Point(20, 76); lblOdo.Text = "Odometer (km)";
        nudOdo.Location = new Point(170, 72); nudOdo.Size = new Size(230, 27); nudOdo.Maximum = 999999;
        lblFuel.AutoSize = true; lblFuel.Location = new Point(20, 114); lblFuel.Text = "Fuel level (%)";
        nudFuel.Location = new Point(170, 110); nudFuel.Size = new Size(230, 27);
        nudFuel.Maximum = 100; nudFuel.Increment = 5; nudFuel.Value = 100;
        chkScratch.AutoSize = true; chkScratch.Location = new Point(170, 150); chkScratch.Text = "Scratches / dents present";
        chkDirty.AutoSize = true; chkDirty.Location = new Point(170, 178); chkDirty.Text = "Interior needs cleaning";
        chkTire.AutoSize = true; chkTire.Location = new Point(170, 206); chkTire.Text = "Tire / light issue";
        lblNotes.AutoSize = true; lblNotes.Location = new Point(20, 244); lblNotes.Text = "Notes";
        txtNotes.Location = new Point(170, 240); txtNotes.Multiline = true; txtNotes.Size = new Size(230, 70);
        lblPhotos.AutoSize = true; lblPhotos.Location = new Point(20, 328); lblPhotos.Text = "Photos";
        btnAddPhotos.Location = new Point(170, 322); btnAddPhotos.Size = new Size(120, 30); btnAddPhotos.Text = "Add photos…";
        btnAddPhotos.Click += btnAddPhotos_Click;
        btnClearPhotos.Location = new Point(300, 322); btnClearPhotos.Size = new Size(100, 30); btnClearPhotos.Text = "Clear";
        btnClearPhotos.Click += btnClearPhotos_Click;
        lblPhotoCount.AutoSize = true; lblPhotoCount.Location = new Point(170, 358); lblPhotoCount.Text = "0 photo(s) attached (max 6, 3 MB each)";
        btnSave.Location = new Point(170, 395); btnSave.Size = new Size(130, 34); btnSave.Text = "Save inspection";
        btnSave.Click += btnSave_Click;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(310, 395); btnCancel.Size = new Size(90, 34); btnCancel.Text = "Cancel";
        AcceptButton = btnSave; CancelButton = btnCancel;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(425, 450);
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent; Text = "Inspection";
        Controls.AddRange(new Control[] { lblHint, lblOdo, nudOdo, lblFuel, nudFuel, chkScratch, chkDirty, chkTire, lblNotes, txtNotes,
            lblPhotos, btnAddPhotos, btnClearPhotos, lblPhotoCount, btnSave, btnCancel });
        ((System.ComponentModel.ISupportInitialize)nudOdo).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudFuel).EndInit();
        ResumeLayout(false); PerformLayout();
    }

    private Label lblHint, lblOdo, lblFuel, lblNotes, lblPhotos, lblPhotoCount;
    private NumericUpDown nudOdo, nudFuel;
    private CheckBox chkScratch, chkDirty, chkTire;
    private TextBox txtNotes;
    private Button btnAddPhotos, btnClearPhotos, btnSave, btnCancel;
}

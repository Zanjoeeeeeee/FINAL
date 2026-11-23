#nullable enable
namespace DriveMate;

partial class CarDialog
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblMake = new Label(); lblModel = new Label(); lblYear = new Label(); lblPlate = new Label(); lblTrans = new Label();
        lblSeats = new Label(); lblLoc = new Label(); lblPrice = new Label(); lblDeposit = new Label();
        txtMake = new TextBox(); txtModel = new TextBox(); nudYear = new NumericUpDown(); txtPlate = new TextBox();
        cboTrans = new ComboBox(); nudSeats = new NumericUpDown(); cboLoc = new ComboBox();
        nudPrice = new NumericUpDown(); nudDeposit = new NumericUpDown();
        btnSave = new Button(); btnCancel = new Button();
        lblCategory = new Label(); cboCategory = new ComboBox(); lblPhotos = new Label(); lblPhotoCount = new Label(); btnAddPhotos = new Button(); btnClearPhotos = new Button();
        ((System.ComponentModel.ISupportInitialize)nudYear).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSeats).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudDeposit).BeginInit();
        SuspendLayout();
        // labels
        lblMake.AutoSize = true; lblMake.Location = new Point(20, 24); lblMake.Text = "Make";
        lblModel.AutoSize = true; lblModel.Location = new Point(20, 62); lblModel.Text = "Model";
        lblYear.AutoSize = true; lblYear.Location = new Point(20, 100); lblYear.Text = "Year";
        lblPlate.AutoSize = true; lblPlate.Location = new Point(20, 138); lblPlate.Text = "Plate no.";
        lblTrans.AutoSize = true; lblTrans.Location = new Point(20, 176); lblTrans.Text = "Transmission";
        lblSeats.AutoSize = true; lblSeats.Location = new Point(20, 214); lblSeats.Text = "Seats";
        lblLoc.AutoSize = true; lblLoc.Location = new Point(20, 252); lblLoc.Text = "Pick-up city";
        lblPrice.AutoSize = true; lblPrice.Location = new Point(20, 290); lblPrice.Text = "Price per day (₱)";
        lblDeposit.AutoSize = true; lblDeposit.Location = new Point(20, 328); lblDeposit.Text = "Security deposit (₱)";
        // inputs
        txtMake.Location = new Point(180, 20); txtMake.Size = new Size(200, 27);
        txtModel.Location = new Point(180, 58); txtModel.Size = new Size(200, 27);
        nudYear.Location = new Point(180, 96); nudYear.Size = new Size(200, 27);
        nudYear.Minimum = 1990; nudYear.Maximum = 2100; nudYear.Value = 2022;
        txtPlate.Location = new Point(180, 134); txtPlate.Size = new Size(200, 27);
        cboTrans.DropDownStyle = ComboBoxStyle.DropDownList; cboTrans.Items.AddRange(new object[] { "Automatic", "Manual" });
        cboTrans.Location = new Point(180, 172); cboTrans.Size = new Size(200, 28);
        nudSeats.Location = new Point(180, 210); nudSeats.Size = new Size(200, 27);
        nudSeats.Minimum = 2; nudSeats.Maximum = 15; nudSeats.Value = 5;
        cboLoc.Location = new Point(180, 248); cboLoc.Size = new Size(200, 28);
        nudPrice.Location = new Point(180, 286); nudPrice.Size = new Size(200, 27);
        nudPrice.Minimum = 500; nudPrice.Maximum = 100000; nudPrice.Increment = 100; nudPrice.Value = 1800;
        nudDeposit.Location = new Point(180, 324); nudDeposit.Size = new Size(200, 27);
        nudDeposit.Minimum = 0; nudDeposit.Maximum = 200000; nudDeposit.Increment = 500; nudDeposit.Value = 3000;
        // photos
        lblPhotos.AutoSize = true; lblCategory.AutoSize = true; lblCategory.Location = new Point(20, 372); lblCategory.Text = "Category";
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList; cboCategory.Location = new Point(180, 368); cboCategory.Size = new Size(200, 28);
        lblPhotos.Location = new Point(20, 410); lblPhotos.Text = "Car photos";
        btnAddPhotos.Location = new Point(180, 404); btnAddPhotos.Size = new Size(110, 30); btnAddPhotos.Text = "Add photos…";
        btnAddPhotos.Click += btnAddPhotos_Click;
        btnClearPhotos.Location = new Point(300, 404); btnClearPhotos.Size = new Size(80, 30); btnClearPhotos.Text = "Clear";
        btnClearPhotos.Click += btnClearPhotos_Click;
        lblPhotoCount.AutoSize = true; lblPhotoCount.Location = new Point(180, 440); lblPhotoCount.Text = "0 photo(s)";
        // buttons
        btnSave.Location = new Point(180, 475); btnSave.Size = new Size(95, 34); btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(285, 475); btnCancel.Size = new Size(95, 34); btnCancel.Text = "Cancel";
        // form
        AcceptButton = btnSave; CancelButton = btnCancel;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(410, 530);
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent; Text = "List a car";
        Controls.AddRange(new Control[] { lblMake, lblModel, lblYear, lblPlate, lblTrans, lblSeats, lblLoc, lblPrice, lblDeposit,
            txtMake, txtModel, nudYear, txtPlate, cboTrans, nudSeats, cboLoc, nudPrice, nudDeposit, lblCategory, cboCategory, lblPhotos, btnAddPhotos, btnClearPhotos, lblPhotoCount, btnSave, btnCancel });
        ((System.ComponentModel.ISupportInitialize)nudYear).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSeats).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudDeposit).EndInit();
        ResumeLayout(false); PerformLayout();
    }

    private Label lblMake, lblModel, lblYear, lblPlate, lblTrans, lblSeats, lblLoc, lblPrice, lblDeposit;
    private TextBox txtMake, txtModel, txtPlate;
    private NumericUpDown nudYear, nudSeats, nudPrice, nudDeposit;
    private ComboBox cboTrans, cboLoc, cboCategory;
    private Label lblCategory, lblPhotos, lblPhotoCount;
    private Button btnSave, btnCancel, btnAddPhotos, btnClearPhotos;
}

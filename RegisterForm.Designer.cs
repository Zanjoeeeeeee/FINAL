#nullable enable
namespace DriveMate;

partial class RegisterForm
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlCard = new RoundPanel();
        picLogo = new PictureBox();
        lblPhone = new Label();
        lblEmail = new Label();
        lblHead = new Label();
        lblName = new Label();
        pnlName = new FieldPanel();
        txtName = new TextBox();
        pnlEmail = new FieldPanel();
        txtEmail = new TextBox();
        pnlPhone = new FieldPanel();
        txtPhone = new TextBox();
        lblPass = new Label();
        pnlPass = new FieldPanel();
        txtPass = new TextBox();
        lblConf = new Label();
        pnlConf = new FieldPanel();
        txtConf = new TextBox();
        lblRole = new Label();
        cboRole = new FlatCombo();
        grpVerify = new LegendPanel();
        lblVerifyInfo = new Label();
        btnDocs = new Button();
        lblDocs = new Label();
        btnCreate = new Button();
        btnCancel = new Button();
        pnlCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
        pnlName.SuspendLayout();
        pnlEmail.SuspendLayout();
        pnlPhone.SuspendLayout();
        pnlPass.SuspendLayout();
        pnlConf.SuspendLayout();
        grpVerify.SuspendLayout();
        SuspendLayout();
        // 
        // pnlCard
        // 
        pnlCard.BackColor = Color.Transparent;
        pnlCard.Controls.Add(picLogo);
        pnlCard.Controls.Add(lblPhone);
        pnlCard.Controls.Add(lblEmail);
        pnlCard.Controls.Add(lblHead);
        pnlCard.Controls.Add(lblName);
        pnlCard.Controls.Add(pnlName);
        pnlCard.Controls.Add(pnlEmail);
        pnlCard.Controls.Add(pnlPhone);
        pnlCard.Controls.Add(lblPass);
        pnlCard.Controls.Add(pnlPass);
        pnlCard.Controls.Add(lblConf);
        pnlCard.Controls.Add(pnlConf);
        pnlCard.Controls.Add(lblRole);
        pnlCard.Controls.Add(cboRole);
        pnlCard.Controls.Add(grpVerify);
        pnlCard.Controls.Add(btnCreate);
        pnlCard.Controls.Add(btnCancel);
        pnlCard.FillColor = Color.FromArgb(243, 244, 247);
        pnlCard.Location = new Point(688, 151);
        pnlCard.Margin = new Padding(3, 4, 3, 4);
        pnlCard.Name = "pnlCard";
        pnlCard.Radius = 28;
        pnlCard.Size = new Size(549, 755);
        pnlCard.TabIndex = 0;
        pnlCard.Paint += pnlCard_Paint;
        // 
        // picLogo
        // 
        picLogo.BackColor = Color.FromArgb(243, 244, 247);
        picLogo.Location = new Point(96, 18);
        picLogo.Margin = new Padding(3, 4, 3, 4);
        picLogo.Name = "picLogo";
        picLogo.Size = new Size(370, 85);
        picLogo.SizeMode = PictureBoxSizeMode.Zoom;
        picLogo.TabIndex = 0;
        picLogo.TabStop = false;
        // 
        // lblPhone
        // 
        lblPhone.AutoSize = true;
        lblPhone.BackColor = Color.FromArgb(243, 244, 247);
        lblPhone.Font = new Font("Segoe UI", 9F);
        lblPhone.ForeColor = Color.FromArgb(16, 0, 1);
        lblPhone.Location = new Point(41, 321);
        lblPhone.Name = "lblPhone";
        lblPhone.Size = new Size(111, 20);
        lblPhone.TabIndex = 6;
        lblPhone.Text = "Mobile number";
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.BackColor = Color.FromArgb(243, 244, 247);
        lblEmail.Font = new Font("Segoe UI", 9F);
        lblEmail.ForeColor = Color.FromArgb(16, 0, 1);
        lblEmail.Location = new Point(41, 246);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(46, 20);
        lblEmail.TabIndex = 4;
        lblEmail.Text = "Email";
        // 
        // lblHead
        // 
        lblHead.BackColor = Color.FromArgb(243, 244, 247);
        lblHead.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblHead.ForeColor = Color.FromArgb(217, 47, 52);
        lblHead.Location = new Point(96, 107);
        lblHead.Name = "lblHead";
        lblHead.Size = new Size(370, 40);
        lblHead.TabIndex = 1;
        lblHead.Text = "Create your account?";
        lblHead.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.BackColor = Color.FromArgb(243, 244, 247);
        lblName.Font = new Font("Segoe UI", 9F);
        lblName.ForeColor = Color.FromArgb(16, 0, 1);
        lblName.Location = new Point(37, 163);
        lblName.Name = "lblName";
        lblName.Size = new Size(73, 20);
        lblName.TabIndex = 2;
        lblName.Text = "Full name";
        // 
        // pnlName
        // 
        pnlName.BackColor = Color.White;
        pnlName.BorderColor = Color.FromArgb(223, 93, 97);
        pnlName.Controls.Add(txtName);
        pnlName.FocusBorderColor = Color.FromArgb(217, 47, 52);
        pnlName.Location = new Point(41, 187);
        pnlName.Margin = new Padding(3, 4, 3, 4);
        pnlName.Name = "pnlName";
        pnlName.Padding = new Padding(9, 8, 9, 5);
        pnlName.Size = new Size(468, 40);
        pnlName.TabIndex = 3;
        // 
        // txtName
        // 
        txtName.BackColor = Color.White;
        txtName.BorderStyle = BorderStyle.None;
        txtName.Dock = DockStyle.Fill;
        txtName.Font = new Font("Segoe UI", 10F);
        txtName.ForeColor = Color.FromArgb(16, 0, 1);
        txtName.Location = new Point(9, 8);
        txtName.Margin = new Padding(3, 4, 3, 4);
        txtName.Name = "txtName";
        txtName.Size = new Size(450, 23);
        txtName.TabIndex = 0;
        // 
        // pnlEmail
        // 
        pnlEmail.BackColor = Color.White;
        pnlEmail.BorderColor = Color.FromArgb(223, 93, 97);
        pnlEmail.Controls.Add(txtEmail);
        pnlEmail.FocusBorderColor = Color.FromArgb(217, 47, 52);
        pnlEmail.Location = new Point(41, 270);
        pnlEmail.Margin = new Padding(3, 4, 3, 4);
        pnlEmail.Name = "pnlEmail";
        pnlEmail.Padding = new Padding(9, 8, 9, 5);
        pnlEmail.Size = new Size(468, 40);
        pnlEmail.TabIndex = 5;
        // 
        // txtEmail
        // 
        txtEmail.BackColor = Color.White;
        txtEmail.BorderStyle = BorderStyle.None;
        txtEmail.Dock = DockStyle.Fill;
        txtEmail.Font = new Font("Segoe UI", 10F);
        txtEmail.ForeColor = Color.FromArgb(16, 0, 1);
        txtEmail.Location = new Point(9, 8);
        txtEmail.Margin = new Padding(3, 4, 3, 4);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(450, 23);
        txtEmail.TabIndex = 1;
        // 
        // pnlPhone
        // 
        pnlPhone.BackColor = Color.White;
        pnlPhone.BorderColor = Color.FromArgb(223, 93, 97);
        pnlPhone.Controls.Add(txtPhone);
        pnlPhone.FocusBorderColor = Color.FromArgb(217, 47, 52);
        pnlPhone.Location = new Point(41, 345);
        pnlPhone.Margin = new Padding(3, 4, 3, 4);
        pnlPhone.Name = "pnlPhone";
        pnlPhone.Padding = new Padding(9, 8, 9, 5);
        pnlPhone.Size = new Size(468, 40);
        pnlPhone.TabIndex = 7;
        // 
        // txtPhone
        // 
        txtPhone.BackColor = Color.White;
        txtPhone.BorderStyle = BorderStyle.None;
        txtPhone.Dock = DockStyle.Fill;
        txtPhone.Font = new Font("Segoe UI", 10F);
        txtPhone.ForeColor = Color.FromArgb(16, 0, 1);
        txtPhone.Location = new Point(9, 8);
        txtPhone.Margin = new Padding(3, 4, 3, 4);
        txtPhone.MaxLength = 20;
        txtPhone.Name = "txtPhone";
        txtPhone.PlaceholderText = "e.g. 0917 123 4567";
        txtPhone.Size = new Size(450, 23);
        txtPhone.TabIndex = 2;
        // 
        // lblPass
        // 
        lblPass.AutoSize = true;
        lblPass.BackColor = Color.FromArgb(243, 244, 247);
        lblPass.Font = new Font("Segoe UI", 9F);
        lblPass.ForeColor = Color.FromArgb(16, 0, 1);
        lblPass.Location = new Point(41, 403);
        lblPass.Name = "lblPass";
        lblPass.Size = new Size(195, 20);
        lblPass.TabIndex = 8;
        lblPass.Text = "Password (min. 6 characters)";
        // 
        // pnlPass
        // 
        pnlPass.BackColor = Color.White;
        pnlPass.BorderColor = Color.FromArgb(223, 93, 97);
        pnlPass.Controls.Add(txtPass);
        pnlPass.FocusBorderColor = Color.FromArgb(217, 47, 52);
        pnlPass.Location = new Point(41, 427);
        pnlPass.Margin = new Padding(3, 4, 3, 4);
        pnlPass.Name = "pnlPass";
        pnlPass.Padding = new Padding(9, 8, 9, 5);
        pnlPass.Size = new Size(239, 40);
        pnlPass.TabIndex = 9;
        // 
        // txtPass
        // 
        txtPass.BackColor = Color.White;
        txtPass.BorderStyle = BorderStyle.None;
        txtPass.Dock = DockStyle.Fill;
        txtPass.Font = new Font("Segoe UI", 10F);
        txtPass.ForeColor = Color.FromArgb(16, 0, 1);
        txtPass.Location = new Point(9, 8);
        txtPass.Margin = new Padding(3, 4, 3, 4);
        txtPass.Name = "txtPass";
        txtPass.Size = new Size(221, 23);
        txtPass.TabIndex = 3;
        txtPass.UseSystemPasswordChar = true;
        // 
        // lblConf
        // 
        lblConf.AutoSize = true;
        lblConf.BackColor = Color.FromArgb(243, 244, 247);
        lblConf.Font = new Font("Segoe UI", 9F);
        lblConf.ForeColor = Color.FromArgb(16, 0, 1);
        lblConf.Location = new Point(282, 403);
        lblConf.Name = "lblConf";
        lblConf.Size = new Size(129, 20);
        lblConf.TabIndex = 10;
        lblConf.Text = "Confirm password";
        // 
        // pnlConf
        // 
        pnlConf.BackColor = Color.White;
        pnlConf.BorderColor = Color.FromArgb(223, 93, 97);
        pnlConf.Controls.Add(txtConf);
        pnlConf.FocusBorderColor = Color.FromArgb(217, 47, 52);
        pnlConf.Location = new Point(286, 427);
        pnlConf.Margin = new Padding(3, 4, 3, 4);
        pnlConf.Name = "pnlConf";
        pnlConf.Padding = new Padding(9, 8, 9, 5);
        pnlConf.Size = new Size(223, 40);
        pnlConf.TabIndex = 11;
        // 
        // txtConf
        // 
        txtConf.BackColor = Color.White;
        txtConf.BorderStyle = BorderStyle.None;
        txtConf.Dock = DockStyle.Fill;
        txtConf.Font = new Font("Segoe UI", 10F);
        txtConf.ForeColor = Color.FromArgb(16, 0, 1);
        txtConf.Location = new Point(9, 8);
        txtConf.Margin = new Padding(3, 4, 3, 4);
        txtConf.Name = "txtConf";
        txtConf.Size = new Size(205, 23);
        txtConf.TabIndex = 4;
        txtConf.UseSystemPasswordChar = true;
        // 
        // lblRole
        // 
        lblRole.AutoSize = true;
        lblRole.BackColor = Color.FromArgb(243, 244, 247);
        lblRole.Font = new Font("Segoe UI", 9F);
        lblRole.ForeColor = Color.FromArgb(16, 0, 1);
        lblRole.Location = new Point(41, 471);
        lblRole.Name = "lblRole";
        lblRole.Size = new Size(78, 20);
        lblRole.TabIndex = 12;
        lblRole.Text = "I want to…";
        // 
        // cboRole
        // 
        cboRole.BackColor = Color.White;
        cboRole.DrawMode = DrawMode.OwnerDrawFixed;
        cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRole.FlatStyle = FlatStyle.Flat;
        cboRole.Font = new Font("Segoe UI", 10F);
        cboRole.ForeColor = Color.FromArgb(16, 0, 1);
        cboRole.ItemHeight = 24;
        cboRole.Items.AddRange(new object[] { "Rent cars (Renter)", "List my car and earn (Host)" });
        cboRole.Location = new Point(41, 495);
        cboRole.Margin = new Padding(3, 4, 3, 4);
        cboRole.Name = "cboRole";
        cboRole.Size = new Size(468, 30);
        cboRole.TabIndex = 5;
        cboRole.SelectedIndexChanged += cboRole_SelectedIndexChanged;
        // 
        // grpVerify
        // 
        grpVerify.BackColor = Color.FromArgb(243, 244, 247);
        grpVerify.BorderColor = Color.FromArgb(223, 93, 97);
        grpVerify.Controls.Add(lblVerifyInfo);
        grpVerify.Controls.Add(btnDocs);
        grpVerify.Controls.Add(lblDocs);
        grpVerify.ForeColor = Color.FromArgb(16, 0, 1);
        grpVerify.LegendFont = new Font("Segoe UI", 9F);
        grpVerify.Location = new Point(41, 533);
        grpVerify.Margin = new Padding(3, 4, 3, 4);
        grpVerify.Name = "grpVerify";
        grpVerify.Size = new Size(468, 133);
        grpVerify.TabIndex = 13;
        grpVerify.Text = "Identity verification (required)";
        // 
        // lblVerifyInfo
        // 
        lblVerifyInfo.BackColor = Color.FromArgb(243, 244, 247);
        lblVerifyInfo.Font = new Font("Segoe UI", 8.25F);
        lblVerifyInfo.ForeColor = Color.FromArgb(16, 0, 1);
        lblVerifyInfo.Location = new Point(14, 29);
        lblVerifyInfo.Name = "lblVerifyInfo";
        lblVerifyInfo.Size = new Size(454, 43);
        lblVerifyInfo.TabIndex = 0;
        lblVerifyInfo.Text = "Upload a valid ID and a selfie. Our admin reviews them manually before you can book or list cars.";
        // 
        // btnDocs
        // 
        btnDocs.Location = new Point(14, 80);
        btnDocs.Margin = new Padding(3, 4, 3, 4);
        btnDocs.Name = "btnDocs";
        btnDocs.Size = new Size(158, 40);
        btnDocs.TabIndex = 6;
        btnDocs.Tag = "brand";
        btnDocs.Text = "Upload ID && Selfie";
        btnDocs.Click += btnDocs_Click;
        // 
        // lblDocs
        // 
        lblDocs.BackColor = Color.FromArgb(243, 244, 247);
        lblDocs.Font = new Font("Segoe UI", 8.5F);
        lblDocs.ForeColor = Color.FromArgb(107, 122, 153);
        lblDocs.Location = new Point(183, 80);
        lblDocs.Name = "lblDocs";
        lblDocs.Size = new Size(174, 40);
        lblDocs.TabIndex = 7;
        lblDocs.Text = "Not uploaded yet";
        lblDocs.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnCreate
        // 
        btnCreate.Location = new Point(37, 685);
        btnCreate.Margin = new Padding(3, 4, 3, 4);
        btnCreate.Name = "btnCreate";
        btnCreate.Size = new Size(243, 51);
        btnCreate.TabIndex = 7;
        btnCreate.Tag = "brand";
        btnCreate.Text = "Create account";
        btnCreate.Click += btnCreate_Click;
        // 
        // btnCancel
        // 
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(286, 685);
        btnCancel.Margin = new Padding(3, 4, 3, 4);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(223, 51);
        btnCancel.TabIndex = 8;
        btnCancel.Tag = "dark";
        btnCancel.Text = "Cancel";
        // 
        // RegisterForm
        // 
        AcceptButton = btnCreate;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(16, 0, 1);
        CancelButton = btnCancel;
        ClientSize = new Size(1902, 1055);
        Controls.Add(pnlCard);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        if (Brand.AppIcon != null) Icon = Brand.AppIcon;
        Margin = new Padding(3, 4, 3, 4);
        MaximizeBox = false;
        Name = "RegisterForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Create account";
        pnlCard.ResumeLayout(false);
        pnlCard.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
        pnlName.ResumeLayout(false);
        pnlName.PerformLayout();
        pnlEmail.ResumeLayout(false);
        pnlEmail.PerformLayout();
        pnlPhone.ResumeLayout(false);
        pnlPhone.PerformLayout();
        pnlPass.ResumeLayout(false);
        pnlPass.PerformLayout();
        pnlConf.ResumeLayout(false);
        pnlConf.PerformLayout();
        grpVerify.ResumeLayout(false);
        ResumeLayout(false);
    }

    private RoundPanel pnlCard;
    private FieldPanel pnlName, pnlEmail, pnlPhone, pnlPass, pnlConf;
    private PictureBox picLogo;
    private Label lblHead, lblName, lblEmail, lblPhone, lblPass, lblConf, lblRole, lblVerifyInfo, lblDocs;
    private TextBox txtName, txtEmail, txtPhone, txtPass, txtConf;
    private FlatCombo cboRole;
    private LegendPanel grpVerify;
    private Button btnDocs, btnCreate, btnCancel;
}
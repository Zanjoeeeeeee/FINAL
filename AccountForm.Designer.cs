#nullable enable
namespace DriveMate;

partial class AccountForm
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        grpProfile = new GroupBox(); grpPassword = new GroupBox();
        grpVerify = new GroupBox(); lblVerifyStatus = new Label(); btnDocs = new Button(); lblPhone = new Label(); txtPhone = new TextBox();
        lblName = new Label(); lblEmail = new Label(); lblRole = new Label(); lblRoleValue = new Label();
        txtName = new TextBox(); txtEmail = new TextBox(); btnSaveProfile = new Button();
        lblCur = new Label(); lblNew = new Label(); lblConf = new Label(); lblHint = new Label();
        txtCur = new TextBox(); txtNew = new TextBox(); txtConf = new TextBox(); btnChangePw = new Button();
        btnClose = new Button();
        grpProfile.SuspendLayout(); grpPassword.SuspendLayout(); grpVerify.SuspendLayout(); SuspendLayout();

        // profile group
        grpProfile.Location = new Point(12, 12); grpProfile.Size = new Size(396, 215); grpProfile.Text = "Profile";
        lblName.AutoSize = true; lblName.Location = new Point(15, 32); lblName.Text = "Full name";
        txtName.Location = new Point(140, 28); txtName.Size = new Size(240, 27);
        lblEmail.AutoSize = true; lblEmail.Location = new Point(15, 70); lblEmail.Text = "Email";
        txtEmail.Location = new Point(140, 66); txtEmail.Size = new Size(240, 27);
        lblPhone.AutoSize = true; lblPhone.Location = new Point(15, 108); lblPhone.Text = "Mobile number";
        txtPhone.Location = new Point(140, 104); txtPhone.Size = new Size(240, 27);
        lblRole.AutoSize = true; lblRole.Location = new Point(15, 146); lblRole.Text = "Account type";
        lblRoleValue.AutoSize = true; lblRoleValue.Location = new Point(140, 146);
        lblRoleValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSaveProfile.Location = new Point(140, 172); btnSaveProfile.Size = new Size(140, 30); btnSaveProfile.Text = "Save profile";
        btnSaveProfile.Click += btnSaveProfile_Click;
        grpProfile.Controls.AddRange(new Control[] { lblName, txtName, lblEmail, txtEmail, lblPhone, txtPhone, lblRole, lblRoleValue, btnSaveProfile });

        // password group
        grpPassword.Location = new Point(12, 238); grpPassword.Size = new Size(396, 205); grpPassword.Text = "Change password";
        lblCur.AutoSize = true; lblCur.Location = new Point(15, 34); lblCur.Text = "Current password";
        txtCur.Location = new Point(140, 30); txtCur.Size = new Size(240, 27); txtCur.UseSystemPasswordChar = true;
        lblNew.AutoSize = true; lblNew.Location = new Point(15, 70); lblNew.Text = "New password";
        txtNew.Location = new Point(140, 66); txtNew.Size = new Size(240, 27); txtNew.UseSystemPasswordChar = true;
        lblConf.AutoSize = true; lblConf.Location = new Point(15, 106); lblConf.Text = "Confirm new";
        txtConf.Location = new Point(140, 102); txtConf.Size = new Size(240, 27); txtConf.UseSystemPasswordChar = true;
        lblHint.AutoSize = true; lblHint.ForeColor = Color.DimGray; lblHint.Location = new Point(140, 134); lblHint.Text = "At least 6 characters";
        btnChangePw.Location = new Point(140, 160); btnChangePw.Size = new Size(140, 30); btnChangePw.Text = "Change password";
        btnChangePw.Click += btnChangePw_Click;
        grpPassword.Controls.AddRange(new Control[] { lblCur, txtCur, lblNew, txtNew, lblConf, txtConf, lblHint, btnChangePw });

        grpVerify.Location = new Point(12, 452); grpVerify.Size = new Size(396, 110); grpVerify.Text = "Identity verification";
        lblVerifyStatus.Location = new Point(15, 26); lblVerifyStatus.Size = new Size(370, 42); lblVerifyStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnDocs.Location = new Point(15, 70); btnDocs.Size = new Size(190, 30); btnDocs.Text = "Upload new documents…"; btnDocs.Tag = "accent";
        btnDocs.Click += btnDocs_Click;
        grpVerify.Controls.AddRange(new Control[] { lblVerifyStatus, btnDocs });
        btnClose.DialogResult = DialogResult.OK; btnClose.Location = new Point(313, 574); btnClose.Size = new Size(95, 32); btnClose.Text = "Close";

        AcceptButton = btnClose; CancelButton = btnClose;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(420, 622);
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent; Text = "My account";
        Controls.AddRange(new Control[] { grpProfile, grpPassword, grpVerify, btnClose });
        grpProfile.ResumeLayout(false); grpProfile.PerformLayout();
        grpPassword.ResumeLayout(false); grpPassword.PerformLayout();
        grpVerify.ResumeLayout(false);
        ResumeLayout(false);
    }

    private GroupBox grpProfile, grpPassword, grpVerify;
    private Label lblName, lblEmail, lblRole, lblRoleValue, lblCur, lblNew, lblConf, lblHint, lblPhone, lblVerifyStatus;
    private TextBox txtName, txtEmail, txtCur, txtNew, txtConf, txtPhone;
    private Button btnSaveProfile, btnChangePw, btnClose, btnDocs;
}

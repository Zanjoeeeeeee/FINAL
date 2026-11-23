#nullable enable
namespace DriveMate;

partial class LoginForm
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlCard = new RoundPanel();
        picLogo = new PictureBox();
        lblEmail = new Label();
        pnlEmail = new FieldPanel();
        txtEmail = new TextBox();
        lblPass = new Label();
        pnlPass = new FieldPanel();
        txtPass = new TextBox();
        lnkForgot = new LinkLabel();
        btnLogin = new Button();
        lblNew = new Label();
        btnRegister = new Button();
        pnlCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
        pnlEmail.SuspendLayout();
        pnlPass.SuspendLayout();
        SuspendLayout();
        // 
        // pnlCard
        // 
        pnlCard.BackColor = Color.Transparent;
        pnlCard.Controls.Add(picLogo);
        pnlCard.Controls.Add(lblEmail);
        pnlCard.Controls.Add(pnlEmail);
        pnlCard.Controls.Add(lblPass);
        pnlCard.Controls.Add(pnlPass);
        pnlCard.Controls.Add(lnkForgot);
        pnlCard.Controls.Add(btnLogin);
        pnlCard.Controls.Add(lblNew);
        pnlCard.Controls.Add(btnRegister);
        pnlCard.FillColor = Color.FromArgb(243, 244, 247);
        pnlCard.Location = new Point(688, 179);
        pnlCard.Margin = new Padding(3, 4, 3, 4);
        pnlCard.Name = "pnlCard";
        pnlCard.Radius = 28;
        pnlCard.Size = new Size(549, 702);
        pnlCard.TabIndex = 0;
        // 
        // picLogo
        // 
        picLogo.BackColor = Color.FromArgb(243, 244, 247);
        picLogo.Location = new Point(117, 38);
        picLogo.Margin = new Padding(3, 4, 3, 4);
        picLogo.Name = "picLogo";
        picLogo.Size = new Size(325, 157);
        picLogo.SizeMode = PictureBoxSizeMode.Zoom;
        picLogo.TabIndex = 0;
        picLogo.TabStop = false;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.BackColor = Color.FromArgb(243, 244, 247);
        lblEmail.Font = new Font("Segoe UI", 10F);
        lblEmail.ForeColor = Color.FromArgb(16, 0, 1);
        lblEmail.Location = new Point(32, 250);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(51, 23);
        lblEmail.TabIndex = 1;
        lblEmail.Text = "Email";
        // 
        // pnlEmail
        // 
        pnlEmail.BackColor = Color.White;
        pnlEmail.BorderColor = Color.FromArgb(223, 93, 97);
        pnlEmail.Controls.Add(txtEmail);
        pnlEmail.FocusBorderColor = Color.FromArgb(217, 47, 52);
        pnlEmail.Location = new Point(32, 277);
        pnlEmail.Margin = new Padding(3, 4, 3, 4);
        pnlEmail.Name = "pnlEmail";
        pnlEmail.Padding = new Padding(11, 11, 11, 8);
        pnlEmail.Size = new Size(485, 48);
        pnlEmail.TabIndex = 2;
        // 
        // txtEmail
        // 
        txtEmail.BackColor = Color.White;
        txtEmail.BorderStyle = BorderStyle.None;
        txtEmail.Dock = DockStyle.Fill;
        txtEmail.Font = new Font("Segoe UI", 10.5F);
        txtEmail.ForeColor = Color.FromArgb(16, 0, 1);
        txtEmail.Location = new Point(11, 11);
        txtEmail.Margin = new Padding(3, 4, 3, 4);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(463, 24);
        txtEmail.TabIndex = 0;
        // 
        // lblPass
        // 
        lblPass.AutoSize = true;
        lblPass.BackColor = Color.FromArgb(243, 244, 247);
        lblPass.Font = new Font("Segoe UI", 10F);
        lblPass.ForeColor = Color.FromArgb(16, 0, 1);
        lblPass.Location = new Point(32, 347);
        lblPass.Name = "lblPass";
        lblPass.Size = new Size(80, 23);
        lblPass.TabIndex = 3;
        lblPass.Text = "Password";
        // 
        // pnlPass
        // 
        pnlPass.BackColor = Color.White;
        pnlPass.BorderColor = Color.FromArgb(223, 93, 97);
        pnlPass.Controls.Add(txtPass);
        pnlPass.FocusBorderColor = Color.FromArgb(217, 47, 52);
        pnlPass.Location = new Point(32, 374);
        pnlPass.Margin = new Padding(3, 4, 3, 4);
        pnlPass.Name = "pnlPass";
        pnlPass.Padding = new Padding(11, 11, 11, 8);
        pnlPass.Size = new Size(485, 48);
        pnlPass.TabIndex = 4;
        // 
        // txtPass
        // 
        txtPass.BackColor = Color.White;
        txtPass.BorderStyle = BorderStyle.None;
        txtPass.Dock = DockStyle.Fill;
        txtPass.Font = new Font("Segoe UI", 10.5F);
        txtPass.ForeColor = Color.FromArgb(16, 0, 1);
        txtPass.Location = new Point(11, 11);
        txtPass.Margin = new Padding(3, 4, 3, 4);
        txtPass.Name = "txtPass";
        txtPass.Size = new Size(463, 24);
        txtPass.TabIndex = 1;
        txtPass.UseSystemPasswordChar = true;
        // 
        // lnkForgot
        // 
        lnkForgot.ActiveLinkColor = Color.FromArgb(217, 47, 52);
        lnkForgot.AutoSize = true;
        lnkForgot.BackColor = Color.FromArgb(243, 244, 247);
        lnkForgot.Cursor = Cursors.Hand;
        lnkForgot.Font = new Font("Segoe UI", 8.5F);
        lnkForgot.LinkBehavior = LinkBehavior.HoverUnderline;
        lnkForgot.LinkColor = Color.FromArgb(16, 0, 1);
        lnkForgot.Location = new Point(32, 426);
        lnkForgot.Name = "lnkForgot";
        lnkForgot.Size = new Size(125, 20);
        lnkForgot.TabIndex = 2;
        lnkForgot.TabStop = true;
        lnkForgot.Text = "Forgot Password?";
        lnkForgot.Click += lnkForgot_Click;
        // 
        // btnLogin
        // 
        btnLogin.Location = new Point(32, 479);
        btnLogin.Margin = new Padding(3, 4, 3, 4);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(485, 56);
        btnLogin.TabIndex = 3;
        btnLogin.Tag = "dark";
        btnLogin.Text = "Log In";
        btnLogin.Click += btnLogin_Click;
        // 
        // lblNew
        // 
        lblNew.AutoSize = true;
        lblNew.BackColor = Color.FromArgb(243, 244, 247);
        lblNew.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        lblNew.ForeColor = Color.FromArgb(217, 47, 52);
        lblNew.Location = new Point(32, 581);
        lblNew.Name = "lblNew";
        lblNew.Size = new Size(154, 25);
        lblNew.TabIndex = 5;
        lblNew.Text = "New to GO CAR?";
        // 
        // btnRegister
        // 
        btnRegister.Location = new Point(32, 610);
        btnRegister.Margin = new Padding(3, 4, 3, 4);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(485, 56);
        btnRegister.TabIndex = 4;
        btnRegister.Tag = "brand";
        btnRegister.Text = "Create an account (renter or host)";
        btnRegister.Click += btnRegister_Click;
        // 
        // LoginForm
        // 
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(16, 0, 1);
        ClientSize = new Size(1902, 1055);
        Controls.Add(pnlCard);
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        if (Brand.AppIcon != null) Icon = Brand.AppIcon;
        Margin = new Padding(3, 4, 3, 4);
        MaximizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sign in";
        pnlCard.ResumeLayout(false);
        pnlCard.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
        pnlEmail.ResumeLayout(false);
        pnlEmail.PerformLayout();
        pnlPass.ResumeLayout(false);
        pnlPass.PerformLayout();
        ResumeLayout(false);
    }

    private RoundPanel pnlCard;
    private FieldPanel pnlEmail, pnlPass;
    private PictureBox picLogo;
    private Label lblEmail, lblPass, lblNew;
    private TextBox txtEmail, txtPass;
    private LinkLabel lnkForgot;
    private Button btnLogin, btnRegister;
}
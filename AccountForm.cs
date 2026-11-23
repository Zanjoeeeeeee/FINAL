#nullable enable
using System.Text.RegularExpressions;

namespace DriveMate;

public partial class AccountForm : ThemedForm
{
    readonly User me;

    public AccountForm(User user)
    {
        InitializeComponent();
        me = user;
        var fresh = Database.GetUserById(me.Id);           
        if (fresh != null) { me.Verification = fresh.Verification; me.VerificationNote = fresh.VerificationNote; }
        txtName.Text = me.FullName; txtEmail.Text = me.Email; txtPhone.Text = me.Phone; lblRoleValue.Text = me.Role.ToString();
        ShowStatus();
    }

    void ShowStatus()
    {
        grpVerify.Visible = me.Role != Role.Admin;
        switch (me.Verification)
        {
            case VerifStatus.Verified:
                lblVerifyStatus.ForeColor = Theme.Green; lblVerifyStatus.Text = "✔ Verified. You have full access to car-to-go.";
                btnDocs.Visible = false; break;
            case VerifStatus.Pending:
                lblVerifyStatus.ForeColor = Theme.Orange; lblVerifyStatus.Text = "⏳ Pending. Our admin is reviewing your documents.";
                btnDocs.Visible = true; btnDocs.Text = "Replace documents…"; break;
            default:
                lblVerifyStatus.ForeColor = Theme.Red;
                lblVerifyStatus.Text = $"✖ Rejected: {(me.VerificationNote == "" ? "please upload new documents." : me.VerificationNote)}";
                btnDocs.Visible = true; btnDocs.Text = "Resubmit documents…"; break;
        }
    }

    private void btnDocs_Click(object? sender, EventArgs e)
    {
        using var d = new DocsDialog(me.Role);
        if (d.ShowDialog(this) != DialogResult.OK) return;
        Database.SaveUserDocs(me.Id, d.IdType, d.IdPhoto!, d.Selfie!);
        me.Verification = VerifStatus.Pending; me.VerificationNote = ""; me.IdType = d.IdType;
        ShowStatus();
        Ui.Info("Documents sent to our admin team for review.");
    }

    private void btnSaveProfile_Click(object? sender, EventArgs e)
    {
        var name = txtName.Text.Trim();
        var mail = txtEmail.Text.Trim().ToLowerInvariant();
        var phone = txtPhone.Text.Trim();
        if (name.Length < 2) { Ui.Info("Please enter your full name."); return; }
        if (!mail.Contains('@') || !mail.Contains('.')) { Ui.Info("Please enter a valid email address."); return; }
        if (phone != "" && !Regex.IsMatch(phone, @"^\+?[\d\s-]{10,15}$")) { Ui.Info("Please enter a valid mobile number."); return; }
        if (Database.EmailTaken(mail, me.Id)) { Ui.Info("That email is already used by another account."); return; }

        Database.UpdateProfile(me.Id, name, mail, phone);
        me.FullName = name; me.Email = mail; me.Phone = phone;
        Ui.Info("Profile updated.");
    }

    private void btnChangePw_Click(object? sender, EventArgs e)
    {
        if (!Security.Verify(txtCur.Text, me.PasswordHash)) { Ui.Info("Your current password is incorrect."); return; }
        if (txtNew.Text.Length < 6) { Ui.Info("The new password must be at least 6 characters."); return; }
        if (txtNew.Text != txtConf.Text) { Ui.Info("The new password and confirmation do not match."); return; }

        var hash = Security.Hash(txtNew.Text);
        Database.UpdatePassword(me.Id, hash);
        me.PasswordHash = hash;
        txtCur.Clear(); txtNew.Clear(); txtConf.Clear();
        Ui.Info("Password changed.");
    }
}

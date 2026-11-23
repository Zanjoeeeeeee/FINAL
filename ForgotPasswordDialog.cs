namespace DriveMate;

public class ForgotPasswordDialog : ThemedForm
{
    readonly TextBox txtEmail = new();
    readonly TextBox txtPhone = new();
    readonly TextBox txtNewPass = new();
    readonly TextBox txtConfPass = new();
    readonly Button btnReset = new();
    readonly Button btnCancel = new();

    public ForgotPasswordDialog(string prefillEmail = "")
    {
        Text = "Reset password";
        ClientSize = new Size(410, 365);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        var lblHead = new Label
        {
            Text = "Reset your password",
            Font = Theme.Bold(14F),
            AutoSize = true,
            Location = new Point(22, 16)
        };

        var lblSub = new Label
        {
            Text = "Enter your registered email and mobile number to verify your account.",
            ForeColor = Theme.Muted,
            Location = new Point(22, 46),
            Size = new Size(366, 34)
        };

        AddField("Registered email", txtEmail, 88, false);
        txtEmail.Text = prefillEmail.Trim();

        AddField("Registered mobile number", txtPhone, 144, false);
        AddField("New password (min. 6 characters)", txtNewPass, 200, true);
        AddField("Confirm new password", txtConfPass, 256, true);

        btnReset.Text = "Reset password";
        btnReset.Location = new Point(155, 314);
        btnReset.Size = new Size(140, 36);
        btnReset.Click += BtnReset_Click;

        btnCancel.Text = "Cancel";
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(303, 314);
        btnCancel.Size = new Size(85, 36);

        AcceptButton = btnReset;
        CancelButton = btnCancel;
        Controls.AddRange(new Control[] { lblHead, lblSub, btnReset, btnCancel });
    }

    void AddField(string label, TextBox tb, int y, bool isPassword)
    {
        var lbl = new Label { Text = label, AutoSize = true, Location = new Point(22, y) };
        tb.Location = new Point(22, y + 20);
        tb.Size = new Size(366, 27);
        tb.UseSystemPasswordChar = isPassword;
        Controls.AddRange(new Control[] { lbl, tb });
    }

    static string NormalizePhone(string raw) =>
        new string(raw.Where(char.IsDigit).ToArray());

    void BtnReset_Click(object? sender, EventArgs e)
    {
        var email = txtEmail.Text.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(email))
        {
            Ui.Info("Please enter your registered email address.");
            return;
        }

        var user = Database.GetUserByEmail(email);
        if (user is null)
        {
            Ui.Info("No account was found with that email address.");
            return;
        }

        if (user.IsDisabled)
        {
            Ui.Info("This account has been disabled by an administrator and cannot reset its password.");
            return;
        }

        var inputDigits = NormalizePhone(txtPhone.Text);
        var storedDigits = NormalizePhone(user.Phone);

        if (string.IsNullOrEmpty(storedDigits))
        {
            Ui.Info("This account does not have a mobile number on file. Please contact an administrator to reset your password.");
            return;
        }

        if (inputDigits != storedDigits)
        {
            Ui.Info("The mobile number does not match our records for this email.");
            return;
        }

        if (txtNewPass.Text.Length < 6)
        {
            Ui.Info("New password must be at least 6 characters.");
            return;
        }

        if (txtNewPass.Text != txtConfPass.Text)
        {
            Ui.Info("The new passwords do not match.");
            return;
        }

        Database.UpdatePassword(user.Id, Security.Hash(txtNewPass.Text));
        Database.Notify(user.Id, "Password Changed", "Your account password was reset from the sign-in screen.");
        Ui.Info("Your password has been reset! You can now sign in with your new password.");
        DialogResult = DialogResult.OK;
    }
}
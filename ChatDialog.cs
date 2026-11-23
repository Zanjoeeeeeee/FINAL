namespace DriveMate;

public class ChatDialog : ThemedForm
{
    readonly Booking booking;
    readonly User me;
    readonly Guid recipientId;
    readonly ListBox lst = new();
    readonly TextBox txtMsg = new();
    readonly Button btnSend = new();

    public ChatDialog(Booking b, User currentUser)
    {
        booking = b;
        me = currentUser;
        recipientId = me.Id == b.RenterId ? b.HostId : b.RenterId;

        Text = $"Messages – {b.CarName} ({b.OtherName})";
        ClientSize = new Size(460, 440);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        lst.Location = new Point(16, 16); lst.Size = new Size(428, 340);
        lst.Font = new Font("Segoe UI", 9.5F);
        txtMsg.Location = new Point(16, 370); txtMsg.Size = new Size(330, 32);
        btnSend.Location = new Point(354, 368); btnSend.Size = new Size(90, 34);
        btnSend.Text = "Send"; btnSend.Tag = "accent";
        btnSend.Click += BtnSend_Click;

        AcceptButton = btnSend;
        Controls.AddRange(new Control[] { lst, txtMsg, btnSend });
        LoadChat();
    }

    void LoadChat()
    {
        lst.Items.Clear();
        var msgs = Database.GetMessages(booking.Id);
        if (msgs.Count == 0) lst.Items.Add("No messages yet. Coordinate pick-up or trip details here.");
        foreach (var m in msgs)
        {
            var local = DateTime.SpecifyKind(m.SentAt, DateTimeKind.Utc).ToLocalTime().ToString("MMM d h:mm tt");
            lst.Items.Add($"[{local}] {m.SenderName}: {m.Body}");
        }
        if (lst.Items.Count > 0) lst.TopIndex = lst.Items.Count - 1;
    }

    void BtnSend_Click(object? sender, EventArgs e)
    {
        var body = txtMsg.Text.Trim();
        if (body == "") return;
        Database.SendMessage(booking.Id, me.Id, me.FullName, recipientId, booking.CarName, body);
        txtMsg.Clear();
        LoadChat();
    }
}
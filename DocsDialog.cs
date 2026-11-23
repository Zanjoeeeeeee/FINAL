namespace DriveMate;

public partial class DocsDialog : ThemedForm
{
    public string IdType => cboIdType.Text;
    public PhotoItem? IdPhoto { get; private set; }
    public PhotoItem? Selfie { get; private set; }

    public DocsDialog(Role role)
    {
        InitializeComponent();
        if (role == Role.Renter)
        {
            cboIdType.Items.Add("Driver's License");
            lblHint.Text = "Renters: upload a clear photo of your driver's license and a selfie holding it. Make sure every detail is readable.";
        }
        else
        {
            cboIdType.Items.AddRange(new object[] { "Driver's License", "Passport", "UMID", "PhilSys National ID", "Postal ID", "Voter's ID" });
            lblHint.Text = "Hosts: upload a valid government ID and a selfie holding it. Make sure every detail is readable.";
        }
        cboIdType.SelectedIndex = 0;
    }

    static void Show(PictureBox pb, PhotoItem p) { pb.Image?.Dispose(); pb.Image = Ui.ToImage(p.Data); }

    private void btnId_Click(object? sender, EventArgs e)
    {
        var p = Ui.PickPhotos(this, 1, false);
        if (p.Count == 1) { IdPhoto = p[0]; Show(picId, p[0]); }
    }

    private void btnSelfie_Click(object? sender, EventArgs e)
    {
        var p = Ui.PickPhotos(this, 1, false);
        if (p.Count == 1) { Selfie = p[0]; Show(picSelfie, p[0]); }
    }

    private void btnSubmit_Click(object? sender, EventArgs e)
    {
        if (IdPhoto is null || Selfie is null) { Ui.Info("Please choose both an ID photo and a selfie."); return; }
        DialogResult = DialogResult.OK;
    }
}

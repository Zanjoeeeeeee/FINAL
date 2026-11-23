namespace DriveMate;

public partial class InspectionDialog : Form
{
    const int MaxPhotos = 6, MaxBytes = 3 * 1024 * 1024;
    public Inspection Result { get; private set; }
    readonly List<PhotoItem> photos = new();

    public InspectionDialog(string stage, Booking b)
    {
        InitializeComponent();
        Result = new Inspection { BookingId = b?.Id ?? Guid.Empty, Stage = stage };
        Text = $"{stage} inspection";
        if (stage == "Return" && b.Pickup != null)
        {
            nudOdo.Minimum = b.Pickup.Odometer; nudOdo.Value = b.Pickup.Odometer;
            lblHint.Text = $"Return check. Deductions from the ₱{b.Deposit:N0} deposit are calculated automatically.";
        }
        else lblHint.Text = "Record the car's condition before handing over the keys. Photos protect both host and renter.";
    }

    void UpdateCount() => lblPhotoCount.Text = $"{photos.Count} photo(s) attached (max {MaxPhotos}, 3 MB each)";

    private void btnAddPhotos_Click(object? sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog { Filter = "Images|*.jpg;*.jpeg;*.png", Multiselect = true, Title = "Select photos" };
        if (dlg.ShowDialog(this) != DialogResult.OK) return;
        foreach (var path in dlg.FileNames)
        {
            if (photos.Count >= MaxPhotos) { Ui.Info($"Only {MaxPhotos} photos per inspection."); break; }
            var info = new FileInfo(path);
            if (info.Length > MaxBytes) { Ui.Info($"{info.Name} is larger than 3 MB and was skipped."); continue; }
            photos.Add(new PhotoItem { FileName = info.Name, Data = File.ReadAllBytes(path) });
        }
        UpdateCount();
    }

    private void btnClearPhotos_Click(object? sender, EventArgs e) { photos.Clear(); UpdateCount(); }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        Result.Odometer = (int)nudOdo.Value; Result.FuelPercent = (int)nudFuel.Value;
        Result.Scratches = chkScratch.Checked; Result.InteriorDirty = chkDirty.Checked;
        Result.TireOrLightIssue = chkTire.Checked; Result.Notes = txtNotes.Text.Trim();
        Result.At = DateTime.Now; Result.NewPhotos = photos;
        DialogResult = DialogResult.OK;
    }
}

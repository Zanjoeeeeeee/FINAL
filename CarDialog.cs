namespace DriveMate;

public partial class CarDialog : ThemedForm
{
    const int MaxPhotos = 8;
    public Car Result { get; private set; }
    public List<PhotoItem> Photos { get; private set; } = new();
    public bool PhotosChanged { get; private set; }

    public CarDialog(Car? existing, Guid hostId)
    {
        InitializeComponent();
        nudYear.Maximum = DateTime.Now.Year + 1;
        cboLoc.Items.AddRange(Ui.Cities);
        cboCategory.Items.AddRange(Ui.Categories);
        Result = existing ?? new Car { HostId = hostId };

        if (existing != null)
        {
            Text = "Edit car";
            txtMake.Text = existing.Make; txtModel.Text = existing.Model; txtPlate.Text = existing.Plate;
            nudYear.Value = existing.Year; nudSeats.Value = existing.Seats;
            nudPrice.Value = existing.PricePerDay; nudDeposit.Value = existing.Deposit;
            cboTrans.Text = existing.Transmission; cboLoc.Text = existing.Location; cboCategory.Text = existing.Category;
            Photos = Database.GetCarPhotos(existing.Id);
        }
        else { cboTrans.SelectedIndex = 0; cboLoc.SelectedIndex = 0; cboCategory.SelectedIndex = 0; }
        UpdateCount();
    }

    void UpdateCount() => lblPhotoCount.Text = $"{Photos.Count} photo(s) (max {MaxPhotos}, 3 MB each) – renters see these when booking";

    private void btnAddPhotos_Click(object? sender, EventArgs e)
    {
        int room = MaxPhotos - Photos.Count;
        if (room <= 0) { Ui.Info($"A car can have up to {MaxPhotos} photos."); return; }
        var added = Ui.PickPhotos(this, room);
        if (added.Count == 0) return;
        Photos.AddRange(added); PhotosChanged = true; UpdateCount();
    }

    private void btnClearPhotos_Click(object? sender, EventArgs e)
    {
        Photos.Clear(); PhotosChanged = true; UpdateCount();
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (txtMake.Text.Trim() == "" || txtModel.Text.Trim() == "" || txtPlate.Text.Trim() == "" || cboLoc.Text.Trim() == "")
        { Ui.Info("Please fill in make, model, plate and city."); return; }

        Result.Make = txtMake.Text.Trim(); Result.Model = txtModel.Text.Trim();
        Result.Plate = txtPlate.Text.Trim().ToUpper();
        Result.Year = (int)nudYear.Value; Result.Seats = (int)nudSeats.Value;
        Result.Category = cboCategory.Text; Result.Transmission = cboTrans.Text; Result.Location = cboLoc.Text.Trim();
        Result.PricePerDay = nudPrice.Value; Result.Deposit = nudDeposit.Value;
        DialogResult = DialogResult.OK;
    }
}

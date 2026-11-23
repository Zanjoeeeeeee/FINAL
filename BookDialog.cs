// BookDialog.cs
namespace DriveMate;

public partial class BookDialog : ThemedForm
{
    public Booking? Result { get; private set; }
    readonly Car car;
    readonly List<PhotoItem> carPhotos;
    int idx;

    public BookDialog(Car car)
    {
        InitializeComponent();
        this.car = car;
        carPhotos = Database.GetCarPhotos(car.Id);
        lblCar.Text = $"{car}  ·  {car.Category}  ·  {car.Location}";
        dtpStart.MinDate = DateTime.Today; dtpEnd.MinDate = DateTime.Today;
        dtpStart.Value = DateTime.Today.AddDays(1); dtpEnd.Value = DateTime.Today.AddDays(3);

        foreach (var p in Pricing.InsurancePlans)
            cboInsurance.Items.Add(p.Desc);
        cboInsurance.SelectedIndex = 0;

        ShowPhoto(0);
        Recalc();
    }

    void ShowPhoto(int i)
    {
        picCar.Image?.Dispose(); picCar.Image = null;
        if (carPhotos.Count == 0)
        {
            picCar.Image = Ui.Placeholder(368, 198, "No photos yet");
            lblPhotoIdx.Text = "The host hasn't uploaded photos yet";
            btnPrev.Enabled = btnNext.Enabled = false;
            return;
        }
        idx = (i + carPhotos.Count) % carPhotos.Count;
        picCar.Image = Ui.ToImage(carPhotos[idx].Data);
        lblPhotoIdx.Text = $"Photo {idx + 1} of {carPhotos.Count}";
        btnPrev.Enabled = btnNext.Enabled = carPhotos.Count > 1;
    }

    private void btnPrev_Click(object? sender, EventArgs e) => ShowPhoto(idx - 1);
    private void btnNext_Click(object? sender, EventArgs e) => ShowPhoto(idx + 1);

    int Days => (dtpEnd.Value.Date - dtpStart.Value.Date).Days;

    void Recalc()
    {
        if (Days < 1) { lblSummary.Text = "Return must be after pick-up."; return; }
        var q = Pricing.Quote(Days, car.PricePerDay);
        var plan = Pricing.InsurancePlans[Math.Max(0, cboInsurance.SelectedIndex)];
        decimal insTotal = plan.DailyRate * Days;
        decimal grandDue = q.Total + insTotal + car.Deposit;

        lblSummary.Text = $"{Days} day(s) × ₱{car.PricePerDay:N0}" +
            (q.Discount > 0 ? $" − ₱{q.Discount:N0} ({q.Label})" : "") +
            (insTotal > 0 ? $"\nInsurance ({plan.Name}): ₱{insTotal:N0}" : "") +
            $"\nRental + Insurance: ₱{q.Total + insTotal:N0}  ·  Deposit: ₱{car.Deposit:N0}" +
            $"\nTotal Due at Payment: ₱{grandDue:N0}";
    }

    private void dtp_ValueChanged(object? sender, EventArgs e) => Recalc();

    private void btnSend_Click(object? sender, EventArgs e)
    {
        if (Days < 1) { Ui.Info("Return date must be after pick-up date."); return; }
        if (Database.HasClash(car.Id, dtpStart.Value, dtpEnd.Value, Guid.Empty))
        { Ui.Info("This car is already booked for those dates."); return; }

        var q = Pricing.Quote(Days, car.PricePerDay);
        var plan = Pricing.InsurancePlans[Math.Max(0, cboInsurance.SelectedIndex)];

        Result = new Booking
        {
            CarId = car.Id,
            HostId = car.HostId,
            Start = dtpStart.Value.Date,
            End = dtpEnd.Value.Date,
            Total = q.Total,
            InsuranceTier = plan.Name,
            InsuranceFee = plan.DailyRate * Days,
            Deposit = car.Deposit
        };
        DialogResult = DialogResult.OK;
    }
}
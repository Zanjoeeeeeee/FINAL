// CarCard.cs
namespace DriveMate;

public partial class CarCard : UserControl
{
    public Car Car { get; }
    public event EventHandler<Car>? BookClicked;
    public event EventHandler<Car>? FavoriteToggled;

    public CarCard(Car car, byte[]? cover)
    {
        InitializeComponent();
        Car = car;
        lblCategory.Text = car.Category.ToUpper();
        lblName.Text = car.ToString();
        lblMeta.Text = $"{car.Transmission} · {car.Seats} seats · {car.Location}";
        lblRating.Text = (car.RatingCount == 0 ? "★ New" : $"★ {car.AvgRating:0.0} ({car.RatingCount})") + $"  ·  Host: {car.HostName}";
        lblPrice.Text = $"₱{car.PricePerDay:N0}/day";
        picCar.Image = (cover != null ? Ui.ToImage(cover) : null) ?? Ui.Placeholder(288, 180, "No photo yet");
        Theme.StyleButton(btnBook);
        UpdateFav();
    }

    public void UpdateFav()
    {
        btnFav.Text = Car.IsFavorite ? "♥" : "♡";
        btnFav.ForeColor = Car.IsFavorite ? Theme.Red : Theme.Navy;
    }

    private void card_Click(object? sender, EventArgs e) => BookClicked?.Invoke(this, Car);
    private void btnFav_Click(object? sender, EventArgs e) => FavoriteToggled?.Invoke(this, Car);

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(Theme.Line);
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
    }
}
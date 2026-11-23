#nullable enable
namespace DriveMate;

partial class CarCard
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        picCar = new PictureBox(); btnFav = new Button(); lblCategory = new Label(); lblName = new Label();
        lblMeta = new Label(); lblRating = new Label(); lblPrice = new Label(); btnBook = new Button();
        ((System.ComponentModel.ISupportInitialize)picCar).BeginInit();
        SuspendLayout();

        picCar.BackColor = Theme.Navy;
        picCar.Cursor = Cursors.Hand;
        picCar.Location = new Point(0, 0);
        picCar.Size = new Size(288, 180);
        picCar.SizeMode = PictureBoxSizeMode.Zoom;
        picCar.Click += card_Click;

        btnFav.BackColor = Color.White;
        btnFav.FlatStyle = FlatStyle.Flat;
        btnFav.FlatAppearance.BorderSize = 0;
        btnFav.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnFav.Location = new Point(246, 8);
        btnFav.Size = new Size(34, 30);
        btnFav.Cursor = Cursors.Hand;
        btnFav.Click += btnFav_Click;
        picCar.Controls.Add(btnFav);

        lblCategory.AutoSize = true;
        lblCategory.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCategory.ForeColor = Theme.Blue;
        lblCategory.Location = new Point(14, 188);

        lblName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblName.ForeColor = Theme.Navy;
        lblName.Location = new Point(14, 206);
        lblName.Size = new Size(260, 24);
        lblName.Cursor = Cursors.Hand;
        lblName.Click += card_Click;

        lblMeta.ForeColor = Theme.Muted;
        lblMeta.Location = new Point(14, 232);
        lblMeta.Size = new Size(260, 20);

        lblRating.ForeColor = Theme.Orange;
        lblRating.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblRating.Location = new Point(14, 254);
        lblRating.Size = new Size(260, 20);

        lblPrice.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblPrice.ForeColor = Theme.Navy;
        lblPrice.Location = new Point(14, 286);
        lblPrice.Size = new Size(155, 32);
        lblPrice.TextAlign = ContentAlignment.MiddleLeft;

        btnBook.Location = new Point(176, 284);
        btnBook.Size = new Size(98, 34);
        btnBook.Text = "Book now";
        btnBook.Click += card_Click;

        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Margin = new Padding(0, 0, 16, 16);
        Size = new Size(288, 330);
        Controls.AddRange(new Control[] { picCar, lblCategory, lblName, lblMeta, lblRating, lblPrice, btnBook });

        ((System.ComponentModel.ISupportInitialize)picCar).EndInit();
        ResumeLayout(false);
    }

    private PictureBox picCar;
    private Button btnFav, btnBook;
    private Label lblCategory, lblName, lblMeta, lblRating, lblPrice;
}
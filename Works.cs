using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;

namespace DriveMate;

/// <summary>
/// "How It Works" page: a car photo background (dark on the left, fading to the car on the right)
/// with a semi-transparent, scrollable user-guide card on top.
/// Drop a photo named howitworks_bg.jpg next to the project files to use your own car picture.
/// </summary>
public class Works : Control
{
    enum Kind { Title, Subtitle, Rule, Heading, Body, Gap }
    record Block(Kind Kind, string Text = "");

    readonly List<Block> blocks = new();
    readonly VScrollBar bar = new() { Dock = DockStyle.Right, Visible = false };
    readonly Font fTitle = new("Segoe UI", 13F, FontStyle.Bold);
    readonly Font fSub = new("Segoe UI", 11F, FontStyle.Bold);
    readonly Font fHead = new("Segoe UI", 11.5F, FontStyle.Bold);
    readonly Font fBody = new("Segoe UI", 10.5F);
    readonly Font fHero = new("Segoe UI", 26F, FontStyle.Bold);
    Image? heroLogo;
    readonly Color ink = Color.FromArgb(28, 28, 32);
    readonly Image? bg = LoadBackground();

    int contentHeight, logoH, heroH, textHeight;
    Rectangle card;   // card in content coordinates (before scrolling)

    public Works()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        Dock = DockStyle.Fill;
        Controls.Add(bar);
        bar.Scroll += (s, e) => Invalidate();
        MouseEnter += (s, e) => Focus();
        BuildContent();
    }

    static Image? LoadBackground()
    {
        try
        {
            using var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("howitworks_bg.jpg");
            if (s == null) return null;
            using var img = Image.FromStream(s);
            return new Bitmap(img);
        }
        catch { return null; }
    }

    // ---------------- content ----------------
    void BuildContent()
    {
        void T(Kind k, string t = "") => blocks.Add(new Block(k, t));

        T(Kind.Title, "Car Rental");
        T(Kind.Subtitle, "car-to-go User Guide");
        T(Kind.Gap);
        T(Kind.Body, "car-to-go is a peer-to-peer car rental desktop application connecting verified vehicle owners (Hosts) with verified drivers (Renters) through a complete booking, payment, inspection, and review lifecycle.");
        T(Kind.Gap); T(Kind.Rule); T(Kind.Gap);

        T(Kind.Heading, "1. Account Registration & Identity Verification");
        T(Kind.Body, "Every user must register and pass verification before renting or listing a vehicle.");
        T(Kind.Gap);
        T(Kind.Body, "1. Create an Account: Launch the application and register using your full name, email address, phone number, and password, choosing either Renter or Host as your account role.");
        T(Kind.Body, "2. Submit Verification Documents: Open \"My account\" from the top navigation bar and upload a valid government-issued ID or Driver's License along with your license information.");
        T(Kind.Body, "3. Admin Approval: Your profile remains in \"Pending\" status until an Administrator reviews your documents. Once approved, a green \"Verified\" badge appears in the header bar, unlocking full marketplace access.");
        T(Kind.Gap); T(Kind.Rule); T(Kind.Gap);

        T(Kind.Heading, "2. Browsing & Booking a Car (Renters)");
        T(Kind.Body, "1. Find a Car: Open \"Browse Cars\" and filter by city, transmission, category, maximum daily price, and available dates. Tap the heart icon to save a car to your favorites.");
        T(Kind.Body, "2. Request a Booking: Click \"Book\" on a car, choose your pick-up and return dates, and select an insurance plan. Your request is sent to the Host.");
        T(Kind.Body, "3. Wait for Approval: You will receive an alert once the Host approves or declines. You can message the Host any time from \"My Trips\".");
        T(Kind.Gap); T(Kind.Rule); T(Kind.Gap);

        T(Kind.Heading, "3. Payment");
        T(Kind.Body, "Once a booking is approved, open \"My Trips\" and click \"Pay now\". The amount due covers the rental total, your insurance plan, and a refundable security deposit. Payment is processed through Xendit Hosted Checkout, and a payment reference is saved to your trip.");
        T(Kind.Gap); T(Kind.Rule); T(Kind.Gap);

        T(Kind.Heading, "4. Pick-up Inspection");
        T(Kind.Body, "At hand-over, the Host records a pick-up inspection with photos of the car's condition and starts the trip. These photos are the baseline for comparing the car when it comes back.");
        T(Kind.Gap); T(Kind.Rule); T(Kind.Gap);

        T(Kind.Heading, "5. Return, Deposit & Payout");
        T(Kind.Body, "When the car is returned, the Host logs a return inspection. Any new damage, fuel, or lateness charges are calculated automatically and deducted from the deposit (reduced by your insurance plan), and the remaining deposit is refunded to the Renter. The Host's payout is released once the trip is settled.");
        T(Kind.Gap); T(Kind.Rule); T(Kind.Gap);

        T(Kind.Heading, "6. Cancellations & Disputes");
        T(Kind.Body, "Pending or approved bookings can be cancelled from \"My Trips\"; the refund depends on how close the cancellation is to the pick-up date, and the policy is shown before you confirm. If you disagree with a deposit deduction, click \"Dispute\" on the completed trip and attach your evidence. An Administrator reviews the pick-up, return, and dispute photos and makes the final decision.");
        T(Kind.Gap); T(Kind.Rule); T(Kind.Gap);

        T(Kind.Heading, "7. Reviews & Trust");
        T(Kind.Body, "After a completed trip, Renters rate the car and Hosts rate the Renter. Ratings build trust across the marketplace and are shown next to each profile.");
        T(Kind.Gap); T(Kind.Rule); T(Kind.Gap);

        T(Kind.Heading, "8. Listing Your Car (Hosts)");
        T(Kind.Body, "1. Add a Car: Open \"My cars\" and click \"Add\", then enter the car details, daily price, security deposit, and upload clear photos.");
        T(Kind.Body, "2. Manage Requests: Open \"Booking requests\" to approve or decline, start the trip at pick-up, end it at return, and track your earnings and payouts.");
        T(Kind.Body, "3. Stay Available: Hide a listing anytime if the car is unavailable. Cars with existing bookings can be hidden but not deleted.");
        T(Kind.Gap);
    }

    // ---------------- layout ----------------
    int Margin => Math.Max(20, Width * 3 / 100);
    int Pad => 36;

    Font FontOf(Kind k) => k switch { Kind.Title => fTitle, Kind.Subtitle => fSub, Kind.Heading => fHead, _ => fBody };

    int BlockHeight(Graphics g, Block b, int textWidth) => b.Kind switch
    {
        Kind.Gap => 10,
        Kind.Rule => 2,
        _ => (int)Math.Ceiling(g.MeasureString(b.Text, FontOf(b.Kind), textWidth, CenterFormat).Height) + (b.Kind == Kind.Body ? 6 : 4)
    };

    static readonly StringFormat CenterFormat = new(StringFormat.GenericTypographic)
    {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Near,
        FormatFlags = StringFormatFlags.LineLimit
    };

    /// <summary>GO CAR logo with its dark text turned white so it reads on the dark background.</summary>
    static Image? MakeHeroLogo()
    {
        try
        {
            var bmp = new Bitmap(Brand.LoginLogo);
            int textTop = (int)(bmp.Height * 0.56);   // arrow + car above, "GO CAR" text below
            for (int y = textTop; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    var c = bmp.GetPixel(x, y);
                    if (c.A > 0 && (c.R * 299 + c.G * 587 + c.B * 114) / 1000 < 110)
                        bmp.SetPixel(x, y, Color.FromArgb(c.A, 255, 255, 255));
                }
            return bmp;
        }
        catch { return null; }
    }

    void Compute(Graphics g, int barW)
    {
        int w = Math.Max(100, Width - barW);
        logoH = Math.Clamp((int)(Height * 0.6), 160, 300);
        heroH = 14 + logoH + 4 + fHero.Height + 28;
        card = new Rectangle(Margin, heroH, Math.Max(100, w - Margin * 2), 0);
        int tw = Math.Max(60, card.Width - Pad * 2);
        textHeight = Pad + blocks.Sum(b => BlockHeight(g, b, tw)) + Pad / 2;
        card.Height = textHeight;
        contentHeight = heroH + textHeight + 16;
    }

    void UpdateLayout()
    {
        using var g = CreateGraphics();
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        Compute(g, 0);
        bool need = contentHeight > Height;
        bar.Visible = need;
        if (need) Compute(g, bar.Width);

        if (bar.Visible)
        {
            bar.Minimum = 0;
            bar.LargeChange = Math.Max(1, Height);
            bar.SmallChange = 40;
            bar.Maximum = Math.Max(0, contentHeight - 1);
            bar.Value = Math.Min(bar.Value, Math.Max(0, bar.Maximum - bar.LargeChange + 1));
        }
        else bar.Value = 0;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (blocks.Count > 0) UpdateLayout();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        if (!bar.Visible) return;
        int max = Math.Max(0, bar.Maximum - bar.LargeChange + 1);
        bar.Value = Math.Clamp(bar.Value - Math.Sign(e.Delta) * 60, 0, max);
        Invalidate();
    }

    // ---------------- painting ----------------
    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        DrawBackground(g, new Rectangle(0, 0, Width, Height));

        if (card.Width <= 0) UpdateLayout();
        int off = bar.Visible ? bar.Value : 0;
        int w = Width - (bar.Visible ? bar.Width : 0);

        // ---- hero: logo + "HOW IT WORKS" ----
        heroLogo ??= MakeHeroLogo();
        if (heroLogo != null)
        {
            float aspect = (float)heroLogo.Width / heroLogo.Height;
            float lh = logoH, lw = lh * aspect;
            if (lw > w - Margin * 2) { lw = w - Margin * 2; lh = lw / aspect; }
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(heroLogo, (w - lw) / 2f, 14 - off + (logoH - lh) / 2f, lw, lh);
        }
        g.TextRenderingHint = TextRenderingHint.AntiAlias;
        using (var red = new SolidBrush(Theme.GoRed))
            g.DrawString("HOW IT WORKS", fHero, red,
                new RectangleF(0, 14 + logoH + 4 - off, w, fHero.Height + 4), CenterFormat);

        // ---- translucent guide card ----
        var cardRect = new Rectangle(card.X, card.Y - off, card.Width, card.Height);
        using (var fill = new SolidBrush(Color.FromArgb(125, 255, 255, 255)))
            g.FillRectangle(fill, cardRect);

        g.SetClip(cardRect);
        int textWidth = Math.Max(60, card.Width - Pad * 2);
        int y = cardRect.Top + Pad;
        using var brush = new SolidBrush(ink);
        using var rulePen = new Pen(Color.FromArgb(150, 40, 40, 44), 1);
        foreach (var b in blocks)
        {
            int h = BlockHeight(g, b, textWidth);
            if (y + h >= 0 && y <= Height)
            {
                if (b.Kind == Kind.Rule)
                    g.DrawLine(rulePen, cardRect.Left + Pad, y + 1, cardRect.Right - Pad, y + 1);
                else if (b.Kind != Kind.Gap)
                    g.DrawString(b.Text, FontOf(b.Kind), brush,
                        new RectangleF(cardRect.Left + Pad, y, textWidth, h), CenterFormat);
            }
            y += h;
        }
        g.ResetClip();
    }

    void DrawBackground(Graphics g, Rectangle r)
    {
        if (bg != null)
        {
            // cover-fit, anchored to the right so the car stays in view
            float scale = Math.Max((float)r.Width / bg.Width, (float)r.Height / bg.Height);
            float w = bg.Width * scale, h = bg.Height * scale;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(bg, r.Right - w, (r.Height - h) / 2, w, h);
        }
        else
        {
            // fallback: near-black on the left fading to light grey on the right
            using var b = new LinearGradientBrush(r, Theme.Ink, Color.FromArgb(232, 232, 236), LinearGradientMode.Horizontal);
            g.FillRectangle(b, r);
        }

        // dark fade over the left half, like the reference design
        var fade = new Rectangle(0, 0, Math.Max(1, (int)(r.Width * 0.62)), r.Height);
        using var fb = new LinearGradientBrush(fade, Color.FromArgb(235, Theme.Ink), Color.FromArgb(0, Theme.Ink), LinearGradientMode.Horizontal);
        g.FillRectangle(fb, fade);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) { fTitle.Dispose(); fSub.Dispose(); fHead.Dispose(); fBody.Dispose(); fHero.Dispose(); bg?.Dispose(); heroLogo?.Dispose(); }
        base.Dispose(disposing);
    }
}
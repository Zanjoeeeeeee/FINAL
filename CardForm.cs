using System.Drawing.Drawing2D;

namespace DriveMate;

/// <summary>
/// Base for the login / sign-up windows: red-black gradient background with a soft shadow under the card.
/// Override <see cref="Card"/> to return the RoundPanel the shadow should follow.
/// </summary>
public class CardForm : ThemedForm
{
    // Pre-rendered background (cover-cropped image + card shadow), rebuilt only when the size changes.
    private Bitmap? _bg;

    protected virtual RoundPanel? Card => null;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);               // applies the shared theme (icon, title, button styles)
        BackColor = Theme.Ink;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        if (_bg == null || _bg.Size != ClientSize) BuildBackground();
        e.Graphics.DrawImageUnscaled(_bg!, 0, 0);
    }

    private void BuildBackground()
    {
        _bg?.Dispose();
        _bg = new Bitmap(Math.Max(1, ClientSize.Width), Math.Max(1, ClientSize.Height));
        using var g = Graphics.FromImage(_bg);

        // Background image, scaled to cover the whole window
        var src = Brand.LoginBackground;
        float scale = Math.Max((float)_bg.Width / src.Width, (float)_bg.Height / src.Height);
        float w = src.Width * scale, h = src.Height * scale;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.DrawImage(src, (_bg.Width - w) / 2f, (_bg.Height - h) / 2f, w, h);

        if (Card is not { } card) return;

        // Soft drop shadow under the card (stacked translucent rounded rects)
        g.SmoothingMode = SmoothingMode.AntiAlias;
        var b = card.Bounds;
        int radius = card.ScaledRadius;
        using var br = new SolidBrush(Color.FromArgb(5, 0, 0, 0));
        for (int i = 18; i >= 1; i--)
        {
            var r = new RectangleF(b.X - i, b.Y - i + 10, b.Width + i * 2, b.Height + i * 2);
            using var path = RoundPanel.RoundRect(r, radius + i);
            g.FillPath(br, path);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _bg?.Dispose();
        base.Dispose(disposing);
    }
}

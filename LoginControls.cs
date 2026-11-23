using System.Drawing.Drawing2D;

namespace DriveMate;

/// <summary>Anti-aliased rounded "card" panel. Its corners are transparent, so the form background shows through.</summary>
public class RoundPanel : Panel
{
    public int Radius { get; set; } = 28;
    public Color FillColor { get; set; } = Theme.CardBg;

    /// <summary>Corner radius in real pixels (DPI-aware).</summary>
    public int ScaledRadius => (int)Math.Round(Radius * DeviceDpi / 96f);

    public RoundPanel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);
        BackColor = Color.Transparent;
    }

    public static GraphicsPath RoundRect(RectangleF r, float radius)
    {
        float d = Math.Max(1f, radius * 2f);
        var p = new GraphicsPath();
        p.AddArc(r.X, r.Y, d, d, 180, 90);
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        p.CloseFigure();
        return p;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var path = RoundRect(new RectangleF(0, 0, Width, Height), ScaledRadius);
        using var br = new SolidBrush(FillColor);
        e.Graphics.FillPath(br, path);
    }
}

/// <summary>
/// A white input box with a coloured border (thicker while focused). Put a borderless TextBox inside it;
/// the box wires up focus highlighting and click-to-focus by itself.
/// </summary>
public class FieldPanel : Panel
{
    public Color BorderColor { get; set; } = Theme.GoRedSoft;
    public Color FocusBorderColor { get; set; } = Theme.GoRed;
    bool _focused;

    public FieldPanel()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        BackColor = Color.White;
        Padding = new Padding(10, 8, 10, 6);
        Cursor = Cursors.IBeam;
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
        base.OnControlAdded(e);
        if (e.Control is TextBox tb)
        {
            tb.BorderStyle = BorderStyle.None;
            tb.Dock = DockStyle.Fill;
            tb.BackColor = Color.White;
            tb.ForeColor = Theme.Ink;
            tb.Enter += (_, _) => { _focused = true; Invalidate(); };
            tb.Leave += (_, _) => { _focused = false; Invalidate(); };
            Click += (_, _) => tb.Focus();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using var pen = new Pen(_focused ? FocusBorderColor : BorderColor);
        e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        if (_focused) e.Graphics.DrawRectangle(pen, 1, 1, Width - 3, Height - 3);
    }
}

/// <summary>A framed group box with the title sitting on the border ("fieldset" look), in the GO CAR red.</summary>
public class LegendPanel : Panel
{
    public Color BorderColor { get; set; } = Theme.GoRedSoft;
    public Font LegendFont { get; set; } = new("Segoe UI", 9F);

    public LegendPanel()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        BackColor = Theme.CardBg;
        ForeColor = Theme.Ink;
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        var sz = TextRenderer.MeasureText(g, Text, LegendFont, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
        int top = sz.Height / 2;

        using (var pen = new Pen(BorderColor))
            g.DrawRectangle(pen, 0, top, Width - 1, Height - top - 1);

        // Knock a gap out of the border behind the title, then draw the title
        using (var br = new SolidBrush(BackColor))
            g.FillRectangle(br, 8, 0, sz.Width + 8, sz.Height);
        TextRenderer.DrawText(g, Text, LegendFont, new Point(12, 0), ForeColor, TextFormatFlags.NoPadding);
    }
}

/// <summary>
/// Drop-down list with a red border and chevron instead of the stock Windows look.
/// Items are owner-drawn (white, with a soft red hover) so no blue selection colour shows up.
/// </summary>
public class FlatCombo : ComboBox
{
    const int WM_PAINT = 0x000F;
    static readonly Color Hover = Color.FromArgb(251, 227, 228);

    public FlatCombo()
    {
        DropDownStyle = ComboBoxStyle.DropDownList;
        DrawMode = DrawMode.OwnerDrawFixed;
        FlatStyle = FlatStyle.Flat;
        ItemHeight = 24;               // closed height = ItemHeight + 6
        BackColor = Color.White;
        ForeColor = Theme.Ink;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        bool inList = (e.State & DrawItemState.ComboBoxEdit) == 0;
        bool hot = inList && (e.State & DrawItemState.Selected) != 0;
        using (var br = new SolidBrush(hot ? Hover : Color.White))
            e.Graphics.FillRectangle(br, e.Bounds);
        if (e.Index < 0) return;

        var text = GetItemText(Items[e.Index]);
        var r = new Rectangle(e.Bounds.X + 8, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height);
        TextRenderer.DrawText(e.Graphics, text, Font, r, Theme.Ink,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
    }

    protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); Invalidate(); }
    protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); Invalidate(); }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == WM_PAINT && IsHandleCreated) PaintChrome();
    }

    void PaintChrome()
    {
        using var g = Graphics.FromHwnd(Handle);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        int w = ClientSize.Width, h = ClientSize.Height;
        const int btn = 28;

        // Replace the system arrow button with a clean white one + chevron
        using (var br = new SolidBrush(Color.White))
            g.FillRectangle(br, w - btn, 1, btn - 1, h - 2);
        float cx = w - btn / 2f - 1, cy = h / 2f;
        using (var pen = new Pen(Theme.Ink, 1.6f) { StartCap = System.Drawing.Drawing2D.LineCap.Round, EndCap = System.Drawing.Drawing2D.LineCap.Round })
            g.DrawLines(pen, new[] { new PointF(cx - 4.5f, cy - 2f), new PointF(cx, cy + 2.5f), new PointF(cx + 4.5f, cy - 2f) });

        // Border (thicker while focused), same as FieldPanel
        using var bp = new Pen(Focused ? Theme.GoRed : Theme.GoRedSoft);
        g.DrawRectangle(bp, 0, 0, w - 1, h - 1);
        if (Focused) g.DrawRectangle(bp, 1, 1, w - 3, h - 3);
    }
}

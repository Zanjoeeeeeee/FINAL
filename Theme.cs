using System.Reflection;

namespace DriveMate;

/// <summary>car-to-go colour palette and control styling (taken from the logo).</summary>
public static class Theme
{
    public static readonly Color Navy = ColorTranslator.FromHtml("#d92f34");
    public static readonly Color NavyLight = ColorTranslator.FromHtml("#d92f34");
    public static readonly Color Blue = ColorTranslator.FromHtml("#d92f34");
    public static readonly Color Orange = ColorTranslator.FromHtml("#FF7A1A");
    public static readonly Color Green = ColorTranslator.FromHtml("#16A34A");
    public static readonly Color Red = ColorTranslator.FromHtml("#DC2626");
    public static readonly Color Bg = ColorTranslator.FromHtml("#F2F5FA");
    public static readonly Color Line = ColorTranslator.FromHtml("#DCE3EF");
    public static readonly Color Muted = ColorTranslator.FromHtml("#6B7A99");

    // GO CAR login look (red / near-black, from the new logo)
    public static readonly Color GoRed = ColorTranslator.FromHtml("#D92F34");
    public static readonly Color GoRedDark = ColorTranslator.FromHtml("#B8242A");
    public static readonly Color GoRedSoft = ColorTranslator.FromHtml("#DF5D61");
    public static readonly Color Ink = ColorTranslator.FromHtml("#100001");
    public static readonly Color InkHover = ColorTranslator.FromHtml("#2B0B0E");
    public static readonly Color CardBg = ColorTranslator.FromHtml("#F3F4F7");

    public static Font Bold(float size = 9F) => new("Segoe UI", size, FontStyle.Bold);

    static Color Hover(Color c) => ControlPaint.Light(c, 0.25f);

    public static void StyleButton(Button b, Color back, Color fore)
    {
        b.UseVisualStyleBackColor = false;
        b.FlatStyle = FlatStyle.Flat;
        b.FlatAppearance.BorderSize = 0;
        b.FlatAppearance.MouseOverBackColor = Hover(back);
        b.BackColor = back; b.ForeColor = fore;
        b.Cursor = Cursors.Hand;
        b.Font = Bold(9F);
    }

    /// <summary>Button.Tag picks the style: ghost, accent, danger, success, secondary, dark, brand (default = primary blue).</summary>
    public static void StyleButton(Button b)
    {
        var tag = b.Tag as string ?? (b.DialogResult == DialogResult.Cancel ? "secondary" : "");
        switch (tag)
        {
            case "ghost": StyleButton(b, Color.Transparent, Color.White); b.FlatAppearance.MouseOverBackColor = Color.Black; break;
            case "accent": StyleButton(b, Color. Transparent, Color.White); break;
            case "danger": StyleButton(b, Red, Color.White); break;
            case "dark": StyleButton(b, Ink, Color.White); b.FlatAppearance.MouseOverBackColor = InkHover; break;
            case "brand": StyleButton(b, GoRed, Color.White); b.FlatAppearance.MouseOverBackColor = GoRedDark; break;
            case "success": StyleButton(b, Green, Color.White); break;
            case "secondary":
                StyleButton(b, Color.White, Navy);
                b.FlatAppearance.BorderSize = 1; b.FlatAppearance.BorderColor = Line; b.FlatAppearance.MouseOverBackColor = Bg; break;
            default: StyleButton(b, Red, Color.White); break;
        }
    }

    public static void StyleGrid(DataGridView g)
    {
        g.ReadOnly = true; g.AllowUserToAddRows = false; g.AllowUserToDeleteRows = false;
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect; g.MultiSelect = false;
        g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; g.RowHeadersVisible = false;
        g.BackgroundColor = Color.White; g.BorderStyle = BorderStyle.None; g.GridColor = Line;
        g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        g.EnableHeadersVisualStyles = false;
        g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        g.ColumnHeadersHeight = 38;
        g.ColumnHeadersDefaultCellStyle.BackColor = Navy;
        g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        g.ColumnHeadersDefaultCellStyle.Font = Bold(9F);
        g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Navy;
        g.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#D6E9FF");
        g.DefaultCellStyle.SelectionForeColor = Navy;
        g.DefaultCellStyle.ForeColor = Navy;
        g.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
        g.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F7F9FD");
        g.RowTemplate.Height = 32;
    }

    public static void StyleTabs(TabControl t)
    {
        t.DrawMode = TabDrawMode.OwnerDrawFixed; t.SizeMode = TabSizeMode.Fixed;
        t.ItemSize = new Size(190, 40); t.Padding = new Point(16, 6);
        t.DrawItem += (s, e) =>
        {
            bool sel = e.Index == t.SelectedIndex;
            using var back = new SolidBrush(sel ? Red : Color.White);
            e.Graphics.FillRectangle(back, e.Bounds);
            TextRenderer.DrawText(e.Graphics, t.TabPages[e.Index].Text, Bold(10F), e.Bounds,
                sel ? Color.White : Red, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        };
    }

    /// <summary>Recursively applies the car-to-go look to a form or control tree.</summary>
    public static void Apply(Control root)
    {
        if (root is Form f) f.BackColor = Bg;
        foreach (Control c in root.Controls)
        {
            switch (c)
            {
                case Button b: StyleButton(b); break;
                case DataGridView g: StyleGrid(g); break;
                case TabControl t: StyleTabs(t); break;
                case TabPage p: p.UseVisualStyleBackColor = false; p.BackColor = Bg; break;
                case GroupBox gb: gb.ForeColor = Navy; break;
            }
            Apply(c);
        }
    }
}

public static class Brand
{
    public static readonly Image Logo = LoadImage("logo_login.png");
    public static readonly Icon? AppIcon = LoadIcon();

    static Image? _loginLogo, _loginBg;
    /// <summary>GO CAR logo with transparent background (login card).</summary>
    public static Image LoginLogo => _loginLogo ??= LoadImage("logo_login.png");
    /// <summary>Red/black gradient behind the login card.</summary>
    public static Image LoginBackground => _loginBg ??= LoadImage("loginbg.jpg");

    static Image LoadImage(string name)
    {
        using var s = Assembly.GetExecutingAssembly().GetManifestResourceStream(name)!;
        using var img = Image.FromStream(s);
        return new Bitmap(img);
    }

    static Icon? LoadIcon()
    {
        try { using var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("icon.ico")!; return new Icon(s); }
        catch { return null; }
    }
}

/// <summary>Base class for every window: car-to-go title, icon and theme.</summary>
public class ThemedForm : Form
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (Brand.AppIcon != null) Icon = Brand.AppIcon;
        var t = Text.Replace("DriveMate – ", "").Replace("DriveMate", "").Trim();
        Text = t == "" || t == "car-to-go" ? "car-to-go" : t.StartsWith("car-to-go") ? t : "car-to-go – " + t;
        Theme.Apply(this);
    }
}

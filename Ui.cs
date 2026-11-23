using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DriveMate;

public static class Ui
{
    public static readonly string[] Cities =
        { "Manila", "Makati", "Quezon City", "Taguig", "Pasig", "Cebu", "Davao", "Baguio", "Clark" };

    public static readonly string[] Categories =
        { "Sedan", "Hatchback", "SUV", "MPV", "Van", "Pickup", "Luxury", "Electric" };

    public static void Info(string m) => MessageBox.Show(m, "car-to-go", MessageBoxButtons.OK, MessageBoxIcon.Information);
    public static bool Ask(string m) => MessageBox.Show(m, "car-to-go", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    public static Image? ToImage(byte[] data)
    {
        try { using var ms = new MemoryStream(data); using var img = Image.FromStream(ms); return new Bitmap(img); }
        catch { return null; }
    }

    /// <summary>Cover-crops a photo to a small JPEG thumbnail (used by the car cards).</summary>
    public static byte[]? MakeThumb(byte[] data, int w = 320, int h = 200)
    {
        using var img = ToImage(data);
        if (img == null) return null;
        using var bmp = new Bitmap(w, h);
        using (var g = Graphics.FromImage(bmp))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            float scale = Math.Max((float)w / img.Width, (float)h / img.Height);
            float sw = img.Width * scale, sh = img.Height * scale;
            g.DrawImage(img, (w - sw) / 2, (h - sh) / 2, sw, sh);
        }
        using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Jpeg);
        return ms.ToArray();
    }

    public static Bitmap Placeholder(int w, int h, string text)
    {
        var bmp = new Bitmap(w, h);
        using var g = Graphics.FromImage(bmp);
        using var br = new LinearGradientBrush(new Rectangle(0, 0, w, h), Theme.NavyLight, Theme.Navy, 45f);
        g.FillRectangle(br, 0, 0, w, h);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using var f = new Font("Segoe UI", 11F, FontStyle.Bold);
        var sz = g.MeasureString(text, f);
        g.DrawString(text, f, Brushes.LightSteelBlue, (w - sz.Width) / 2, (h - sz.Height) / 2);
        return bmp;
    }

    /// <summary>Opens a file picker and returns the chosen photos (JPG/PNG, max 3 MB each).</summary>
    public static List<PhotoItem> PickPhotos(IWin32Window owner, int max, bool multi = true)
    {
        var list = new List<PhotoItem>();
        using var dlg = new OpenFileDialog { Filter = "Images|*.jpg;*.jpeg;*.png", Multiselect = multi, Title = "Select photo(s)" };
        if (dlg.ShowDialog(owner) != DialogResult.OK) return list;
        foreach (var path in dlg.FileNames)
        {
            if (list.Count >= max) { Info($"Only {max} photo(s) allowed here."); break; }
            var fi = new FileInfo(path);
            if (fi.Length > 3 * 1024 * 1024) { Info($"{fi.Name} is larger than 3 MB and was skipped."); continue; }
            list.Add(new PhotoItem { FileName = fi.Name, Data = File.ReadAllBytes(path) });
        }
        return list;
    }
}

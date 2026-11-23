namespace DriveMate;

public partial class PhotoViewerForm : ThemedForm
{
    /// <summary>Shows titled sections of photos (inspection photos, or a user's ID documents).</summary>
    public PhotoViewerForm(string title, params (string Title, List<PhotoItem>? Photos, string Empty)[] sections)
    {
        InitializeComponent();
        Text = title;
        foreach (var s in sections) AddSection(s.Title, s.Photos, s.Empty);
    }

    void AddSection(string title, List<PhotoItem>? photos, string emptyText)
    {
        var header = new Label
        {
            Text = title, Font = Theme.Bold(11F), ForeColor = Theme.Navy, AutoSize = false,
            Width = 700, Height = 30, Margin = new Padding(0, 10, 0, 4)
        };
        flowPhotos.Controls.Add(header);
        flowPhotos.SetFlowBreak(header, true);

        if (photos == null || photos.Count == 0)
        {
            var none = new Label { Text = emptyText, AutoSize = true, ForeColor = Theme.Muted };
            flowPhotos.Controls.Add(none); flowPhotos.SetFlowBreak(none, true);
            return;
        }
        foreach (var p in photos)
        {
            var img = Ui.ToImage(p.Data);
            if (img == null) continue;
            var pb = new PictureBox
            {
                Image = img, SizeMode = PictureBoxSizeMode.Zoom, Size = new Size(300, 220), Cursor = Cursors.Hand,
                BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(0, 0, 10, 10), BackColor = Color.White
            };
            pb.DoubleClick += (s, e) => ShowFull(p);
            flowPhotos.Controls.Add(pb);
        }
        flowPhotos.SetFlowBreak(flowPhotos.Controls[^1], true);
    }

    void ShowFull(PhotoItem p)
    {
        using var f = new Form { Text = "car-to-go – " + p.FileName, Size = new Size(1000, 720), StartPosition = FormStartPosition.CenterParent, BackColor = Color.Black };
        if (Brand.AppIcon != null) f.Icon = Brand.AppIcon;
        f.Controls.Add(new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, Image = Ui.ToImage(p.Data) });
        f.ShowDialog(this);
    }
}

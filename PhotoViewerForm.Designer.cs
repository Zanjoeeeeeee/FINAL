#nullable enable
namespace DriveMate;

partial class PhotoViewerForm
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        flowPhotos = new FlowLayoutPanel();
        SuspendLayout();
        flowPhotos.AutoScroll = true; flowPhotos.Dock = DockStyle.Fill; flowPhotos.Padding = new Padding(10);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(760, 520);
        StartPosition = FormStartPosition.CenterParent; Text = "Inspection photos";
        Controls.Add(flowPhotos);
        ResumeLayout(false);
    }

    private FlowLayoutPanel flowPhotos;
}

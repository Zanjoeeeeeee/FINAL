namespace DriveMate;

public partial class ReviewDialog : Form
{
    public int Rating { get; private set; }
    public string Comment { get; private set; } = "";

    public ReviewDialog(string title = "Rate your trip")
    {
        InitializeComponent();
        Text = title;
        cboStars.SelectedIndex = 0;
    }

    private void btnSubmit_Click(object? sender, EventArgs e)
    {
        Rating = 5 - cboStars.SelectedIndex;
        Comment = txtComment.Text.Trim();
        DialogResult = DialogResult.OK;
    }
}

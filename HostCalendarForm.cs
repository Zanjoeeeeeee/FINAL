namespace DriveMate;

public class HostCalendarForm : ThemedForm
{
    readonly List<Booking> bookings;
    DateTime currentMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);
    readonly Label lblMonth = new();
    readonly TableLayoutPanel grid = new();

    public HostCalendarForm(List<Booking> hostBookings)
    {
        bookings = hostBookings.Where(b => b.Status is BookingStatus.Approved or BookingStatus.Active or BookingStatus.Completed).ToList();
        Text = "Booking Calendar (Month View)";
        ClientSize = new Size(920, 620);
        StartPosition = FormStartPosition.CenterParent;

        var pnlTop = new Panel { Dock = DockStyle.Top, Height = 52, BackColor = Color.White };
        var btnPrev = new Button { Text = "◀ Prev", Location = new Point(16, 10), Size = new Size(90, 32), Tag = "secondary" };
        var btnNext = new Button { Text = "Next ▶", Location = new Point(814, 10), Size = new Size(90, 32), Tag = "secondary" };
        lblMonth.Location = new Point(120, 10); lblMonth.Size = new Size(680, 32);
        lblMonth.Font = Theme.Bold(13F); lblMonth.TextAlign = ContentAlignment.MiddleCenter;

        btnPrev.Click += (s, e) => { currentMonth = currentMonth.AddMonths(-1); Render(); };
        btnNext.Click += (s, e) => { currentMonth = currentMonth.AddMonths(1); Render(); };
        pnlTop.Controls.AddRange(new Control[] { btnPrev, lblMonth, btnNext });

        grid.Dock = DockStyle.Fill;
        grid.ColumnCount = 7;
        grid.RowCount = 7;
        grid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        for (int c = 0; c < 7; c++) grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7f));
        grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        for (int r = 1; r < 7; r++) grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6f));

        Controls.Add(grid);
        Controls.Add(pnlTop);
        Render();
    }

    void Render()
    {
        lblMonth.Text = currentMonth.ToString("MMMM yyyy");
        grid.SuspendLayout();
        grid.Controls.Clear();

        string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        for (int c = 0; c < 7; c++)
            grid.Controls.Add(new Label { Text = days[c], Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = Theme.Bold(9.5F), BackColor = Theme.Navy, ForeColor = Color.White }, c, 0);

        int startCol = (int)currentMonth.DayOfWeek;
        int daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);

        for (int d = 1; d <= daysInMonth; d++)
        {
            int idx = startCol + d - 1;
            int col = idx % 7;
            int row = (idx / 7) + 1;
            var date = new DateTime(currentMonth.Year, currentMonth.Month, d);

            var cell = new Panel { Dock = DockStyle.Fill, BackColor = date == DateTime.Today ? ColorTranslator.FromHtml("#EBF5FF") : Color.White, Padding = new Padding(4) };
            var lblDay = new Label { Text = d.ToString(), Dock = DockStyle.Top, Height = 18, Font = Theme.Bold(9F), ForeColor = Theme.Navy };
            cell.Controls.Add(lblDay);

            var dayBookings = bookings.Where(b => date >= b.Start.Date && date <= b.End.Date).Take(3).ToList();
            foreach (var b in dayBookings)
            {
                var tag = new Label
                {
                    Text = $"{b.CarName.Split(' ').Last()} ({b.OtherName.Split(' ').First()})",
                    Dock = DockStyle.Top,
                    Height = 18,
                    Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                    BackColor = b.Status == BookingStatus.Active ? Theme.Orange : b.Status == BookingStatus.Approved ? Theme.Blue : Theme.Green,
                    ForeColor = Color.White,
                    Margin = new Padding(0, 2, 0, 0)
                };
                cell.Controls.Add(tag);
                tag.BringToFront();
            }
            grid.Controls.Add(cell, col, row);
        }
        grid.ResumeLayout();
    }
}
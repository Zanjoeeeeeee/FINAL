using System.Text.RegularExpressions;

namespace DriveMate;

public partial class MainForm : ThemedForm
{
    User me;
    public bool LoggedOut { get; private set; }
    List<Car> browseCars = new(), myCars = new();
    List<Booking> trips = new(), reqs = new(), disputes = new();
    List<User> users = new();
    string category = "All";

    // my account tab
    TextBox txtProfName = new(), txtProfEmail = new(), txtProfPhone = new(), txtCurPass = new(), txtNewPass = new(), txtConfPass = new();
    Label lblProfStatus = new();
    Button btnResubmitDocs = new();

    // how it works tab
    TabPage tabHow = new() { Text = "How It Works", Name = "tabHow", Padding = new Padding(0) };

    public MainForm(User user)
    {
        me = user;
        InitializeComponent();
        picBrand.Image = Brand.Logo;
        dtpFrom.MinDate = DateTime.Today; dtpTo.MinDate = DateTime.Today;
        dtpFrom.Value = DateTime.Today.AddDays(1); dtpTo.Value = DateTime.Today.AddDays(3);

        BuildProfileTab();
        BuildHowItWorksTab();
        RefreshMe();

        switch (me.Role)
        {
            case Role.Renter:
                tabs.TabPages.Remove(tabMyCars); tabs.TabPages.Remove(tabRequests);
                tabs.TabPages.Remove(tabVerify); tabs.TabPages.Remove(tabDisputes);
                tabs.TabPages.Remove(tabAudit); tabs.TabPages.Remove(tabStats);
                BuildChips(); LoadCars(); LoadTrips();
                break;
            case Role.Host:
                tabs.TabPages.Remove(tabBrowse); tabs.TabPages.Remove(tabTrips);
                tabs.TabPages.Remove(tabVerify); tabs.TabPages.Remove(tabDisputes);
                tabs.TabPages.Remove(tabAudit); tabs.TabPages.Remove(tabStats);
                LoadMyCars(); LoadReqs();
                break;
            case Role.Admin:
                tabs.TabPages.Remove(tabBrowse); tabs.TabPages.Remove(tabTrips);
                tabs.TabPages.Remove(tabMyCars); tabs.TabPages.Remove(tabRequests);
                cboVerifyFilter.SelectedIndex = 0; LoadDisputes(); LoadAudit(); LoadStats();
                break;
        }

        // 'How it works' sits right after the first tab
        tabs.TabPages.Insert(Math.Min(1, tabs.TabPages.Count), tabHow);

        // hide my account tab
        tabs.TabPages.Remove(tabProfile);
        tabs.SelectedIndexChanged += (s, e) =>
        {
            if (tabs.SelectedTab != tabProfile && tabs.TabPages.Contains(tabProfile))
                tabs.TabPages.Remove(tabProfile);
        };
    }

    private void btnProfile_Click(object? sender, EventArgs e)
    {
        if (!tabs.TabPages.Contains(tabProfile))
            tabs.TabPages.Add(tabProfile);
        tabs.SelectedTab = tabProfile;
    }

    void RefreshMe()
    {
        me = Database.GetUserById(me.Id) ?? me;
        var badge = me.Role == Role.Admin ? "Admin" : me.Verification switch
        {
            VerifStatus.Verified => "✔ Verified",
            VerifStatus.Rejected => "✖ Rejected",
            _ => "⏳ Pending verification"
        };
        var trust = me.Role == Role.Renter && me.RatingCount > 0 ? $"  ·  ★ {me.AvgRating:0.0} ({me.RatingCount})" : "";
        lblUser.Text = $"{me.FullName}  ·  {me.Role}  ·  {badge}{trust}";
        lblProfStatus.Text = me.Verification switch
        {
            VerifStatus.Verified => $"Verified ({me.IdType})",
            VerifStatus.Rejected => $"Rejected – {me.VerificationNote}",
            _ => $"Pending admin review ({me.IdType})"
        };
        btnResubmitDocs.Visible = me.Role != Role.Admin && me.Verification == VerifStatus.Rejected;
        UpdateNotifBadge();
    }

    void UpdateNotifBadge()
    {
        int unread = Database.UnreadNotificationCount(me.Id);
        btnNotifs.Text = unread > 0 ? $"🔔 Alerts ({unread})" : "🔔 Alerts";
        btnNotifs.Tag = unread > 0 ? "accent" : "ghost";
        Theme.StyleButton(btnNotifs);
    }

    private void btnNotifs_Click(object? sender, EventArgs e)
    {
        var list = Database.GetNotifications(me.Id);
        Database.MarkNotificationsRead(me.Id);
        UpdateNotifBadge();
        if (list.Count == 0) { Ui.Info("No notifications yet."); return; }
        var text = string.Join("\n\n", list.Take(12).Select(n =>
            $"{(n.IsRead ? "" : "[NEW] ")}{n.Title} ({DateTime.SpecifyKind(n.CreatedAt, DateTimeKind.Utc).ToLocalTime():MMM d h:mm tt})\n{n.Body}"));
        Ui.Info(text);
    }

    bool RequireVerified(string action)
    {
        RefreshMe();
        if (me.Verification == VerifStatus.Verified) return true;
        Ui.Info(me.Verification == VerifStatus.Pending
            ? $"You can't {action} yet — your ID is still waiting for admin verification."
            : $"You can't {action} — your verification was rejected ({me.VerificationNote}). Open 'My account' to upload new photos.");
        return false;
    }

    static T? Sel<T>(DataGridView g, List<T> list) where T : class =>
        g.CurrentRow is { Index: >= 0 } r && r.Index < list.Count ? list[r.Index] : null;

    private void btnLogout_Click(object? sender, EventArgs e) { LoggedOut = true; Close(); }

    // ================= RENTER: BROWSE =================
    void BuildChips()
    {
        foreach (var name in new[] { "All", "Favorites" }.Concat(Ui.Categories))
        {
            var chip = new RadioButton
            {
                Text = name == "All" ? "All cars" : name == "Favorites" ? "♥ Saved" : name,
                Tag = name,
                Appearance = Appearance.Button,
                AutoSize = true,
                Padding = new Padding(14, 4, 14, 4),
                Margin = new Padding(0, 0, 8, 0),
                FlatStyle = FlatStyle.Flat,
                Font = Theme.Bold(9.5F),
                Cursor = Cursors.Hand,
                Checked = name == "All",
                TextAlign = ContentAlignment.MiddleCenter
            };
            chip.FlatAppearance.BorderColor = Theme.Line; chip.FlatAppearance.CheckedBackColor = Theme.Blue;
            StyleChip(chip);
            chip.CheckedChanged += (s, e) =>
            {
                StyleChip(chip);
                if (chip.Checked) { category = (string)chip.Tag!; LoadCars(); }
            };
            flowChips.Controls.Add(chip);
        }
    }

    static void StyleChip(RadioButton r)
    {
        r.BackColor = r.Checked ? Theme.Red : Color.White;
        r.ForeColor = r.Checked ? Color.White : Theme.Red;
    }

    void LoadCars()
    {
        DateTime? from = chkDates.Checked ? dtpFrom.Value.Date : null;
        DateTime? to = chkDates.Checked ? dtpTo.Value.Date : null;
        var sort = cboSort.SelectedIndex switch { 1 => "priceDesc", 2 => "rating", _ => "priceAsc" };
        bool favOnly = category == "Favorites";
        browseCars = Database.SearchCars(me.Id, cboCity.Text, cboGear.Text, favOnly ? "All" : category, nudMax.Value, from, to, sort, favOnly);
        var covers = Database.GetCovers(browseCars.Select(c => c.Id));

        flowCars.SuspendLayout();
        flowCars.Controls.Clear();
        foreach (var car in browseCars)
        {
            covers.TryGetValue(car.Id, out var img);
            var card = new CarCard(car, img);
            card.BookClicked += (s, c) => Book(c);
            card.FavoriteToggled += (s, c) =>
            {
                c.IsFavorite = Database.ToggleFavorite(me.Id, c.Id);
                card.UpdateFav();
                if (category == "Favorites" && !c.IsFavorite) LoadCars();
            };
            flowCars.Controls.Add(card);
        }
        flowCars.ResumeLayout();
        lblResults.Text = browseCars.Count == 0
            ? (favOnly ? "No saved cars yet — click the ♥ icon on any car to save it." : "No cars match your filters — try clearing the date filter or raising the max price.")
            : $"{browseCars.Count} car(s) available" + (category == "All" ? "" : favOnly ? " in Saved" : $" in {category}");
    }

    private void btnSearch_Click(object? sender, EventArgs e)
    {
        if (chkDates.Checked && dtpTo.Value.Date <= dtpFrom.Value.Date) { Ui.Info("Return date must be after pick-up date."); return; }
        LoadCars();
    }

    private void btnReset_Click(object? sender, EventArgs e)
    {
        cboCity.SelectedIndex = 0; cboGear.SelectedIndex = 0; cboSort.SelectedIndex = 0; nudMax.Value = 20000; chkDates.Checked = false;
        foreach (var r in flowChips.Controls.OfType<RadioButton>()) r.Checked = (string)r.Tag! == "All";
        category = "All"; LoadCars();
    }

    void Book(Car car)
    {
        if (!RequireVerified("book a car")) return;
        using var d = new BookDialog(car);
        if (d.ShowDialog(this) == DialogResult.OK && d.Result != null)
        {
            d.Result.RenterId = me.Id;
            Database.AddBooking(d.Result);
            Database.Notify(car.HostId, "New Booking Request", $"{me.FullName} requested {car} ({d.Result.Start:MMM d}–{d.Result.End:MMM d}).");
            LoadTrips();
            Ui.Info("Request sent! Once the host approves, use 'Pay now' in My trips.");
        }
    }

    // ================= RENTER: TRIPS =================
    void LoadTrips()
    {
        trips = Database.BookingsForRenter(me.Id);
        gridTrips.DataSource = trips.Select(b => new TripRow(
            b.CarName, b.OtherName, b.Start.ToString("MMM d, yyyy"), b.End.ToString("MMM d, yyyy"), b.Days,
            b.Total.ToString("₱#,##0"),
            b.InsuranceTier == "None" ? "None" : $"{b.InsuranceTier} (₱{b.InsuranceFee:N0})",
            b.Deposit.ToString("₱#,##0"), b.Status.ToString(),
            b.IsPaid ? $"{b.PaymentMethod} ({b.PaymentRef})" : "Unpaid",
            b.Status is BookingStatus.Completed or BookingStatus.Disputed or BookingStatus.Cancelled
                ? $"₱{b.Deductions:N0} ded. / ₱{b.Refund:N0} ref." : "",
            b.Rating > 0 ? new string('★', b.Rating) : ""
        )).ToList();
    }

    private void btnPay_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridTrips, trips);
        if (b == null) return;
        if (b.IsPaid) { Ui.Info("This booking is already paid."); return; }
        if (b.Status != BookingStatus.Approved) { Ui.Info("You can pay once the host has approved your request."); return; }

        using var d = new PaymentDialog(b, me);
        if (d.ShowDialog(this) != DialogResult.OK) return;

        b.PaymentMethod = d.Method;
        b.PaymentRef = d.Reference;
        b.PaidAt = DateTime.Now;
        Database.SaveBooking(b);
        Database.Notify(b.HostId, "Booking Paid (Xendit)", $"{me.FullName} paid ₱{b.AmountDue:N0} for {b.CarName}. Ready for pickup!");
        LoadTrips();
        Ui.Info($"Xendit Hosted Checkout confirmed!\nMethod: {b.PaymentMethod}\nReference: {b.PaymentRef}");
    }

    private void btnCancelTrip_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridTrips, trips);
        if (b == null) return;
        if (b.Status != BookingStatus.Pending && b.Status != BookingStatus.Approved) { Ui.Info("Only pending or approved bookings can be cancelled."); return; }

        var (refundAmt, policyNote) = Pricing.ComputeCancellationRefund(b);
        if (!Ui.Ask($"Cancel this booking?\n\n{policyNote}" + (b.IsPaid ? $"\nRefund amount: ₱{refundAmt:N0}" : ""))) return;

        if (b.IsPaid && refundAmt > 0)
        {
            b.RefundRef = Pay.Gateway.Refund(b.PaymentMethod, b.PaymentRef, refundAmt);
            b.Refund = refundAmt;
            b.HostPayout = Math.Max(0, (b.Total + b.InsuranceFee) - (refundAmt - b.Deposit));
        }
        b.Status = BookingStatus.Cancelled;
        Database.SaveBooking(b);
        Database.Notify(b.HostId, "Booking Cancelled", $"{me.FullName} cancelled {b.CarName}. {policyNote}");
        LoadTrips();
    }

    private void btnTripChat_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridTrips, trips);
        if (b == null) return;
        using var d = new ChatDialog(b, me);
        d.ShowDialog(this);
    }

    private void btnDispute_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridTrips, trips);
        if (b == null) return;
        if (b.Status != BookingStatus.Completed || b.Deductions <= 0)
        { Ui.Info("You can only dispute a completed trip that has deposit deductions."); return; }

        using var d = new DisputeDialog(b, isAdminResolve: false);
        if (d.ShowDialog(this) != DialogResult.OK) return;

        b.Status = BookingStatus.Disputed;
        b.DisputeReason = d.Reason;
        Database.SaveBooking(b);
        if (d.Photos.Count > 0) Database.SaveDisputePhotos(b.Id, "Renter", d.Photos);
        Database.Notify(b.HostId, "Deposit Deduction Disputed", $"{me.FullName} disputed ₱{b.Deductions:N0} deductions on {b.CarName}. Admin will review.");
        LoadTrips();
        Ui.Info("Dispute submitted with your evidence. An admin will review the pickup, return, and dispute photos.");
    }

    private void btnRate_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridTrips, trips);
        if (b == null) return;
        if (b.Status != BookingStatus.Completed || b.Rating > 0) { Ui.Info("You can rate once after a completed trip."); return; }
        using var d = new ReviewDialog($"Rate your trip – {b.CarName}");
        if (d.ShowDialog(this) != DialogResult.OK) return;
        b.Rating = d.Rating; b.Review = d.Comment;
        Database.SaveBooking(b);
        Database.Notify(b.HostId, "New Trip Review", $"{me.FullName} left a {new string('★', d.Rating)} review on {b.CarName}.");
        LoadTrips();
    }

    private void btnTripPhotos_Click(object? sender, EventArgs e) => ShowPhotos(Sel(gridTrips, trips));

    // ================= HOST: CARS & REQUESTS =================
    void LoadMyCars()
    {
        myCars = Database.CarsByHost(me.Id);
        gridMyCars.DataSource = myCars.Select(c => new HostCarRow(
            c.ToString(), c.Category, c.Plate, c.Transmission, c.Seats, c.Location,
            c.PricePerDay.ToString("₱#,##0"), c.Deposit.ToString("₱#,##0"), $"{c.PhotoCount} photo(s)",
            c.Available ? "Listed" : "Hidden")).ToList();
    }

    private void btnAddCar_Click(object? sender, EventArgs e)
    {
        if (!RequireVerified("list a car")) return;
        using var d = new CarDialog(null, me.Id);
        if (d.ShowDialog(this) != DialogResult.OK || d.Result == null) return;
        Database.SaveCar(d.Result);
        Database.SaveCarPhotos(d.Result.Id, d.Photos);
        LoadMyCars();
    }

    private void btnEditCar_Click(object? sender, EventArgs e)
    {
        var c = Sel(gridMyCars, myCars);
        if (c == null) return;
        using var d = new CarDialog(c, me.Id);
        if (d.ShowDialog(this) != DialogResult.OK || d.Result == null) return;
        Database.SaveCar(d.Result);
        Database.SaveCarPhotos(d.Result.Id, d.Photos);
        LoadMyCars();
    }

    private void btnToggleCar_Click(object? sender, EventArgs e)
    {
        var c = Sel(gridMyCars, myCars);
        if (c == null) return;
        c.Available = !c.Available;
        Database.SaveCar(c); LoadMyCars();
    }

    private void btnDelCar_Click(object? sender, EventArgs e)
    {
        var c = Sel(gridMyCars, myCars);
        if (c == null) return;
        if (!Database.DeleteCar(c.Id)) { Ui.Info("This car already has bookings — hide it instead."); return; }
        LoadMyCars();
    }

    void LoadReqs()
    {
        reqs = Database.BookingsForHost(me.Id);
        gridReqs.DataSource = reqs.Select(b => new HostReqRow(
            b.CarName,
            b.OtherRatingCount > 0 ? $"{b.OtherName} (★ {b.OtherRating:0.0})" : $"{b.OtherName} (New)",
            b.Start.ToString("MMM d"), b.End.ToString("MMM d"), b.Days,
            b.Total.ToString("₱#,##0"), b.InsuranceTier, b.Status.ToString(),
            b.IsPaid ? "Paid" : "Unpaid",
            b.Status == BookingStatus.Completed ? $"₱{b.HostPayout:N0} ({(b.PayoutReleased ? "Released" : "Pending")})" : "—",
            b.RenterRating > 0 ? new string('★', b.RenterRating) : ""
        )).ToList();

        var released = reqs.Where(b => b.Status == BookingStatus.Completed && b.PayoutReleased).Sum(b => b.HostPayout);
        var pendingPayout = reqs.Where(b => b.Status == BookingStatus.Completed && !b.PayoutReleased).Sum(b => b.HostPayout);
        var upcoming = reqs.Where(b => b.Status is BookingStatus.Approved or BookingStatus.Active).Sum(b => b.Total);
        lblEarnings.Text = $"Released Payouts: ₱{released:N0}   ·   Pending Payout: ₱{pendingPayout:N0}   ·   Upcoming: ₱{upcoming:N0}   ·   Requests: {reqs.Count(b => b.Status == BookingStatus.Pending)}";
    }

    private void btnApprove_Click(object? sender, EventArgs e)
    {
        if (!RequireVerified("approve bookings")) return;
        var b = Sel(gridReqs, reqs);
        if (b == null || b.Status != BookingStatus.Pending) return;
        if (Database.HasClash(b.CarId, b.Start, b.End, b.Id)) { Ui.Info("Another booking overlaps those dates."); return; }
        b.Status = BookingStatus.Approved;
        Database.SaveBooking(b);
        Database.Notify(b.RenterId, "Booking Approved", $"{me.FullName} approved your booking for {b.CarName}! Open My trips to pay.");
        LoadReqs();
    }

    private void btnDecline_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridReqs, reqs);
        if (b == null || b.Status != BookingStatus.Pending) return;
        b.Status = BookingStatus.Declined;
        Database.SaveBooking(b);
        Database.Notify(b.RenterId, "Booking Declined", $"{me.FullName} declined your request for {b.CarName}.");
        LoadReqs();
    }

    private void btnStart_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridReqs, reqs);
        if (b == null) return;
        if (b.Status != BookingStatus.Approved) { Ui.Info("Approve the booking first."); return; }
        if (!b.IsPaid) { Ui.Info("The renter hasn't paid yet."); return; }
        using var d = new InspectionDialog("Pickup", null);
        if (d.ShowDialog(this) != DialogResult.OK) return;
        d.Result.BookingId = b.Id;
        Database.SaveInspection(d.Result);
        b.Status = BookingStatus.Active;
        Database.SaveBooking(b);
        Database.Notify(b.RenterId, "Trip Started", $"Pick-up inspection logged for {b.CarName}. Drive safely!");
        LoadReqs();
    }

    private void btnEnd_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridReqs, reqs);
        if (b == null) return;
        if (b.Status != BookingStatus.Active) { Ui.Info("Start the trip at pick-up first."); return; }
        using var d = new InspectionDialog("Return", b);
        if (d.ShowDialog(this) != DialogResult.OK) return;
        d.Result.BookingId = b.Id;
        Database.SaveInspection(d.Result);
        b.Return = d.Result;

        var (lines, total) = Charges.Compute(b);
        b.Deductions = total;
        b.Refund = b.Deposit - total;
        b.HostPayout = b.Total + b.Deductions;
        b.PayoutReleased = total == 0;
        if (b.Refund > 0) b.RefundRef = Pay.Gateway.Refund(b.PaymentMethod, b.PaymentRef, b.Refund);
        b.Status = BookingStatus.Completed;
        Database.SaveBooking(b);

        Database.Notify(b.RenterId, "Trip Completed & Deposit Settled",
            total > 0
                ? $"{b.CarName} returned with ₱{total:N0} deductions (Refund: ₱{b.Refund:N0}). You may file a Dispute in My trips if you disagree."
                : $"{b.CarName} returned with ₱0 deductions! Full ₱{b.Deposit:N0} deposit refunded.");
        LoadReqs();

        var msg = lines.Count == 0
            ? $"No deductions — full ₱{b.Deposit:N0} deposit refunded to renter ({b.RefundRef}).\nHost payout ₱{b.HostPayout:N0} released."
            : "Deductions:\n• " + string.Join("\n• ", lines) + $"\n\nTotal deducted: ₱{total:N0}\nRefunded: ₱{b.Refund:N0} ({b.RefundRef})";
        Ui.Info(msg);
    }

    private void btnReqChat_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridReqs, reqs);
        if (b == null) return;
        using var d = new ChatDialog(b, me);
        d.ShowDialog(this);
    }

    private void btnRateRenter_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridReqs, reqs);
        if (b == null) return;
        if (b.Status != BookingStatus.Completed || b.RenterRating > 0)
        { Ui.Info("You can rate the renter once after a completed trip."); return; }
        using var d = new ReviewDialog($"Rate renter – {b.OtherName}");
        if (d.ShowDialog(this) != DialogResult.OK) return;
        b.RenterRating = d.Rating; b.RenterReview = d.Comment;
        Database.SaveBooking(b);
        Database.Notify(b.RenterId, "New Host Review", $"{me.FullName} rated you {new string('★', d.Rating)} for your {b.CarName} trip.");
        LoadReqs();
    }

    private void btnCalendar_Click(object? sender, EventArgs e)
    {
        using var f = new HostCalendarForm(reqs);
        f.ShowDialog(this);
    }

    private void btnReqPhotos_Click(object? sender, EventArgs e) => ShowPhotos(Sel(gridReqs, reqs));

    void ShowPhotos(Booking? b)
    {
        if (b == null) return;
        if (b.Pickup is null && b.Return is null) { Ui.Info("No inspections recorded yet."); return; }
        using var f = new PhotoViewerForm($"Inspection photos – {b.CarName}",
            ("Pick-up", b.Pickup == null ? null : Database.GetPhotos(b.Pickup.Id), "Not recorded yet"),
            ("Return", b.Return == null ? null : Database.GetPhotos(b.Return.Id), "Not recorded yet"));
        f.ShowDialog(this);
    }

    // ================= ADMIN: USERS, DISPUTES, AUDIT & STATS =================
    private void cboVerifyFilter_SelectedIndexChanged(object? sender, EventArgs e) => LoadUsers();

    void LoadUsers()
    {
        int f = cboVerifyFilter.SelectedIndex == 4 ? -1 : cboVerifyFilter.SelectedIndex;
        users = Database.UsersForAdmin(f);
        gridUsers.DataSource = users.Select(u => new AdminUserRow(
            u.FullName, u.Email, u.Role.ToString(), u.IdType,
            u.IsDisabled ? "DISABLED" : u.Verification.ToString(),
            u.IsDisabled ? u.DisableReason : u.VerificationNote,
            DateTime.SpecifyKind(u.CreatedAt, DateTimeKind.Utc).ToLocalTime().ToString("MMM d, yyyy"))).ToList();
    }

    private void btnViewDocs_Click(object? sender, EventArgs e)
    {
        var u = Sel(gridUsers, users);
        if (u == null) return;
        var (id, selfie) = Database.GetUserDocs(u.Id);
        if (id == null && selfie == null) { Ui.Info("This account has no uploaded documents."); return; }
        using var f = new PhotoViewerForm($"{u.FullName} · {u.Role} · {u.IdType}",
            ("Government / Driver ID", id == null ? null : new List<PhotoItem> { id }, "Not uploaded"),
            ("Selfie", selfie == null ? null : new List<PhotoItem> { selfie }, "Not uploaded"));
        f.ShowDialog(this);
    }

    private void btnApproveUser_Click(object? sender, EventArgs e)
    {
        var u = Sel(gridUsers, users);
        if (u == null) return;
        Database.SetVerification(u.Id, VerifStatus.Verified, "", me.FullName);
        LoadUsers(); LoadStats(); LoadAudit();
    }

    private void btnRejectUser_Click(object? sender, EventArgs e)
    {
        var u = Sel(gridUsers, users);
        if (u == null) return;
        using var d = new ReasonDialog();
        if (d.ShowDialog(this) != DialogResult.OK) return;
        Database.SetVerification(u.Id, VerifStatus.Rejected, d.Reason, me.FullName);
        LoadUsers(); LoadStats(); LoadAudit();
    }

    private void btnToggleDisable_Click(object? sender, EventArgs e)
    {
        var u = Sel(gridUsers, users);
        if (u == null) return;
        if (u.IsDisabled)
        {
            if (!Ui.Ask($"Re-enable {u.FullName}'s account?")) return;
            Database.SetUserDisabled(u.Id, false, "", me.FullName);
        }
        else
        {
            using var d = new ReasonDialog { Text = "Disable user account" };
            if (d.ShowDialog(this) != DialogResult.OK) return;
            Database.SetUserDisabled(u.Id, true, d.Reason, me.FullName);
        }
        LoadUsers(); LoadStats(); LoadAudit();
    }

    void LoadDisputes()
    {
        disputes = Database.DisputedBookings();
        gridDisputes.DataSource = disputes.Select(b => new DisputeRow(
            b.CarName, b.OtherName, $"{b.Start:MMM d}–{b.End:MMM d}",
            b.Deposit.ToString("₱#,##0"), b.Deductions.ToString("₱#,##0"), b.InsuranceTier, b.DisputeReason
        )).ToList();
    }

    private void btnDisputePhotos_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridDisputes, disputes);
        if (b == null) return;
        using var f = new PhotoViewerForm($"Dispute Evidence – {b.CarName}",
            ("Pickup inspection", b.Pickup == null ? null : Database.GetPhotos(b.Pickup.Id), "None"),
            ("Return inspection", b.Return == null ? null : Database.GetPhotos(b.Return.Id), "None"),
            ("Renter dispute evidence", Database.GetDisputePhotos(b.Id), "No extra dispute photos uploaded."));
        f.ShowDialog(this);
    }

    private void btnResolveDispute_Click(object? sender, EventArgs e)
    {
        var b = Sel(gridDisputes, disputes);
        if (b == null) return;
        using var d = new DisputeDialog(b, isAdminResolve: true);
        if (d.ShowDialog(this) != DialogResult.OK) return;

        decimal prevRefund = b.Refund;
        b.Deductions = d.FinalDeduction;
        b.Refund = b.Deposit - b.Deductions;
        b.HostPayout = b.Total + b.Deductions;
        b.PayoutReleased = true;
        b.DisputeResolution = d.Reason;
        b.Status = BookingStatus.Completed;

        decimal extraRefund = Math.Max(0, b.Refund - prevRefund);
        if (extraRefund > 0)
            b.RefundRef = Pay.Gateway.Refund(b.PaymentMethod, b.PaymentRef, extraRefund);

        Database.SaveBooking(b);
        Database.LogAudit(me.FullName, "Resolved Dispute", b.CarName, $"Final deduction ₱{b.Deductions:N0} · {d.Reason}");
        Database.Notify(b.RenterId, "Damage Dispute Resolved", $"Admin settled dispute on {b.CarName}: Deductions ₱{b.Deductions:N0}, Refund ₱{b.Refund:N0}. Note: {d.Reason}");
        Database.Notify(b.HostId, "Damage Dispute Resolved", $"Admin settled dispute on {b.CarName}: Final payout ₱{b.HostPayout:N0} released. Note: {d.Reason}");
        LoadDisputes(); LoadStats(); LoadAudit();
    }

    void LoadAudit()
    {
        var logs = Database.GetAuditLogs();
        gridAudit.DataSource = logs.Select(l => new AuditRow(
            DateTime.SpecifyKind(l.CreatedAt, DateTimeKind.Utc).ToLocalTime().ToString("MMM d, yyyy h:mm tt"),
            l.AdminName, l.Action, l.TargetName, l.Details
        )).ToList();
    }

    void LoadStats()
    {
        var s = Database.GetStats();
        flowStats.Controls.Clear();
        (string, string, Color)[] tiles =
        {
            ("Users", s.Users.ToString(), Theme.Blue),
            ("Hosts / Renters", $"{s.Hosts} / {s.Renters}", Theme.Navy),
            ("Pending verify", s.Pending.ToString(), Theme.Orange),
            ("Disabled users", s.Disabled.ToString(), Theme.Red),
            ("Cars listed", s.Cars.ToString(), Theme.Blue),
            ("Open disputes", s.Disputed.ToString(), Theme.Red),
            ("Completed trips", s.Completed.ToString(), Theme.Green),
            ("Gross volume", $"₱{s.Revenue:N0}", Theme.Green),
        };
        foreach (var (k, v, col) in tiles)
        {
            var card = new Panel { Size = new Size(210, 108), BackColor = Color.White, Margin = new Padding(0, 0, 14, 14), Padding = new Padding(16, 12, 16, 12) };
            var bar = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = col };
            var l1 = new Label { Text = k, Dock = DockStyle.Top, Height = 24, ForeColor = Theme.Muted };
            var l2 = new Label { Text = v, Dock = DockStyle.Fill, Font = Theme.Bold(20F), ForeColor = Theme.Navy };
            card.Controls.AddRange(new Control[] { l2, l1, bar });
            flowStats.Controls.Add(card);
        }
    }

    // ================= HOW IT WORKS =================
    void BuildHowItWorksTab()
    {
        tabHow.UseVisualStyleBackColor = false;
        tabHow.BackColor = Theme.Ink;
        tabHow.Controls.Add(new Works());
    }

    // ================= MY ACCOUNT =================
    void BuildProfileTab()
    {
        var wrap = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };

        var gInfo = new GroupBox { Text = "Account details", Size = new Size(460, 295) };
        AddField(gInfo, "Full name", txtProfName, 26); txtProfName.Text = me.FullName;
        AddField(gInfo, "Email", txtProfEmail, 82); txtProfEmail.Text = me.Email;
        AddField(gInfo, "Mobile", txtProfPhone, 138); txtProfPhone.Text = me.Phone;
        lblProfStatus.Location = new Point(16, 198); lblProfStatus.Size = new Size(428, 22); lblProfStatus.Font = Theme.Bold(9F);
        var btnSave = new Button { Text = "Save details", Location = new Point(16, 232), Size = new Size(135, 36) };
        btnSave.Click += (s, e) => SaveProfile();
        btnResubmitDocs = new Button { Text = "Resubmit ID…", Location = new Point(160, 232), Size = new Size(135, 36), Tag = "accent" };
        btnResubmitDocs.Click += (s, e) => ResubmitDocs();
        gInfo.Controls.AddRange(new Control[] { lblProfStatus, btnSave, btnResubmitDocs });

        var gPass = new GroupBox { Text = "Change password", Size = new Size(460, 255), Margin = new Padding(0, 14, 0, 0) };
        AddField(gPass, "Current password", txtCurPass, 26, true);
        AddField(gPass, "New password", txtNewPass, 82, true);
        AddField(gPass, "Confirm new password", txtConfPass, 138, true);
        var btnPass = new Button { Text = "Update password", Location = new Point(16, 196), Size = new Size(155, 36) };
        btnPass.Click += (s, e) => ChangePassword();
        gPass.Controls.Add(btnPass);

        wrap.Controls.AddRange(new Control[] { gInfo, gPass });
        tabProfile.Controls.Add(wrap);
    }

    static void AddField(GroupBox g, string label, TextBox tb, int y, bool pass = false)
    {
        var l = new Label { Text = label, AutoSize = true, Location = new Point(16, y) };
        tb.Location = new Point(16, y + 20); tb.Size = new Size(428, 27); tb.UseSystemPasswordChar = pass;
        g.Controls.AddRange(new Control[] { l, tb });
    }

    void SaveProfile()
    {
        var mail = txtProfEmail.Text.Trim().ToLowerInvariant();
        if (txtProfName.Text.Trim().Length < 2) { Ui.Info("Please enter your full name."); return; }
        if (!mail.Contains('@') || !mail.Contains('.')) { Ui.Info("Please enter a valid email address."); return; }
        if (txtProfPhone.Text.Trim() != "" && !Regex.IsMatch(txtProfPhone.Text.Trim(), @"^\+?[\d\s-]{10,15}$"))
        { Ui.Info("Please enter a valid mobile number."); return; }
        if (Database.EmailTaken(mail, me.Id)) { Ui.Info("Another account already uses that email."); return; }
        Database.UpdateProfile(me.Id, txtProfName.Text.Trim(), mail, txtProfPhone.Text.Trim());
        RefreshMe();
        Ui.Info("Your details were updated.");
    }

    void ResubmitDocs()
    {
        using var d = new DocsDialog(me.Role);
        if (d.ShowDialog(this) != DialogResult.OK) return;
        Database.SaveUserDocs(me.Id, d.IdType, d.IdPhoto!, d.Selfie!);
        RefreshMe();
        Ui.Info("New documents uploaded and sent for verification.");
    }

    void ChangePassword()
    {
        if (!Security.Verify(txtCurPass.Text, me.PasswordHash)) { Ui.Info("Your current password is incorrect."); return; }
        if (txtNewPass.Text.Length < 6) { Ui.Info("New password must be at least 6 characters."); return; }
        if (txtNewPass.Text != txtConfPass.Text) { Ui.Info("The new passwords do not match."); return; }
        Database.UpdatePassword(me.Id, Security.Hash(txtNewPass.Text));
        txtCurPass.Clear(); txtNewPass.Clear(); txtConfPass.Clear();
        RefreshMe();
        Ui.Info("Password updated.");
    }

    record TripRow(string Car, string Host, string From, string To, int Days, string Total, string Insurance, string Deposit, string Status, string Payment, string Settlement, string Rated);
    record HostCarRow(string Car, string Category, string Plate, string Gearbox, int Seats, string City, string PricePerDay, string Deposit, string Photos, string Status);
    record HostReqRow(string Car, string Renter, string From, string To, int Days, string Total, string Insurance, string Status, string Paid, string Payout, string RenterRated);
    record AdminUserRow(string Name, string Email, string Role, string IdType, string Status, string Note, string Joined);
    record DisputeRow(string Car, string Parties, string Dates, string Deposit, string ClaimedDeduction, string Insurance, string Reason);
    record AuditRow(string Timestamp, string Admin, string Action, string Target, string Details);
}
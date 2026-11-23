#nullable enable
namespace DriveMate;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlTop = new Panel();
        picBrand = new PictureBox();
        lblUser = new Label();
        btnNotifs = new Button();
        btnProfile = new Button();
        btnLogout = new Button();
        tabs = new TabControl();
        tabBrowse = new TabPage();
        flowCars = new FlowLayoutPanel();
        lblResults = new Label();
        flowChips = new FlowLayoutPanel();
        pnlFilter = new Panel();
        lblCity = new Label();
        cboCity = new ComboBox();
        lblGear = new Label();
        cboGear = new ComboBox();
        lblMax = new Label();
        nudMax = new NumericUpDown();
        lblSort = new Label();
        cboSort = new ComboBox();
        chkDates = new CheckBox();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        btnSearch = new Button();
        btnReset = new Button();
        tabTrips = new TabPage();
        gridTrips = new DataGridView();
        pnlTripBtns = new FlowLayoutPanel();
        btnPay = new Button();
        btnCancelTrip = new Button();
        btnTripChat = new Button();
        btnDispute = new Button();
        btnTripPhotos = new Button();
        btnRate = new Button();
        tabMyCars = new TabPage();
        gridMyCars = new DataGridView();
        pnlCarBtns = new FlowLayoutPanel();
        btnAddCar = new Button();
        btnEditCar = new Button();
        btnToggleCar = new Button();
        btnDelCar = new Button();
        tabRequests = new TabPage();
        gridReqs = new DataGridView();
        pnlReqBtns = new FlowLayoutPanel();
        btnApprove = new Button();
        btnDecline = new Button();
        btnStart = new Button();
        btnEnd = new Button();
        btnReqChat = new Button();
        btnRateRenter = new Button();
        btnCalendar = new Button();
        btnReqPhotos = new Button();
        lblEarnings = new Label();
        tabVerify = new TabPage();
        gridUsers = new DataGridView();
        pnlVerifyBtns = new FlowLayoutPanel();
        btnViewDocs = new Button();
        btnApproveUser = new Button();
        btnRejectUser = new Button();
        btnToggleDisable = new Button();
        pnlVerifyFilter = new FlowLayoutPanel();
        lblVerifyFilter = new Label();
        cboVerifyFilter = new ComboBox();
        tabDisputes = new TabPage();
        gridDisputes = new DataGridView();
        pnlDisputeBtns = new FlowLayoutPanel();
        btnDisputePhotos = new Button();
        btnResolveDispute = new Button();
        tabAudit = new TabPage();
        gridAudit = new DataGridView();
        tabStats = new TabPage();
        flowStats = new FlowLayoutPanel();
        lblStatsTitle = new Label();
        tabProfile = new TabPage();
        pnlTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picBrand).BeginInit();
        tabs.SuspendLayout();
        tabBrowse.SuspendLayout();
        pnlFilter.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudMax).BeginInit();
        tabTrips.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridTrips).BeginInit();
        pnlTripBtns.SuspendLayout();
        tabMyCars.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridMyCars).BeginInit();
        pnlCarBtns.SuspendLayout();
        tabRequests.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridReqs).BeginInit();
        pnlReqBtns.SuspendLayout();
        tabVerify.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridUsers).BeginInit();
        pnlVerifyBtns.SuspendLayout();
        pnlVerifyFilter.SuspendLayout();
        tabDisputes.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridDisputes).BeginInit();
        pnlDisputeBtns.SuspendLayout();
        tabAudit.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridAudit).BeginInit();
        tabStats.SuspendLayout();
        SuspendLayout();
        // 
        // pnlTop
        // 
        pnlTop.BackColor = Color.FromArgb(16, 0, 1);
        pnlTop.Controls.Add(picBrand);
        pnlTop.Controls.Add(lblUser);
        pnlTop.Controls.Add(btnNotifs);
        pnlTop.Controls.Add(btnProfile);
        pnlTop.Controls.Add(btnLogout);
        pnlTop.Dock = DockStyle.Top;
        pnlTop.Location = new Point(0, 0);
        pnlTop.Name = "pnlTop";
        pnlTop.Size = new Size(1902, 62);
        pnlTop.TabIndex = 1;
        // 
        // picBrand
        // 
        picBrand.Location = new Point(16, 13);
        picBrand.Name = "picBrand";
        picBrand.Size = new Size(148, 36);
        picBrand.SizeMode = PictureBoxSizeMode.Zoom;
        picBrand.TabIndex = 0;
        picBrand.TabStop = false;
        // 
        // lblUser
        // 
        lblUser.AutoSize = true;
        lblUser.ForeColor = Color.FromArgb(203, 213, 225);
        lblUser.Location = new Point(176, 22);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(0, 20);
        lblUser.TabIndex = 1;
        // 
        // btnNotifs
        // 
        btnNotifs.Dock = DockStyle.Right;
        btnNotifs.Location = new Point(1537, 0);
        btnNotifs.Name = "btnNotifs";
        btnNotifs.Size = new Size(135, 62);
        btnNotifs.TabIndex = 2;
        btnNotifs.Tag = "ghost";
        btnNotifs.Text = "🔔 Alerts";
        btnNotifs.Click += btnNotifs_Click;
        // 
        // btnProfile
        // 
        btnProfile.Dock = DockStyle.Right;
        btnProfile.Location = new Point(1672, 0);
        btnProfile.Name = "btnProfile";
        btnProfile.Size = new Size(125, 62);
        btnProfile.TabIndex = 3;
        btnProfile.Tag = "ghost";
        btnProfile.Text = "My account";
        btnProfile.Click += btnProfile_Click;
        // 
        // btnLogout
        // 
        btnLogout.Dock = DockStyle.Right;
        btnLogout.Location = new Point(1797, 0);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(105, 62);
        btnLogout.TabIndex = 4;
        btnLogout.Tag = "ghost";
        btnLogout.Text = "Sign out";
        btnLogout.Click += btnLogout_Click;
        // 
        // tabs
        // 
        tabs.Controls.Add(tabBrowse);
        tabs.Controls.Add(tabTrips);
        tabs.Controls.Add(tabMyCars);
        tabs.Controls.Add(tabRequests);
        tabs.Controls.Add(tabVerify);
        tabs.Controls.Add(tabDisputes);
        tabs.Controls.Add(tabAudit);
        tabs.Controls.Add(tabStats);
        tabs.Controls.Add(tabProfile);
        tabs.Dock = DockStyle.Fill;
        tabs.Location = new Point(0, 62);
        tabs.Name = "tabs";
        tabs.SelectedIndex = 0;
        tabs.Size = new Size(1902, 993);
        tabs.TabIndex = 0;
        // 
        // tabBrowse
        // 
        tabBrowse.Controls.Add(flowCars);
        tabBrowse.Controls.Add(lblResults);
        tabBrowse.Controls.Add(flowChips);
        tabBrowse.Controls.Add(pnlFilter);
        tabBrowse.Location = new Point(4, 29);
        tabBrowse.Name = "tabBrowse";
        tabBrowse.Padding = new Padding(12);
        tabBrowse.Size = new Size(1894, 960);
        tabBrowse.TabIndex = 0;
        tabBrowse.Text = "Browse cars";
        // 
        // flowCars
        // 
        flowCars.AutoScroll = true;
        flowCars.Dock = DockStyle.Fill;
        flowCars.Location = new Point(12, 180);
        flowCars.Name = "flowCars";
        flowCars.Padding = new Padding(0, 6, 0, 12);
        flowCars.Size = new Size(1870, 768);
        flowCars.TabIndex = 0;
        // 
        // lblResults
        // 
        lblResults.Dock = DockStyle.Top;
        lblResults.Location = new Point(12, 154);
        lblResults.Name = "lblResults";
        lblResults.Size = new Size(1870, 26);
        lblResults.TabIndex = 1;
        lblResults.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // flowChips
        // 
        flowChips.Dock = DockStyle.Top;
        flowChips.Location = new Point(12, 108);
        flowChips.Name = "flowChips";
        flowChips.Padding = new Padding(2, 8, 0, 4);
        flowChips.Size = new Size(1870, 46);
        flowChips.TabIndex = 2;
        flowChips.WrapContents = false;
        // 
        // pnlFilter
        // 
        pnlFilter.BackColor = Color.White;
        pnlFilter.Controls.Add(lblCity);
        pnlFilter.Controls.Add(cboCity);
        pnlFilter.Controls.Add(lblGear);
        pnlFilter.Controls.Add(cboGear);
        pnlFilter.Controls.Add(lblMax);
        pnlFilter.Controls.Add(nudMax);
        pnlFilter.Controls.Add(lblSort);
        pnlFilter.Controls.Add(cboSort);
        pnlFilter.Controls.Add(chkDates);
        pnlFilter.Controls.Add(lblFrom);
        pnlFilter.Controls.Add(dtpFrom);
        pnlFilter.Controls.Add(lblTo);
        pnlFilter.Controls.Add(dtpTo);
        pnlFilter.Controls.Add(btnSearch);
        pnlFilter.Controls.Add(btnReset);
        pnlFilter.Dock = DockStyle.Top;
        pnlFilter.Location = new Point(12, 12);
        pnlFilter.Name = "pnlFilter";
        pnlFilter.Padding = new Padding(12, 8, 12, 8);
        pnlFilter.Size = new Size(1870, 96);
        pnlFilter.TabIndex = 3;
        // 
        // lblCity
        // 
        lblCity.AutoSize = true;
        lblCity.Location = new Point(12, 12);
        lblCity.Name = "lblCity";
        lblCity.Size = new Size(34, 20);
        lblCity.TabIndex = 0;
        lblCity.Text = "City";
        // 
        // cboCity
        // 
        cboCity.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCity.Items.AddRange(new object[] { "All", "Manila", "Makati", "Quezon City", "Taguig", "Pasig", "Cebu", "Davao", "Baguio", "Clark" });
        cboCity.Location = new Point(46, 8);
        cboCity.Name = "cboCity";
        cboCity.Size = new Size(125, 28);
        cboCity.TabIndex = 1;
        // 
        // lblGear
        // 
        lblGear.AutoSize = true;
        lblGear.Location = new Point(185, 12);
        lblGear.Name = "lblGear";
        lblGear.Size = new Size(65, 20);
        lblGear.TabIndex = 2;
        lblGear.Text = "Gearbox";
        // 
        // cboGear
        // 
        cboGear.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGear.Items.AddRange(new object[] { "Any", "Automatic", "Manual" });
        cboGear.Location = new Point(255, 8);
        cboGear.Name = "cboGear";
        cboGear.Size = new Size(115, 28);
        cboGear.TabIndex = 3;
        // 
        // lblMax
        // 
        lblMax.AutoSize = true;
        lblMax.Location = new Point(381, 12);
        lblMax.Name = "lblMax";
        lblMax.Size = new Size(80, 20);
        lblMax.TabIndex = 4;
        lblMax.Text = "Max ₱/day";
        // 
        // nudMax
        // 
        nudMax.Increment = new decimal(new int[] { 500, 0, 0, 0 });
        nudMax.Location = new Point(466, 8);
        nudMax.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
        nudMax.Name = "nudMax";
        nudMax.Size = new Size(95, 27);
        nudMax.TabIndex = 5;
        nudMax.Value = new decimal(new int[] { 20000, 0, 0, 0 });
        // 
        // lblSort
        // 
        lblSort.AutoSize = true;
        lblSort.Location = new Point(576, 12);
        lblSort.Name = "lblSort";
        lblSort.Size = new Size(36, 20);
        lblSort.TabIndex = 6;
        lblSort.Text = "Sort";
        // 
        // cboSort
        // 
        cboSort.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSort.Items.AddRange(new object[] { "Price: low to high", "Price: high to low", "Top rated" });
        cboSort.Location = new Point(616, 8);
        cboSort.Name = "cboSort";
        cboSort.Size = new Size(165, 28);
        cboSort.TabIndex = 7;
        // 
        // chkDates
        // 
        chkDates.AutoSize = true;
        chkDates.Location = new Point(12, 54);
        chkDates.Name = "chkDates";
        chkDates.Size = new Size(157, 24);
        chkDates.TabIndex = 8;
        chkDates.Text = "Available on dates:";
        // 
        // lblFrom
        // 
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(173, 54);
        lblFrom.Name = "lblFrom";
        lblFrom.Size = new Size(43, 20);
        lblFrom.TabIndex = 9;
        lblFrom.Text = "From";
        // 
        // dtpFrom
        // 
        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(221, 50);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.Size = new Size(110, 27);
        dtpFrom.TabIndex = 10;
        // 
        // lblTo
        // 
        lblTo.AutoSize = true;
        lblTo.Location = new Point(341, 54);
        lblTo.Name = "lblTo";
        lblTo.Size = new Size(25, 20);
        lblTo.TabIndex = 11;
        lblTo.Text = "To";
        // 
        // dtpTo
        // 
        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(374, 50);
        dtpTo.Name = "dtpTo";
        dtpTo.Size = new Size(110, 27);
        dtpTo.TabIndex = 12;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(509, 48);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(95, 32);
        btnSearch.TabIndex = 13;
        btnSearch.Text = "Search";
        btnSearch.Click += btnSearch_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(614, 48);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(80, 32);
        btnReset.TabIndex = 14;
        btnReset.Tag = "secondary";
        btnReset.Text = "Reset";
        btnReset.Click += btnReset_Click;
        // 
        // tabTrips
        // 
        tabTrips.Controls.Add(gridTrips);
        tabTrips.Controls.Add(pnlTripBtns);
        tabTrips.Location = new Point(4, 29);
        tabTrips.Name = "tabTrips";
        tabTrips.Padding = new Padding(12);
        tabTrips.Size = new Size(1894, 960);
        tabTrips.TabIndex = 1;
        tabTrips.Text = "My trips";
        // 
        // gridTrips
        // 
        gridTrips.ColumnHeadersHeight = 29;
        gridTrips.Dock = DockStyle.Fill;
        gridTrips.Location = new Point(12, 12);
        gridTrips.Name = "gridTrips";
        gridTrips.RowHeadersWidth = 51;
        gridTrips.Size = new Size(1870, 886);
        gridTrips.TabIndex = 0;
        // 
        // pnlTripBtns
        // 
        pnlTripBtns.Controls.Add(btnPay);
        pnlTripBtns.Controls.Add(btnCancelTrip);
        pnlTripBtns.Controls.Add(btnTripChat);
        pnlTripBtns.Controls.Add(btnDispute);
        pnlTripBtns.Controls.Add(btnTripPhotos);
        pnlTripBtns.Controls.Add(btnRate);
        pnlTripBtns.Dock = DockStyle.Bottom;
        pnlTripBtns.Location = new Point(12, 898);
        pnlTripBtns.Name = "pnlTripBtns";
        pnlTripBtns.Padding = new Padding(0, 8, 0, 0);
        pnlTripBtns.Size = new Size(1870, 50);
        pnlTripBtns.TabIndex = 1;
        // 
        // btnPay
        // 
        btnPay.Location = new Point(3, 11);
        btnPay.Name = "btnPay";
        btnPay.Size = new Size(115, 34);
        btnPay.TabIndex = 0;
        btnPay.Tag = "accent";
        btnPay.Text = "Pay now";
        btnPay.Click += btnPay_Click;
        // 
        // btnCancelTrip
        // 
        btnCancelTrip.Location = new Point(124, 11);
        btnCancelTrip.Name = "btnCancelTrip";
        btnCancelTrip.Size = new Size(125, 34);
        btnCancelTrip.TabIndex = 1;
        btnCancelTrip.Tag = "danger";
        btnCancelTrip.Text = "Cancel booking";
        btnCancelTrip.Click += btnCancelTrip_Click;
        // 
        // btnTripChat
        // 
        btnTripChat.Location = new Point(255, 11);
        btnTripChat.Name = "btnTripChat";
        btnTripChat.Size = new Size(115, 34);
        btnTripChat.TabIndex = 2;
        btnTripChat.Tag = "secondary";
        btnTripChat.Text = "Messages";
        btnTripChat.Click += btnTripChat_Click;
        // 
        // btnDispute
        // 
        btnDispute.Location = new Point(376, 11);
        btnDispute.Name = "btnDispute";
        btnDispute.Size = new Size(145, 34);
        btnDispute.TabIndex = 3;
        btnDispute.Tag = "secondary";
        btnDispute.Text = "Dispute deduction";
        btnDispute.Click += btnDispute_Click;
        // 
        // btnTripPhotos
        // 
        btnTripPhotos.Location = new Point(527, 11);
        btnTripPhotos.Name = "btnTripPhotos";
        btnTripPhotos.Size = new Size(135, 34);
        btnTripPhotos.TabIndex = 4;
        btnTripPhotos.Tag = "secondary";
        btnTripPhotos.Text = "Inspection photos";
        btnTripPhotos.Click += btnTripPhotos_Click;
        // 
        // btnRate
        // 
        btnRate.Location = new Point(668, 11);
        btnRate.Name = "btnRate";
        btnRate.Size = new Size(120, 34);
        btnRate.TabIndex = 5;
        btnRate.Text = "Rate trip";
        btnRate.Click += btnRate_Click;
        // 
        // tabMyCars
        // 
        tabMyCars.Controls.Add(gridMyCars);
        tabMyCars.Controls.Add(pnlCarBtns);
        tabMyCars.Location = new Point(4, 29);
        tabMyCars.Name = "tabMyCars";
        tabMyCars.Padding = new Padding(12);
        tabMyCars.Size = new Size(1894, 960);
        tabMyCars.TabIndex = 2;
        tabMyCars.Text = "My cars";
        // 
        // gridMyCars
        // 
        gridMyCars.ColumnHeadersHeight = 29;
        gridMyCars.Dock = DockStyle.Fill;
        gridMyCars.Location = new Point(12, 12);
        gridMyCars.Name = "gridMyCars";
        gridMyCars.RowHeadersWidth = 51;
        gridMyCars.Size = new Size(1870, 886);
        gridMyCars.TabIndex = 0;
        // 
        // pnlCarBtns
        // 
        pnlCarBtns.Controls.Add(btnAddCar);
        pnlCarBtns.Controls.Add(btnEditCar);
        pnlCarBtns.Controls.Add(btnToggleCar);
        pnlCarBtns.Controls.Add(btnDelCar);
        pnlCarBtns.Dock = DockStyle.Bottom;
        pnlCarBtns.Location = new Point(12, 898);
        pnlCarBtns.Name = "pnlCarBtns";
        pnlCarBtns.Padding = new Padding(0, 8, 0, 0);
        pnlCarBtns.Size = new Size(1870, 50);
        pnlCarBtns.TabIndex = 1;
        // 
        // btnAddCar
        // 
        btnAddCar.Location = new Point(3, 11);
        btnAddCar.Name = "btnAddCar";
        btnAddCar.Size = new Size(120, 34);
        btnAddCar.TabIndex = 0;
        btnAddCar.Text = "Add car";
        btnAddCar.Click += btnAddCar_Click;
        // 
        // btnEditCar
        // 
        btnEditCar.Location = new Point(129, 11);
        btnEditCar.Name = "btnEditCar";
        btnEditCar.Size = new Size(115, 34);
        btnEditCar.TabIndex = 1;
        btnEditCar.Tag = "secondary";
        btnEditCar.Text = "Edit / Photos";
        btnEditCar.Click += btnEditCar_Click;
        // 
        // btnToggleCar
        // 
        btnToggleCar.Location = new Point(250, 11);
        btnToggleCar.Name = "btnToggleCar";
        btnToggleCar.Size = new Size(155, 34);
        btnToggleCar.TabIndex = 2;
        btnToggleCar.Tag = "secondary";
        btnToggleCar.Text = "Toggle availability";
        btnToggleCar.Click += btnToggleCar_Click;
        // 
        // btnDelCar
        // 
        btnDelCar.Location = new Point(411, 11);
        btnDelCar.Name = "btnDelCar";
        btnDelCar.Size = new Size(120, 34);
        btnDelCar.TabIndex = 3;
        btnDelCar.Tag = "danger";
        btnDelCar.Text = "Remove car";
        btnDelCar.Click += btnDelCar_Click;
        // 
        // tabRequests
        // 
        tabRequests.Controls.Add(gridReqs);
        tabRequests.Controls.Add(pnlReqBtns);
        tabRequests.Controls.Add(lblEarnings);
        tabRequests.Location = new Point(4, 29);
        tabRequests.Name = "tabRequests";
        tabRequests.Padding = new Padding(12);
        tabRequests.Size = new Size(1894, 960);
        tabRequests.TabIndex = 3;
        tabRequests.Text = "Booking requests";
        // 
        // gridReqs
        // 
        gridReqs.ColumnHeadersHeight = 29;
        gridReqs.Dock = DockStyle.Fill;
        gridReqs.Location = new Point(12, 44);
        gridReqs.Name = "gridReqs";
        gridReqs.RowHeadersWidth = 51;
        gridReqs.Size = new Size(1870, 854);
        gridReqs.TabIndex = 0;
        // 
        // pnlReqBtns
        // 
        pnlReqBtns.Controls.Add(btnApprove);
        pnlReqBtns.Controls.Add(btnDecline);
        pnlReqBtns.Controls.Add(btnStart);
        pnlReqBtns.Controls.Add(btnEnd);
        pnlReqBtns.Controls.Add(btnReqChat);
        pnlReqBtns.Controls.Add(btnRateRenter);
        pnlReqBtns.Controls.Add(btnCalendar);
        pnlReqBtns.Controls.Add(btnReqPhotos);
        pnlReqBtns.Dock = DockStyle.Bottom;
        pnlReqBtns.Location = new Point(12, 898);
        pnlReqBtns.Name = "pnlReqBtns";
        pnlReqBtns.Padding = new Padding(0, 8, 0, 0);
        pnlReqBtns.Size = new Size(1870, 50);
        pnlReqBtns.TabIndex = 1;
        // 
        // btnApprove
        // 
        btnApprove.Location = new Point(3, 11);
        btnApprove.Name = "btnApprove";
        btnApprove.Size = new Size(105, 34);
        btnApprove.TabIndex = 0;
        btnApprove.Tag = "success";
        btnApprove.Text = "Approve";
        btnApprove.Click += btnApprove_Click;
        // 
        // btnDecline
        // 
        btnDecline.Location = new Point(114, 11);
        btnDecline.Name = "btnDecline";
        btnDecline.Size = new Size(100, 34);
        btnDecline.TabIndex = 1;
        btnDecline.Tag = "danger";
        btnDecline.Text = "Decline";
        btnDecline.Click += btnDecline_Click;
        // 
        // btnStart
        // 
        btnStart.Location = new Point(220, 11);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(140, 34);
        btnStart.TabIndex = 2;
        btnStart.Text = "Start trip (pick-up)";
        btnStart.Click += btnStart_Click;
        // 
        // btnEnd
        // 
        btnEnd.Location = new Point(366, 11);
        btnEnd.Name = "btnEnd";
        btnEnd.Size = new Size(135, 34);
        btnEnd.TabIndex = 3;
        btnEnd.Tag = "accent";
        btnEnd.Text = "End trip (return)";
        btnEnd.Click += btnEnd_Click;
        // 
        // btnReqChat
        // 
        btnReqChat.Location = new Point(507, 11);
        btnReqChat.Name = "btnReqChat";
        btnReqChat.Size = new Size(105, 34);
        btnReqChat.TabIndex = 4;
        btnReqChat.Tag = "secondary";
        btnReqChat.Text = "Messages";
        btnReqChat.Click += btnReqChat_Click;
        // 
        // btnRateRenter
        // 
        btnRateRenter.Location = new Point(618, 11);
        btnRateRenter.Name = "btnRateRenter";
        btnRateRenter.Size = new Size(115, 34);
        btnRateRenter.TabIndex = 5;
        btnRateRenter.Tag = "secondary";
        btnRateRenter.Text = "Rate renter";
        btnRateRenter.Click += btnRateRenter_Click;
        // 
        // btnCalendar
        // 
        btnCalendar.Location = new Point(739, 11);
        btnCalendar.Name = "btnCalendar";
        btnCalendar.Size = new Size(130, 34);
        btnCalendar.TabIndex = 6;
        btnCalendar.Tag = "secondary";
        btnCalendar.Text = "Month calendar";
        btnCalendar.Click += btnCalendar_Click;
        // 
        // btnReqPhotos
        // 
        btnReqPhotos.Location = new Point(875, 11);
        btnReqPhotos.Name = "btnReqPhotos";
        btnReqPhotos.Size = new Size(135, 34);
        btnReqPhotos.TabIndex = 7;
        btnReqPhotos.Tag = "secondary";
        btnReqPhotos.Text = "Inspection photos";
        btnReqPhotos.Click += btnReqPhotos_Click;
        // 
        // lblEarnings
        // 
        lblEarnings.Dock = DockStyle.Top;
        lblEarnings.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblEarnings.Location = new Point(12, 12);
        lblEarnings.Name = "lblEarnings";
        lblEarnings.Size = new Size(1870, 32);
        lblEarnings.TabIndex = 2;
        // 
        // tabVerify
        // 
        tabVerify.Controls.Add(gridUsers);
        tabVerify.Controls.Add(pnlVerifyBtns);
        tabVerify.Controls.Add(pnlVerifyFilter);
        tabVerify.Location = new Point(4, 29);
        tabVerify.Name = "tabVerify";
        tabVerify.Padding = new Padding(12);
        tabVerify.Size = new Size(1894, 960);
        tabVerify.TabIndex = 4;
        tabVerify.Text = "Users && verification";
        // 
        // gridUsers
        // 
        gridUsers.ColumnHeadersHeight = 29;
        gridUsers.Dock = DockStyle.Fill;
        gridUsers.Location = new Point(12, 52);
        gridUsers.Name = "gridUsers";
        gridUsers.RowHeadersWidth = 51;
        gridUsers.Size = new Size(1870, 846);
        gridUsers.TabIndex = 0;
        // 
        // pnlVerifyBtns
        // 
        pnlVerifyBtns.Controls.Add(btnViewDocs);
        pnlVerifyBtns.Controls.Add(btnApproveUser);
        pnlVerifyBtns.Controls.Add(btnRejectUser);
        pnlVerifyBtns.Controls.Add(btnToggleDisable);
        pnlVerifyBtns.Dock = DockStyle.Bottom;
        pnlVerifyBtns.Location = new Point(12, 898);
        pnlVerifyBtns.Name = "pnlVerifyBtns";
        pnlVerifyBtns.Padding = new Padding(0, 8, 0, 0);
        pnlVerifyBtns.Size = new Size(1870, 50);
        pnlVerifyBtns.TabIndex = 1;
        // 
        // btnViewDocs
        // 
        btnViewDocs.Location = new Point(3, 11);
        btnViewDocs.Name = "btnViewDocs";
        btnViewDocs.Size = new Size(145, 34);
        btnViewDocs.TabIndex = 0;
        btnViewDocs.Text = "View ID && selfie";
        btnViewDocs.Click += btnViewDocs_Click;
        // 
        // btnApproveUser
        // 
        btnApproveUser.Location = new Point(154, 11);
        btnApproveUser.Name = "btnApproveUser";
        btnApproveUser.Size = new Size(115, 34);
        btnApproveUser.TabIndex = 1;
        btnApproveUser.Tag = "success";
        btnApproveUser.Text = "Verify user";
        btnApproveUser.Click += btnApproveUser_Click;
        // 
        // btnRejectUser
        // 
        btnRejectUser.Location = new Point(275, 11);
        btnRejectUser.Name = "btnRejectUser";
        btnRejectUser.Size = new Size(110, 34);
        btnRejectUser.TabIndex = 2;
        btnRejectUser.Tag = "danger";
        btnRejectUser.Text = "Reject…";
        btnRejectUser.Click += btnRejectUser_Click;
        // 
        // btnToggleDisable
        // 
        btnToggleDisable.Location = new Point(391, 11);
        btnToggleDisable.Name = "btnToggleDisable";
        btnToggleDisable.Size = new Size(150, 34);
        btnToggleDisable.TabIndex = 3;
        btnToggleDisable.Tag = "secondary";
        btnToggleDisable.Text = "Disable / Enable";
        btnToggleDisable.Click += btnToggleDisable_Click;
        // 
        // pnlVerifyFilter
        // 
        pnlVerifyFilter.Controls.Add(lblVerifyFilter);
        pnlVerifyFilter.Controls.Add(cboVerifyFilter);
        pnlVerifyFilter.Dock = DockStyle.Top;
        pnlVerifyFilter.Location = new Point(12, 12);
        pnlVerifyFilter.Name = "pnlVerifyFilter";
        pnlVerifyFilter.Size = new Size(1870, 40);
        pnlVerifyFilter.TabIndex = 2;
        // 
        // lblVerifyFilter
        // 
        lblVerifyFilter.AutoSize = true;
        lblVerifyFilter.Location = new Point(0, 8);
        lblVerifyFilter.Margin = new Padding(0, 8, 8, 0);
        lblVerifyFilter.Name = "lblVerifyFilter";
        lblVerifyFilter.Size = new Size(48, 20);
        lblVerifyFilter.TabIndex = 0;
        lblVerifyFilter.Text = "Show:";
        // 
        // cboVerifyFilter
        // 
        cboVerifyFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cboVerifyFilter.Items.AddRange(new object[] { "Pending", "Verified", "Rejected", "Disabled", "All" });
        cboVerifyFilter.Location = new Point(59, 3);
        cboVerifyFilter.Name = "cboVerifyFilter";
        cboVerifyFilter.Size = new Size(150, 28);
        cboVerifyFilter.TabIndex = 1;
        cboVerifyFilter.SelectedIndexChanged += cboVerifyFilter_SelectedIndexChanged;
        // 
        // tabDisputes
        // 
        tabDisputes.Controls.Add(gridDisputes);
        tabDisputes.Controls.Add(pnlDisputeBtns);
        tabDisputes.Location = new Point(4, 29);
        tabDisputes.Name = "tabDisputes";
        tabDisputes.Padding = new Padding(12);
        tabDisputes.Size = new Size(1894, 960);
        tabDisputes.TabIndex = 5;
        tabDisputes.Text = "Damage disputes";
        // 
        // gridDisputes
        // 
        gridDisputes.ColumnHeadersHeight = 29;
        gridDisputes.Dock = DockStyle.Fill;
        gridDisputes.Location = new Point(12, 12);
        gridDisputes.Name = "gridDisputes";
        gridDisputes.RowHeadersWidth = 51;
        gridDisputes.Size = new Size(1870, 886);
        gridDisputes.TabIndex = 0;
        // 
        // pnlDisputeBtns
        // 
        pnlDisputeBtns.Controls.Add(btnDisputePhotos);
        pnlDisputeBtns.Controls.Add(btnResolveDispute);
        pnlDisputeBtns.Dock = DockStyle.Bottom;
        pnlDisputeBtns.Location = new Point(12, 898);
        pnlDisputeBtns.Name = "pnlDisputeBtns";
        pnlDisputeBtns.Padding = new Padding(0, 8, 0, 0);
        pnlDisputeBtns.Size = new Size(1870, 50);
        pnlDisputeBtns.TabIndex = 1;
        // 
        // btnDisputePhotos
        // 
        btnDisputePhotos.Location = new Point(3, 11);
        btnDisputePhotos.Name = "btnDisputePhotos";
        btnDisputePhotos.Size = new Size(175, 34);
        btnDisputePhotos.TabIndex = 0;
        btnDisputePhotos.Tag = "secondary";
        btnDisputePhotos.Text = "Inspect all photos";
        btnDisputePhotos.Click += btnDisputePhotos_Click;
        // 
        // btnResolveDispute
        // 
        btnResolveDispute.Location = new Point(184, 11);
        btnResolveDispute.Name = "btnResolveDispute";
        btnResolveDispute.Size = new Size(165, 34);
        btnResolveDispute.TabIndex = 1;
        btnResolveDispute.Tag = "accent";
        btnResolveDispute.Text = "Resolve dispute…";
        btnResolveDispute.Click += btnResolveDispute_Click;
        // 
        // tabAudit
        // 
        tabAudit.Controls.Add(gridAudit);
        tabAudit.Location = new Point(4, 29);
        tabAudit.Name = "tabAudit";
        tabAudit.Padding = new Padding(12);
        tabAudit.Size = new Size(1894, 960);
        tabAudit.TabIndex = 6;
        tabAudit.Text = "Audit log";
        // 
        // gridAudit
        // 
        gridAudit.ColumnHeadersHeight = 29;
        gridAudit.Dock = DockStyle.Fill;
        gridAudit.Location = new Point(12, 12);
        gridAudit.Name = "gridAudit";
        gridAudit.RowHeadersWidth = 51;
        gridAudit.Size = new Size(1870, 936);
        gridAudit.TabIndex = 0;
        // 
        // tabStats
        // 
        tabStats.Controls.Add(flowStats);
        tabStats.Controls.Add(lblStatsTitle);
        tabStats.Location = new Point(4, 29);
        tabStats.Name = "tabStats";
        tabStats.Padding = new Padding(16);
        tabStats.Size = new Size(1894, 960);
        tabStats.TabIndex = 7;
        tabStats.Text = "Overview";
        // 
        // flowStats
        // 
        flowStats.Dock = DockStyle.Fill;
        flowStats.Location = new Point(16, 52);
        flowStats.Name = "flowStats";
        flowStats.Padding = new Padding(0, 8, 0, 0);
        flowStats.Size = new Size(1862, 892);
        flowStats.TabIndex = 0;
        // 
        // lblStatsTitle
        // 
        lblStatsTitle.Dock = DockStyle.Top;
        lblStatsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblStatsTitle.Location = new Point(16, 16);
        lblStatsTitle.Name = "lblStatsTitle";
        lblStatsTitle.Size = new Size(1862, 36);
        lblStatsTitle.TabIndex = 1;
        lblStatsTitle.Text = "Platform snapshot";
        // 
        // tabProfile
        // 
        tabProfile.Location = new Point(4, 29);
        tabProfile.Name = "tabProfile";
        tabProfile.Padding = new Padding(16);
        tabProfile.Size = new Size(1894, 960);
        tabProfile.TabIndex = 8;
        tabProfile.Text = "My account";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1902, 1055);
        Controls.Add(tabs);
        Controls.Add(pnlTop);
        if (Brand.AppIcon != null) Icon = Brand.AppIcon;
        MinimumSize = new Size(940, 620);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "car-to-go";
        pnlTop.ResumeLayout(false);
        pnlTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picBrand).EndInit();
        tabs.ResumeLayout(false);
        tabBrowse.ResumeLayout(false);
        pnlFilter.ResumeLayout(false);
        pnlFilter.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudMax).EndInit();
        tabTrips.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridTrips).EndInit();
        pnlTripBtns.ResumeLayout(false);
        tabMyCars.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridMyCars).EndInit();
        pnlCarBtns.ResumeLayout(false);
        tabRequests.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridReqs).EndInit();
        pnlReqBtns.ResumeLayout(false);
        tabVerify.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridUsers).EndInit();
        pnlVerifyBtns.ResumeLayout(false);
        pnlVerifyFilter.ResumeLayout(false);
        pnlVerifyFilter.PerformLayout();
        tabDisputes.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridDisputes).EndInit();
        pnlDisputeBtns.ResumeLayout(false);
        tabAudit.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridAudit).EndInit();
        tabStats.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel pnlTop, pnlFilter;
    private PictureBox picBrand;
    private Label lblUser, lblCity, lblGear, lblMax, lblFrom, lblTo, lblSort, lblResults, lblEarnings, lblVerifyFilter, lblStatsTitle;
    private Button btnProfile, btnNotifs, btnLogout, btnSearch, btnReset, btnPay, btnCancelTrip, btnTripChat, btnDispute, btnTripPhotos, btnRate,
        btnAddCar, btnEditCar, btnToggleCar, btnDelCar, btnApprove, btnDecline, btnStart, btnEnd, btnReqChat, btnRateRenter, btnCalendar, btnReqPhotos,
        btnViewDocs, btnApproveUser, btnRejectUser, btnToggleDisable, btnDisputePhotos, btnResolveDispute;
    private TabControl tabs;
    private TabPage tabBrowse, tabTrips, tabMyCars, tabRequests, tabVerify, tabDisputes, tabAudit, tabStats, tabProfile;
    private ComboBox cboCity, cboGear, cboSort, cboVerifyFilter;
    private NumericUpDown nudMax;
    private DateTimePicker dtpFrom, dtpTo;
    private CheckBox chkDates;
    private FlowLayoutPanel flowChips, flowCars, pnlTripBtns, pnlCarBtns, pnlReqBtns, pnlVerifyFilter, pnlVerifyBtns, pnlDisputeBtns, flowStats;
    private DataGridView gridTrips, gridMyCars, gridReqs, gridUsers, gridDisputes, gridAudit;
}
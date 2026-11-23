using Microsoft.Data.SqlClient;

namespace DriveMate;

public static class Database
{
    public const string Server = @"(LocalDB)\MSSQLLocalDB";

    const string ConnectionString =
        @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DriveMate.mdf;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True;Encrypt=False;";

    static SqlConnection Open() { var c = new SqlConnection(ConnectionString); c.Open(); return c; }

    static string DataDir()
    {
        var d = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        while (d != null && !File.Exists(Path.Combine(d.FullName, "DriveMate.csproj"))) d = d.Parent;
        var dir = d != null
            ? Path.Combine(d.FullName, "Data")
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DriveMate");
        Directory.CreateDirectory(dir);
        return dir;
    }

    static void CreateDbFile(string mdf)
    {
        var ldf = Path.Combine(Path.GetDirectoryName(mdf)!, "DriveMate_log.ldf");
        static string Q(string x) => x.Replace("'", "''");
        using var c = new SqlConnection($"Server={Server};Database=master;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;");
        c.Open();
        foreach (var sql in new[]
        {
            "IF DB_ID(N'DriveMateTemp') IS NOT NULL DROP DATABASE [DriveMateTemp]",
            $"CREATE DATABASE [DriveMateTemp] ON PRIMARY (NAME=N'DriveMate', FILENAME=N'{Q(mdf)}') LOG ON (NAME=N'DriveMate_log', FILENAME=N'{Q(ldf)}')",
            "EXEC sp_detach_db N'DriveMateTemp', 'true'"
        })
        { using var cmd = c.CreateCommand(); cmd.CommandText = sql; cmd.ExecuteNonQuery(); }
    }

    static SqlCommand Cmd(SqlConnection c, string sql, (string n, object? v)[] p, SqlTransaction? tx = null)
    {
        var cmd = c.CreateCommand();
        cmd.CommandText = sql; cmd.Transaction = tx;
        foreach (var (n, v) in p) cmd.Parameters.AddWithValue(n, v ?? DBNull.Value);
        return cmd;
    }

    static int Exec(string sql, params (string n, object? v)[] p)
    { using var c = Open(); using var cmd = Cmd(c, sql, p); return cmd.ExecuteNonQuery(); }

    static int Count(string sql, params (string n, object? v)[] p)
    { using var c = Open(); using var cmd = Cmd(c, sql, p); return (int)cmd.ExecuteScalar()!; }

    static decimal Sum(string sql)
    { using var c = Open(); using var cmd = Cmd(c, sql, Array.Empty<(string, object?)>()); var o = cmd.ExecuteScalar(); return o is DBNull or null ? 0 : (decimal)o; }

    static List<T> Query<T>(string sql, Func<SqlDataReader, T> map, params (string n, object? v)[] p)
    {
        using var c = Open(); using var cmd = Cmd(c, sql, p); using var r = cmd.ExecuteReader();
        var list = new List<T>();
        while (r.Read()) list.Add(map(r));
        return list;
    }

    const string Schema = @"
IF OBJECT_ID('dbo.Users','U') IS NULL CREATE TABLE dbo.Users(
  Id UNIQUEIDENTIFIER PRIMARY KEY, FullName NVARCHAR(100) NOT NULL, Email NVARCHAR(150) NOT NULL UNIQUE,
  PasswordHash NVARCHAR(200) NOT NULL, [Role] INT NOT NULL,
  Phone NVARCHAR(30) NOT NULL DEFAULT '', IdType NVARCHAR(40) NOT NULL DEFAULT '',
  VerificationStatus INT NOT NULL DEFAULT 0, VerificationNote NVARCHAR(300) NOT NULL DEFAULT '',
  IsDisabled BIT NOT NULL DEFAULT 0, DisableReason NVARCHAR(300) NOT NULL DEFAULT '',
  CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME());

IF OBJECT_ID('dbo.UserDocs','U') IS NULL CREATE TABLE dbo.UserDocs(
  UserId UNIQUEIDENTIFIER PRIMARY KEY REFERENCES dbo.Users(Id) ON DELETE CASCADE,
  IdFileName NVARCHAR(200) NOT NULL, IdPhoto VARBINARY(MAX) NOT NULL,
  SelfieFileName NVARCHAR(200) NOT NULL, SelfiePhoto VARBINARY(MAX) NOT NULL);

IF OBJECT_ID('dbo.Cars','U') IS NULL CREATE TABLE dbo.Cars(
  Id UNIQUEIDENTIFIER PRIMARY KEY, HostId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Users(Id),
  Make NVARCHAR(50) NOT NULL, Model NVARCHAR(50) NOT NULL, [Year] INT NOT NULL, Plate NVARCHAR(20) NOT NULL,
  Category NVARCHAR(30) NOT NULL DEFAULT 'Sedan',
  Transmission NVARCHAR(20) NOT NULL, Seats INT NOT NULL, Location NVARCHAR(60) NOT NULL,
  PricePerDay DECIMAL(10,2) NOT NULL, Deposit DECIMAL(10,2) NOT NULL, Available BIT NOT NULL DEFAULT 1);

IF OBJECT_ID('dbo.Favorites','U') IS NULL CREATE TABLE dbo.Favorites(
  UserId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Users(Id) ON DELETE CASCADE,
  CarId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Cars(Id) ON DELETE CASCADE,
  PRIMARY KEY (UserId, CarId));

IF OBJECT_ID('dbo.Bookings','U') IS NULL CREATE TABLE dbo.Bookings(
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  CarId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Cars(Id),
  RenterId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Users(Id),
  HostId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Users(Id),
  StartDate DATE NOT NULL, EndDate DATE NOT NULL, [Status] INT NOT NULL,
  Total DECIMAL(10,2) NOT NULL, InsuranceFee DECIMAL(10,2) NOT NULL DEFAULT 0,
  InsuranceTier NVARCHAR(30) NOT NULL DEFAULT 'None',
  Deposit DECIMAL(10,2) NOT NULL, Deductions DECIMAL(10,2) NOT NULL DEFAULT 0, Refund DECIMAL(10,2) NOT NULL DEFAULT 0,
  HostPayout DECIMAL(10,2) NOT NULL DEFAULT 0, PayoutReleased BIT NOT NULL DEFAULT 0,
  Rating INT NOT NULL DEFAULT 0, Review NVARCHAR(500) NOT NULL DEFAULT '',
  RenterRating INT NOT NULL DEFAULT 0, RenterReview NVARCHAR(500) NOT NULL DEFAULT '',
  PaymentMethod NVARCHAR(40) NOT NULL DEFAULT '', PaymentRef NVARCHAR(60) NOT NULL DEFAULT '',
  PaidAt DATETIME2 NULL, RefundRef NVARCHAR(60) NOT NULL DEFAULT '',
  DisputeReason NVARCHAR(500) NOT NULL DEFAULT '', DisputeResolution NVARCHAR(500) NOT NULL DEFAULT '');

IF OBJECT_ID('dbo.Inspections','U') IS NULL CREATE TABLE dbo.Inspections(
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  BookingId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Bookings(Id) ON DELETE CASCADE,
  Stage NVARCHAR(10) NOT NULL, Odometer INT NOT NULL, FuelPercent INT NOT NULL,
  Scratches BIT NOT NULL, InteriorDirty BIT NOT NULL, TireOrLightIssue BIT NOT NULL,
  Notes NVARCHAR(500) NOT NULL, TakenAt DATETIME2 NOT NULL);

IF OBJECT_ID('dbo.InspectionPhotos','U') IS NULL CREATE TABLE dbo.InspectionPhotos(
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  InspectionId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Inspections(Id) ON DELETE CASCADE,
  FileName NVARCHAR(200) NOT NULL, Photo VARBINARY(MAX) NOT NULL);

IF OBJECT_ID('dbo.DisputePhotos','U') IS NULL CREATE TABLE dbo.DisputePhotos(
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  BookingId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Bookings(Id) ON DELETE CASCADE,
  UploadedBy NVARCHAR(100) NOT NULL, FileName NVARCHAR(200) NOT NULL, Photo VARBINARY(MAX) NOT NULL);

IF OBJECT_ID('dbo.CarPhotos','U') IS NULL CREATE TABLE dbo.CarPhotos(
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  CarId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Cars(Id) ON DELETE CASCADE,
  SortOrder INT NOT NULL, FileName NVARCHAR(200) NOT NULL, Photo VARBINARY(MAX) NOT NULL, Thumb VARBINARY(MAX) NULL);

IF OBJECT_ID('dbo.Notifications','U') IS NULL CREATE TABLE dbo.Notifications(
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  UserId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Users(Id) ON DELETE CASCADE,
  Title NVARCHAR(120) NOT NULL, Body NVARCHAR(500) NOT NULL,
  IsRead BIT NOT NULL DEFAULT 0, CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME());

IF OBJECT_ID('dbo.BookingMessages','U') IS NULL CREATE TABLE dbo.BookingMessages(
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  BookingId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Bookings(Id) ON DELETE CASCADE,
  SenderId UNIQUEIDENTIFIER NOT NULL, SenderName NVARCHAR(100) NOT NULL,
  Body NVARCHAR(1000) NOT NULL, SentAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME());

IF OBJECT_ID('dbo.AuditLogs','U') IS NULL CREATE TABLE dbo.AuditLogs(
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  AdminName NVARCHAR(100) NOT NULL, [Action] NVARCHAR(80) NOT NULL,
  TargetName NVARCHAR(120) NOT NULL, Details NVARCHAR(400) NOT NULL,
  CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME());";

    static readonly string[] Migrations =
    {
        "IF COL_LENGTH('dbo.Users','IsDisabled') IS NULL ALTER TABLE dbo.Users ADD IsDisabled BIT NOT NULL CONSTRAINT DF_Users_Dis DEFAULT 0",
        "IF COL_LENGTH('dbo.Users','DisableReason') IS NULL ALTER TABLE dbo.Users ADD DisableReason NVARCHAR(300) NOT NULL CONSTRAINT DF_Users_DisR DEFAULT ''",
        "IF COL_LENGTH('dbo.Bookings','InsuranceFee') IS NULL ALTER TABLE dbo.Bookings ADD InsuranceFee DECIMAL(10,2) NOT NULL CONSTRAINT DF_Book_InsF DEFAULT 0",
        "IF COL_LENGTH('dbo.Bookings','InsuranceTier') IS NULL ALTER TABLE dbo.Bookings ADD InsuranceTier NVARCHAR(30) NOT NULL CONSTRAINT DF_Book_InsT DEFAULT 'None'",
        "IF COL_LENGTH('dbo.Bookings','HostPayout') IS NULL ALTER TABLE dbo.Bookings ADD HostPayout DECIMAL(10,2) NOT NULL CONSTRAINT DF_Book_Pay DEFAULT 0",
        "IF COL_LENGTH('dbo.Bookings','PayoutReleased') IS NULL ALTER TABLE dbo.Bookings ADD PayoutReleased BIT NOT NULL CONSTRAINT DF_Book_PayR DEFAULT 0",
        "IF COL_LENGTH('dbo.Bookings','RenterRating') IS NULL ALTER TABLE dbo.Bookings ADD RenterRating INT NOT NULL CONSTRAINT DF_Book_RR DEFAULT 0",
        "IF COL_LENGTH('dbo.Bookings','RenterReview') IS NULL ALTER TABLE dbo.Bookings ADD RenterReview NVARCHAR(500) NOT NULL CONSTRAINT DF_Book_RRev DEFAULT ''",
        "IF COL_LENGTH('dbo.Bookings','DisputeReason') IS NULL ALTER TABLE dbo.Bookings ADD DisputeReason NVARCHAR(500) NOT NULL CONSTRAINT DF_Book_Disp DEFAULT ''",
        "IF COL_LENGTH('dbo.Bookings','DisputeResolution') IS NULL ALTER TABLE dbo.Bookings ADD DisputeResolution NVARCHAR(500) NOT NULL CONSTRAINT DF_Book_DispR DEFAULT ''",
        "ALTER TABLE dbo.Bookings ALTER COLUMN PaymentMethod NVARCHAR(40) NOT NULL",
        "ALTER TABLE dbo.Bookings ALTER COLUMN PaymentRef NVARCHAR(60) NOT NULL",
        "ALTER TABLE dbo.Bookings ALTER COLUMN RefundRef NVARCHAR(60) NOT NULL"
    };

    public static void Init()
    {
        var dir = DataDir();
        AppDomain.CurrentDomain.SetData("DataDirectory", dir);
        var mdf = Path.Combine(dir, "DriveMate.mdf");
        if (!File.Exists(mdf)) CreateDbFile(mdf);

        Exec(Schema);
        foreach (var m in Migrations) Exec(m);

        if (Count("SELECT COUNT(*) FROM dbo.Users") == 0) Seed();
        if (Count("SELECT COUNT(*) FROM dbo.Users WHERE [Role]=2") == 0)
            AddUser(new User
            {
                FullName = "car-to-go Admin",
                Email = "admin@cartogo.com",
                PasswordHash = Security.Hash("admin123"),
                Role = Role.Admin,
                Verification = VerifStatus.Verified
            });
    }

    static void Seed()
    {
        var host = new User { FullName = "Maria Host", Email = "host@demo.com", PasswordHash = Security.Hash("demo123"), Role = Role.Host, Verification = VerifStatus.Verified };
        var renter = new User { FullName = "Juan Renter", Email = "renter@demo.com", PasswordHash = Security.Hash("demo123"), Role = Role.Renter, Verification = VerifStatus.Verified };
        AddUser(host); AddUser(renter);
        SaveCar(new Car { HostId = host.Id, Make = "Toyota", Model = "Vios", Year = 2022, Plate = "ABC 1234", Category = "Sedan", Seats = 5, Location = "Makati", PricePerDay = 1800, Deposit = 3000 });
        SaveCar(new Car { HostId = host.Id, Make = "Mitsubishi", Model = "Xpander", Year = 2023, Plate = "XYZ 5678", Category = "MPV", Seats = 7, Location = "Quezon City", PricePerDay = 2800, Deposit = 5000 });
    }

    // users and accounts
    const string UserSelect = @"SELECT u.*,
      ISNULL((SELECT AVG(CAST(b.RenterRating AS FLOAT)) FROM dbo.Bookings b WHERE b.RenterId=u.Id AND b.RenterRating>0), 0) AS RAvg,
      (SELECT COUNT(*) FROM dbo.Bookings b WHERE b.RenterId=u.Id AND b.RenterRating>0) AS RCnt
      FROM dbo.Users u ";

    static User MapUser(SqlDataReader r) => new()
    {
        Id = (Guid)r["Id"],
        FullName = (string)r["FullName"],
        Email = (string)r["Email"],
        PasswordHash = (string)r["PasswordHash"],
        Role = (Role)(int)r["Role"],
        Phone = (string)r["Phone"],
        IdType = (string)r["IdType"],
        Verification = (VerifStatus)(int)r["VerificationStatus"],
        VerificationNote = (string)r["VerificationNote"],
        IsDisabled = (bool)r["IsDisabled"],
        DisableReason = (string)r["DisableReason"],
        AvgRating = Convert.ToDouble(r["RAvg"]),
        RatingCount = (int)r["RCnt"],
        CreatedAt = (DateTime)r["CreatedAt"]
    };

    public static User? GetUserByEmail(string email) =>
        Query<User>(UserSelect + "WHERE u.Email=@e", MapUser, ("@e", email)).FirstOrDefault();

    public static User? GetUserById(Guid id) =>
        Query<User>(UserSelect + "WHERE u.Id=@i", MapUser, ("@i", id)).FirstOrDefault();

    public static bool EmailTaken(string email, Guid exceptId) =>
        Count("SELECT COUNT(*) FROM dbo.Users WHERE Email=@e AND Id<>@x", ("@e", email), ("@x", exceptId)) > 0;

    static void InsertDocs(SqlConnection c, SqlTransaction tx, Guid userId, PhotoItem id, PhotoItem selfie)
    {
        using (var cmd = Cmd(c, "DELETE FROM dbo.UserDocs WHERE UserId=@u", new (string, object?)[] { ("@u", userId) }, tx)) cmd.ExecuteNonQuery();
        using (var cmd = Cmd(c, "INSERT INTO dbo.UserDocs(UserId,IdFileName,IdPhoto,SelfieFileName,SelfiePhoto) VALUES(@u,@a,@b,@c,@d)",
            new (string, object?)[] { ("@u", userId), ("@a", id.FileName), ("@b", id.Data), ("@c", selfie.FileName), ("@d", selfie.Data) }, tx))
            cmd.ExecuteNonQuery();
    }

    public static void AddUser(User u, PhotoItem? idPhoto = null, PhotoItem? selfie = null)
    {
        using var c = Open();
        using var tx = c.BeginTransaction();
        using (var cmd = Cmd(c, @"INSERT INTO dbo.Users(Id,FullName,Email,PasswordHash,[Role],Phone,IdType,VerificationStatus,VerificationNote)
                                  VALUES(@i,@n,@e,@p,@r,@ph,@it,@vs,@vn)",
            new (string, object?)[] { ("@i", u.Id), ("@n", u.FullName), ("@e", u.Email), ("@p", u.PasswordHash), ("@r", (int)u.Role),
                ("@ph", u.Phone), ("@it", u.IdType), ("@vs", (int)u.Verification), ("@vn", u.VerificationNote) }, tx))
            cmd.ExecuteNonQuery();
        if (idPhoto != null && selfie != null) InsertDocs(c, tx, u.Id, idPhoto, selfie);
        tx.Commit();
    }

    public static void SaveUserDocs(Guid userId, string idType, PhotoItem id, PhotoItem selfie)
    {
        using var c = Open();
        using var tx = c.BeginTransaction();
        InsertDocs(c, tx, userId, id, selfie);
        using (var cmd = Cmd(c, "UPDATE dbo.Users SET IdType=@t, VerificationStatus=0, VerificationNote='' WHERE Id=@u",
            new (string, object?)[] { ("@t", idType), ("@u", userId) }, tx))
            cmd.ExecuteNonQuery();
        tx.Commit();
    }

    public static (PhotoItem? Id, PhotoItem? Selfie) GetUserDocs(Guid userId)
    {
        var rows = Query<(PhotoItem?, PhotoItem?)>(
            "SELECT IdFileName, IdPhoto, SelfieFileName, SelfiePhoto FROM dbo.UserDocs WHERE UserId=@u",
            r => (new PhotoItem { FileName = (string)r["IdFileName"], Data = (byte[])r["IdPhoto"] },
                  new PhotoItem { FileName = (string)r["SelfieFileName"], Data = (byte[])r["SelfiePhoto"] }),
            ("@u", userId));
        return rows.Count == 0 ? (null, null) : rows[0];
    }

    /// <summary>filter: 0 pending, 1 verified, 2 rejected, 3 disabled, -1 all.</summary>
    public static List<User> UsersForAdmin(int filter) => filter switch
    {
        3 => Query<User>(UserSelect + "WHERE u.[Role]<>2 AND u.IsDisabled=1 ORDER BY u.CreatedAt DESC", MapUser),
        -1 => Query<User>(UserSelect + "WHERE u.[Role]<>2 ORDER BY u.CreatedAt DESC", MapUser),
        _ => Query<User>(UserSelect + "WHERE u.[Role]<>2 AND u.VerificationStatus=@s ORDER BY u.CreatedAt DESC", MapUser, ("@s", filter))
    };

    public static void SetVerification(Guid userId, VerifStatus status, string note, string adminName)
    {
        Exec("UPDATE dbo.Users SET VerificationStatus=@s, VerificationNote=@n WHERE Id=@i", ("@s", (int)status), ("@n", note), ("@i", userId));
        var u = GetUserById(userId);
        if (u != null)
        {
            LogAudit(adminName, status == VerifStatus.Verified ? "Verified User" : "Rejected User", $"{u.FullName} ({u.Email})", note == "" ? "Approved ID & selfie" : note);
            Notify(userId, status == VerifStatus.Verified ? "Identity Verified" : "Verification Rejected",
                status == VerifStatus.Verified
                    ? "Your ID has been verified! You now have full access to car-to-go."
                    : $"Your verification was rejected: {note}. Open 'My account' to resubmit.");
        }
    }

    public static void SetUserDisabled(Guid userId, bool disabled, string reason, string adminName)
    {
        Exec("UPDATE dbo.Users SET IsDisabled=@d, DisableReason=@r WHERE Id=@i", ("@d", disabled), ("@r", disabled ? reason : ""), ("@i", userId));
        var u = GetUserById(userId);
        if (u != null)
        {
            LogAudit(adminName, disabled ? "Disabled Account" : "Enabled Account", $"{u.FullName} ({u.Role})", disabled ? reason : "Account restored");
            if (!disabled)
                Notify(userId, "Account Re-enabled", "Your car-to-go account has been restored by an administrator.");
        }
    }

    public static void UpdateProfile(Guid id, string name, string email, string phone) =>
        Exec("UPDATE dbo.Users SET FullName=@n, Email=@e, Phone=@p WHERE Id=@i", ("@n", name), ("@e", email), ("@p", phone), ("@i", id));

    public static void UpdatePassword(Guid id, string hash) =>
        Exec("UPDATE dbo.Users SET PasswordHash=@p WHERE Id=@i", ("@p", hash), ("@i", id));

    public static Stats GetStats() => new(
        Count("SELECT COUNT(*) FROM dbo.Users WHERE [Role]<>2"),
        Count("SELECT COUNT(*) FROM dbo.Users WHERE [Role]=1"),
        Count("SELECT COUNT(*) FROM dbo.Users WHERE [Role]=0"),
        Count("SELECT COUNT(*) FROM dbo.Users WHERE [Role]<>2 AND VerificationStatus=0"),
        Count("SELECT COUNT(*) FROM dbo.Users WHERE [Role]<>2 AND IsDisabled=1"),
        Count("SELECT COUNT(*) FROM dbo.Cars"),
        Count("SELECT COUNT(*) FROM dbo.Bookings"),
        Count($"SELECT COUNT(*) FROM dbo.Bookings WHERE [Status]={(int)BookingStatus.Disputed}"),
        Count($"SELECT COUNT(*) FROM dbo.Bookings WHERE [Status]={(int)BookingStatus.Completed}"),
        Sum($"SELECT SUM(Total+InsuranceFee+Deductions) FROM dbo.Bookings WHERE [Status]={(int)BookingStatus.Completed}"));

    // admin audit logs
    public static void LogAudit(string adminName, string action, string targetName, string details) =>
        Exec("INSERT INTO dbo.AuditLogs(Id,AdminName,[Action],TargetName,Details) VALUES(NEWID(),@a,@ac,@t,@d)",
            ("@a", adminName), ("@ac", action), ("@t", targetName), ("@d", details));

    public static List<AuditEntry> GetAuditLogs() => Query<AuditEntry>(
        "SELECT TOP 200 * FROM dbo.AuditLogs ORDER BY CreatedAt DESC",
        r => new AuditEntry
        {
            Id = (Guid)r["Id"],
            AdminName = (string)r["AdminName"],
            Action = (string)r["Action"],
            TargetName = (string)r["TargetName"],
            Details = (string)r["Details"],
            CreatedAt = (DateTime)r["CreatedAt"]
        });

    // notifications & chat messages
    public static void Notify(Guid userId, string title, string body) =>
        Exec("INSERT INTO dbo.Notifications(Id,UserId,Title,Body) VALUES(NEWID(),@u,@t,@b)", ("@u", userId), ("@t", title), ("@b", body));

    public static List<AppNotification> GetNotifications(Guid userId) => Query<AppNotification>(
        "SELECT TOP 100 * FROM dbo.Notifications WHERE UserId=@u ORDER BY CreatedAt DESC",
        r => new AppNotification
        {
            Id = (Guid)r["Id"],
            UserId = (Guid)r["UserId"],
            Title = (string)r["Title"],
            Body = (string)r["Body"],
            IsRead = (bool)r["IsRead"],
            CreatedAt = (DateTime)r["CreatedAt"]
        }, ("@u", userId));

    public static int UnreadNotificationCount(Guid userId) =>
        Count("SELECT COUNT(*) FROM dbo.Notifications WHERE UserId=@u AND IsRead=0", ("@u", userId));

    public static void MarkNotificationsRead(Guid userId) =>
        Exec("UPDATE dbo.Notifications SET IsRead=1 WHERE UserId=@u", ("@u", userId));

    public static List<ChatMessage> GetMessages(Guid bookingId) => Query<ChatMessage>(
        "SELECT * FROM dbo.BookingMessages WHERE BookingId=@b ORDER BY SentAt ASC",
        r => new ChatMessage
        {
            Id = (Guid)r["Id"],
            BookingId = (Guid)r["BookingId"],
            SenderId = (Guid)r["SenderId"],
            SenderName = (string)r["SenderName"],
            Body = (string)r["Body"],
            SentAt = (DateTime)r["SentAt"]
        }, ("@b", bookingId));

    public static void SendMessage(Guid bookingId, Guid senderId, string senderName, Guid recipientId, string carName, string body)
    {
        Exec("INSERT INTO dbo.BookingMessages(Id,BookingId,SenderId,SenderName,Body) VALUES(NEWID(),@b,@s,@sn,@bd)",
            ("@b", bookingId), ("@s", senderId), ("@sn", senderName), ("@bd", body));
        Notify(recipientId, $"New message on {carName}", $"{senderName}: {body}");
    }

    // favorites
    const string CarSelect = @"SELECT c.*, u.FullName AS HostName, ISNULL(r.AvgR,0) AS AvgR, ISNULL(r.Cnt,0) AS Cnt,
  (SELECT COUNT(*) FROM dbo.CarPhotos p WHERE p.CarId=c.Id) AS PhotoCount,
  CASE WHEN EXISTS(SELECT 1 FROM dbo.Favorites f WHERE f.CarId=c.Id AND f.UserId=@uid) THEN 1 ELSE 0 END AS IsFav
FROM dbo.Cars c JOIN dbo.Users u ON u.Id=c.HostId
LEFT JOIN (SELECT CarId, AVG(CAST(Rating AS FLOAT)) AS AvgR, COUNT(*) AS Cnt FROM dbo.Bookings WHERE Rating>0 GROUP BY CarId) r ON r.CarId=c.Id ";

    static Car MapCar(SqlDataReader r) => new()
    {
        Id = (Guid)r["Id"],
        HostId = (Guid)r["HostId"],
        Make = (string)r["Make"],
        Model = (string)r["Model"],
        Year = (int)r["Year"],
        Plate = (string)r["Plate"],
        Category = (string)r["Category"],
        Transmission = (string)r["Transmission"],
        Seats = (int)r["Seats"],
        Location = (string)r["Location"],
        PricePerDay = (decimal)r["PricePerDay"],
        Deposit = (decimal)r["Deposit"],
        Available = (bool)r["Available"],
        HostName = (string)r["HostName"],
        AvgRating = Convert.ToDouble(r["AvgR"]),
        RatingCount = (int)r["Cnt"],
        PhotoCount = (int)r["PhotoCount"],
        IsFavorite = (int)r["IsFav"] == 1
    };

    public static List<Car> SearchCars(Guid renterId, string city, string gear, string category, decimal maxPrice,
        DateTime? from, DateTime? to, string sort, bool favoritesOnly = false)
    {
        var p = new List<(string, object?)> { ("@uid", renterId), ("@m", maxPrice), ("@city", city), ("@gear", gear), ("@cat", category) };
        var sql = CarSelect.Replace("SELECT c.*", "SELECT TOP 120 c.*") +
            @"WHERE c.Available=1 AND u.VerificationStatus=1 AND u.IsDisabled=0 AND c.PricePerDay<=@m
              AND (@city='All' OR c.Location=@city) AND (@gear='Any' OR c.Transmission=@gear) AND (@cat='All' OR c.Category=@cat) ";
        if (favoritesOnly)
            sql += "AND EXISTS(SELECT 1 FROM dbo.Favorites f WHERE f.CarId=c.Id AND f.UserId=@uid) ";
        if (from.HasValue && to.HasValue)
        {
            sql += $@"AND NOT EXISTS(SELECT 1 FROM dbo.Bookings b WHERE b.CarId=c.Id
                      AND b.[Status] IN ({(int)BookingStatus.Approved},{(int)BookingStatus.Active})
                      AND @f < b.EndDate AND @t > b.StartDate) ";
            p.Add(("@f", from.Value.Date)); p.Add(("@t", to.Value.Date));
        }
        sql += sort switch
        {
            "priceDesc" => "ORDER BY c.PricePerDay DESC",
            "rating" => "ORDER BY ISNULL(r.AvgR,0) DESC, ISNULL(r.Cnt,0) DESC",
            _ => "ORDER BY c.PricePerDay"
        };
        return Query<Car>(sql, MapCar, p.ToArray());
    }

    public static bool ToggleFavorite(Guid userId, Guid carId)
    {
        if (Count("SELECT COUNT(*) FROM dbo.Favorites WHERE UserId=@u AND CarId=@c", ("@u", userId), ("@c", carId)) > 0)
        {
            Exec("DELETE FROM dbo.Favorites WHERE UserId=@u AND CarId=@c", ("@u", userId), ("@c", carId));
            return false;
        }
        Exec("INSERT INTO dbo.Favorites(UserId,CarId) VALUES(@u,@c)", ("@u", userId), ("@c", carId));
        return true;
    }

    public static List<Car> CarsByHost(Guid hostId) =>
        Query<Car>(CarSelect + "WHERE c.HostId=@h ORDER BY c.Make, c.Model", MapCar, ("@uid", hostId), ("@h", hostId));

    public static void SaveCar(Car c) => Exec(@"
IF EXISTS(SELECT 1 FROM dbo.Cars WHERE Id=@Id)
  UPDATE dbo.Cars SET Make=@Make, Model=@Model, [Year]=@Year, Plate=@Plate, Category=@Cat, Transmission=@Trans, Seats=@Seats,
         Location=@Loc, PricePerDay=@Price, Deposit=@Dep, Available=@Avail WHERE Id=@Id
ELSE
  INSERT INTO dbo.Cars(Id,HostId,Make,Model,[Year],Plate,Category,Transmission,Seats,Location,PricePerDay,Deposit,Available)
  VALUES(@Id,@HostId,@Make,@Model,@Year,@Plate,@Cat,@Trans,@Seats,@Loc,@Price,@Dep,@Avail)",
        ("@Id", c.Id), ("@HostId", c.HostId), ("@Make", c.Make), ("@Model", c.Model), ("@Year", c.Year),
        ("@Plate", c.Plate), ("@Cat", c.Category), ("@Trans", c.Transmission), ("@Seats", c.Seats), ("@Loc", c.Location),
        ("@Price", c.PricePerDay), ("@Dep", c.Deposit), ("@Avail", c.Available));

    public static bool DeleteCar(Guid id)
    {
        if (Count("SELECT COUNT(*) FROM dbo.Bookings WHERE CarId=@i", ("@i", id)) > 0) return false;
        Exec("DELETE FROM dbo.Cars WHERE Id=@i", ("@i", id));
        return true;
    }

    // car photos
    static PhotoItem MapPhoto(SqlDataReader r) => new() { FileName = (string)r["FileName"], Data = (byte[])r["Photo"] };

    public static List<PhotoItem> GetCarPhotos(Guid carId) => Query<PhotoItem>(
        "SELECT FileName, Photo FROM dbo.CarPhotos WHERE CarId=@c ORDER BY SortOrder", MapPhoto, ("@c", carId));

    public static Dictionary<Guid, byte[]> GetCovers(IEnumerable<Guid> carIds)
    {
        var ps = carIds.Select((id, i) => ($"@p{i}", (object?)id)).ToArray();
        var result = new Dictionary<Guid, byte[]>();
        if (ps.Length == 0) return result;
        var sql = $"SELECT CarId, ISNULL(Thumb, Photo) AS Img FROM dbo.CarPhotos WHERE SortOrder=0 AND CarId IN ({string.Join(",", ps.Select(x => x.Item1))})";
        foreach (var (id, img) in Query<(Guid, byte[])>(sql, r => ((Guid)r["CarId"], (byte[])r["Img"]), ps)) result[id] = img;
        return result;
    }

    public static void SaveCarPhotos(Guid carId, List<PhotoItem> photos)
    {
        using var c = Open();
        using var tx = c.BeginTransaction();
        using (var cmd = Cmd(c, "DELETE FROM dbo.CarPhotos WHERE CarId=@c", new (string, object?)[] { ("@c", carId) }, tx))
            cmd.ExecuteNonQuery();
        for (int n = 0; n < photos.Count; n++)
            using (var cmd = Cmd(c, "INSERT INTO dbo.CarPhotos(Id,CarId,SortOrder,FileName,Photo,Thumb) VALUES(NEWID(),@c,@o,@n,@p,@t)",
                new (string, object?)[] { ("@c", carId), ("@o", n), ("@n", photos[n].FileName), ("@p", photos[n].Data), ("@t", Ui.MakeThumb(photos[n].Data)) }, tx))
                cmd.ExecuteNonQuery();
        tx.Commit();
    }

    // bookings & inspections
    const string BookingSelect = @"SELECT b.*, CAST(c.[Year] AS NVARCHAR(4))+' '+c.Make+' '+c.Model AS CarName, u.FullName AS OtherName,
  ISNULL((SELECT AVG(CAST(b2.RenterRating AS FLOAT)) FROM dbo.Bookings b2 WHERE b2.RenterId=b.RenterId AND b2.RenterRating>0),0) AS RenterAvg,
  (SELECT COUNT(*) FROM dbo.Bookings b2 WHERE b2.RenterId=b.RenterId AND b2.RenterRating>0) AS RenterCnt
FROM dbo.Bookings b JOIN dbo.Cars c ON c.Id=b.CarId JOIN dbo.Users u ON u.Id=";

    static Booking MapBooking(SqlDataReader r) => new()
    {
        Id = (Guid)r["Id"],
        CarId = (Guid)r["CarId"],
        RenterId = (Guid)r["RenterId"],
        HostId = (Guid)r["HostId"],
        Start = (DateTime)r["StartDate"],
        End = (DateTime)r["EndDate"],
        Status = (BookingStatus)(int)r["Status"],
        Total = (decimal)r["Total"],
        InsuranceFee = (decimal)r["InsuranceFee"],
        InsuranceTier = (string)r["InsuranceTier"],
        Deposit = (decimal)r["Deposit"],
        Deductions = (decimal)r["Deductions"],
        Refund = (decimal)r["Refund"],
        HostPayout = (decimal)r["HostPayout"],
        PayoutReleased = (bool)r["PayoutReleased"],
        Rating = (int)r["Rating"],
        Review = (string)r["Review"],
        RenterRating = (int)r["RenterRating"],
        RenterReview = (string)r["RenterReview"],
        PaymentMethod = (string)r["PaymentMethod"],
        PaymentRef = (string)r["PaymentRef"],
        PaidAt = r["PaidAt"] as DateTime?,
        RefundRef = (string)r["RefundRef"],
        DisputeReason = (string)r["DisputeReason"],
        DisputeResolution = (string)r["DisputeResolution"],
        CarName = (string)r["CarName"],
        OtherName = (string)r["OtherName"],
        OtherRating = Convert.ToDouble(r["RenterAvg"]),
        OtherRatingCount = (int)r["RenterCnt"]
    };

    static Inspection MapInspection(SqlDataReader r) => new()
    {
        Id = (Guid)r["Id"],
        BookingId = (Guid)r["BookingId"],
        Stage = (string)r["Stage"],
        Odometer = (int)r["Odometer"],
        FuelPercent = (int)r["FuelPercent"],
        Scratches = (bool)r["Scratches"],
        InteriorDirty = (bool)r["InteriorDirty"],
        TireOrLightIssue = (bool)r["TireOrLightIssue"],
        Notes = (string)r["Notes"],
        At = (DateTime)r["TakenAt"],
        PhotoCount = (int)r["PhotoCount"]
    };

    static List<Booking> AttachInspections(List<Booking> list)
    {
        foreach (var b in list)
            foreach (var i in Query<Inspection>(
                @"SELECT i.*, (SELECT COUNT(*) FROM dbo.InspectionPhotos p WHERE p.InspectionId=i.Id) AS PhotoCount
                  FROM dbo.Inspections i WHERE i.BookingId=@b", MapInspection, ("@b", b.Id)))
            {
                if (i.Stage == "Pickup") b.Pickup = i; else b.Return = i;
            }
        return list;
    }

    public static List<Booking> BookingsForRenter(Guid renterId) => AttachInspections(Query<Booking>(
        BookingSelect + "b.HostId WHERE b.RenterId=@id ORDER BY b.StartDate DESC", MapBooking, ("@id", renterId)));

    public static List<Booking> BookingsForHost(Guid hostId) => AttachInspections(Query<Booking>(
        BookingSelect + "b.RenterId WHERE b.HostId=@id ORDER BY b.StartDate DESC", MapBooking, ("@id", hostId)));

    public static List<Booking> DisputedBookings() => AttachInspections(Query<Booking>(
        @"SELECT b.*, CAST(c.[Year] AS NVARCHAR(4))+' '+c.Make+' '+c.Model AS CarName,
          r.FullName + ' (vs Host: ' + h.FullName + ')' AS OtherName, 0.0 AS RenterAvg, 0 AS RenterCnt
          FROM dbo.Bookings b JOIN dbo.Cars c ON c.Id=b.CarId
          JOIN dbo.Users r ON r.Id=b.RenterId JOIN dbo.Users h ON h.Id=b.HostId
          WHERE b.[Status]=@st ORDER BY b.EndDate DESC", MapBooking, ("@st", (int)BookingStatus.Disputed)));

    public static bool HasClash(Guid carId, DateTime start, DateTime end, Guid exceptBookingId) => Count(
        $@"SELECT COUNT(*) FROM dbo.Bookings WHERE CarId=@c AND Id<>@x
           AND [Status] IN ({(int)BookingStatus.Approved},{(int)BookingStatus.Active})
           AND @s < EndDate AND @e > StartDate",
        ("@c", carId), ("@x", exceptBookingId), ("@s", start.Date), ("@e", end.Date)) > 0;

    public static void AddBooking(Booking b) => Exec(
        @"INSERT INTO dbo.Bookings(Id,CarId,RenterId,HostId,StartDate,EndDate,[Status],Total,InsuranceFee,InsuranceTier,Deposit)
          VALUES(@i,@c,@r,@h,@s,@e,@st,@t,@ifee,@itier,@d)",
        ("@i", b.Id), ("@c", b.CarId), ("@r", b.RenterId), ("@h", b.HostId), ("@s", b.Start.Date),
        ("@e", b.End.Date), ("@st", (int)b.Status), ("@t", b.Total), ("@ifee", b.InsuranceFee), ("@itier", b.InsuranceTier), ("@d", b.Deposit));

    public static void SaveBooking(Booking b) => Exec(
        @"UPDATE dbo.Bookings SET [Status]=@st, Deductions=@dd, Refund=@rf, HostPayout=@hp, PayoutReleased=@prl,
          Rating=@ra, Review=@rv, RenterRating=@rra, RenterReview=@rrv,
          PaymentMethod=@pm, PaymentRef=@pr, PaidAt=@pa, RefundRef=@rr,
          DisputeReason=@dr, DisputeResolution=@drs WHERE Id=@i",
        ("@st", (int)b.Status), ("@dd", b.Deductions), ("@rf", b.Refund), ("@hp", b.HostPayout), ("@prl", b.PayoutReleased),
        ("@ra", b.Rating), ("@rv", b.Review), ("@rra", b.RenterRating), ("@rrv", b.RenterReview),
        ("@pm", b.PaymentMethod), ("@pr", b.PaymentRef), ("@pa", b.PaidAt), ("@rr", b.RefundRef),
        ("@dr", b.DisputeReason), ("@drs", b.DisputeResolution), ("@i", b.Id));

    // ---------- inspections & dispute photos ----------
    public static void SaveInspection(Inspection i)
    {
        using var c = Open();
        using var tx = c.BeginTransaction();
        using (var cmd = Cmd(c, @"INSERT INTO dbo.Inspections(Id,BookingId,Stage,Odometer,FuelPercent,Scratches,InteriorDirty,TireOrLightIssue,Notes,TakenAt)
                                  VALUES(@i,@b,@s,@o,@f,@sc,@d,@t,@n,@a)",
            new (string, object?)[] { ("@i", i.Id), ("@b", i.BookingId), ("@s", i.Stage), ("@o", i.Odometer), ("@f", i.FuelPercent),
                ("@sc", i.Scratches), ("@d", i.InteriorDirty), ("@t", i.TireOrLightIssue), ("@n", i.Notes), ("@a", i.At) }, tx))
            cmd.ExecuteNonQuery();

        foreach (var p in i.NewPhotos)
            using (var cmd = Cmd(c, "INSERT INTO dbo.InspectionPhotos(Id,InspectionId,FileName,Photo) VALUES(NEWID(),@i,@n,@d)",
                new (string, object?)[] { ("@i", i.Id), ("@n", p.FileName), ("@d", p.Data) }, tx))
                cmd.ExecuteNonQuery();

        tx.Commit();
        i.PhotoCount = i.NewPhotos.Count;
    }

    public static List<PhotoItem> GetPhotos(Guid inspectionId) => Query<PhotoItem>(
        "SELECT FileName, Photo FROM dbo.InspectionPhotos WHERE InspectionId=@i", MapPhoto, ("@i", inspectionId));

    public static void SaveDisputePhotos(Guid bookingId, string uploadedBy, List<PhotoItem> photos)
    {
        foreach (var p in photos)
            Exec("INSERT INTO dbo.DisputePhotos(Id,BookingId,UploadedBy,FileName,Photo) VALUES(NEWID(),@b,@u,@n,@d)",
                ("@b", bookingId), ("@u", uploadedBy), ("@n", p.FileName), ("@d", p.Data));
    }

    public static List<PhotoItem> GetDisputePhotos(Guid bookingId) => Query<PhotoItem>(
        "SELECT '['+UploadedBy+'] '+FileName AS FileName, Photo FROM dbo.DisputePhotos WHERE BookingId=@b", MapPhoto, ("@b", bookingId));
}
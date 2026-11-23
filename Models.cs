using System.Security.Cryptography;

namespace DriveMate;

public enum Role { Renter, Host, Admin }
public enum VerifStatus { Pending, Verified, Rejected }
public enum BookingStatus { Pending, Approved, Declined, Cancelled, Active, Completed, Disputed }

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public Role Role { get; set; }
    public string Phone { get; set; } = "";
    public string IdType { get; set; } = "";
    public VerifStatus Verification { get; set; } = VerifStatus.Pending;
    public string VerificationNote { get; set; } = "";
    public bool IsDisabled { get; set; }
    public string DisableReason { get; set; } = "";
    public double AvgRating { get; set; }
    public int RatingCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Car
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid HostId { get; set; }
    public string Make { get; set; } = "";
    public string Model { get; set; } = "";
    public int Year { get; set; }
    public string Plate { get; set; } = "";
    public string Category { get; set; } = "Sedan";
    public string Transmission { get; set; } = "Automatic";
    public int Seats { get; set; } = 5;
    public string Location { get; set; } = "Manila";
    public decimal PricePerDay { get; set; }
    public decimal Deposit { get; set; }
    public bool Available { get; set; } = true;
    public string HostName { get; set; } = "";
    public double AvgRating { get; set; }
    public int RatingCount { get; set; }
    public int PhotoCount { get; set; }
    public bool IsFavorite { get; set; }
    public override string ToString() => $"{Year} {Make} {Model}";
}

public class PhotoItem
{
    public string FileName { get; set; } = "";
    public byte[] Data { get; set; } = Array.Empty<byte>();
}

public class Inspection
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookingId { get; set; }
    public string Stage { get; set; } = "Pickup";
    public int Odometer { get; set; }
    public int FuelPercent { get; set; } = 100;
    public bool Scratches { get; set; }
    public bool InteriorDirty { get; set; }
    public bool TireOrLightIssue { get; set; }
    public string Notes { get; set; } = "";
    public DateTime At { get; set; } = DateTime.Now;
    public int PhotoCount { get; set; }
    public List<PhotoItem> NewPhotos { get; set; } = new();
}

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CarId { get; set; }
    public Guid RenterId { get; set; }
    public Guid HostId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public decimal Total { get; set; }
    public decimal InsuranceFee { get; set; }
    public string InsuranceTier { get; set; } = "None";
    public decimal Deposit { get; set; }
    public decimal Deductions { get; set; }
    public decimal Refund { get; set; }
    public decimal HostPayout { get; set; }
    public bool PayoutReleased { get; set; }

    // Two-way reviews
    public int Rating { get; set; }           // Renter -> Car/Host
    public string Review { get; set; } = "";
    public int RenterRating { get; set; }     // Host -> Renter
    public string RenterReview { get; set; } = "";

    // Payments & Disputes
    public string PaymentMethod { get; set; } = "";
    public string PaymentRef { get; set; } = "";
    public DateTime? PaidAt { get; set; }
    public string RefundRef { get; set; } = "";
    public string DisputeReason { get; set; } = "";
    public string DisputeResolution { get; set; } = "";

    // Joined display fields
    public string CarName { get; set; } = "";
    public string OtherName { get; set; } = "";
    public double OtherRating { get; set; }
    public int OtherRatingCount { get; set; }
    public Inspection? Pickup { get; set; }
    public Inspection? Return { get; set; }

    public int Days => Math.Max(1, (End.Date - Start.Date).Days);
    public decimal AmountDue => Total + InsuranceFee + Deposit;
    public bool IsPaid => PaidAt != null;
}

public class AppNotification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ChatMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookingId { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}

public class AuditEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AdminName { get; set; } = "";
    public string Action { get; set; } = "";
    public string TargetName { get; set; } = "";
    public string Details { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public record Stats(int Users, int Hosts, int Renters, int Pending, int Disabled, int Cars, int Bookings, int Disputed, int Completed, decimal Revenue);

public static class Pricing
{
    public static readonly (string Name, decimal DailyRate, string Desc)[] InsurancePlans =
    {
        ("None", 0m, "No extra cover (standard deposit applies)"),
        ("Basic Cover", 250m, "₱250/day — Caps scratch & tire deductions by 50%"),
        ("Full Shield", 500m, "₱500/day — ₱0 liability on accidental scratches/tires")
    };

    public static (decimal Total, decimal Discount, string Label) Quote(int days, decimal rate)
    {
        decimal gross = days * rate;
        decimal pct = days >= 30 ? 0.20m : days >= 7 ? 0.10m : 0m;
        var disc = Math.Round(gross * pct, 0);
        return (gross - disc, disc, pct > 0 ? $"{pct:P0} long-stay discount" : "");
    }

    /// <summary>
    /// Tiered Cancellation Policy:
    /// - Pending booking or >= 48h before pickup: 100% refund
    /// - 24h to 48h before pickup: 50% rental fare refund + 100% deposit & insurance
    /// - < 24h before pickup: 0% rental fare refund + 100% deposit refunded
    /// </summary>
    public static (decimal RefundAmount, string PolicyNote) ComputeCancellationRefund(Booking b)
    {
        if (!b.IsPaid) return (0m, "Unpaid booking — no charge.");
        if (b.Status == BookingStatus.Pending)
            return (b.AmountDue, "Pending request cancelled — 100% full refund.");

        double hoursUntilPickup = (b.Start.Date.AddHours(9) - DateTime.Now).TotalHours;
        if (hoursUntilPickup >= 48)
            return (b.AmountDue, "Cancelled 48+ hours before pick-up — 100% full refund.");
        if (hoursUntilPickup >= 24)
        {
            decimal refund = Math.Round(b.Total * 0.50m, 0) + b.InsuranceFee + b.Deposit;
            return (refund, "Cancelled 24–48h before pick-up — 50% fare + full deposit refunded.");
        }
        return (b.Deposit, "Cancelled under 24h before pick-up — fare non-refundable; deposit refunded in full.");
    }
}

public static class Charges
{
    public const decimal FuelPerPercent = 15m;
    public const decimal ExtraKm = 12m;
    public const int FreeKmPerDay = 200;
    public const decimal Scratch = 1500m, Interior = 500m, Tire = 1000m;

    public static (List<string> Lines, decimal Total) Compute(Booking b)
    {
        var lines = new List<string>();
        decimal t = 0;
        if (b.Pickup is null || b.Return is null) return (lines, 0);

        decimal coverMult = b.InsuranceTier == "Full Shield" ? 0m : b.InsuranceTier == "Basic Cover" ? 0.5m : 1m;

        int fuel = b.Pickup.FuelPercent - b.Return.FuelPercent;
        if (fuel > 0) { var a = fuel * FuelPerPercent; t += a; lines.Add($"Fuel shortfall ({fuel}%): ₱{a:N0}"); }

        int extra = (b.Return.Odometer - b.Pickup.Odometer) - FreeKmPerDay * b.Days;
        if (extra > 0) { var a = extra * ExtraKm; t += a; lines.Add($"Excess mileage ({extra} km): ₱{a:N0}"); }

        if (b.Return.Scratches && !b.Pickup.Scratches)
        {
            var a = Math.Round(Scratch * coverMult, 0);
            t += a;
            lines.Add(coverMult < 1m ? $"New scratches ({b.InsuranceTier}): ₱{a:N0}" : $"New scratches/dents: ₱{a:N0}");
        }
        if (b.Return.InteriorDirty && !b.Pickup.InteriorDirty)
        {
            t += Interior;
            lines.Add($"Interior cleaning: ₱{Interior:N0}");
        }
        if (b.Return.TireOrLightIssue && !b.Pickup.TireOrLightIssue)
        {
            var a = Math.Round(Tire * coverMult, 0);
            t += a;
            lines.Add(coverMult < 1m ? $"Tire/light issue ({b.InsuranceTier}): ₱{a:N0}" : $"Tire/light issue: ₱{a:N0}");
        }

        return (lines, Math.Min(t, b.Deposit));
    }
}

public static class Security
{
    const int Iterations = 100_000;

    public static string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var h = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(salt) + "." + Convert.ToBase64String(h);
    }

    public static bool Verify(string password, string stored)
    {
        var p = stored.Split('.');
        if (p.Length != 2) return false;
        var h = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromBase64String(p[0]), Iterations, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(h, Convert.FromBase64String(p[1]));
    }
}
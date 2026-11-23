SELECT Id, FullName, Email, [Role], Phone, IdType,
       VerificationStatus, VerificationNote, IsDisabled, CreatedAt
FROM dbo.Users
ORDER BY CreatedAt DESC;
-- Role: 0 Renter, 1 Host, 2 Admin
-- VerificationStatus: 0 Pending, 1 Verified, 2 Rejected
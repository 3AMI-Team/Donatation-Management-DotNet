-- Extended Seed Data (Fixed Version)
USE [DonationManagementDb];
GO

-- 0. Clean up existing data to avoid conflicts (Optional, but recommended for clean seed)
DELETE FROM [Distributions];
DELETE FROM [Donations];
DELETE FROM [Cases];
DELETE FROM [Donors];
DELETE FROM [Categories];
DELETE FROM [Employees] WHERE Username != 'Ibrahim'; -- Keep the main admin if exists
GO

-- 1. Categories
INSERT INTO [Categories] ([Type], [Description]) VALUES 
('Education', 'Scholarships and school supplies'),
('Health', 'Surgeries and medicine'),
('Food Supply', 'Baskets and meals'),
('Orphans', 'Monthly sponsorship'),
('Debt Relief', 'Help for debtors'),
('Housing', 'Building and repair'),
('Water Access', 'Wells and pipes'),
('General Charity', 'Various aids');
GO

-- 2. Employees (Password: 12345678)
IF NOT EXISTS (SELECT 1 FROM [Employees] WHERE [Username] = 'sarah.a')
INSERT INTO [Employees] ([Name], [Username], [Password], [Role], [Phone], [Address], [Email]) VALUES 
('Sarah Ahmed', 'sarah.a', '$2a$11$K98K0W2G6.H.n2.666666O88888888888888888888888888888', 'Supervisor', '01011112222', 'Giza', 'sarah@charity.com');

IF NOT EXISTS (SELECT 1 FROM [Employees] WHERE [Username] = 'mohamed.a')
INSERT INTO [Employees] ([Name], [Username], [Password], [Role], [Phone], [Address], [Email]) VALUES 
('Mohamed Ali', 'mohamed.a', '$2a$11$K98K0W2G6.H.n2.666666O88888888888888888888888888888', 'Supervisor', '01022223333', 'Alex', 'mohamed@charity.com');
GO

-- 3. Donors (With default password)
INSERT INTO [Donors] ([Name], [Email], [Phone], [RegisterDate], [Address], [Type], [Password]) VALUES 
('James Wilson', 'james@example.com', '01044445555', '2026-01-10', 'London', 'Individual', '$2a$11$K98K0W2G6.H.n2.666666O88888888888888888888888888888'),
('Tech Solutions', 'info@techcorp.com', '01055556666', '2026-01-15', 'Dubai', 'Corporate', '$2a$11$K98K0W2G6.H.n2.666666O88888888888888888888888888888'),
('Mona Zaki', 'mona@example.com', '01066667777', '2026-02-01', 'Cairo', 'Individual', '$2a$11$K98K0W2G6.H.n2.666666O88888888888888888888888888888'),
('Global Aid', 'contact@globalaid.org', '01077778888', '2026-02-10', 'New York', 'Corporate', '$2a$11$K98K0W2G6.H.n2.666666O88888888888888888888888888888');
GO

-- 4. Cases
DECLARE @CatEdu int = (SELECT TOP 1 Id FROM Categories WHERE Type = 'Education');
DECLARE @CatHealth int = (SELECT TOP 1 Id FROM Categories WHERE Type = 'Health');
DECLARE @EmpSarah int = (SELECT TOP 1 Id FROM Employees WHERE Username = 'sarah.a');

INSERT INTO [Cases] ([Name], [Phone], [Address], [RegistDate], [Status], [Description], [CategoryId], [SupervisorId]) VALUES 
('Ahmed Ibrahim', '01111111111', 'Cairo', '2026-01-05', 'Approved', 'University fees', @CatEdu, @EmpSarah),
('Zainab Mahmoud', '01222222222', 'Giza', '2026-01-12', 'Approved', 'Surgery', @CatHealth, @EmpSarah);
GO

-- 5. Donations
DECLARE @DonorJames int = (SELECT TOP 1 Id FROM Donors WHERE Email = 'james@example.com');
DECLARE @CatGen int = (SELECT TOP 1 Id FROM Categories WHERE Type = 'General Charity');
DECLARE @EmpIbrahim int = (SELECT TOP 1 Id FROM Employees WHERE Username = 'admin' OR Username = 'Ibrahim');

INSERT INTO [Donations] ([Amount], [Description], [Status], [Date], [DonorId], [CategoryId], [SupervisorId]) VALUES 
(10000, 'Zakat', 'Completed', '2026-01-20', @DonorJames, @CatGen, @EmpIbrahim);
GO

-- 6. Add batches for Dashboard data in one batch to keep variables
DECLARE @EmpIbrahimLoop int = (SELECT TOP 1 Id FROM Employees WHERE Username = 'admin' OR Username = 'Ibrahim' OR Username = 'sarah.a');
DECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    -- Create unique email for each donor to avoid unique constraint if exists
    DECLARE @DonorEmail VARCHAR(100) = 'test' + CAST(@i as varchar) + '@charity.com';
    
    INSERT INTO [Donors] ([Name], [Email], [Phone], [RegisterDate], [Address], [Type], [Password])
    VALUES ('Donor ' + CAST(@i as varchar), @DonorEmail, '01000000' + CAST(@i as varchar), DATEADD(day, -@i, GETDATE()), 'City', 'Individual', '$2a$11$K98K0W2G6.H.n2.666666O88888888888888888888888888888');
    
    -- Get the ID of the donor we just created or a random one
    DECLARE @CurrentDonorId int = (SELECT Id FROM Donors WHERE Email = @DonorEmail);
    DECLARE @RandomCatId int = (SELECT TOP 1 Id FROM Categories ORDER BY NEWID());

    INSERT INTO [Donations] ([Amount], [Description], [Status], [Date], [DonorId], [CategoryId], [SupervisorId])
    VALUES (ABS(CHECKSUM(NEWID()) % 5000) + 100, 'Contribution ' + CAST(@i as varchar), 'Completed', DATEADD(day, -@i, GETDATE()), @CurrentDonorId, @RandomCatId, @EmpIbrahimLoop);
    
    SET @i = @i + 1;
END;
GO

-- =============================================
-- Full Database Script for MonsterASP Deployment
-- Schema + Extensive Realistic Seed Data
-- Real BCrypt hash for password: 12345678
-- =============================================

-- 1. DROP existing tables
IF OBJECT_ID('Distributions', 'U') IS NOT NULL DROP TABLE Distributions;
IF OBJECT_ID('Donations', 'U') IS NOT NULL DROP TABLE Donations;
IF OBJECT_ID('Cases', 'U') IS NOT NULL DROP TABLE Cases;
IF OBJECT_ID('Donors', 'U') IS NOT NULL DROP TABLE Donors;
IF OBJECT_ID('Categories', 'U') IS NOT NULL DROP TABLE Categories;
IF OBJECT_ID('Employees', 'U') IS NOT NULL DROP TABLE Employees;
IF OBJECT_ID('__EFMigrationsHistory', 'U') IS NOT NULL DROP TABLE __EFMigrationsHistory;
GO

-- 2. CREATE Tables
CREATE TABLE [Employees] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [Username] NVARCHAR(MAX) NOT NULL,
    [Password] NVARCHAR(MAX) NOT NULL,
    [Role] NVARCHAR(MAX) NOT NULL,
    [Phone] NVARCHAR(MAX) NULL,
    [Address] NVARCHAR(MAX) NULL,
    [Email] NVARCHAR(MAX) NULL
);

CREATE TABLE [Categories] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Type] NVARCHAR(MAX) NOT NULL,
    [Description] NVARCHAR(MAX) NULL
);

CREATE TABLE [Donors] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [Email] NVARCHAR(MAX) NOT NULL,
    [Phone] NVARCHAR(MAX) NOT NULL,
    [Password] NVARCHAR(MAX) NOT NULL,
    [RegisterDate] DATETIME2 NOT NULL,
    [Address] NVARCHAR(MAX) NULL,
    [Type] NVARCHAR(MAX) NULL
);

CREATE TABLE [Cases] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [Phone] NVARCHAR(MAX) NOT NULL,
    [Address] NVARCHAR(MAX) NOT NULL,
    [RegistDate] DATETIME2 NOT NULL,
    [Status] NVARCHAR(MAX) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [CategoryId] INT NOT NULL,
    [SupervisorId] INT NOT NULL,
    CONSTRAINT [FK_Cases_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Cases_Employees] FOREIGN KEY ([SupervisorId]) REFERENCES [Employees]([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Donations] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Amount] DECIMAL(18,2) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Status] NVARCHAR(MAX) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [DonorId] INT NOT NULL,
    [CategoryId] INT NOT NULL,
    [SupervisorId] INT NOT NULL,
    CONSTRAINT [FK_Donations_Donors] FOREIGN KEY ([DonorId]) REFERENCES [Donors]([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Donations_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Donations_Employees] FOREIGN KEY ([SupervisorId]) REFERENCES [Employees]([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Distributions] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Amount] DECIMAL(18,2) NOT NULL,
    [DistributionDate] DATETIME2 NOT NULL,
    [Status] NVARCHAR(MAX) NOT NULL,
    [Notes] NVARCHAR(MAX) NULL,
    [CaseId] INT NOT NULL,
    [DonationId] INT NOT NULL,
    [HandledByEmployeeId] INT NOT NULL,
    CONSTRAINT [FK_Distributions_Cases] FOREIGN KEY ([CaseId]) REFERENCES [Cases]([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Distributions_Donations] FOREIGN KEY ([DonationId]) REFERENCES [Donations]([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Distributions_Employees] FOREIGN KEY ([HandledByEmployeeId]) REFERENCES [Employees]([Id]) ON DELETE NO ACTION
);
GO

-- =============================================
-- 3. SEED DATA (Password: 12345678)
-- =============================================
DECLARE @hash NVARCHAR(200) = '$2a$11$9NENWbwfSwun3Cd7j4bjwuTxC5W3uQ6buecY1yS06ZoNV6oukkwj.';

-- Categories (8)
INSERT INTO [Categories] ([Type], [Description]) VALUES 
('Education', 'Scholarships and school supplies'),
('Health', 'Surgeries and medicine'),
('Food Supply', 'Food baskets and meals'),
('Orphans', 'Monthly orphan sponsorship'),
('Debt Relief', 'Help paying off debts'),
('Housing', 'Building and home repair'),
('Water Access', 'Wells and clean water pipes'),
('General Charity', 'Various charitable aids');

-- Employees (10)
INSERT INTO [Employees] ([Name], [Username], [Password], [Role], [Phone], [Address], [Email]) VALUES 
('Ibrahim Admin', 'Ibrahim', @hash, 'Admin', '01012345678', 'Cairo', 'admin@charity.com'),
('Sarah Ahmed', 'sarah', @hash, 'Supervisor', '01011112222', 'Giza', 'sarah@charity.com'),
('Mohamed Ali', 'mohamed', @hash, 'Supervisor', '01022223333', 'Alexandria', 'mohamed@charity.com'),
('Fatma Hassan', 'fatma', @hash, 'Supervisor', '01033334444', 'Mansoura', 'fatma@charity.com'),
('Ahmed Mostafa', 'ahmed', @hash, 'Employee', '01044445555', 'Tanta', 'ahmed@charity.com'),
('Nour ElDin', 'nour', @hash, 'Employee', '01055556666', 'Aswan', 'nour@charity.com'),
('Youssef Kamal', 'youssef', @hash, 'Employee', '01066667777', 'Luxor', 'youssef@charity.com'),
('Hana Mahmoud', 'hana', @hash, 'Employee', '01077778888', 'Suez', 'hana@charity.com'),
('Omar Sayed', 'omar', @hash, 'Supervisor', '01088889999', 'Hurghada', 'omar@charity.com'),
('Layla Adel', 'layla', @hash, 'Employee', '01099990000', 'Ismailia', 'layla@charity.com');

-- Donors (120 realistic names)
INSERT INTO [Donors] ([Name], [Email], [Phone], [RegisterDate], [Address], [Type], [Password]) VALUES 
('Youssef El-Masry', 'youssef.masry@email.com', '01200000001', '2025-09-01', 'Cairo', 'Individual', @hash),
('Nadia Farouk', 'nadia.farouk@email.com', '01200000002', '2025-09-03', 'Giza', 'Individual', @hash),
('Hassan Abdel-Fattah', 'hassan.af@email.com', '01200000003', '2025-09-05', 'Alexandria', 'Individual', @hash),
('Mariam Soliman', 'mariam.s@email.com', '01200000004', '2025-09-08', 'Mansoura', 'Individual', @hash),
('Tareq Mansour', 'tareq.m@email.com', '01200000005', '2025-09-10', 'Tanta', 'Individual', @hash),
('Amira Khalil', 'amira.k@email.com', '01200000006', '2025-09-12', 'Aswan', 'Individual', @hash),
('Essam El-Din', 'essam.d@email.com', '01200000007', '2025-09-15', 'Luxor', 'Individual', @hash),
('Dina Ragab', 'dina.r@email.com', '01200000008', '2025-09-18', 'Suez', 'Individual', @hash),
('Karim Shawky', 'karim.sh@email.com', '01200000009', '2025-09-20', 'Hurghada', 'Individual', @hash),
('Salma Adel', 'salma.a@email.com', '01200000010', '2025-09-22', 'Ismailia', 'Individual', @hash),
('Al-Baraka Foundation', 'info@albaraka.org', '01200000011', '2025-09-25', 'Cairo', 'Corporate', @hash),
('Gulf Trading Co.', 'donate@gulftrade.com', '01200000012', '2025-09-28', 'Dubai', 'Corporate', @hash),
('Mahmoud Ismail', 'mahmoud.i@email.com', '01200000013', '2025-10-01', 'Cairo', 'Individual', @hash),
('Reem Hassan', 'reem.h@email.com', '01200000014', '2025-10-03', 'Giza', 'Individual', @hash),
('Waleed Mostafa', 'waleed.m@email.com', '01200000015', '2025-10-05', 'Alex', 'Individual', @hash),
('Heba Nour', 'heba.n@email.com', '01200000016', '2025-10-08', 'Cairo', 'Individual', @hash),
('Sherif Gamal', 'sherif.g@email.com', '01200000017', '2025-10-10', 'Tanta', 'Individual', @hash),
('Noha Ibrahim', 'noha.i@email.com', '01200000018', '2025-10-12', 'Mansoura', 'Individual', @hash),
('Ayman Farid', 'ayman.f@email.com', '01200000019', '2025-10-15', 'Aswan', 'Individual', @hash),
('Yasmin Lotfy', 'yasmin.l@email.com', '01200000020', '2025-10-18', 'Luxor', 'Individual', @hash),
('Nile Charity Group', 'hello@nilecharity.org', '01200000021', '2025-10-20', 'Cairo', 'Corporate', @hash),
('Sunrise Holdings', 'csr@sunrise.com', '01200000022', '2025-10-22', 'Riyadh', 'Corporate', @hash),
('Adel Ramadan', 'adel.r@email.com', '01200000023', '2025-10-25', 'Cairo', 'Individual', @hash),
('Sahar Magdy', 'sahar.m@email.com', '01200000024', '2025-10-28', 'Giza', 'Individual', @hash),
('Tamer Hosny', 'tamer.h@email.com', '01200000025', '2025-11-01', 'Cairo', 'Individual', @hash),
('Maha El-Sayed', 'maha.s@email.com', '01200000026', '2025-11-03', 'Alex', 'Individual', @hash),
('Khaled Zain', 'khaled.z@email.com', '01200000027', '2025-11-05', 'Luxor', 'Individual', @hash),
('Rana Osman', 'rana.o@email.com', '01200000028', '2025-11-08', 'Aswan', 'Individual', @hash),
('Hesham Barakat', 'hesham.b@email.com', '01200000029', '2025-11-10', 'Suez', 'Individual', @hash),
('Farida Nabil', 'farida.n@email.com', '01200000030', '2025-11-12', 'Hurghada', 'Individual', @hash),
('Abdallah Samir', 'abdallah.s@email.com', '01200000031', '2025-11-15', 'Cairo', 'Individual', @hash),
('Nourhan Tarek', 'nourhan.t@email.com', '01200000032', '2025-11-18', 'Giza', 'Individual', @hash),
('Oasis Development', 'info@oasisdev.com', '01200000033', '2025-11-20', 'Dubai', 'Corporate', @hash),
('Badr El-Din', 'badr.d@email.com', '01200000034', '2025-11-22', 'Cairo', 'Individual', @hash),
('Manal Fouad', 'manal.f@email.com', '01200000035', '2025-11-25', 'Tanta', 'Individual', @hash),
('Sami Youssef', 'sami.y@email.com', '01200000036', '2025-11-28', 'Mansoura', 'Individual', @hash),
('Laila Morsy', 'laila.m@email.com', '01200000037', '2025-12-01', 'Alex', 'Individual', @hash),
('Mostafa Kamel', 'mostafa.k@email.com', '01200000038', '2025-12-03', 'Cairo', 'Individual', @hash),
('Ghada Abdel-Aziz', 'ghada.a@email.com', '01200000039', '2025-12-05', 'Giza', 'Individual', @hash),
('Hany Shaker', 'hany.sh@email.com', '01200000040', '2025-12-08', 'Ismailia', 'Individual', @hash),
('Sawsan Morsi', 'sawsan.m@email.com', '01200000041', '2025-12-10', 'Cairo', 'Individual', @hash),
('Delta Pharma Corp', 'giving@deltapharma.com', '01200000042', '2025-12-12', 'Cairo', 'Corporate', @hash),
('Horizon Investments', 'csr@horizon.com', '01200000043', '2025-12-15', 'Jeddah', 'Corporate', @hash),
('Wael Gamil', 'wael.g@email.com', '01200000044', '2025-12-18', 'Cairo', 'Individual', @hash),
('Samira Fahmy', 'samira.f@email.com', '01200000045', '2025-12-20', 'Giza', 'Individual', @hash),
('Hamza Rifaat', 'hamza.r@email.com', '01200000046', '2025-12-22', 'Alex', 'Individual', @hash),
('Dalia Nasser', 'dalia.n@email.com', '01200000047', '2025-12-25', 'Cairo', 'Individual', @hash),
('Emad Helmy', 'emad.h@email.com', '01200000048', '2025-12-28', 'Mansoura', 'Individual', @hash),
('Rania Zakaria', 'rania.z@email.com', '01200000049', '2026-01-01', 'Cairo', 'Individual', @hash),
('Osama Fathy', 'osama.f@email.com', '01200000050', '2026-01-03', 'Tanta', 'Individual', @hash),
('Aya Mohammed', 'aya.m@email.com', '01200000051', '2026-01-05', 'Luxor', 'Individual', @hash),
('Nabil Shawkat', 'nabil.sh@email.com', '01200000052', '2026-01-08', 'Cairo', 'Individual', @hash),
('Mervat Ali', 'mervat.a@email.com', '01200000053', '2026-01-10', 'Alex', 'Individual', @hash),
('Ihab Ezzat', 'ihab.e@email.com', '01200000054', '2026-01-12', 'Giza', 'Individual', @hash),
('Nagwa Said', 'nagwa.s@email.com', '01200000055', '2026-01-15', 'Cairo', 'Individual', @hash),
('Al-Amal Charity', 'info@alamal.org', '01200000056', '2026-01-18', 'Cairo', 'Corporate', @hash),
('Red Crescent Friends', 'help@rcfriends.org', '01200000057', '2026-01-20', 'Giza', 'Corporate', @hash),
('Ziad Hossam', 'ziad.h@email.com', '01200000058', '2026-01-22', 'Cairo', 'Individual', @hash),
('Abeer Saad', 'abeer.s@email.com', '01200000059', '2026-01-25', 'Alex', 'Individual', @hash),
('Tarek El-Shenawy', 'tarek.sh@email.com', '01200000060', '2026-01-28', 'Mansoura', 'Individual', @hash),
('Hoda Galal', 'hoda.g@email.com', '01200000061', '2026-02-01', 'Cairo', 'Individual', @hash),
('Ashraf Sobhy', 'ashraf.s@email.com', '01200000062', '2026-02-03', 'Tanta', 'Individual', @hash),
('Eman Fawzy', 'eman.f@email.com', '01200000063', '2026-02-05', 'Giza', 'Individual', @hash),
('Hazem Imam', 'hazem.i@email.com', '01200000064', '2026-02-08', 'Cairo', 'Individual', @hash),
('Omnia Wael', 'omnia.w@email.com', '01200000065', '2026-02-10', 'Suez', 'Individual', @hash),
('Alaa Bayoumy', 'alaa.b@email.com', '01200000066', '2026-02-12', 'Cairo', 'Individual', @hash),
('Neveen Hamed', 'neveen.h@email.com', '01200000067', '2026-02-15', 'Alex', 'Individual', @hash),
('Goodwill Partners', 'care@goodwill.org', '01200000068', '2026-02-18', 'Dubai', 'Corporate', @hash),
('Mohamed Rashad', 'mrashad@email.com', '01200000069', '2026-02-20', 'Cairo', 'Individual', @hash),
('Shaimaa Gamal', 'shaimaa.g@email.com', '01200000070', '2026-02-22', 'Giza', 'Individual', @hash),
('Amr Diab', 'amr.d@email.com', '01200000071', '2026-02-25', 'Cairo', 'Individual', @hash),
('Hala Sedky', 'hala.s@email.com', '01200000072', '2026-02-28', 'Alex', 'Individual', @hash),
('Medhat El-Adl', 'medhat.a@email.com', '01200000073', '2026-03-01', 'Cairo', 'Individual', @hash),
('Safaa Hegazy', 'safaa.h@email.com', '01200000074', '2026-03-03', 'Mansoura', 'Individual', @hash),
('Pyramid Construction', 'csr@pyramid.com', '01200000075', '2026-03-05', 'Cairo', 'Corporate', @hash),
('Reda Hafez', 'reda.h@email.com', '01200000076', '2026-03-08', 'Giza', 'Individual', @hash),
('Karima Abdel-Nour', 'karima.a@email.com', '01200000077', '2026-03-10', 'Tanta', 'Individual', @hash),
('Magdy Shams', 'magdy.s@email.com', '01200000078', '2026-03-12', 'Cairo', 'Individual', @hash),
('Basma Yousry', 'basma.y@email.com', '01200000079', '2026-03-15', 'Alex', 'Individual', @hash),
('Fathy Abdel-Wahab', 'fathy.a@email.com', '01200000080', '2026-03-18', 'Cairo', 'Individual', @hash),
('Ingy Nazif', 'ingy.n@email.com', '01200000081', '2026-03-20', 'Giza', 'Individual', @hash),
('Gamal Mubarak', 'gamal.m@email.com', '01200000082', '2026-03-22', 'Cairo', 'Individual', @hash),
('Soha Darwish', 'soha.d@email.com', '01200000083', '2026-03-25', 'Luxor', 'Individual', @hash),
('United Arab Fund', 'info@uafund.org', '01200000084', '2026-03-28', 'Abu Dhabi', 'Corporate', @hash),
('Fouad Negm', 'fouad.n@email.com', '01200000085', '2026-03-30', 'Cairo', 'Individual', @hash),
('Rawya Mansour', 'rawya.m@email.com', '01200000086', '2026-04-01', 'Alex', 'Individual', @hash),
('Mohsen Gaber', 'mohsen.g@email.com', '01200000087', '2026-04-03', 'Giza', 'Individual', @hash),
('Asmaa Lotfy', 'asmaa.l@email.com', '01200000088', '2026-04-05', 'Cairo', 'Individual', @hash),
('Ehab Tawfik', 'ehab.t@email.com', '01200000089', '2026-04-07', 'Tanta', 'Individual', @hash),
('Sanaa Gameel', 'sanaa.g@email.com', '01200000090', '2026-04-09', 'Cairo', 'Individual', @hash);

-- Additional 30 donors via loop
DECLARE @d int = 91;
WHILE @d <= 120
BEGIN
    INSERT INTO [Donors] ([Name], [Email], [Phone], [RegisterDate], [Address], [Type], [Password])
    VALUES (
        CASE @d % 10
            WHEN 1 THEN 'Yasser Ragab' WHEN 2 THEN 'Menna Allah' WHEN 3 THEN 'Saeed Morsy'
            WHEN 4 THEN 'Nashwa Karim' WHEN 5 THEN 'Taha Hussein' WHEN 6 THEN 'Khadiga Omar'
            WHEN 7 THEN 'Ramzy Helal' WHEN 8 THEN 'Azza Fahmy' WHEN 9 THEN 'Bahaa Taher'
            ELSE 'Wafaa Wassef' END + ' ' + CAST(@d as varchar),
        'donor' + CAST(@d as varchar) + '@charity.com',
        '0125000' + RIGHT('0000' + CAST(@d as varchar), 4),
        DATEADD(day, -(120 - @d), GETDATE()),
        CASE @d % 5 WHEN 0 THEN 'Cairo' WHEN 1 THEN 'Giza' WHEN 2 THEN 'Alex' WHEN 3 THEN 'Luxor' ELSE 'Aswan' END,
        CASE WHEN @d % 7 = 0 THEN 'Corporate' ELSE 'Individual' END,
        @hash
    );
    SET @d = @d + 1;
END;

-- Cases (110 realistic names)
DECLARE @firstEmpId int = (SELECT MIN(Id) FROM Employees);
DECLARE @empCount int = (SELECT COUNT(*) FROM Employees);

INSERT INTO [Cases] ([Name], [Phone], [Address], [RegistDate], [Status], [Description], [CategoryId], [SupervisorId]) VALUES 
('Abdel-Rahman Mahmoud', '01300000001', 'Cairo', '2025-09-10', 'Approved', 'Needs school fees for 3 children', (SELECT TOP 1 Id FROM Categories WHERE Type='Education'), @firstEmpId),
('Fatma El-Zahraa', '01300000002', 'Giza', '2025-09-15', 'Approved', 'Heart surgery required', (SELECT TOP 1 Id FROM Categories WHERE Type='Health'), @firstEmpId+1),
('Saeed Abdel-Ghany', '01300000003', 'Alex', '2025-09-20', 'Completed', 'Monthly food basket', (SELECT TOP 1 Id FROM Categories WHERE Type='Food Supply'), @firstEmpId+2),
('Aisha Mohamed', '01300000004', 'Mansoura', '2025-09-25', 'Approved', 'Orphan sponsorship - 2 children', (SELECT TOP 1 Id FROM Categories WHERE Type='Orphans'), @firstEmpId+3),
('Gamal Abdel-Nasser', '01300000005', 'Tanta', '2025-10-01', 'Pending', 'Debt of 15000 EGP', (SELECT TOP 1 Id FROM Categories WHERE Type='Debt Relief'), @firstEmpId),
('Khadiga Othman', '01300000006', 'Aswan', '2025-10-05', 'Approved', 'House roof collapsed', (SELECT TOP 1 Id FROM Categories WHERE Type='Housing'), @firstEmpId+1),
('Ramadan Sobhy', '01300000007', 'Luxor', '2025-10-10', 'Approved', 'Village needs water well', (SELECT TOP 1 Id FROM Categories WHERE Type='Water Access'), @firstEmpId+2),
('Hanan Tharwat', '01300000008', 'Suez', '2025-10-15', 'Completed', 'General aid package', (SELECT TOP 1 Id FROM Categories WHERE Type='General Charity'), @firstEmpId+3),
('Mohammed Anwar', '01300000009', 'Cairo', '2025-10-20', 'Approved', 'University tuition fees', (SELECT TOP 1 Id FROM Categories WHERE Type='Education'), @firstEmpId),
('Suzanne Moustafa', '01300000010', 'Giza', '2025-10-25', 'Approved', 'Kidney dialysis sessions', (SELECT TOP 1 Id FROM Categories WHERE Type='Health'), @firstEmpId+1),
('Ali El-Gendy', '01300000011', 'Alex', '2025-11-01', 'Pending', 'Monthly food support', (SELECT TOP 1 Id FROM Categories WHERE Type='Food Supply'), @firstEmpId+2),
('Zeinab Abdel-Halim', '01300000012', 'Cairo', '2025-11-05', 'Approved', 'Orphan - lost both parents', (SELECT TOP 1 Id FROM Categories WHERE Type='Orphans'), @firstEmpId+3),
('Magdi Yaqoub', '01300000013', 'Mansoura', '2025-11-10', 'Completed', 'Medical debt cleared', (SELECT TOP 1 Id FROM Categories WHERE Type='Debt Relief'), @firstEmpId),
('Samia Gamal', '01300000014', 'Tanta', '2025-11-15', 'Approved', 'Home renovation needed', (SELECT TOP 1 Id FROM Categories WHERE Type='Housing'), @firstEmpId+1),
('Hussein Fadel', '01300000015', 'Aswan', '2025-11-20', 'Approved', 'Water pipe installation', (SELECT TOP 1 Id FROM Categories WHERE Type='Water Access'), @firstEmpId+2),
('Nagat El-Saghira', '01300000016', 'Luxor', '2025-11-25', 'Pending', 'Winter clothing', (SELECT TOP 1 Id FROM Categories WHERE Type='General Charity'), @firstEmpId+3),
('Abdel-Halim Hafez', '01300000017', 'Cairo', '2025-12-01', 'Approved', 'School supplies for 5 kids', (SELECT TOP 1 Id FROM Categories WHERE Type='Education'), @firstEmpId),
('Fardous Abdel-Hamid', '01300000018', 'Giza', '2025-12-05', 'Completed', 'Eye surgery completed', (SELECT TOP 1 Id FROM Categories WHERE Type='Health'), @firstEmpId+1),
('Ragab El-Tayyeb', '01300000019', 'Alex', '2025-12-10', 'Approved', 'Ramadan food package', (SELECT TOP 1 Id FROM Categories WHERE Type='Food Supply'), @firstEmpId+2),
('Soad Hosny', '01300000020', 'Cairo', '2025-12-15', 'Approved', 'Orphan child sponsorship', (SELECT TOP 1 Id FROM Categories WHERE Type='Orphans'), @firstEmpId+3);

-- Generate remaining 90 cases
DECLARE @c int = 1;
WHILE @c <= 90
BEGIN
    INSERT INTO [Cases] ([Name], [Phone], [Address], [RegistDate], [Status], [Description], [CategoryId], [SupervisorId])
    VALUES (
        CASE @c % 20
            WHEN 1 THEN 'Hamdy El-Wazir' WHEN 2 THEN 'Warda Gamal' WHEN 3 THEN 'Naguib Mahfouz'
            WHEN 4 THEN 'Tahany Rashid' WHEN 5 THEN 'Shady Habash' WHEN 6 THEN 'Maysoon Fawzy'
            WHEN 7 THEN 'Baher Saeed' WHEN 8 THEN 'Nihad Sherif' WHEN 9 THEN 'Wagdy Ghoneim'
            WHEN 10 THEN 'Afaf Shoeib' WHEN 11 THEN 'Sobhy Bidair' WHEN 12 THEN 'Thoraya Ibrahim'
            WHEN 13 THEN 'Lotfy El-Sayed' WHEN 14 THEN 'Hoda Rostom' WHEN 15 THEN 'Radwan Ismail'
            WHEN 16 THEN 'Nawal Darwish' WHEN 17 THEN 'Amin Nabil' WHEN 18 THEN 'Lubna Helmy'
            WHEN 19 THEN 'Fahmy Abdel-Aziz' ELSE 'Sharifa Fadel' END + ' ' + CAST(20 + @c as varchar),
        '01350' + RIGHT('00000' + CAST(@c as varchar), 5),
        CASE @c % 6 WHEN 0 THEN 'Cairo' WHEN 1 THEN 'Giza' WHEN 2 THEN 'Alex' WHEN 3 THEN 'Mansoura' WHEN 4 THEN 'Tanta' ELSE 'Aswan' END,
        DATEADD(day, -(@c * 2), GETDATE()),
        CASE @c % 4 WHEN 0 THEN 'Pending' WHEN 1 THEN 'Approved' WHEN 2 THEN 'Approved' ELSE 'Completed' END,
        CASE @c % 8
            WHEN 0 THEN 'School fees assistance' WHEN 1 THEN 'Medical treatment needed'
            WHEN 2 THEN 'Monthly food supplies' WHEN 3 THEN 'Orphan monthly allowance'
            WHEN 4 THEN 'Debt settlement needed' WHEN 5 THEN 'House renovation work'
            WHEN 6 THEN 'Clean water access' ELSE 'General welfare support' END,
        (SELECT TOP 1 Id FROM Categories ORDER BY NEWID()),
        @firstEmpId + (@c % @empCount)
    );
    SET @c = @c + 1;
END;

-- Donations (120 rows spread over 6 months for rich charts)
DECLARE @donorCount int = (SELECT COUNT(*) FROM Donors);
DECLARE @firstDonorId int = (SELECT MIN(Id) FROM Donors);

DECLARE @dn int = 1;
WHILE @dn <= 120
BEGIN
    INSERT INTO [Donations] ([Amount], [Description], [Status], [Date], [DonorId], [CategoryId], [SupervisorId])
    VALUES (
        CAST((ABS(CHECKSUM(NEWID())) % 9500) + 500 as DECIMAL(18,2)),
        CASE @dn % 7
            WHEN 0 THEN 'Zakat Al-Maal' WHEN 1 THEN 'Sadaqah Jariyah' WHEN 2 THEN 'Monthly donation'
            WHEN 3 THEN 'Emergency relief' WHEN 4 THEN 'Ramadan campaign' WHEN 5 THEN 'Eid Al-Adha gift'
            ELSE 'General contribution' END,
        CASE @dn % 5 WHEN 0 THEN 'Pending' WHEN 4 THEN 'Cancelled' ELSE 'Completed' END,
        DATEADD(day, -(@dn * 1.5), GETDATE()),
        @firstDonorId + (@dn % @donorCount),
        (SELECT TOP 1 Id FROM Categories ORDER BY NEWID()),
        @firstEmpId + (@dn % @empCount)
    );
    SET @dn = @dn + 1;
END;

-- Distributions (80 rows)
DECLARE @caseCount int = (SELECT COUNT(*) FROM Cases);
DECLARE @firstCaseId int = (SELECT MIN(Id) FROM Cases);
DECLARE @donationCount int = (SELECT COUNT(*) FROM Donations);
DECLARE @firstDonationId int = (SELECT MIN(Id) FROM Donations);

DECLARE @di int = 1;
WHILE @di <= 80
BEGIN
    INSERT INTO [Distributions] ([Amount], [DistributionDate], [Status], [Notes], [CaseId], [DonationId], [HandledByEmployeeId])
    VALUES (
        CAST((ABS(CHECKSUM(NEWID())) % 4000) + 200 as DECIMAL(18,2)),
        DATEADD(day, -(@di * 2) + 1, GETDATE()),
        CASE @di % 3 WHEN 0 THEN 'Pending' WHEN 1 THEN 'Delivered' ELSE 'Delivered' END,
        CASE @di % 5
            WHEN 0 THEN 'Delivered to beneficiary at home'
            WHEN 1 THEN 'Collected from office'
            WHEN 2 THEN 'Bank transfer completed'
            WHEN 3 THEN 'Delivered via volunteer'
            ELSE 'Handed to family member' END,
        @firstCaseId + (@di % @caseCount),
        @firstDonationId + (@di % @donationCount),
        @firstEmpId + (@di % @empCount)
    );
    SET @di = @di + 1;
END;

-- =============================================
-- TARGETED DONATIONS FOR COMPLETE CHARTS
-- =============================================

-- donationByDay: Add donations for each hour of today (24 hours)
DECLARE @todayBase DATETIME2 = CAST(CAST(GETUTCDATE() AS DATE) AS DATETIME2);
DECLARE @hr int = 0;
WHILE @hr <= 23
BEGIN
    INSERT INTO [Donations] ([Amount], [Description], [Status], [Date], [DonorId], [CategoryId], [SupervisorId])
    VALUES (
        CAST((ABS(CHECKSUM(NEWID())) % 3000) + 500 as DECIMAL(18,2)),
        'Hourly donation H' + CAST(@hr as varchar),
        'Completed',
        DATEADD(hour, @hr, @todayBase),
        @firstDonorId + (@hr % @donorCount),
        (SELECT TOP 1 Id FROM Categories ORDER BY NEWID()),
        @firstEmpId + (@hr % @empCount)
    );
    SET @hr = @hr + 1;
END;

-- donationByWeek: Add donations for each day of the current week (Sun-Sat)
DECLARE @startOfWeek DATETIME2 = DATEADD(day, -DATEPART(dw, GETUTCDATE()) + 1, CAST(CAST(GETUTCDATE() AS DATE) AS DATETIME2));
DECLARE @wd int = 0;
WHILE @wd <= 6
BEGIN
    DECLARE @weekDay DATETIME2 = DATEADD(day, @wd, @startOfWeek);
    -- Only add if the day is today or earlier
    IF @weekDay <= GETUTCDATE()
    BEGIN
        -- Add 3 donations per day for richer data
        DECLARE @wk int = 1;
        WHILE @wk <= 3
        BEGIN
            INSERT INTO [Donations] ([Amount], [Description], [Status], [Date], [DonorId], [CategoryId], [SupervisorId])
            VALUES (
                CAST((ABS(CHECKSUM(NEWID())) % 5000) + 1000 as DECIMAL(18,2)),
                'Weekly donation D' + CAST(@wd as varchar) + '_' + CAST(@wk as varchar),
                'Completed',
                DATEADD(hour, @wk * 3, @weekDay),
                @firstDonorId + ((@wd * 3 + @wk) % @donorCount),
                (SELECT TOP 1 Id FROM Categories ORDER BY NEWID()),
                @firstEmpId + ((@wd + @wk) % @empCount)
            );
            SET @wk = @wk + 1;
        END;
    END;
    SET @wd = @wd + 1;
END;

-- donationByMonth: Ensure ALL months Jan-Dec have data (backfill previous year for months 5-12)
DECLARE @mn int = 1;
WHILE @mn <= 12
BEGIN
    DECLARE @monthDate DATETIME2;
    IF @mn <= MONTH(GETUTCDATE())
        SET @monthDate = DATEFROMPARTS(YEAR(GETUTCDATE()), @mn, 15);
    ELSE
        SET @monthDate = DATEFROMPARTS(YEAR(GETUTCDATE()) - 1, @mn, 15);
    
    -- Add 5 donations per month
    DECLARE @mk int = 1;
    WHILE @mk <= 5
    BEGIN
        INSERT INTO [Donations] ([Amount], [Description], [Status], [Date], [DonorId], [CategoryId], [SupervisorId])
        VALUES (
            CAST((ABS(CHECKSUM(NEWID())) % 8000) + 2000 as DECIMAL(18,2)),
            'Monthly fill M' + CAST(@mn as varchar),
            'Completed',
            DATEADD(day, @mk * 2, @monthDate),
            @firstDonorId + ((@mn * 5 + @mk) % @donorCount),
            (SELECT TOP 1 Id FROM Categories ORDER BY NEWID()),
            @firstEmpId + ((@mn + @mk) % @empCount)
        );
        SET @mk = @mk + 1;
    END;
    SET @mn = @mn + 1;
END;
GO

-- Verification
SELECT 'Employees' as TableName, COUNT(*) as [Total] FROM Employees
UNION ALL SELECT 'Categories', COUNT(*) FROM Categories
UNION ALL SELECT 'Donors', COUNT(*) FROM Donors
UNION ALL SELECT 'Cases', COUNT(*) FROM Cases
UNION ALL SELECT 'Donations', COUNT(*) FROM Donations
UNION ALL SELECT 'Distributions', COUNT(*) FROM Distributions;
GO

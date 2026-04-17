-- 1. Insert Admin Employee (Ibrahim)
-- Password '12345678' hashed with BCrypt
IF NOT EXISTS (SELECT 1 FROM Employees WHERE Username = 'Ibrahim')
BEGIN
    INSERT INTO Employees (Name, Phone, Address, Email, Role, Username, Password)
    VALUES ('Ibrahim Admin', '01012345678', 'Cairo, Egypt', 'ibrahim@admin.com', 'Admin', 'Ibrahim', '$2a$11$6p7uI5f8e6G6E7L3A1V2B.mF5qU7d8e9F0G1H2I3J4K5L6M7N8O9P');
END

-- 2. Insert Categories
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Type = 'Education')
    INSERT INTO Categories (Type, Description) VALUES ('Education', 'School and University support');
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Type = 'Health')
    INSERT INTO Categories (Type, Description) VALUES ('Health', 'Medical treatments and surgeries');
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Type = 'Food')
    INSERT INTO Categories (Type, Description) VALUES ('Food', 'Monthly food packages');

-- 3. Insert Sample Donors
IF NOT EXISTS (SELECT 1 FROM Donors WHERE Email = 'donor1@test.com')
    INSERT INTO Donors (Name, Email, Phone, Address, Type, RegisterDate) 
    VALUES ('Mohamed Ali', 'donor1@test.com', '01111112222', 'Alexandria', 'Individual', GETDATE());

-- 4. Insert Sample Cases (Beneficiaries)
-- Using dummy supervisorId = 1 (Ibrahim) and categoryId = 1 (Education)
IF NOT EXISTS (SELECT 1 FROM Cases WHERE Name = 'Case Example 1')
    INSERT INTO Cases (Name, Phone, Address, RegistDate, Status, Description, CategoryId, SupervisorId)
    VALUES ('Case Example 1', '01233334444', 'Giza, Egypt', GETDATE(), 'Approved', 'Needs help for surgery', 2, 1);

-- 5. Insert Sample Donations
IF NOT EXISTS (SELECT 1 FROM Donations WHERE Description = 'Initial Donation')
    INSERT INTO Donations (Amount, Description, Status, Date, DonorId, CategoryId, SupervisorId)
    VALUES (5000.00, 'Initial Donation', 'Completed', GETDATE(), 1, 3, 1);

-- 6. Insert Sample Distribution
IF NOT EXISTS (SELECT 1 FROM Distributions WHERE Amount = 1000.00)
    INSERT INTO Distributions (Amount, DistributionDate, Status, Notes, CaseId, DonationId, HandledByEmployeeId)
    VALUES (1000.00, GETDATE(), 'Completed', 'Handed to the family', 1, 1, 1);

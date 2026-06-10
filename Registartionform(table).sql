CREATE TABLE Employees (
    EmployeeId SERIAL PRIMARY KEY,
    FullName VARCHAR(100),
    FatherName VARCHAR(100),
    MotherName VARCHAR(100),
    DateOfBirth DATE,
    Gender VARCHAR(20),
    MaritalStatus VARCHAR(20),
    SpouseName VARCHAR(100),
    Email VARCHAR(100),
    MobileNumber VARCHAR(15),
    EmergencyContact VARCHAR(15),
    PermanentAddress TEXT,
    CurrentAddress TEXT,
    Department VARCHAR(100),
    Role VARCHAR(100),
    Salary DECIMAL(10,2),
    PFNumber VARCHAR(50),
    TaxNumber VARCHAR(50)
);
SELECT * FROM Employees;


CREATE TABLE EmployeeMaster (
    EmployeeId INT PRIMARY KEY,
    FullName VARCHAR(100),
    FatherName VARCHAR(100),
    MotherName VARCHAR(100),
    DateOfBirth DATE,
    Gender VARCHAR(20),
    MaritalStatus VARCHAR(20),
    SpouseName VARCHAR(100),
    Email VARCHAR(100),
    MobileNumber VARCHAR(15),
    EmergencyContact VARCHAR(15),
    PermanentAddress TEXT,
    CurrentAddress TEXT,
    Department VARCHAR(100),
    Role VARCHAR(100),
    Salary DECIMAL(10,2),
    PFNumber VARCHAR(50),
    TaxNumber VARCHAR(50)
);

INSERT INTO EmployeeMaster (
    EmployeeId,
    FullName, FatherName, MotherName,
    DateOfBirth, Gender, MaritalStatus,
    SpouseName, Email, MobileNumber,
    EmergencyContact, PermanentAddress, CurrentAddress,
    Department, Role, Salary, PFNumber, TaxNumber
)
SELECT 
    EmployeeId,
    FullName, FatherName, MotherName,
    DateOfBirth, Gender, MaritalStatus,
    SpouseName, Email, MobileNumber,
    EmergencyContact, PermanentAddress, CurrentAddress,
    Department, Role, Salary, PFNumber, TaxNumber
FROM Employees;
SELECT * FROM EmployeeMaster;


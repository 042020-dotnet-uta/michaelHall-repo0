-- clear the tables for recreation
DROP TABLE Empdetails;
DROP TABLE Employee;
DROP TABLE Department;

-- create all the tables
CREATE TABLE Department (
	DeptID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	Name NVARCHAR(50) NOT NULL,
	Location NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Employee (
	EmployeeID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	SSN INT NULL,
	DeptID INT NOT NULL FOREIGN KEY REFERENCES Department (DeptID)
);
GO

CREATE TABLE EmpDetails (
	EmpDetailsID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	EmployeeID INT NOT NULL FOREIGN KEY REFERENCES Employee (EmployeeID),
	Salary INT NOT NULL,
	Address_1 NVARCHAR(100) NOT NULL,
	Address_2 NVARCHAR(100) NULL,
	City NVARCHAR(100) NOT NULL,
	State NVARCHAR(100) NOT NULL,
	Country NVARCHAR(100) NOT NULL
);
GO

-- Add the 3 records to each table
INSERT INTO Department (Name, Location) VALUES ('Human Resources', 'Middle Building');
INSERT INTO Department (Name, Location) VALUES ('Technology', 'East Building');
-- Add marketing department
INSERT INTO Department (Name, Location) VALUES ('Marketing', 'West Building');

INSERT INTO Employee (FirstName, LastName, SSN, DeptID) VALUES ('Michael', 'Hall', 182316523, '2');
INSERT INTO Employee (FirstName, LastName, SSN, DeptID) VALUES ('Jayson', 'Lennon', 732623461, '1');
INSERT INTO Employee (FirstName, LastName, SSN, DeptID) VALUES ('Ken', 'Endo', 827332254, '3');

INSERT INTO EmpDetails (EmployeeID, Salary, Address_1, Address_2, City, State, Country) VALUES 
(2, $62000, '727 Rose Ln', '283 weedle Town', 'Melville', 'CA', 'USA');
INSERT INTO EmpDetails (EmployeeID, Salary, Address_1, Address_2, City, State, Country) VALUES 
(3, $58000, '42 Sesame Ave', '19 Peachtree St', 'Oxford', 'Wales', 'United Kingdom');
INSERT INTO EmpDetails (EmployeeID, Salary, Address_1, Address_2, City, State, Country) VALUES 
(1, $60000, '827 Belwood St', NULL, 'Mechanicsburg', 'PA', 'USA');

-- add tina to employees and Marketing department
INSERT INTO Employee (FirstName, LastName, SSN, DeptID) VALUES ('Tina', 'Smith', 827364957, '3');
INSERT INTO EmpDetails (EmployeeID, Salary, Address_1, Address_2, City, State, Country) VALUES 
(4, $59000, '23 Lebron Ln', NULL, 'Austin', 'Texas', 'USA');

-- show each table to see if they were made correctly
SELECT * FROM Department;
SELECT * FROM Employee;
SELECT * FROM EmpDetails;

-- List all employees in Marketing
SELECT FirstName, LastName
FROM Employee
WHERE DeptID 
	IN (SELECT DeptID FROM Department WHERE Name = 'Marketing');

-- report total salary of Marketing
SELECT SUM(Salary) AS MarketingTeamTotalSalary
FROM EmpDetails
WHERE EmployeeID 
	IN (SELECT EmployeeID FROM Employee WHERE DeptID
		IN (SELECT DeptID FROM Department WHERE Name = 'Marketing'));

-- report total employees by department
SELECT Department.Name, COUNT(EmployeeID) AS totalEmployees
FROM Employee JOIN Department ON Department.DeptID = Employee.DeptID
GROUP BY Department.Name;

-- increase salary of Tina Smith to $90,000
UPDATE EmpDetails
SET Salary = $90000
WHERE EmployeeID
	IN (SELECT EmployeeID FROM Employee WHERE FirstName = 'Tina' AND LastName = 'Smith');

-- show change to salary
SELECT * FROM EmpDetails;
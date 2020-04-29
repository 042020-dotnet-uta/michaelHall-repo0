-- Done with Ryan and Sulav

-- clear the tables for recreation
--DROP TABLE State;
--DROP TABLE Country;

-- create all the tables
CREATE TABLE Country (
	countryID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	country NVARCHAR(50) NULL
);
GO

CREATE TABLE State (
	stateID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	countryID INT NULL FOREIGN KEY REFERENCES Country (countryID),
	state NVARCHAR(50) NULL
);
GO

INSERT INTO Country (country) VALUES ('USA');
INSERT INTO Country (country) VALUES ('GB');
INSERT INTO Country (country) VALUES ('Canada');
INSERT INTO Country (country) VALUES ('Brazil');
INSERT INTO Country (country) VALUES ('China');
INSERT INTO Country (country) VALUES (null);

INSERT INTO State (countryID, state) VALUES (1, 'Texas');
INSERT INTO State (countryID, state) VALUES (4, 'Sao Paolo');
INSERT INTO State (countryID, state) VALUES (3, 'Ontario');
INSERT INTO State (countryID, state) VALUES (2, 'Wales');
INSERT INTO State(countryID, state) VALUES (5, 'Wuhan');
INSERT INTO State(countryID, state) VALUES (null, 'Bavaria');

Select * From Country;
Select * From State;

Select * From Country Cross Join State;
Select * From Country As c 
Inner Join State As s
On c.countryID = s.countryID;
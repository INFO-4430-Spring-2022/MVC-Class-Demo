CREATE TABLE dbo.ThingTypes(
	ThingTypeID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(30) NOT NULL
    ,CanShare bit NOT NULL
) 

CREATE TABLE dbo.Things(
	ThingID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(30) NOT NULL
	,Description nvarchar(400) NOT NULL
	,ThingTypeID int NOT NULL
) 

CREATE TABLE dbo.People(
	PersonID int IDENTITY(1,1) PRIMARY KEY
	,FirstName nvarchar(40) NOT NULL
	,LastName nvarchar(40)  NOT NULL
	,DateOfBirth datetime  NOT NULL
	,IsManager bit  NOT NULL
	,Prefix nvarchar(6)  NOT NULL
	,Postfix nvarchar(8)  NOT NULL
	,Phone nvarchar(15)  NOT NULL
	,Email nvarchar(250)  NOT NULL
	,Homepage nvarchar(400) NOT NULL
)

CREATE TABLE dbo.PeopleThings(
	PersonID int REFERENCES People(PersonID)
	,ThingID int REFERENCES Things(ThingID)
	,CONSTRAINT PK_PeopleThings PRIMARY KEY(PersonID,ThingID)
)

CREATE TABLE dbo.Roles(
	RoleID int IDENTITY(1,1) PRIMARY KEY
	,Name nvarchar(50) NOT NULL
	,IsAdmin bit NOT NULL
	,CanViewPerson bit NOT NULL
	,CanAddPerson bit NOT NULL
	,CanEditPerson bit NOT NULL
	,CanViewThing bit NOT NULL
	,CanAddThing bit NOT NULL
	,CanEditThing bit NOT NULL
	,CanViewThingType bit NOT NULL
	,CanAddThingType bit NOT NULL
	,CanEditThingType bit NOT NULL
	,CanViewUser bit NOT NULL
	,CanAddUser bit NOT NULL
	,CanEditUser bit NOT NULL
	)

CREATE TABLE dbo.Users(
	UserID int IDENTITY(1,1) PRIMARY KEY
	,UserName nvarchar(75) NOT NULL
	,Password char(64) NOT NULL  -- 48 bytes -> base 64 = 64 ASCII characters
	,Salt char(24) NOT NULL -- 18 bytes -> base 64 = 24 ASCII characters
	,Email nvarchar(200) NOT NULL
	,RoleID int REFERENCES Roles(RoleID)
)


-- https://generatedata.com/ 
SET IDENTITY_INSERT dbo.People ON -- Needed to allow keys to be defined. 
INSERT INTO dbo.People (PersonID, FirstName, LastName, DateOfBirth, IsManager, Prefix, Postfix, Phone, Email, Homepage) VALUES
							(1,'Yuli', 'Torres', '2000-09-14', 0, 'Dr.', '', '(484)520-5318', 'dolor@nonsollicitudina.net', 'http://example.com/')
							,(2,'Hyatt', 'Hodges', '1967-08-21', 0, '', 'III', '(238)753-9038', 'pede.malda.vel@risusNullaeget.edu', 'https://baby.example.com/?baby=act')
							,(3,'Peter', 'Hurst', '1988-11-06', 0, 'Mr.', '', '(818)454-5091', 'dolor@a.edu', 'https://www.example.com/')
							,(4,'Loga', 'O''Connor', '1974-04-30', 0, '', '', '(577)816-6487', 'cursus@netus.org', 'http://www.example.com/')
							,(5,'Rae', 'Hines', '1966-12-11', 1, 'Mrs.', '', '(674)542-1281', 'at@orciquis.org', 'https://brother.example.com/bikes')
							,(6,'Alma', 'Emerso', '1983-03-18', 0, 'Mr.', '', '(835)648-0434', 'tempus.risus@hendrerit.org', 'https://www.example.com/air')
							,(7,'Glenna', 'Reeves', '1997-01-28', 0, 'Ms.', '', '(488)824-6617', 'elit@Maecenasmifelis.ca', 'http://blade.example.com/books/attractio')
							,(8,'Whilemina', 'Roberso', '1981-09-20', 0, '', '', '671-8062', 'ultrices.rhoncus@etmagnisdis.net', 'http://www.example.edu/')
							,(9,'Price', 'Macias', '1993-04-27', 0, 'Mr.', '', '(735)177-6451', 'tincidunt@elitAliquam.com', 'http://www.example.edu/basin.html')
SET IDENTITY_INSERT dbo.People OFF

SET IDENTITY_INSERT dbo.ThingTypes ON
INSERT INTO dbo.ThingTypes (ThingTypeID,Name,CanShare) VALUES
                            (1,  'Hygenetic', 0)
							,(2, 'Books', 1)
							,(3, 'Personal', 0)
SET IDENTITY_INSERT dbo.ThingTypes OFF

SET IDENTITY_INSERT dbo.Things ON
INSERT INTO dbo.Things (ThingID, [Name], [Description],ThingTypeID) VALUES
							(1,'Toothbrush','You can brush your teeth.',1)
							,(2,'Mouse and the Motorcycle', 'Book about a mouse and a motorcycle.',2)
							,(3,'Wallet', 'Holds your money.',3)
							,(4,'Hair Dryer','Used to dry your hair.',1)
							,(5,'Shaver', 'Used to remove hair from your face.',1)
							,(6,'The Hobbit', 'Book that a lot of people read.',2)
							,(7,'Phone', 'Personal communication device.',3)
SET IDENTITY_INSERT dbo.Things OFF

-- Need defined keys so that we know who to add to which items.
INSERT INTO dbo.PeopleThings VALUES
			(1,1)
			,(1,3)
			,(1,5)
			,(1,7)
			,(2,1)
			,(2,2)
			,(2,3)
			,(2,5)
			,(3,1)
			,(3,7)
			,(3,5)
			,(4,1)
			,(4,4)
			,(5,1)
			,(5,4)
			,(5,7)
			,(6,1)
			,(7,1)
			,(7,2)
			,(8,1)
			,(8,7)
			,(8,3)

GO

-- Add Users and Permissions

SET IDENTITY_INSERT dbo.Roles ON

INSERT INTO Roles (RoleID,Name,IsAdmin ,
	CanViewPerson ,CanAddPerson ,CanEditPerson ,
	CanViewThing ,CanAddThing ,CanEditThing ,
	CanViewThingType ,CanAddThingType ,CanEditThingType ,
	CanViewUser ,CanAddUser ,CanEditUser)
	VALUES
		(1, 'Admin', 1,
		1,1,1,
		1,1,1,
		1,1,1,
		1,1,1)
		,(2, 'Power User', 0,
		1,1,1,
		1,1,1,
		1,1,1,
		1,1,1)
		,(3, 'User', 0,
		1,1,0,
		1,1,0,
		1,1,0,
		1,1,0)
		,(4, 'Guest', 0,
		1,0,0,
		1,0,0,
		1,0,0,
		1,0,0)

SET IDENTITY_INSERT dbo.Roles OFF

INSERT INTO Users (UserName,Password,Email,RoleID)
	VALUES
		('bob','12345trgsfds','bob@isu.edu',1)
		,('sally','12345trgsfds','sally@isu.edu',2)
		,('asim','12345trgsfds','asim@isu.edu',3)
		,('ben','12345trgsfds','benjamin@isu.edu',4)
		,('alex','12345trgsfds','alexandria@isu.edu',4)
		,('jose','12345trgsfds','jose.torez@isu.edu',4)
		,('bobby','12345trgsfds','robert@isu.edu',4)

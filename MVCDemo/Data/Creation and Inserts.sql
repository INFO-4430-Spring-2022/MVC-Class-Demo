CREATE TABLE dbo.ThingTypes(
	ThingTypeID int IDENTITY(1,1) NOT NULL
	,Name nvarchar(30) NOT NULL
    ,CanShare bit NOT NULL
) 

CREATE TABLE dbo.Things(
	ThingID int IDENTITY(1,1) NOT NULL
	,Name nvarchar(30) NOT NULL
	,Description nvarchar(400) NOT NULL
	,TypeID int NOT NULL
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



-- https://generatedata.com/ 
INSERT INTO dbo.People VALUES ('Yuli', 'Torres', '2000-09-14', 0, 'Dr.', '', '(484)520-5318', 'dolor@nonsollicitudina.net', 'http://example.com/')
							,('Hyatt', 'Hodges', '1967-08-21', 0, '', 'III', '(238)753-9038', 'pede.malda.vel@risusNullaeget.edu', 'https://baby.example.com/?baby=act')
							,('Peter', 'Hurst', '1988-11-06', 0, 'Mr.', '', '(818)454-5091', 'dolor@a.edu', 'https://www.example.com/')
							,('Loga', 'O''Connor', '1974-04-30', 0, '', '', '(577)816-6487', 'cursus@netus.org', 'http://www.example.com/')
							,('Rae', 'Hines', '1966-12-11', 1, 'Mrs.', '', '(674)542-1281', 'at@orciquis.org', 'https://brother.example.com/bikes')
							,('Alma', 'Emerso', '1983-03-18', 0, 'Mr.', '', '(835)648-0434', 'tempus.risus@hendrerit.org', 'https://www.example.com/air')
							,('Glenna', 'Reeves', '1997-01-28', 0, 'Ms.', '', '(488)824-6617', 'elit@Maecenasmifelis.ca', 'http://blade.example.com/books/attractio')
							,('Whilemina', 'Roberso', '1981-09-20', 0, '', '', '671-8062', 'ultrices.rhoncus@etmagnisdis.net', 'http://www.example.edu/')
							,('Price', 'Macias', '1993-04-27', 0, 'Mr.', '', '(735)177-6451', 'tincidunt@elitAliquam.com', 'http://www.example.edu/basin.html')

SET IDENTITY_INSERT dbo.ThingTypes ON
INSERT INTO dbo.ThingTypes (ThingTypeID,Name,CanShare) VALUES
                            (1,  'Hygenetic', 0)
							,(2, 'Books', 1)
							,(3, 'Personal', 0)
SET IDENTITY_INSERT dbo.ThingTypes OFF


INSERT INTO dbo.Things VALUES
                            ('Toothbrush','You can brush your teeth.',1)
							,('Mouse and the Motorcycle', 'Book about a mouse and a motorcycle.',2)
							,('Wallet', 'Holds your money.',3)
							,('Hair Dryer','Used to dry your hair.',1)
							,('Shaver', 'Used to remove hair from your face.',1)
							,('The Hobbit', 'Book that a lot of people read.',2)
							,('Phone', 'Personal communication device.',3)



GO
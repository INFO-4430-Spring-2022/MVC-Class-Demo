-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Add a new  ThingType to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_ThingTypeAdd
@ThingTypeID int OUTPUT,
@Name nvarchar(30),
@CanShare bit
AS
     INSERT INTO ThingTypes(Name,CanShare)
               VALUES(@Name,@CanShare)
     SET @ThingTypeID = @@IDENTITY
GO

-- GRANT EXECUTE ON dbo.sproc_ThingTypeAdd TO db_editor
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Update ThingType in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_ThingTypeUpdate
@ThingTypeID int,
@Name nvarchar(30),
@CanShare bit
AS
     UPDATE ThingTypes
          SET
               Name = @Name,
               CanShare = @CanShare
          WHERE ThingTypeID = @ThingTypeID
GO

-- GRANT EXECUTE ON dbo.sproc_ThingTypeUpdate TO db_editor
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Retrieve specific ThingType from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocThingTypeGet
@ThingTypeID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM ThingTypes
     WHERE ThingTypeID = @ThingTypeID
END
GO

-- GRANT EXECUTE ON dbo.sprocThingTypeGet TO db_reader
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Retrieve all ThingTypes from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocThingTypesGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM ThingTypes
END
GO

-- GRANT EXECUTE ON dbo.sprocThingTypesGetAll TO db_reader
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Remove specific ThingType from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_ThingTypeRemove
@ThingTypeID int
AS
BEGIN
     DELETE FROM ThingTypes
          WHERE ThingTypeID = @ThingTypeID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

-- GRANT EXECUTE ON dbo.sproc_ThingTypeRemove TO db_editor
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Add a new  Thing to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_ThingAdd
@ThingID int OUTPUT,
@Name nvarchar(30),
@Description nvarchar(400),
@ThingTypeID int
AS
     INSERT INTO Things(Name,Description,ThingTypeID)
               VALUES(@Name,@Description,@ThingTypeID)
     SET @ThingID = @@IDENTITY
GO

-- GRANT EXECUTE ON dbo.sproc_ThingAdd TO db_editor
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Update Thing in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_ThingUpdate
@ThingID int,
@Name nvarchar(30),
@Description nvarchar(400),
@ThingTypeID int
AS
     UPDATE Things
          SET
               Name = @Name,
               Description = @Description,
               ThingTypeID = @ThingTypeID
          WHERE ThingID = @ThingID
GO

-- GRANT EXECUTE ON dbo.sproc_ThingUpdate TO db_editor
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Retrieve specific Thing from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocThingGet
@ThingID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Things
     WHERE ThingID = @ThingID
END
GO

-- GRANT EXECUTE ON dbo.sprocThingGet TO db_reader
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Retrieve all Things from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocThingsGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Things
END
GO

-- GRANT EXECUTE ON dbo.sprocThingsGetAll TO db_reader
-- GO 

-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Retrieve all Things from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocThingsGetForPerson
@PersonID int 
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Things t
        JOIN PeopleThings pt ON t.ThingID = pt.ThingID
        WHERE pt.PersonID = @PersonID
END
GO

-- GRANT EXECUTE ON dbo.sprocThingsGetForPerson TO db_reader
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Remove specific Thing and Person Association from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_PersonThingRemove
@PersonID int
,@ThingID int
AS
BEGIN
     DELETE FROM PeopleThings
          WHERE ThingID = @ThingID
		  AND PersonID = @PersonID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

-- GRANT EXECUTE ON dbo.sproc_PersonThingRemove TO db_editor
-- GO 

-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Remove specific Thing from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_ThingRemove
@ThingID int
AS
BEGIN
     DELETE FROM Things
          WHERE ThingID = @ThingID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

-- GRANT EXECUTE ON dbo.sproc_ThingRemove TO db_editor
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Add a new  Person to the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_PersonAdd
@PersonID int OUTPUT,
@FirstName nvarchar(40),
@LastName nvarchar(40),
@DateOfBirth datetime,
@IsManager bit,
@Prefix nvarchar(6),
@Postfix nvarchar(8),
@Phone nvarchar(15),
@Email nvarchar(250),
@Homepage nvarchar(400)
AS
     INSERT INTO People(FirstName,LastName,DateOfBirth,IsManager,Prefix,Postfix,Phone,Email,Homepage)
               VALUES(@FirstName,@LastName,@DateOfBirth,@IsManager,
               @Prefix,@Postfix,@Phone,@Email,@Homepage)
               
     SET @PersonID = @@IDENTITY
GO

-- GRANT EXECUTE ON dbo.sproc_PersonAdd TO db_editor
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Update Person in the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_PersonUpdate
@PersonID int,
@FirstName nvarchar(40),
@LastName nvarchar(40),
@DateOfBirth datetime,
@IsManager bit,
@Prefix nvarchar(6),
@Postfix nvarchar(8),
@Phone nvarchar(15),
@Email nvarchar(250),
@Homepage nvarchar(400)
AS
     UPDATE People
          SET
               FirstName = @FirstName,
               LastName = @LastName,
               DateOfBirth = @DateOfBirth,
               IsManager = @IsManager,
               Prefix = @Prefix,
               Postfix = @Postfix,
               Phone = @Phone,
               Email = @Email,
               Homepage = @Homepage
          WHERE PersonID = @PersonID
GO

-- GRANT EXECUTE ON dbo.sproc_PersonUpdate TO db_editor
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Retrieve specific Person from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocPersonGet
@PersonID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM People
     WHERE PersonID = @PersonID
END
GO

-- GRANT EXECUTE ON dbo.sprocPersonGet TO db_reader
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Retrieve all People from the database.
-- =============================================
CREATE PROCEDURE dbo.sprocPeopleGetAll
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM People
END
GO

-- GRANT EXECUTE ON dbo.sprocPeopleGetAll TO db_reader
-- GO 


-- =============================================
-- Author:		Jon Holmes
-- Create date:	14 Mar 2022
-- Description:	Remove specific Person from the database.
-- =============================================
CREATE PROCEDURE dbo.sproc_PersonRemove
@PersonID int
AS
BEGIN
     DELETE FROM People
          WHERE PersonID = @PersonID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

-- GRANT EXECUTE ON dbo.sproc_PeopleRemove TO db_editor
-- GO 



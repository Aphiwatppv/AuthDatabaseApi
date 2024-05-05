CREATE PROCEDURE [dbo].[spRegisterUser]
    @FirstName NVARCHAR(30),
    @LastName NVARCHAR(30),
    @Email NVARCHAR(50),
    @PhoneNumber NVARCHAR(20),
    @IdentityID NVARCHAR(20),
    @HashPassword NVARCHAR(MAX),
    @Salt NVARCHAR(MAX),
    @Result VARCHAR(100) OUTPUT
AS
BEGIN
    
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM [User] WHERE FirstName = @FirstName AND LastName = @LastName)
    BEGIN 
        SET @Result = 'First name and last name aleady exist';
        RETURN ;
    END
    ELSE IF EXISTS(SELECT 1 FROM [User] WHERE Email = @Email)
    BEGIN
        SET @Result = 'A user with the given email already exists.';
        RETURN;
    END
    ELSE IF EXISTS(SELECT 1 FROM [User] WHERE PhoneNumber = @PhoneNumber)
    BEGIN 
        SET @Result = 'Phone number already exists.';
        RETURN;
    END
    ELSE IF EXISTS(SELECT 1 FROM [User] WHERE IdentityID = @IdentityID)
    BEGIN 
        SET @Result = 'Identity ID already exists.';
        RETURN;
    END
    ELSE
    BEGIN
        INSERT INTO [User] (FirstName, LastName, Email, PhoneNumber, IdentityID, HashPassword, Salt)
        VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @IdentityID, @HashPassword, @Salt);

        SET @Result = 'You have been registered successfully.';
    END
END




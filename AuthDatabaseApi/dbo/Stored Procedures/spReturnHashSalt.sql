CREATE PROCEDURE [dbo].[spReturnHashSalt]
    @Email NVARCHAR(50)
AS 
BEGIN 

    SELECT 
           FirstName,
           LastName,
           HashPassword, 
           Salt
           FROM dbo.[User]
           WHERE Email = @Email;
END


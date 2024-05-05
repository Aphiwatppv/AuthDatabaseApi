CREATE PROCEDURE [dbo].[spInsertSecrete]
  @HashSecret NVARCHAR(MAX),
    @Salt NVARCHAR(MAX),
    @SecretIdentity NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRY
        INSERT INTO dbo.[SecreteTb](HashSecrete, Salt, SecreteIdentity)
        VALUES (@HashSecret, @Salt, @SecretIdentity)
    END TRY
    BEGIN CATCH
        -- Handle the error appropriately
        -- For example, you can log the error details and rethrow or return a custom error message
    END CATCH
END
CREATE PROCEDURE [dbo].[spGetSecrete]
AS
BEGIN
    SELECT Id, HashSecrete, Salt, SecreteIdentity FROM dbo.SecreteTb
END
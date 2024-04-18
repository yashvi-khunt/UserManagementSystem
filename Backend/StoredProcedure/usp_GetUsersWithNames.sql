USE [login-system]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetUsersWithNames]    Script Date: 17-04-2024 23:44:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetUsersWithNames]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id AS 'Value',
           FirstName + ' ' + LastName AS 'Label'
    FROM AspNetUsers
    WHERE FirstName IS NOT NULL AND LastName IS NOT NULL
END
GO


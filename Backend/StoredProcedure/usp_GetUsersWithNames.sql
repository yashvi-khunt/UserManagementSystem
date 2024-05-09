USE [login-system]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetUsersWithNames]    Script Date: 09-05-2024 11:17:11 ******/
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


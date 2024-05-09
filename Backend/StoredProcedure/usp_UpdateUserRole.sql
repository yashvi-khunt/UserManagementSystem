USE [login-system]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateUserRole]    Script Date: 09-05-2024 11:17:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateUserRole]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(450) = '',
	@RoleId NVARCHAR(450) =''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

      IF NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId)
    BEGIN
        -- Insert statement if UserId doesn't exist
        INSERT INTO AspNetUserRoles (UserId, RoleId)
        VALUES (@UserId, @RoleId)

        SELECT 1 AS IsValid, 'Record Inserted Successfully!' AS Message
    END
    ELSE
    BEGIN
        -- Update statement if UserId exists
        UPDATE AspNetUserRoles
        SET RoleId = @RoleId
        WHERE UserId = @UserId

        SELECT 1 AS IsValid, 'Record Updated Successfully!' AS Message
    END
END
GO


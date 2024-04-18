USE [login-system]
GO

/****** Object:  StoredProcedure [dbo].[usp_UpdateUserRole]    Script Date: 18-04-2024 10:24:27 ******/
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

    -- Insert statements for procedure here
	UPDATE AspNetUserRoles
	SET RoleId = @RoleId WHERE UserId=@UserId

	select 1 as IsValid, 'Record Updated Successfully!' as Message
END
GO


USE [login-system]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetLoginHistory]    Script Date: 17-04-2024 11:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_GetLoginHistory]
   
    @Field NVARCHAR(50) = 'Id', --code
    @Sort NVARCHAR(50) = 'asc', -- asc
    @Page BIGINT = 1,
    @PageSize BIGINT = 10,
	@Text NVARCHAR(MAX)='',
	@UserIds nvarchar(MAX) = '',
	@FromDate DATE = '',
	@ToDate DATE = ''
AS
BEGIN


 DROP TABLE IF EXISTS #tempLoginHistory
 
	SELECT lh.Id
	INTO #tempLoginHistory
	FROM LoginHistories lh
	WHERE (@UserIds = '' OR lh.UserId IN (SELECT value FROM STRING_SPLIT(@UserIds, ','))) AND (@Text = '' OR (lh.IpAddress LIKE '%'+@Text+'%') OR (lh.Browser LIKE '%'+@Text+'%') OR (lh.OS LIKE '%'+@Text+'%'))
	  AND ((@FromDate = '' AND @ToDate = '') OR (lh.DateTime BETWEEN @FromDate AND @ToDate))	
	GROUP BY lh.Id

   -- It returns number of users after applying filters.
   SELECT Count(*) AS count FROM #tempLoginHistory

   -- it returns all data for users from #tempLoginHistory
    SELECT 
    ROW_NUMBER() OVER (Order By 
	 CASE WHEN @Field = 'firstName' AND @Sort = 'asc' THEN firstname END ASC,
                CASE WHEN @Field = 'firstName' AND @Sort = 'desc' THEN firstname END DESC,
                CASE WHEN @Field = 'lastName' AND @Sort = 'asc' THEN lastname END ASC,
                CASE WHEN @Field = 'lastName' AND @Sort = 'desc' THEN lastname END DESC,
                CASE WHEN @Field = 'email' AND @Sort = 'asc' THEN email END ASC,
                CASE WHEN @Field = 'email' AND @Sort = 'desc' THEN email END DESC,
                CASE WHEN @Field = 'createdDate' AND @Sort = 'asc' THEN createdDate END ASC,
                CASE WHEN @Field = 'createdDate' AND @Sort = 'desc' THEN createdDate END DESC
     )
     AS Id ,a.Id AS UserId,a.UserName,lh.DateTime,lh.IpAddress,lh.Browser,lh.OS,lh.Device
	 FROM AspNetUsers a
	 INNER JOIN LoginHistories lh ON a.Id = lh.UserId
	 INNER JOIN #tempLoginHistory tlh ON tlh.Id = lh.Id	
   ORDER BY Id
    OFFSET (@Page-1) * @pageSize ROWS FETCH NEXT @pagesize ROWS ONLY

END

--exec [usp_GetLoginHistory] @Page = 1,@pageSize =10, @Field = '',@Sort ='asc',@Text='',@fromDate='',@toDate='',@UserIds=''
GO



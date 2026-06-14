-- Run on QTSecurity database (ApplicationDbContextConnection).

IF OBJECT_ID('dbo.SP_GetAdminUserDetails', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_GetAdminUserDetails;
GO

CREATE PROCEDURE dbo.SP_GetAdminUserDetails
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ass.[Id],
           ass.[UserName],
           ass.[Email],
           anr.[Name]
    FROM [dbo].[AspNetUsers] ass
    INNER JOIN [dbo].[AspNetUserRoles] aur ON aur.[UserId] = ass.[Id]
    INNER JOIN [dbo].[AspNetRoles] anr ON anr.[Id] = aur.[RoleId]
    WHERE aur.[RoleId] = '5fcd283d-fb18-4e23-868e-37b465c616e7';
END
GO

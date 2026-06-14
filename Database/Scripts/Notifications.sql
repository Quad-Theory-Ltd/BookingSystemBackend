-- Run against your booking database after creating Notifications and NotificationRecipients tables.

IF OBJECT_ID('dbo.SP_CreateNotification', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_CreateNotification;
GO

CREATE PROCEDURE dbo.SP_CreateNotification
    @Title NVARCHAR(200),
    @Message NVARCHAR(MAX),
    @NotificationType VARCHAR(50) = NULL,
    @ReferenceId BIGINT = NULL,
    @ReferenceType VARCHAR(50) = NULL,
    @RedirectUrl NVARCHAR(500) = NULL,
    @CreatedBy BIGINT = NULL,
    @RecipientUserIds NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @NotificationId BIGINT;

        INSERT INTO dbo.Notifications
        (
            Title,
            Message,
            NotificationType,
            ReferenceId,
            ReferenceType,
            RedirectUrl,
            CreatedBy
        )
        VALUES
        (
            @Title,
            @Message,
            @NotificationType,
            @ReferenceId,
            @ReferenceType,
            @RedirectUrl,
            @CreatedBy
        );

        SET @NotificationId = SCOPE_IDENTITY();

        INSERT INTO dbo.NotificationRecipients (NotificationId, UserId)
        SELECT @NotificationId, LTRIM(RTRIM(value))
        FROM STRING_SPLIT(@RecipientUserIds, ',')
        WHERE LTRIM(RTRIM(value)) <> '';

        COMMIT TRANSACTION;

        SELECT @NotificationId AS NotificationId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO

IF OBJECT_ID('dbo.SP_GetNotificationsByUserId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_GetNotificationsByUserId;
GO

CREATE PROCEDURE dbo.SP_GetNotificationsByUserId
    @UserId NVARCHAR(450),
    @UnreadOnly BIT = 0,
    @Page INT = 1,
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        nr.Id AS RecipientId,
        n.Id AS NotificationId,
        n.Title,
        n.Message,
        n.NotificationType,
        n.ReferenceId,
        n.ReferenceType,
        n.RedirectUrl,
        n.CreatedAt,
        n.CreatedBy,
        nr.IsRead,
        nr.ReadAt
    FROM dbo.NotificationRecipients nr
    INNER JOIN dbo.Notifications n ON n.Id = nr.NotificationId
    WHERE nr.UserId = @UserId
      AND nr.IsDeleted = 0
      AND (@UnreadOnly = 0 OR nr.IsRead = 0)
    ORDER BY n.CreatedAt DESC
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO

IF OBJECT_ID('dbo.SP_GetUnreadNotificationCountByUserId', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_GetUnreadNotificationCountByUserId;
GO

CREATE PROCEDURE dbo.SP_GetUnreadNotificationCountByUserId
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(1)
    FROM dbo.NotificationRecipients
    WHERE UserId = @UserId
      AND IsRead = 0
      AND IsDeleted = 0;
END
GO

IF OBJECT_ID('dbo.SP_MarkNotificationRecipientAsRead', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_MarkNotificationRecipientAsRead;
GO

CREATE PROCEDURE dbo.SP_MarkNotificationRecipientAsRead
    @RecipientId BIGINT,
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.NotificationRecipients
    SET IsRead = 1,
        ReadAt = SYSDATETIME()
    WHERE Id = @RecipientId
      AND UserId = @UserId
      AND IsDeleted = 0
      AND IsRead = 0;
END
GO

IF OBJECT_ID('dbo.SP_MarkAllNotificationRecipientsAsRead', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_MarkAllNotificationRecipientsAsRead;
GO

CREATE PROCEDURE dbo.SP_MarkAllNotificationRecipientsAsRead
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.NotificationRecipients
    SET IsRead = 1,
        ReadAt = SYSDATETIME()
    WHERE UserId = @UserId
      AND IsDeleted = 0
      AND IsRead = 0;
END
GO

IF OBJECT_ID('dbo.SP_SoftDeleteNotificationRecipient', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SP_SoftDeleteNotificationRecipient;
GO

CREATE PROCEDURE dbo.SP_SoftDeleteNotificationRecipient
    @RecipientId BIGINT,
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.NotificationRecipients
    SET IsDeleted = 1
    WHERE Id = @RecipientId
      AND UserId = @UserId
      AND IsDeleted = 0;
END
GO

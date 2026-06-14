-- Run on booking database if NotificationRecipients.UserId was created as BIGINT.
-- Required for AspNet user ids (GUID strings) from QTSecurity.

IF EXISTS (
    SELECT 1
    FROM sys.columns c
    INNER JOIN sys.tables t ON t.object_id = c.object_id
    WHERE t.name = 'NotificationRecipients'
      AND c.name = 'UserId'
      AND c.system_type_id = 127 -- bigint
)
BEGIN
    ALTER TABLE dbo.NotificationRecipients
    ALTER COLUMN UserId NVARCHAR(450) NOT NULL;
END
GO

USE GpsTrackingDatabase;
GO

IF EXISTS ( SELECT  *
            FROM    sys.triggers
            WHERE   name = N'User_OnInsert'
                    AND parent_class_desc = N'GpsTrackingDatabase' )
    DROP TRIGGER User_OnInsert ON DATABASE;
GO

CREATE TRIGGER User_OnInsert ON dbo.[User]
    FOR INSERT
AS
    BEGIN
        INSERT  INTO dbo.Log
                ( EventId ,
                  Message ,
                  EventDate
                )
        VALUES  ( 1 , -- EventId - int
                  N'New user #' + ( SELECT  dbo.[User].UserId
                                    FROM    dbo.[User]
                                  ) + ' has been inserted with DeviceId'
                  + ( SELECT    dbo.Person.DeviceId
                      FROM      dbo.Person
                    ) , -- Message - nvarchar(200)
                  GETDATE()  -- EventDate - datetime
                );
    END; 
GO

SELECT  *
FROM    dbo.Event
ORDER BY EventId ASC;

  -- Get Event codes

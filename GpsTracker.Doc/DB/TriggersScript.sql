SELECT  *
FROM    dbo.Event
ORDER BY EventId ASC;  -- Get Event codes
GO

--====================================
		--  User_OnDelete--
--====================================
--BEGIN
       
IF EXISTS ( SELECT  *
            FROM    sys.triggers
            WHERE   name = N'User_OnDelete'
                    AND parent_class_desc = N'GpsTrackingDatabase' )
    DROP TRIGGER User_OnDelete  --Delete trigger it exists 
GO  

CREATE TRIGGER User_OnDelete ON dbo.[User]
    FOR DELETE
AS
    BEGIN
        INSERT  INTO dbo.Log
                ( EventId ,
                  Message,
                  DeviceId
                )
		SELECT 3, CONCAT(N'User with id: ', del.UserId, ' has been deleted'), del.DeviceId
		FROM Deleted del		 
    END; 

--END

--====================================
		--  User_OnUpdate--
--====================================
BEGIN
	IF EXISTS ( SELECT  *
            FROM    sys.triggers
            WHERE   name = N'User_OnUpdate'
                    AND parent_class_desc = N'DATABASE')
    DROP TRIGGER User_OnUpdate	
END

GO

CREATE TRIGGER User_OnUpdate ON dbo.[User] 
    AFTER UPDATE 
AS
    BEGIN
        INSERT  INTO dbo.[Log]
                ( EventId ,
                  [Message]
                )
		SELECT 2, CONCAT(N'User with id: ', del.UserId, ' has been edited')
		FROM Deleted del
    END; 
GO

--====================================
		--  User_OnInsert--
--====================================
--BEGIN

IF EXISTS ( SELECT  *
            FROM    sys.triggers
            WHERE   name = N'User_OnInsert'
                    AND parent_class_desc = N'GpsTrackingDatabase' )
    DROP TRIGGER User_OnInsert
	GO

CREATE TRIGGER User_OnInsert ON dbo.[User]
    FOR INSERT
AS
	BEGIN
	    INSERT  INTO dbo.Log
                ( EventId ,
                  Message,
                  DeviceId
                )
		SELECT 1, CONCAT(N'New user #', ins.UserId, ' has been inserted'), ins.DeviceId
		FROM Inserted ins
	END
	        
--END
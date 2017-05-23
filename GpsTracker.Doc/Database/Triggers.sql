USE GpsTrackingDatabase;

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
                  Message
                )
		SELECT 1, CONCAT(N'New user #', ins.UserId, ' has been inserted with DeviceId: ', ins.DeviceId)
		FROM Inserted ins
	END
        
--END

IF EXISTS ( SELECT  *
            FROM    sys.triggers
            WHERE   name = N'User_OnDelete'
                    AND parent_class_desc = N'GpsTrackingDatabase' )
    DROP TRIGGER User_OnDelete ON DATABASE; --Delete trigger it exists 
GO  

CREATE TRIGGER User_OnDelete ON dbo.[User]
    FOR DELETE
AS
    BEGIN
        INSERT  INTO dbo.Log
                ( EventId ,
                  Message
                )
		SELECT 3, CONCAT(N'User with id: ', del.UserId, ' has been deleted with DeviceId: ', del.DeviceId)
		FROM Deleted del		 
    END; 


--====================================
		--  User_OnUpdate--
--====================================
BEGIN
	IF EXISTS ( SELECT  *
            FROM    sys.triggers
            WHERE   name = N'User_OnUpdate'
                    AND parent_class_desc = N'DATABASE')
    DROP TRIGGER User_OnUpdate ON DATABASE;	
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
		--  User_OnUpdate_TestCase--
--====================================
--INSERT INTO dbo.[User]
--        ( Login ,
--          Password ,
--          IsAdmin ,
--		  DeviceId
--        )
--VALUES  ( N'admin' , -- Login - nchar(16)
--          N'password' , -- Password - nvarchar(16)
--          1, -- IsAdmin - bit
--		  'abfn5kgjdhfu5068' --DeviceId - nvarcahr(16)
--        )

--INSERT INTO dbo.[User]
--        ( Login ,
--          Password ,
--          IsAdmin , 
--		  DeviceId 
--        )
--VALUES  ( N'admin2' , -- Login - nchar(16)
--          N'password' , -- Password - nvarchar(16)
--          1, -- IsAdmin - bit
--		  'abfn5kgjdhfu5062' --DeviceId - nvarcahr(16)
--        )

--SELECT * FROM dbo.[User]

--UPDATE  dbo.[User]
--SET     Login = 'taras2' ,
--        Password = 'taraspassword'
--WHERE   UserId = ( SELECT   MAX(UserId)
--                   FROM     dbo.[User]
--                 )
--DELETE FROM dbo.[User]
--WHERE UserId = ( SELECT   MAX(UserId)
--                   FROM     dbo.[User]
--               )

--SELECT * FROM dbo.Log
SELECT  *
FROM    dbo.Event
ORDER BY EventId ASC;  -- Get Event codes
GO

--====================================
		--  User_OnDelete--
--====================================
BEGIN

USE GpsTrackingDatabase;


IF EXISTS ( SELECT  *
            FROM    sys.triggers
            WHERE   name = N'User_OnInsert'
                    AND parent_class_desc = N'GpsTrackingDatabase' )
    DROP TRIGGER User_OnInsert ON DATABASE; 

CREATE TRIGGER User_OnInsert ON dbo.[User]
    FOR INSERT
AS
	BEGIN

	    INSERT  INTO dbo.Log
                ( EventId ,
                  Message
                )
        VALUES  ( 1 , -- EventId - int
                  N'New user #' + ( SELECT  MAX(dbo.[User].UserId)
                                    FROM    dbo.[User]
                                  ) + ' has been inserted with DeviceId'
                  + ( SELECT    dbo.Person.DeviceId
                      FROM      dbo.Person
                    )-- Message - nvarchar(200)
                );
	END
        
END	

USE GpsTrackingDatabase;

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
        VALUES  ( 3 , -- EventId - int
                  N'User with id: ' + ( SELECT Deleted.UserId FROM Deleted del) + ' has been deleted with DeviceId'
                  + ( SELECT    dbo.Person.DeviceId
                      FROM      dbo.Person)
                );
    END; 


USE GpsTrackingDatabase;
GO


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

DROP TRIGGER User_OnUpdate ON DATABASE

CREATE TRIGGER User_OnUpdate ON dbo.[User] 
    AFTER UPDATE 
AS
    BEGIN
        INSERT  INTO dbo.[Log]
                ( EventId ,
                  [Message]
                )
        VALUES  ( 3 , -- EventId - int
                  N'User with id: ' + ( SELECT MAX(dbo.[User].UserId)FROM dbo.[User] ) + ' has been edited with DeviceId'
                  + ( SELECT    dbo.Person.DeviceId
                      FROM      dbo.Person)
                );
    END; 
GO


--====================================
		--  User_OnUpdate_TestCase--
--====================================
INSERT INTO dbo.[User]
        ( Login ,
          Password ,
          IsAdmin
        )
VALUES  ( N'admin' , -- Login - nchar(16)
          N'password' , -- Password - nvarchar(16)
          1 -- IsAdmin - bit
        )

INSERT INTO dbo.[User]
        ( Login ,
          Password ,
          IsAdmin
        )
VALUES  ( N'admin2' , -- Login - nchar(16)
          N'password' , -- Password - nvarchar(16)
          1 -- IsAdmin - bit
        )

SELECT * FROM dbo.[User]

UPDATE  dbo.[User]
SET     Login = 'taras2' ,
        Password = 'taraspassword' ,
        IsAdmin = 0
WHERE   UserId = ( SELECT   MAX(UserId)
                   FROM     dbo.[User]
                 )

SELECT * FROM dbo.Log
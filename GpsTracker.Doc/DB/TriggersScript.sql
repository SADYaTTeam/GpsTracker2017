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


--====================================
		--  Marker_OnInsert--
--====================================
--BEGIN

DROP TRIGGER Marker_OnInsert

CREATE TRIGGER Marker_OnInsert ON dbo.[Marker]
	FOR INSERT
AS
	BEGIN
		DECLARE @count INTEGER, @x FLOAT, @y FLOAT, @i INTEGER, @zones ZoneType, @countZones INTEGER, @j INTEGER, @id VARCHAR(16), @eventId INTEGER
		SET @count = (SELECT MAX(MarkerId) FROM Inserted)
		
		SET @i = (SELECT MIN(MarkerId) FROM Inserted)
		WHILE(@i <= @count)
		BEGIN
			SET @x = (SELECT TOP 1 Latitude FROM Inserted WHERE MarkerId = @i)
			SET @y = (SELECT TOP 1 Longitude FROM Inserted WHERE MarkerId = @i)
			SET @id = (SELECT TOP 1 DeviceId FROM [User] WHERE UserId = (SELECT UserId FROM Inserted WHERE MarkerId = @i))
			INSERT INTO @zones(Longitude, Latitude, Radius, Name)
			SELECT DISTINCT zn.Longitude, zn.Latitude, zn.Radius, zn.Name
			FROM [User] usr JOIN Friendlist fl ON usr.UserId = fl.Sender OR usr.UserId = fl.Marked
				JOIN Zone zn ON fl.Sender = zn.UserId
			WHERE usr.UserId = (SELECT UserId FROM Inserted WHERE MarkerId = @i)
			SET @countZones = (SELECT COUNT(Longitude) FROM @zones)
			SET @j = 0
			IF((SELECT COUNT(LogId) FROM Log WHERE EventId = 4 OR EventId = 5) > 0)
			BEGIN
				SET @eventId = (SELECT TOP 1 EventId FROM Log WHERE EventId = 4 OR EventId = 5 ORDER BY LogId DESC)
				IF(@eventId = 4)
				BEGIN
					PRINT 'In first insert'
					INSERT INTO Log(EventId, Message, DeviceId)
					SELECT 5, 'User has left zone: ' + zn.Name, usr.DeviceId
					FROM @zones zn JOIN [User] usr ON usr.DeviceId = @id
				END
				ELSE
				BEGIN
					IF(@eventId = 5)
					BEGIN
						PRINT 'In second insert'
						INSERT INTO Log(EventId, Message, DeviceId)
						SELECT 4, 'User has enter zone: ' + zn.Name, usr.DeviceId
						FROM @zones zn JOIN [User] usr ON usr.DeviceId = @id
					END
				END
			END
			ELSE
			BEGIN
				PRINT 'In third insert'
				PRINT 'X= ' + CONVERT(VARCHAR(500), @x)
				PRINT 'I= ' + CONVERT(VARCHAR(500), @i)
				PRINT 'Y= ' + CONVERT(VARCHAR(500), @y)
				DECLARE @temp VARCHAR(500)
				SET @temp = 'COUNT' + CONVERT(VARCHAR(500), (SELECT COUNT(Longitude) FROM @zones))
				PRINT @temp
				INSERT INTO Log(EventId, Message, DeviceId)
				SELECT 4, 'User has left zone: ' + zn.Name, usr.DeviceId
				FROM @zones zn JOIN [User] usr ON usr.DeviceId = @id
				WHERE POWER(@x - zn.Latitude, 2) + POWER(@y - zn.Longitude, 2) > POWER(zn.Radius/111000, 2)


				INSERT INTO Log(EventId, Message, DeviceId)
				SELECT 4, 'User has enter zone: ' + zn.Name, usr.DeviceId
				FROM @zones zn JOIN [User] usr ON usr.DeviceId = @id
				WHERE POWER(@x - zn.Latitude, 2) + POWER(@y - zn.Longitude, 2) <= POWER(zn.Radius/111000, 2)
			END
			SET @i += 1
		END
	END
--END


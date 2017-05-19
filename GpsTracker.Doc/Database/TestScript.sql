USE GpsTrackingDatabase;
GO	

-- Event
BEGIN
    BEGIN
        INSERT  INTO dbo.Event
                ( Name
                )
        VALUES  ( '}[][sd@!#$!@dasdsafdjioamdasdoikoi'  -- Name - varchar(50)
                );
    END; -- Below 50

    BEGIN
        INSERT  INTO dbo.Event
                ( Name
                )
        VALUES  ( 'asdnjasndndfdnsasdnjvnajvysdydsyfajdsfayasjfbsjdfbjdsabfjsdbafjdyasy'  -- Name - varchar(50)
		        );
    END; -- Above 50

    BEGIN
        INSERT  INTO dbo.Event
                ( Name )
        VALUES  ( ''  -- Name - varchar(50)
                  );

        SELECT  *
        FROM    dbo.[Event]; 
    END; -- Name is Null
    
END;	
GO

-- Log

BEGIN
    
    BEGIN
        INSERT  INTO dbo.[Log]
                ( EventId, Message, EventDate )
        VALUES  ( 0, -- EventId - int
                  N'Test', -- Message - nvarchar(200)
                  GETDATE()  -- EventDate - datetime
                  );
    

        SELECT  *
        FROM    dbo.[Log];

    END; -- Message below 200


    BEGIN
        INSERT  INTO dbo.[Log]
                ( EventId, Message, EventDate )
        VALUES  ( 0, -- EventId - int
                  N'Test', -- Message - nvarchar(200)
                  '2017-05-19 12:29:30' -- EventDate - datetime
                  );


    

        SELECT  *
        FROM    dbo.[Log];
    END; -- Date manually entered (maybe in wrong format)    

END;
GO	

-- User
BEGIN

    BEGIN
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
                )
        VALUES  ( N'user' , -- Login - nchar(16)
                  N'password' , -- Password - nvarchar(16)
                  1 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
                ); 

        SELECT  *
        FROM    dbo.[User];
    END; -- Normal login 'user' and password 'password'

    BEGIN
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
	            )
        VALUES  ( N'' , -- Login - nchar(16)
                  N'' , -- Password - nvarchar(16)
                  0 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
	            );

        SELECT  *
        FROM    dbo.[User];
    END; -- Empty login and password

    BEGIN
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
	            )
        VALUES  ( N'!@#^$@!$' , -- Login - nchar(16)
                  N'' , -- Password - nvarchar(16)
                  0 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
	            );

        SELECT  *
        FROM    dbo.[User];
    END; -- Login consists of special symbols and password is empty

    BEGIN
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
	            )
        VALUES  ( N'123user' , -- Login - nchar(16)
                  N'123' , -- Password - nvarchar(16)
                  0 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
	            );

        SELECT  *
        FROM    dbo.[User];
    END; -- Login starts with number

    BEGIN
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
	            )
        VALUES  ( N'user123' , -- Login - nchar(16)
                  N'123' , -- Password - nvarchar(16)
                  0 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
	            );

        SELECT  *
        FROM    dbo.[User];
    END; -- Login starts with letter
	 
END;
GO 

-- Marker

BEGIN
    
    BEGIN
        INSERT  INTO dbo.Marker
                ( UserId ,
                  Name ,
                  Longtitude ,
                  Latitude ,
                  Timestamp
                )
        VALUES  ( 0 , -- UserId - int
                  N'' , -- Name - nvarchar(60)
                  49.806092, -- Longtitude - float
                  23.991218  , -- Latitude - float
                  GETDATE()  -- Timestamp - datetime
                );

        SELECT  *
        FROM    dbo.Marker;

        SELECT  *
        FROM    dbo.[User];
    END; -- UserId that doesn`t exist in Users table

	BEGIN
        INSERT  INTO dbo.Marker
                ( UserId ,
                  Name ,
                  Longtitude ,
                  Latitude ,
                  Timestamp
                )
        VALUES  ( 1 , -- UserId - int
                  N'' , -- Name - nvarchar(60)
                  49.806092, -- Longtitude - float
                  23.991218 , -- Latitude - float
                  GETDATE()  -- Timestamp - datetime
                );

        SELECT  *
        FROM    dbo.Marker;

        SELECT  *
        FROM    dbo.[User];
    END; -- UserId that exists in Users table

	BEGIN
        INSERT  INTO dbo.Marker
                ( UserId ,
                  Name ,
                  Longtitude ,
                  Latitude ,
                  Timestamp
                )
        VALUES  ( 1 , -- UserId - int
                  N'' , -- Name - nvarchar(60)
                  NULL , -- Longtitude - float
                  NULL , -- Latitude - float
                  GETDATE()  -- Timestamp - datetime
                );

        SELECT  *
        FROM    dbo.Marker;

        SELECT  *
        FROM    dbo.[User];
    END; -- Longtitude and Latitude are NULL

END;
GO 

-- Track

BEGIN
    BEGIN
        INSERT  INTO dbo.Track
                ( MarkerId, UserId )
        VALUES  ( 0, -- MarkerId - int
                  0  -- UserId - int
                  );
        SELECT  *
        FROM    dbo.Track;

		SELECT  *
        FROM    dbo.Marker;

		SELECT  *
        FROM    dbo.[User];
    END; -- MarkerId and UserId that doesn`t exist in Marker and User table 

	BEGIN
        INSERT  INTO dbo.Track
                ( MarkerId, UserId )
        VALUES  ( 2, -- MarkerId - int
                  1  -- UserId - int
                  );
        SELECT  *
        FROM    dbo.Track;

		SELECT  *
        FROM    dbo.Marker;

		SELECT  *
        FROM    dbo.[User];
    END; -- MarkerId and UserId that exist in Marker and User table 

END;
GO
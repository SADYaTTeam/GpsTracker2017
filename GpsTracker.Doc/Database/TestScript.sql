USE GpsTrackingDatabase;
GO	

-- Event
BEGIN
    BEGIN
        INSERT  INTO dbo.Event
                ( Name
                )
        VALUES  ( 'CREATE USER'  -- Name - varchar(50)
                );
    END; -- Below 50

    BEGIN
        INSERT  INTO dbo.Event
                ( Name
                )
        VALUES  ( 'NEW USER'  -- Name - varchar(50)
		        );

        SELECT  *
        FROM    dbo.[Event];

    END; -- Above 50

    BEGIN
        INSERT  INTO dbo.Event
                ( Name )
        VALUES  ( 'FUCKIN KILL ME!'  -- Name - varchar(50)
                  );

        SELECT  *
        FROM    dbo.[Event]; 
    END; -- Name is Null
    
END;	
GO

-- Log

BEGIN
    
    BEGIN

		BEGIN
        INSERT  INTO dbo.Event
                ( Name
                )
        VALUES  ( 'DEL USER'  -- Name - varchar(50)
                );
    END; -- Event normal name size - Below 50

        INSERT  INTO dbo.[Log]
                ( EventId, Message, EventDate )
        VALUES  ( 1, -- EventId - int
                  N'Test', -- Message - nvarchar(200)
                  GETDATE()  -- EventDate - datetime
                  );
    

        SELECT  *
        FROM    dbo.[Log];

    END; -- Message below 200

    BEGIN
        INSERT  INTO dbo.[Log]
                ( EventId, Message )
        VALUES  ( 1, -- EventId - int
                  N'Test' -- Message - nvarchar(200)
                  --NULL -- EventDate - datetime
                  );

        SELECT  *
        FROM    dbo.[Log];

        SELECT  *
        FROM    dbo.[Event];
    END; -- Date manually entered (maybe in wrong format)    

END;
GO	

-- Marker

BEGIN
    
    BEGIN

		BEGIN
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
                )
        VALUES  ( N'user2' , -- Login - nchar(16)
                  N'password' , -- Password - nvarchar(16)
                  1 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
                ); 

        SELECT  *
        FROM    dbo.[User];
    END; -- Normal login 'user' and password 'password'

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
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
                )
        VALUES  ( N'user1' , -- Login - nchar(16)
                  N'password' , -- Password - nvarchar(16)
                  1 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
                ); 

        SELECT  *
        FROM    dbo.[User];
    END; -- Normal login 'user' and password 'password

        INSERT  INTO dbo.Track
                ( MarkerId, UserId )
        VALUES  ( 1, -- MarkerId - int
                  1  -- UserId - int
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

-- User
BEGIN

    BEGIN
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
                )
        VALUES  ( N'user1' , -- Login - nchar(16)
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
                  N'123456' , -- Password - nvarchar(16)
                  0 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
	            );

        SELECT  *
        FROM    dbo.[User];
    END; -- Login starts with letter
	 
END;
GO 

-- Person

BEGIN

	BEGIN
        INSERT  INTO dbo.[User]
                ( Login ,
                  Password ,
                  IsAdmin ,
                  DateCreatedAt
                )
        VALUES  ( N'user1' , -- Login - nchar(16)
                  N'password' , -- Password - nvarchar(16)
                  1 , -- IsAdmin - bit
                  GETDATE()  -- DateCreatedAt - date
                ); 

        SELECT  *
        FROM    dbo.[User];
    END; -- Normal login 'user' and password 'password'

    BEGIN
		INSERT INTO dbo.Person
		        ( UserId ,
		          FirstName ,
		          LastName ,
		          MiddleName ,
		          Gender ,
		          DateOfBirth ,
		          AddressCountry ,
		          AddressCity ,
		          DeviceId ,
		          Photo ,
		          Email ,
		          Phone
		        )
		VALUES  ( 4 , -- UserId - int
		          N'' , -- FirstName - nchar(40)
		          N'' , -- LastName - nchar(40)
		          N'' , -- MiddleName - nchar(40)
		          0 , -- Gender - bit
		           '1995-02-10'  , -- DateOfBirth - date
		          N'' , -- AddressCountry - nchar(40)
		          N'' , -- AddressCity - nchar(40)
		          '12345adcbe12342' , -- DeviceId - int
		          NULL , -- Photo - image
		          N'aa@gmail.com' , -- Email - nvarchar(50)
		          N'+380666016113'  -- Phone - nvarchar(13)
		        ) 
    END -- BirthDate in real; phone is '+380... '; Email is real (xx@xx.xx)

END
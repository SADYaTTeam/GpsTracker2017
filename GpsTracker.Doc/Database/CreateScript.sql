--CREATE DATABASE GpsTrackingDatabase
--GO

CREATE TABLE dbo.[Event]
    (
      EventId  int NOT NULL IDENTITY(1, 1),
      Name VARCHAR(50) NOT NULL,
      CONSTRAINT PK_Id_Event PRIMARY KEY CLUSTERED (EventId ),
	  CONSTRAINT UQ_Event_Name UNIQUE(Name),
	  CONSTRAINT CH_Event_Name_Empty CHECK(Name <> '')
    );
GO

CREATE TABLE dbo.[Log]	
(
	LogId INT NOT NULL IDENTITY(1, 1),
	EventId INT NOT NULL, 
	[Message] NVARCHAR(200) NULL,
	EventDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(), 
	DeviceId CHAR(16) NULL,
	CONSTRAINT PK_Id_Log PRIMARY KEY CLUSTERED (LogId),
	CONSTRAINT FK_Log_Event_Id_Event FOREIGN KEY(EventId) REFERENCES [Event](EventId),
	CONSTRAINT CH_Log_DeviceId CHECK (LEN(DeviceId) = 16 OR DeviceId IS NULL),
	CONSTRAINT CH_Log_EventDate CHECK (EventDate > '2017-01-01')
)
GO

CREATE TABLE dbo.[User]
(
	UserId INT NOT NULL IDENTITY(1, 1),
	[Login] NCHAR(16) NOT NULL,
	[Password] NVARCHAR(16) NOT NULL,
	DeviceId CHAR(16) NOT NULL,
	IsAdmin BIT NOT NULL DEFAULT 0,
	DateCreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
	CONSTRAINT PK_Id_User PRIMARY KEY CLUSTERED (UserId),
	CONSTRAINT UQ_User_Login UNIQUE ([Login]),
	CONSTRAINT CH_User_Login_StartsWithNumber_And_Length_More_Than_5 CHECK (LEN([Login]) > 4),
	CONSTRAINT CH_User_Password_Null CHECK (LEN([Password]) > 5),
	CONSTRAINT CH_User_DeviceId CHECK (LEN(DeviceId) = 16),
	CONSTRAINT UQ_User_Device_Id UNIQUE(DeviceId),
)
GO

CREATE TABLE dbo.[Person]
(
	PersonId INT NOT NULL IDENTITY(1, 1),
	UserId INT NOT NULL,
	FirstName NCHAR(40) NULL,
	LastName NCHAR(40) NULL,
	MiddleName NCHAR(40) NULL,
	Gender BIT NULL,
	DateOfBirth DATETIME2 NULL,
	--AddressCountry NCHAR(40) NULL,
	--AddressCity NCHAR(40) NULL,
	Photo IMAGE NULL,
	Email NVARCHAR(50) NULL,
	Phone NVARCHAR(13) NULL,
	CONSTRAINT PK_Id_Person PRIMARY KEY CLUSTERED (PersonId),
	CONSTRAINT FK_Person_User_Id_User FOREIGN KEY(UserId) REFERENCES dbo.[User](UserId),
	CONSTRAINT CH_Person_BirthDate CHECK (DateOfBirth > '1950-01-01' AND DateOfBirth < DATEADD(YEAR, -10, SYSDATETIME())),
	CONSTRAINT CH_Person_Phone CHECK (Phone LIKE '+380%[0-9]%'), 
	CONSTRAINT CH_Person_Email CHECK (Email LIKE '[a-z]%[a-z]@[a-z]%[a-z].[a-z][a-z]%')
)
GO

CREATE TABLE dbo.Marker
(
	MarkerId INT NOT NULL IDENTITY(1, 1),
	UserId INT NOT NULL,
	Name NVARCHAR(60) NULL,
	Longtitude FLOAT NOT NULL,
	Latitude FLOAT NOT NULL,
	[Timestamp] DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
	CONSTRAINT PK_Id_Marker PRIMARY KEY CLUSTERED (MarkerId),
	CONSTRAINT FK_Marker_Track_Id_User FOREIGN KEY(UserId) REFERENCES dbo.[User](UserId),
	CONSTRAINT CH_Marker_Longtitude_Latitude CHECK(Longtitude <= 180 OR Longtitude >= -180 AND Latitude <= 90 OR Latitude >= 0 )
	--CONSTRAINT CH_Marker_Longtitude_Null CHECK (Longtitude IS NOT NULL),
	--CONSTRAINT CH_Marker_Latitude_Null CHECK (Latitude IS NOT NULL)
)
GO

INSERT  INTO dbo.Event
        ( Name )
VALUES  ( 'NEW_USER'  -- Name - varchar(50)
          ),
        ( 'EDIT_USER'  -- Name - varchar(50)
          ),
        ( 'DELETE_USER'  -- Name - varchar(50)
          ),
        ( 'OUT_OF_SAFE_ZONE'  -- Name - varchar(50)
          ),
        ( 'IN_SAFE_ZONE'  -- Name - varchar(50)
          ),
        ( 'SOS_BUTTON_CLICK'  -- Name - varchar(50)
          );

SELECT * FROM dbo.Event ORDER BY EventId ASC







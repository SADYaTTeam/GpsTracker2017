CREATE DATABASE GpsTrackingDatabase
GO

USE GpsTrackingDatabase
GO

CREATE TABLE dbo.[Event]
    (
      EventId  int NOT NULL IDENTITY(1, 1),
      Name VARCHAR(50) NOT NULL,
      CONSTRAINT PK_Id_Event PRIMARY KEY CLUSTERED (EventId ),
	  CONSTRAINT CH_Event_Name UNIQUE(Name),
	  CONSTRAINT CH_Event_Name_Null CHECK(Name IS NULL)
    );
GO

CREATE TABLE dbo.[Log]	
(
	LogId INT NOT NULL IDENTITY(1, 1),
	EventId INT NOT NULL, 
	[Message] NVARCHAR(200) NULL,
	EventDate DATETIME NOT NULL DEFAULT SYSDATETIME(), 
	CONSTRAINT PK_Id_Log PRIMARY KEY CLUSTERED (LogId),
	CONSTRAINT FK_Log_Event_Id_Event FOREIGN KEY(EventId) REFERENCES [Event](EventId),
	CONSTRAINT CH_Log_EventDate CHECK (EventDate > '01\01\2017') ,
	CONSTRAINT CH_Log_EventDate_Null CHECK (EventDate IS NULL),
	CONSTRAINT CH_Log_EventId_Null CHECK (EventId IS NULL)
)
GO

CREATE TABLE dbo.[User]
(
	UserId INT NOT NULL IDENTITY(1, 1),
	[Login] NCHAR(16) NOT NULL,
	[Password] NVARCHAR(16) NOT NULL,
	IsAdmin BIT NOT NULL,
	DateCreatedAt DATE NOT NULL DEFAULT SYSDATETIME(),
	CONSTRAINT PK_Id_User PRIMARY KEY CLUSTERED (UserId),
	CONSTRAINT CH_User_Login UNIQUE ([Login]),
	CONSTRAINT CH_User_Login_Null CHECK ([Login] IS NOT NULL),
	CONSTRAINT CH_User_Login_StartsWithNumber CHECK ([Login] NOT LIKE '[0-9]%'),
	CONSTRAINT CH_User_Password_Null CHECK ([Password] IS NOT NULL) 
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
	DateOfBirth DATE NULL,
	AddressCountry NCHAR(40) NULL,
	AddressCity NCHAR(40) NULL,
	DeviceId INT NOT NULL,
	Photo IMAGE NULL,
	Email NVARCHAR(50) NOT NULL,
	Phone NVARCHAR(13) NULL,
	CONSTRAINT PK_Id_Person PRIMARY KEY CLUSTERED (PersonId),
	CONSTRAINT FK_Person_User_Id_User FOREIGN KEY(UserId) REFERENCES dbo.[User](UserId),
	CONSTRAINT CH_Person_Device_Id UNIQUE(DeviceId),
	CONSTRAINT CH_Person_BirthDate CHECK (DateOfBirth > '01\01\1950' AND DateOfBirth < DATEADD(YEAR, -10, SYSDATETIME())),
	CONSTRAINT CH_Person_Phone CHECK (Phone LIKE '+380[0-9]{9}'),
	CONSTRAINT CH_Person_Email CHECK (Email LIKE '%@%.%')
)
GO

CREATE TABLE dbo.Marker
(
	MarkerId INT NOT NULL IDENTITY(1, 1),
	UserId INT NOT NULL,
	Name NVARCHAR(60) NULL,
	Longtitude FLOAT NOT NULL,
	Latitude FLOAT NOT NULL,
	[Timestamp] DATETIME NOT NULL DEFAULT SYSDATETIME(),
	CONSTRAINT PK_Id_Marker PRIMARY KEY CLUSTERED (MarkerId),
	CONSTRAINT FK_Marker_Track_Id_User FOREIGN KEY(UserId) REFERENCES dbo.[User](UserId),
	CONSTRAINT CH_Marker_Longtitude_Latitude CHECK(Longtitude <= 180 OR Longtitude >= -180 AND Latitude <= 90 OR Latitude >= 0 ),
	CONSTRAINT CH_Marker_Longtitude_Null CHECK (Longtitude IS NOT NULL),
	CONSTRAINT CH_Marker_Latitude_Null CHECK (Latitude IS NOT NULL)
)
GO

CREATE TABLE dbo.Track
(
	TrackId INT NOT NULL IDENTITY(1, 1),
	MarkerId INT NOT NULL,
	UserId INT NOT NULL,
	CONSTRAINT PK_Id_Track PRIMARY KEY CLUSTERED (TrackId),
	CONSTRAINT FK_Track_User_Id_User FOREIGN KEY(UserId) REFERENCES dbo.[User](UserId),
	CONSTRAINT FK_Track_Marker_Id_Marker FOREIGN KEY(MarkerId) REFERENCES dbo.Marker(MarkerId)
)
GO




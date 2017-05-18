--CREATE DATABASE TournamentMangerDB
--GO

USE TournamentMangerDB
GO

CREATE TABLE [User]
(
	Id INTEGER NOT NULL IDENTITY(1, 1),
	Nickname VARCHAR(16) NOT NULL,
	Password VARCHAR(16) NOT NULL,
	CreateDate DATETIME NOT NULL DEFAULT SYSDATETIME(),
	Wins INTEGER NOT NULL DEFAULT 0,
	Loses INTEGER NOT NULL DEFAULT 0,
	WonTournaments INTEGER NOT NULL DEFAULT 0,
	CONSTRAINT PK_User PRIMARY KEY(Id),
	CONSTRAINT UK_User_Nickname UNIQUE(Nickname)
)
GO

CREATE TABLE Game
(
	Id INTEGER NOT NULL IDENTITY(1, 1),
	Name VARCHAR(100) NOT NULL,
	GameImage IMAGE NULL,
	CONSTRAINT PK_Game PRIMARY KEY(Id),
	CONSTRAINT UK_Game_Name UNIQUE(Name)
)
GO

CREATE TABLE Tournament
(
	Id INTEGER NOT NULL IDENTITY(1, 1),
	Title VARCHAR(100) NOT NULL,
	GameId INTEGER NOT NULL,
	AdministartorId INTEGER NOT NULL,
	CreateDate DATETIME NOT NULL DEFAULT SYSDATETIME(),
	EventDate DATETIME NOT NULL,
	WaitTime INTEGER NOT NULL DEFAULT 2,
	WinerId INTEGER NULL,
	MaxNumberOfMembers INTEGER NOT NULL,
	CONSTRAINT PK_Tournament PRIMARY KEY(Id),
	CONSTRAINT FK_Tournament_Game_GameId FOREIGN KEY(GameId) REFERENCES Game(id),
	CONSTRAINT FK_Tournament_User_AdministratorId FOREIGN KEY(AdministartorId) REFERENCES [User](Id),
	CONSTRAINT FK_Tournament_User_WinerId FOREIGN KEY(WinerId) REFERENCES [User](Id),
	CONSTRAINT CH_Tournament_MaxNumberOfMemebers CHECK(MaxNumberOfMembers > 1),
	CONSTRAINT CH_Tournament_Gameid CHECK(GameId > 0),
	CONSTRAINT CH_Tournament_AdministartorId CHECK(AdministartorId > 0),
	CONSTRAINT CH_Tournament_EventDate CHECK(EventDate > CreateDate)
)
GO

CREATE TABLE Members
(
	TournamentId INTEGER NOT NULL,
	MemberId INTEGER NOT NULL,
	CONSTRAINT FK_Members_Tournament FOREIGN KEY (TournamentId) REFERENCES Tournament(id),
	CONSTRAINT FK_Members_User FOREIGN KEY (TournamentId) REFERENCES [User](id),
	CONSTRAINT CH_Members_TournamentId CHECK(TournamentId > 0),
	CONSTRAINT CH_Members_MemberId CHECK(MemberId > 0)
)
GO

CREATE TABLE Pairs
(
	TournamentId INTEGER NOT NULL,
	MemberA INTEGER NOT NULL,
	MemberB INTEGER NULL,
	Winer INTEGER NULL,
	Stage INTEGER NOT NULL,
	CONSTRAINT FK_Pairs_Members_MemberA FOREIGN KEY(MemberA) REFERENCES [User](Id),
	CONSTRAINT FK_Pairs_Members_MemberB FOREIGN KEY(MemberB) REFERENCES [User](Id),
	CONSTRAINT CH_Pairs_MemberA_MemberB CHECK(MemberA <> MemberB),
	CONSTRAINT CH_Pairs_MemberA CHECK(MemberA > 0),
	CONSTRAINT CH_Pairs_MemberB CHECK(MemberB > 0 OR MemberB IS NULL),
	CONSTRAINT CH_Pairs_Winer CHECK(Winer = MemberA OR Winer = MemberB),
	CONSTRAINT CH_Pairs_Stage CHECK(Stage > 1)
)
GO
--Delete all data from all tables in database
USE GPS_TRACKING_DATABASE
GO
-- disable referential integrity
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' 
GO 

EXEC sp_MSForEachTable 'DELETE FROM ?' 
GO 

-- enable referential integrity again 
EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL' 
GO

--Drop all tables in database 

USE GpsTrackingDatabase
GO

DROP TABLE [Log]
DROP TABLE [Event]
DROP TABLE [Track]
DROP TABLE [User]
DROP TABLE [Marker]
DROP TABLE [Person]

USE BD_LAB2_BUY_SELL

DROP DATABASE GPS_TRACKING_DATABASE
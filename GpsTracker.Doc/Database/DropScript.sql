--Delete all data from all tables in database
USE GpsTrackingDatabase
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
DROP TABLE [Marker]
DROP TABLE [Person]
DROP TABLE [User]

--Drop database 
Use master;

ALTER database GpsTrackingDatabase set offline with ROLLBACK IMMEDIATE;

DROP database GpsTrackingDatabase;

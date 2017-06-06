--Delete all data from all tables in database
DELETE FROM Log
DELETE FROM Person
DELETE FROM Marker
DELETE FROM [Friendlist]
DELETE FROM [Zone]
DELETE FROM [User]
--DELETE FROM Event

--Drop all tables in database 

DROP TABLE [Log]
DROP TABLE [Event]
DROP TABLE [Marker]
DROP TABLE [Person]
DROP TABLE [Friendlist]
DROP TABLE [Zone]
DROP TABLE [User]

--Drop database 
Use master;

ALTER database GpsTrackingDatabase set offline with ROLLBACK IMMEDIATE;

DROP database GpsTrackingDatabase;
-- 01.Create a table in SQL Server with 10 000 000 log entries (date + text).
-- Search in the table by date range. Check the speed (without caching).

USE master
GO

--Create Database PerformanceDB
CREATE DATABASE PerformanceDB
GO

USE PerformanceDB
GO

--Create Table Logs
CREATE TABLE Logs(
	Id int NOT NULL PRIMARY KEY IDENTITY,
	SimpleText varchar(100),
	SimpleDate date
)
GO

INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('HQC exam', GETDATE())
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('Databases exam', GETDATE() + 1)
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('DSA exam', GETDATE() + 2)
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('I miss javascript', GETDATE() + 3)
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('JS Apps exam', GETDATE() + 4)
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('WTF per second', GETDATE() + 5)
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('If it works dont touch it', GETDATE() + 6)
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('HQC above all', GETDATE() + 7)
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('I hate MySql', GETDATE() + 8)
INSERT INTO Logs(SimpleText, SimpleDate) VALUES ('They are green!', GETDATE() + 9)
GO

DECLARE @Counter int = 0
WHILE (SELECT COUNT(*) FROM Logs) < 10000000
BEGIN
	INSERT INTO Logs(SimpleText, SimpleDate)
	SELECT SimpleText + CONVERT(varchar, @Counter), GETDATE() + @Counter FROM Logs
	SET @Counter = @Counter + 1
END
GO

CHECKPOINT; DBCC DROPCLEANBUFFERS; -- Empty the SQL Server cache

SELECT * 
FROM Logs
WHERE SimpleDate BETWEEN GETDATE() AND GETDATE() + 15


--02.Create index on the dateime column

CREATE INDEX IDX_Logs_SimpleDate
ON Logs(SimpleDate)

-- 03.Add a full text index for the text column.
-- Try to search with and without the full-text index and compare the speed.

SELECT * FROM Logs
WHERE SimpleText LIKE '%MySql'
GO

-- Create full text index
-- Note: you need to have full  text search installed on the Sql Server.
CREATE FULLTEXT CATALOG LogsSimpleTextCatalog
WITH ACCENT_SENSITIVITY = OFF

CREATE FULLTEXT INDEX ON Logs(SimpleText)
KEY INDEX PK__Logs__3214EC077181997B
ON LogsSimpleTextCatalog
WITH CHANGE_TRACKING AUTO

-- 04.Create the same table in MySQL and partition it by date (1990, 2000, 2010).
-- Fill 1 000 000 log entries.
-- Compare the searching speed in all partitions (random dates) to certain partition (e.g. year 1995).

-- without partitioning
CREATE SCHEMA `performancedb`;

CREATE TABLE `logs`(
	`Id` INT NOT NULL AUTO_INCREMENT,
	`SimpleText` TEXT NOT NULL,
	`SimpleDate` DATETIME NOT NULL,
	PRIMARY KEY (`Id`));

-- With partitioning
CREATE SCHEMA `performancedb`;

CREATE TABLE `logs`(
	`Id` INT NOT NULL AUTO_INCREMENT,
	`SimpleText` TEXT NOT NULL,
	`SimpleDate` DATETIME NOT NULL,
	PRIMARY KEY (`Id`, `SimpleDate`)
) PARTITION BY RANGE(YEAR(SimpleDate)) (
    PARTITION p0 VALUES LESS THAN (1990),
    PARTITION p1 VALUES LESS THAN (2000),
    PARTITION p2 VALUES LESS THAN (2010),
    PARTITION p3 VALUES LESS THAN MAXVALUE
);


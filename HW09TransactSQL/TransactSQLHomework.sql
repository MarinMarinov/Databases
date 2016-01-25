USE TransactSQLHomeworkDB

-- 1. Create a database with two tables: Persons(Id(PK), FirstName, LastName, SSN) and Accounts(Id(PK), PersonId(FK), Balance).
-- Insert few records for testing. Write a stored procedure that selects the full names of all persons.

CREATE TABLE Persons (
        PersonID  INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        SSN NVARCHAR(40) NULL
)

INSERT INTO Persons
VALUES	('Solomon','Robert','9452B344C17EDB78EC4336019989FB9E'),
		('Castor','Chester','01C08C3DEEDA45143C33CAFEDC4C14B4'),
		('Isaac','Paul','14BB2B3F-7A69-7D8C-AE9C-5D368C53B456'),
		('Keaton','Nero','63A17AB6-18C0-554C-8AC4-29140B6A0311'),
		('Jordan','Linus','A659271C-C038-5C99-4F43-647CBC254489');

CREATE TABLE Accounts (
        AccountID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        PersonID INT 
			CONSTRAINT FK_PersonID 
			FOREIGN KEY(PersonID) 
			REFERENCES Persons(PersonID) NOT NULL,
        Balance money NULL
)

INSERT INTO Accounts
VALUES 
	(1, 6542654),
	(2, 35454),
	(3, 1258),
	(4, 324265),
	(5, 54255)
GO

CREATE PROC usp_SelectFullNamesFromPersons
AS
        SELECT p.FirstName + ' ' + p.LastName AS FullName
        FROM Persons p
GO
 
EXEC usp_SelectFullNamesFromPersons

-- 2. Create a stored procedure that accepts a number as a parameter and returns all persons who have more money
-- in their accounts than the supplied number.

CREATE PROC usp_SelectPersonsWithMoreMoneyThan(@money int)
AS
        SELECT p.FirstName + ' ' + p.LastName AS "Persons with bigger account balance"
        FROM Persons p
			JOIN Accounts a
			  ON p.PersonID = a.AccountID
		WHERE a.Balance >= @money
GO

EXEC usp_SelectPersonsWithMoreMoneyThan 50000

-- 3. Create a function that accepts as parameters – sum, yearly interest rate and number of months.
-- It should calculate and return the new sum. Write a SELECT to test whether the function works as expected.

CREATE PROC usp_CalculateSumOfMoneyWithInterest(@sum int, @yearlyInterest float, @months int)
AS
        SELECT @sum + @sum * (((@yearlyInterest / 12) / 100) * @months) AS [Sum with interest]
GO

EXEC usp_CalculateSumOfMoneyWithInterest 10000, 8, 12 -- will return 10800 = 10000(the initial money ammount) + 800(the interest)

-- 4. Create a stored procedure that uses the function from the previous example to give an interest to a person's account for one month.
-- It should take the AccountId and the interest rate as parameters.

CREATE PROC usp_CalculateSumOfMoneyWithOneMonthInterest (@accountID int, @yearlyInterest float)
AS
		DECLARE @initialBalance money 
		SET @initialBalance = (SELECT Balance FROM Accounts WHERE AccountID = @accountID)

		DECLARE @newBalance money

		CREATE TABLE #tmpTable
		(
			OutputValue money
		)

		INSERT INTO #tmpTable (OutputValue)

		EXEC usp_CalculateSumOfMoneyWithInterest @initialBalance, @yearlyInterest, 1;

		SELECT @newBalance = OutputValue

		FROM #tmpTable
		DROP TABLE #tmpTable

        UPDATE Accounts
		SET Balance = @newBalance
		FROM Accounts
		WHERE AccountID = @accountID
GO

EXEC usp_CalculateSumOfMoneyWithOneMonthInterest 5, 10.5 -- the new balance of AccountID 5 is 54729,7313, was 54255.00

-- 5. Add two more stored procedures WithdrawMoney( AccountId, money) and DepositMoney (AccountId, money) that operate in transactions

CREATE PROC usp_WithdrawMoney(@accountID int, @amount money)
AS
	BEGIN TRAN
	UPDATE Accounts
	SET Balance = Balance - @amount
	FROM Accounts
	WHERE AccountID = @accountID
	COMMIT TRAN
GO

CREATE PROC usp_DepositMoney (@accountID int, @amount money)
AS
	BEGIN TRAN
	UPDATE Accounts
	SET Balance = Balance + @amount
	FROM Accounts
	WHERE AccountID = @accountID
	COMMIT TRAN
GO
 
EXEC usp_WithdrawMoney 1, 100000 -- balance was 6542654,00, now is 6442654,00

EXEC dbo.usp_DepositMoney 3, 100000 -- balance was 1258,00, now is 101258,00

-- 6. Create another table – Logs(LogID, AccountID, OldSum, NewSum).
-- Add a trigger to the Accounts table that enters a new entry into the Logs table every time the sum on an account changes.

CREATE TABLE Logs (
        LogID  INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        AccountID INT
			CONSTRAINT FK_AccountID 
			FOREIGN KEY(AccountID) 
			REFERENCES Accounts(AccountID) NOT NULL,
        OldSum INT NOT NULL,
        NewSum INT NOT NULL
)

CREATE Trigger TR_AccountUpdate ON dbo.Accounts FOR UPDATE
AS
	BEGIN
		INSERT INTO dbo.Logs(AccountID, OldSum, NewSum)
		SELECT inserted.AccountID, deleted.Balance, inserted.Balance
		FROM inserted, deleted
	END

EXEC usp_WithdrawMoney 4, 24265.00 -- balance was 324265,00, now is 300000.00 and there is new log with ID 1

EXEC usp_DepositMoney 4, 100.00 -- balance was 300000.00, now is 300100.00 and there is new log with ID 2

-- 7. Define a function in the database TelerikAcademy that returns all Employee's names (first or middle or last name)
-- and all town's names that are comprised of given set of letters.
--  Example 'oistmiahf' will return 'Sofia', 'Smith', … but not 'Rob' and 'Guy'.

USE TelerikAcademy
GO

CREATE FUNCTION fn_StringContainsName (@string nvarchar(MAX), @name nvarchar(MAX))
RETURNS bit
AS
BEGIN
	DECLARE @counter int = 1
	WHILE (@counter <= LEN(@name))
		BEGIN
		IF (CHARINDEX(SUBSTRING(@name, @counter, 1), @string) = 0)
			RETURN 0
		SET @counter = @counter + 1
		END
RETURN 1
END
GO

CREATE FUNCTION fn_ReturnTownsAndPersonsWithLetters(@letters nvarchar(MAX))

RETURNS TABLE
AS
  RETURN
  (
  SELECT FirstName
  FROM Employees
  WHERE dbo.fn_StringContainsName(@letters, FirstName) = 1
  UNION
  SELECT MiddleName
  FROM Employees
  WHERE dbo.fn_StringContainsName(@letters, MiddleName) = 1
  UNION
  SELECT LastName
  FROM Employees
  WHERE dbo.fn_StringContainsName(@letters, LastName) = 1
  UNION
  SELECT Name
  FROM Towns
  WHERE dbo.fn_StringContainsName(@letters, Name) = 1
  )
GO

SELECT * FROM fn_ReturnTownsAndPersonsWithLetters('ohoboho')

-- 8. Using database cursor write a T-SQL script that scans all employees and their addresses and prints all pairs of employees
--  that live in the same town.

DECLARE empCursor CURSOR READ_ONLY FOR
 
SELECT a.FirstName, a.LastName, t1.Name, b.FirstName, b.LastName
FROM Employees a
JOIN Addresses adr
ON a.AddressID = adr.AddressID
JOIN Towns t1
ON adr.TownID = t1.TownID,
 Employees b
JOIN Addresses ad
ON b.AddressID = ad.AddressID
JOIN Towns t2
ON ad.TownID = t2.TownID
WHERE t1.Name = t2.Name
  AND a.EmployeeID <> b.EmployeeID
ORDER BY a.FirstName, b.FirstName
 
OPEN empCursor
DECLARE @firstName1 NVARCHAR(50)
DECLARE @lastName1 NVARCHAR(50)
DECLARE @town NVARCHAR(50)
DECLARE @firstName2 NVARCHAR(50)
DECLARE @lastName2 NVARCHAR(50)
FETCH NEXT FROM empCursor
        INTO @firstName1, @lastName1, @town, @firstName2, @lastName2
 
WHILE @@FETCH_STATUS = 0
        BEGIN
                PRINT @firstName1 + ' ' + @lastName1 +
                        '     ' + @town + '      ' + @firstName2 + ' ' + @lastName2
                FETCH NEXT FROM empCursor
                        INTO @firstName1, @lastName1, @town, @firstName2, @lastName2
        END
 
CLOSE empCursor
DEALLOCATE empCursor

-- 9. * Write a T-SQL script that shows for each town a list of all employees that live in it.
-- Sample output: 	 sql Sofia -> Martin Kulov, George Denchev Ottawa -> Jose Saraiva …

USE TelerikAcademy

DECLARE empCursor CURSOR READ_ONLY FOR
SELECT Name FROM Towns
OPEN empCursor
DECLARE @townName VARCHAR(50), @userNames VARCHAR(MAX)
FETCH NEXT FROM empCursor INTO @townName
  
WHILE @@FETCH_STATUS = 0
  BEGIN
                BEGIN
                DECLARE nameCursor CURSOR READ_ONLY FOR
                SELECT a.FirstName, a.LastName
                FROM Employees a
                JOIN Addresses adr
                ON a.AddressID = adr.AddressID
                JOIN Towns t1
                ON adr.TownID = t1.TownID
                WHERE t1.Name = @townName
                OPEN nameCursor
               
                DECLARE @firstName VARCHAR(50), @lastName VARCHAR(50)
                FETCH NEXT FROM nameCursor INTO @firstName,  @lastName
                WHILE @@FETCH_STATUS = 0
                        BEGIN
                                SET @userNames = CONCAT(@userNames, @firstName, ' ', @lastName, ', ')
                                FETCH NEXT FROM nameCursor
                                INTO @firstName,  @lastName
                        END
        CLOSE nameCursor
        DEALLOCATE nameCursor
                END
                SET @userNames = LEFT(@userNames, LEN(@userNames) - 1)
    PRINT @townName + ' -> ' + @userNames
    FETCH NEXT FROM empCursor
    INTO @townName
  END
CLOSE empCursor
DEALLOCATE empCursor
 
GO

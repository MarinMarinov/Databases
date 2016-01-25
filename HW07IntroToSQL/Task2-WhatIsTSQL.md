## What is Transact-SQL (T-SQL)?


Transact-SQL (T-SQL) is Microsoft's and Sybase's proprietary extension to SQL.  T-SQL expands on the SQL standard to include procedural programming, local variables, various support functions for string processing, date processing, mathematics, etc. and changes to the DELETE and UPDATE statements.These additional features make Transact-SQL Turing complete.[citation needed]

Transact-SQL is central to using Microsoft SQL Server. All applications that communicate with an instance of SQL Server do so by sending Transact-SQL statements to the server, regardless of the user interface of the application

Transact-SQL Supports

* if statements
* loops
* exceptions
* constructions used in the high-level procedural programming languages


T-SQL is used for writing stored procedures, functions, triggers, etc.

Example:

	CREATE PROCEDURE EmpDump AS
	  DECLARE @EmpId INT, @EmpFName NVARCHAR(100), 
	    @EmpLName NVARCHAR(100)
	  DECLARE emps CURSOR FOR
	    SELECT EmployeeID, FirstName, LastName FROM Employees
	  OPEN emps
	  FETCH NEXT FROM emps INTO @EmpId, @EmpFName, @EmpLName
	  WHILE (@@FETCH_STATUS = 0) BEGIN
	    PRINT CAST(@EmpId AS VARCHAR(10)) + ' '
	      + @EmpFName + ' ' + @EmpLName
	    FETCH NEXT FROM emps INTO @EmpId, @EmpFName, @EmpLName
	  END
	  CLOSE emps
	  DEALLOCATE emps
	GO
--9. Write a SQL query to find all different employee salaries.

SELECT  DISTINCT Salary AS Salary, FirstName +' '+ LastName AS FullName 
FROM dbo.Employees
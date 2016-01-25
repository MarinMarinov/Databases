--12. Write a SQL query to find the names of all employees whose last name contains "ei".

SELECT  FirstName +' '+ LastName AS FullName
FROM dbo.Employees e
WHERE e.LastName LIKE '%ei%'
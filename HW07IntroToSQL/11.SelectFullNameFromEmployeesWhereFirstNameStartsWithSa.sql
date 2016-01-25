--11. Write a SQL query to find the names of all employees whose first name starts with "SA".

SELECT  FirstName +' '+ LastName AS FullName
FROM dbo.Employees e
WHERE e.FirstName LIKE 'SA%' -- or 'Sa%' - depends from the collation settings of MS SQLServer
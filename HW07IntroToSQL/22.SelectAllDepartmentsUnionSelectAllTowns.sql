--22. Write a SQL query to find all departments and all town names as a single list. Use UNION.

SELECT Departments.Name
FROM dbo.Departments
UNION
SELECT Towns.Name
FROM dbo.Towns
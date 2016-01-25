--15. Write a SQL query to find all employees that do not have manager.

SELECT *
FROM dbo.Employees
WHERE ManagerID IS NULL
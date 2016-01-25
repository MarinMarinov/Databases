--20. Write a SQL query to find all employees along with their manager.

SELECT e.FirstName +' '+ e.LastName AS Employee, m.FirstName +' '+ m.LastName AS Manager
FROM dbo.Employees e, dbo.Employees m
WHERE e.ManagerID = m.EmployeeID
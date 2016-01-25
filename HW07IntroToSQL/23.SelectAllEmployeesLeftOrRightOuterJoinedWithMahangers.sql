--23. Write a SQL query to find all the employees and the manager for each of them along with the employees 
--that do not have manager. Use right outer join. Rewrite the query to use left outer join.

-- RIGHT OUTER JOIN
SELECT e.FirstName +' '+ e.LastName AS Employee, m.FirstName +' '+ m.LastName AS Manager
FROM dbo.Employees m
RIGHT OUTER JOIN dbo.Employees e
ON m.EmployeeID = e.ManagerID

-- LEFT OUTER JOIN
SELECT e.FirstName +' '+ e.LastName AS Employee, m.FirstName +' '+ m.LastName AS Manager
FROM dbo.Employees e
LEFT OUTER JOIN dbo.Employees m
ON e.ManagerID = m.EmployeeID 

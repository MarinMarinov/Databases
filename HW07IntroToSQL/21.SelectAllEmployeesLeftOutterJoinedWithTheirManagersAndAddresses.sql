--21. Write a SQL query to find all employees, along with their manager and their address. 
--Join the 3 tables: Employees e, Employees m and Addresses a.

SELECT e.FirstName +' '+ e.LastName AS Employee, m.FirstName +' '+ m.LastName AS Manager, a.AddressText
FROM dbo.Employees e
LEFT OUTER JOIN dbo.Employees m -- will have all employees with and without(NULL) managers
ON e.ManagerID = m.EmployeeID 
INNER JOIN dbo.Addresses a
ON e.AddressID = a.AddressID
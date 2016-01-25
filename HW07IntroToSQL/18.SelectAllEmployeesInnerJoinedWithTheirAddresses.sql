--18. Write a SQL query to find all employees along with their address. Use inner join with ON clause.

SELECT FirstName, LastName, e.AddressID, a.AddressID, a.AddressText
FROM dbo.Employees e
JOIN dbo.Addresses a
ON e.AddressID = a.AddressID
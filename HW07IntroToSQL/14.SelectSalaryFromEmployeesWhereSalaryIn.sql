--14. Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.

SELECT  FirstName +' '+ LastName AS FullName, Salary
FROM dbo.Employees
WHERE Salary IN(25000, 14000, 12500, 23600)
ORDER BY Salary DESC
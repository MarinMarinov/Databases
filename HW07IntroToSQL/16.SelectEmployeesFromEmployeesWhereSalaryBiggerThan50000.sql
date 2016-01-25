--16. Write a SQL query to find all employees that have salary more than 50000.
-- Order them in decreasing order by salary.

SELECT FirstName, LastName, Salary
FROM dbo.Employees
WHERE Salary > 5000
ORDER BY Salary DESC
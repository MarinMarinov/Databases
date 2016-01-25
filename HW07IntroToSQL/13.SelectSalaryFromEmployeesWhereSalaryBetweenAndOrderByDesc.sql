--13. Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].

SELECT  FirstName +' '+ LastName AS FullName, Salary
FROM dbo.Employees
WHERE Salary BETWEEN 20000 AND 30000
ORDER BY Salary DESC
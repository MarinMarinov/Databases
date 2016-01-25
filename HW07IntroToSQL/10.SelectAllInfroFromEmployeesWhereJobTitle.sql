-- 10. Write a SQL query to find all information about the employees
-- whose job title is “Sales Representative“.

SELECT  *
FROM dbo.Employees e
WHERE e.JobTitle = 'Sales Representative'
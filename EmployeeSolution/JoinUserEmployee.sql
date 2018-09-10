select Users.Username, Employee.FirstName, Employee.LastName, Employee.DOB
FROM Users
INNER JOIN Employee 
ON Users.User_Id = Employee.User_id;
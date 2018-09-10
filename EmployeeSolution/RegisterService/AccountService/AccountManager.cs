using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Security.Cryptography;
using Solutions.Data.DataModel;
using SharedModels;

namespace RegisterService
{
    public class AccountManager
    {
        EmployeeSolutions db = new EmployeeSolutions();//initialize the DBContext

        public void CreateUser(AccountViewModel model)
        {
            try
            {

                User user = new User();

                var data = Encoding.UTF8.GetBytes(model.Password);//convert password to byte[]
                using (SHA512 shaM = new SHA512Managed())//use sha512 to encode password to binary
                {
                    var hashPassword = shaM.ComputeHash(data);//complete conversion
                    user.Password = hashPassword;
                }
                //var idCount = db.Users.Count();//get the total number of id's
                //idCount++;//create new id for insertion


                //user.User_Id = idCount;
               // user.Username = model.User.Username;
                //db.Users.Add(user);

                Employee employee = new Employee();
                //employee.Id = idCount;
                employee.FirstName = model.Employee.FirstName;
                employee.LastName = model.Employee.LastName;
                employee.DOB = model.Employee.DOB;
                //employee.User = user; //associate the employee with the this user
                //employee.User_id = user.User_Id;
                user.Username = model.User.Username;
                user.Employees.Add(employee);
                db.Users.Add(user);
                //db.Employees.Add(employee);
                Console.WriteLine("Employee added to the Database {0} ", employee.User.Username);
                db.SaveChanges();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                db.Dispose();
            }

        }


        public bool UpdateEmployee(EmployeeUserDescriptionViewModel updateData)
        {
            Employee updateEmp = new Employee();
            User updateUser = new User();
            try
            {
                if (updateData != null)
                {
                    //gather password from user to be added to the updated employee
                    var userPassword = from user in db.Users
                                 where user.User_Id == updateData.userId
                                 select user.Password;

                    updateEmp.FirstName = updateData.employeeFirstName;
                    updateEmp.LastName = updateData.employeeLastName;
                    updateEmp.DOB = updateData.employeeDOB;
                    updateEmp.Id = updateData.userId;
                    updateEmp.User_id = updateData.userId;
                    updateUser.Username = updateData.userName;
                    updateUser.Password =  userPassword.FirstOrDefault();
                    updateUser.User_Id =  updateData.userId;
                    db.Entry(updateUser).State = EntityState.Modified;
                    db.Entry(updateEmp).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;

        }


        public bool DeleteUser(int id)
        {
            var user = (from thisUser in db.Users
                        where thisUser.User_Id == id
                        select thisUser).First();
            if(user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }

            return false;

        }

        public void ChangePassword(int id, String newPassword)
        {
            //User user = this.findUser(id);
            var data = Encoding.UTF8.GetBytes(newPassword);
            using (SHA512 shaM = new SHA512Managed())
            {
                var hash = shaM.ComputeHash(data);
                //  user.Password = hash;
                var changeUser = (from user in db.Users
                                 where user.User_Id == id
                                 select user).FirstOrDefault();
                db.Users.Add(changeUser);
                db.SaveChanges();
            }

        }
        public List<EmployeeUserDescriptionViewModel> FindAllUsersEmployees()
        {
            List<EmployeeUserDescriptionViewModel> employeeUserList = new List<EmployeeUserDescriptionViewModel>();
            //AccountViewModel model = new AccountViewModel();
            try
            {
                //result is a query that joins the employee and customer table and orders the result
                //by employee first name in descending order
                var result = from user in db.Users
                             join employee in db.Employees on user.User_Id equals employee.User_id
                             orderby employee.FirstName descending
                             select new EmployeeUserDescriptionViewModel
                             {//Uses the EmployeeUserDescriptionViewModel class to fill getters/setters
                                 userId = user.User_Id,
                                 userName = user.Username,
                                 employeeFirstName = employee.FirstName,
                                 employeeLastName = employee.LastName,
                                 employeeDOB = (DateTime)employee.DOB,
                             };

                employeeUserList = result.ToList();//convert IQueryable object to List

                return employeeUserList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        //returns a list of user data (username/password)
        //else it returns null if user not found
        public List<EmployeeUserDescriptionViewModel> Login(string userName, string password)
        {
            List<EmployeeUserDescriptionViewModel> foundUser = new List<EmployeeUserDescriptionViewModel>();
            try
            {

                var data = Encoding.UTF8.GetBytes(password);//convert password to byte[]

                using (SHA512 shaM = new SHA512Managed())//use sha512 to encode password to binary
                {
                    var hashPassword = shaM.ComputeHash(data);//complete conversion
                                                              //query to find a user that has the same username and password in database
                    var userLoginQuery = from user in db.Users
                                         where user.Username == userName && user.Password == hashPassword
                                         select new EmployeeUserDescriptionViewModel
                                         {
                                             userId = user.User_Id,
                                             userName = userName,
                                         };
                    if (userLoginQuery.Any())
                    {
                        return foundUser = userLoginQuery.ToList();//return user information from DB
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;

        }
    }
}

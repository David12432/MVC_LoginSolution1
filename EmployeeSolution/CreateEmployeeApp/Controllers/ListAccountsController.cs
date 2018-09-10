using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RegisterService;
using SharedModels;

namespace CreateEmployeeApp.Controllers
{
    public class ListAccountsController : Controller
    {

        // GET: ListAccounts
        public ActionResult Index()
        {
            AccountManager manager = new AccountManager();
            List<EmployeeUserDescriptionViewModel> employeeUser = new List<EmployeeUserDescriptionViewModel>();


            employeeUser = manager.FindAllUsersEmployees();

            return View(employeeUser);
        }

        // GET: ListAccounts/Edit/5
        [HttpGet]
        public ActionResult Edit(int id, DateTime dob, string firstName, string lastName,
                                 string userName)
        {
            EmployeeUserDescriptionViewModel data = new EmployeeUserDescriptionViewModel();
            data.employeeDOB = dob;
            data.employeeFirstName = firstName.Trim();
            data.employeeLastName = lastName.Trim();
            data.userName = userName.Trim();
            data.userId = id;

            return View(data);
        }

        // POST: ListAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeUserDescriptionViewModel model)
        {
            AccountManager manager = new AccountManager();
            try
            {
                if (ModelState.IsValid)
                {

                    if (manager.UpdateEmployee(model))
                        Session["Update"] = "Employee Successfully modified";
                    return RedirectToAction("Index", "ListAccounts");
                }
                Session["Update"] = "Could not modify employee, please contact support.";
                return RedirectToAction("Index", "ListAccounts", ViewBag);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(model);
        }

        // POST: ListAccounts/Delete/5
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                AccountManager manager = new AccountManager();
                manager.DeleteUser(id);
                TempData["Update"] = "Employee deleted.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }
    }
}

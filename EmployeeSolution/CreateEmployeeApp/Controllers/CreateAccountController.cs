using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using CreateEmployeeApp.Models;
using SharedModels;
using RegisterService;


namespace CreateEmployeeApp.Controllers
{
    public class CreateAccountController : Controller
    {
        AccountManager manager = new AccountManager();

       [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        // GET: CreateAccount/Details/5
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //[RequireHttps]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyLogin(LoginViewModel model)
        {
            List<EmployeeUserDescriptionViewModel> userInfo = new List<EmployeeUserDescriptionViewModel>();
            AccountManager manager = new AccountManager();
            if (ModelState.IsValid) {
                var userName = model.Username;
                var password = model.Password;
                userInfo = manager.Login(userName, password);//verify user and return user information
                if (userInfo != null)
                {
                    Session["userInfo"] = userInfo; //begin a session for this user
                    return RedirectToAction("Index", "ListAccounts");
                }
            }
            TempData["Update"] = "Unable to log in. Please Check Username or Password";
                return RedirectToAction("Login", "CreateAccount");
        }

       [ValidateAntiForgeryToken]
        public ActionResult Create(AccountViewModel viewModel)//pass the view model which contains both employee and user info
        {
            try
            {
                if (ModelState.IsValid && viewModel.Password == viewModel.ConfirmPassword) {

                    manager.CreateUser(viewModel);//pass all info from the view model
                                                                                   //do the account manager(businesslogic)

                    return RedirectToAction("Login", "CreateAccount");
                }
                return View();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View(viewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: CreateAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}

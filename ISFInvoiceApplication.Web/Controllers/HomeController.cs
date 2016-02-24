using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISFInvoiceApplication.Core;
using ISFInvoiceApplication.Core.Entities;
using ISFInvoiceApplication.Core.ValueObjects;
using ISFInvoiceApplication.Infrastructure.Data;
using ISFInvoiceApplication.Infrastructure.Data.Repositories;
using ISFInvoiceApplication.Web.Models;

namespace ISFInvoiceApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserAccountRepository _userAccountRepository;

        public HomeController()
        {
            _userAccountRepository = new UserAccountRepository(new InvoiceApplicationEntities());
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginCredentials)
        {
            bool authenticate = _userAccountRepository.RegisteredUser(loginCredentials.Username, loginCredentials.Password);
            if (authenticate)
            {
                Session["Username"] = loginCredentials.Username;
                return RedirectToAction("GetInvoices", "Invoice", null);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationViewModel registrationDetails)
        {
            if (ModelState.IsValid)
            {
                var userAccount = new UserAccount(0, registrationDetails.Username, registrationDetails.Password, registrationDetails.Email, 0, new InvoiceLimit(0,0));
                userAccount.State = TrackingState.Added;
                _userAccountRepository.SaveUserAccount(userAccount);

                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpGet]
        public ActionResult UpdateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateAccount(int warningLimit, int errorLimit)
        {
            var userAccount = _userAccountRepository.GetUserAccount(Session["Username"].ToString(), false);
            userAccount.InvoiceLimit.WarningLimit = warningLimit;
            userAccount.InvoiceLimit.ErrorLimit = errorLimit;
            userAccount.State = TrackingState.Modified;
            _userAccountRepository.SaveUserAccount(userAccount);
            
            return RedirectToAction("GetInvoices", "Invoice", null);
        }
    }
}

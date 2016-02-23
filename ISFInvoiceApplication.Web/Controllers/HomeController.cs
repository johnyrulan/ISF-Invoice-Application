using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View();
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(string username, string password)
        {
            return View();
        }
    }
}

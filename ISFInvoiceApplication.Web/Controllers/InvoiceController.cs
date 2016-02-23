using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISFInvoiceApplication.Core.Entities;
using ISFInvoiceApplication.Infrastructure.Data;
using ISFInvoiceApplication.Infrastructure.Data.Repositories;

namespace ISFInvoiceApplication.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly UserAccountRepository _userAccountRepository;
        private UserAccount _userAccount;

        public InvoiceController()
        {
            _userAccountRepository = new UserAccountRepository(new InvoiceApplicationEntities());           
        }

        [HttpGet]
        public ActionResult GetInvoices()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateInvoice(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateInvoice()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteInvoice(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateInvoice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInvoice(Invoice invoice)
        {
            return View();
        }
    }
}

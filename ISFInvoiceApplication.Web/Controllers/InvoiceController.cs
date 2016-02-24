using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISFInvoiceApplication.Core.Entities;
using ISFInvoiceApplication.Core.ValueObjects;
using ISFInvoiceApplication.Infrastructure.Data;
using ISFInvoiceApplication.Infrastructure.Data.Repositories;

namespace ISFInvoiceApplication.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly UserAccountRepository _userAccountRepository;

        public InvoiceController()
        {
            _userAccountRepository = new UserAccountRepository(new InvoiceApplicationEntities());
        }

        [HttpGet]
        public ActionResult CreateInvoice()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInvoice(string name, int price, int quantity)
        {
            var userAccount = _userAccountRepository.GetUserAccount(Session["Username"].ToString(), false);
            var result = userAccount.AddInvoice(new Invoice(0, userAccount.Id, new OrderDetails(name, quantity, price), DateTime.Now));
            ViewBag.Message = result.Item2;

            if (result.Item3)
            {
                _userAccountRepository.SaveInvoices(userAccount);
            }
            
            return View();
        }


        [HttpGet]
        public ActionResult GetInvoices()
        {
            var userAccount = _userAccountRepository.GetUserAccount(Session["Username"].ToString(), true);
            ViewBag.Username = userAccount.Username;
            ViewBag.Email = userAccount.Email;
            ViewBag.WarningLimit = userAccount.InvoiceLimit.WarningLimit;
            ViewBag.ErrorLimit = userAccount.InvoiceLimit.ErrorLimit;
            ViewBag.SumTotal = userAccount.InvoiceTotal;

            return View(userAccount.Invoices);
        }

        [HttpGet]
        public ActionResult UpdateInvoice(int id)
        {
            var invoice = _userAccountRepository.GetUserAccount(Session["Username"].ToString(), true).Invoices
                                                .FirstOrDefault(i => i.Id == id);
            
            return View(invoice);
        }

        [HttpPost]
        public ActionResult UpdateInvoice(int id, string name, int quantity, int price)
        {
            var userAccount = _userAccountRepository.GetUserAccount(Session["Username"].ToString(), true);
            var result = userAccount.UpdateInvoice(new Invoice(id, userAccount.Id, new OrderDetails(name, quantity, price), DateTime.Now));
            ViewBag.Message = result.Item2;

            if (result.Item3)
            {
                _userAccountRepository.SaveInvoices(userAccount);
            }

            return UpdateInvoice(id);
        }

        [HttpGet]
        public ActionResult DeleteInvoice(int id)
        {
            var userAccount = _userAccountRepository.GetUserAccount(Session["Username"].ToString(), true);
            ViewBag.Message = "Delete Failed";
            if (userAccount.DeleteInvoice(new Invoice(id, userAccount.Id, null, DateTime.Now)))
            {
                _userAccountRepository.SaveInvoices(userAccount);
                ViewBag.Message = "Delete Succeeded";
            }

            return RedirectToAction("GetInvoices");
        }       
    }
}

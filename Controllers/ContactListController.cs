using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactList.Models;
using ContactList.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ContactList.Controllers
{
    public class ContactList : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ContactCreate(ContactFormViewModel contactForm)
        {
            return Json(contactForm);
            //return View();
        }
    }
}
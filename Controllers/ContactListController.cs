using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactList.Models;
using ContactList.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ContactList.Services;
using Microsoft.AspNetCore.Identity;

namespace ContactList.Controllers
{
    public class ContactList : Controller
    {
        private readonly IContactListService _contactService;
        private readonly UserManager<ApplicationUser> _user;

        public ContactList(IContactListService contactService, UserManager<ApplicationUser> user)
        {
            _contactService = contactService;
            _user = user;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _user.GetUserAsync(User);
            if(currentUser == null) return Challenge();

            var temp = _contactService.GetContactsAsync(currentUser);

            AllContactListViewModel all = new AllContactListViewModel
            {
                contactList = (IEnumerable<FullContactListViewModel>)temp
            };

            return Json(all);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ContactCreate(ContactFormViewModel contactForm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _user.GetUserAsync(User);

            if(currentUser == null) return Unauthorized();

            var successfull = await _contactService.CreateContact(contactForm,currentUser);

            if(!successfull)
            {
                return BadRequest(new { error = "Could not add item. Please try again"});
            }

            return Ok();
            //return View();
        }
    }
}
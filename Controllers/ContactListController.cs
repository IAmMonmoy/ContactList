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
using System.IO;

namespace ContactList.Controllers
{
    [Authorize]
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

            var temp = await _contactService.GetContactsAsync(currentUser);

            AllContactListViewModel all = new AllContactListViewModel
            {
                contactList = temp
            };

            return View(all);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid personId)
        {
            var entity = await _contactService.GetContactByIdAsync(personId);
            Person person = entity;
            return View(entity);
        }

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

            return RedirectToAction("Index");
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactDelete(Guid deleteButton)
        {
            var successfull = await _contactService.DeleteContact(deleteButton);

            if(!successfull)
            {
                return BadRequest(new { error = "Could not delete item. Please try again"});
            }

            return RedirectToAction("Index");
        }   

        [HttpPost]
        public async Task<IActionResult> ContactUpdate(Person person, Guid personId)
        {
             var saveResult = await _contactService.UpdateContactAsync(person,personId);

             if(!saveResult)
             {
                 return BadRequest(new { error = "Could not delete item. Please try again"});
             }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> DownloadCsv()
        {
            var currentUser = await _user.GetUserAsync(User);
            if(currentUser == null) return Challenge();

            var temp = await _contactService.GetContactsAsync(currentUser);

            AllContactListViewModel list = new AllContactListViewModel
            {
                contactList = temp
            };
            
            var memory = new MemoryStream();

            memory = await _contactService.BuildCsvString(list);
            
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", @"ContactList.xlsx");
        }
    }
}
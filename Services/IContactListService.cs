using System;
using System.Threading.Tasks;
using ContactList.Models;
using ContactList.ViewModels;

namespace ContactList.Services
{
    public interface IContactListService
    {
        Task<bool> CreateContact(ContactFormViewModel contact, ApplicationUser user);
    }
}
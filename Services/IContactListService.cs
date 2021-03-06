using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ContactList.Models;
using ContactList.ViewModels;
using System.IO;

namespace ContactList.Services
{
    public interface IContactListService
    {
        Task<bool> CreateContact(ContactFormViewModel contact, ApplicationUser user);

        Task<IEnumerable<Person>> GetContactsAsync(ApplicationUser user);

        Task<bool> DeleteContact(Guid PersonId);

        Task<Person> GetContactByIdAsync(Guid personId);

        Task<bool> UpdateContactAsync(Person person, Guid personId);

        Task<MemoryStream> BuildCsvString(AllContactListViewModel list);
    }
}
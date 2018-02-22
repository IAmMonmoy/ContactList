using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactList.Models;

namespace ContactList.Services
{
    public interface IContactService
    {
        Task<bool> AddContact(Person person, List<PhoneNumber> phones);

        //Task<bool> RemoveContact();

        //Task<IEnumerable<Person>> GetContacts();
    }
}
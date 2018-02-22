using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactList.Models;
using ContactList.Data;

namespace ContactList.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddContact(Person person, List<PhoneNumber> phones)
        {

            _context.Persons.Add(person);
            foreach(PhoneNumber phone in phones)
            {
                _context.Phones.Add(phone);
            }

            var saveResult = await _context.SaveChangesAsync();
            int totalChanges = phones.Count+1;

            return saveResult == totalChanges;
        }
    }
}
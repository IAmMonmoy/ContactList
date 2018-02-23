using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ContactList.Models;
using ContactList.ViewModels;
using ContactList.Data;
using Microsoft.AspNetCore.Identity;

namespace ContactList.Services
{
    public class ContactListService : IContactListService
    {
        private readonly ApplicationDbContext _context;

        public ContactListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateContact(ContactFormViewModel contact, ApplicationUser user)
        {
            var personEntity = new Person
            {
                Id = new Guid(),
                UserId = user.Id,
                NickName = contact.NickName,
                FullName = contact.FullName,
                Address = contact.Address,
                Website =contact.Website,
                DateOfBirth = contact.DateOfBirth
            };
            _context.Person.Add(personEntity);

            foreach(PhoneNumberViewModel phoneNumber in contact.Phones)
            {
                var phoneEntity = new PhoneNumber
                {
                    Id = new Guid(),
                    Phone = phoneNumber.Phone,
                    PersonId = personEntity.Id
                };
                _context.PhoneNumbers.Add(phoneEntity);
            }

            var saveResult = await _context.SaveChangesAsync();
            var totalChanges = contact.Phones.Count+1;
            
            return saveResult == totalChanges;
        }

        public Task<IEnumerable<ContactFormViewModel>> GetContactsAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
            // var entity = await _context.Person
            //                 .Include(person => person.Phones).ToArrayAsync();

            // ContactFormViewModel contact = new ContactFormViewModel
            // {
                
            // };
        }
    }
}
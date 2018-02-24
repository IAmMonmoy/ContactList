using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ContactList.Data;
using ContactList.Models;
using Microsoft.EntityFrameworkCore;
using ContactList.ViewModels;


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

        public async Task<IEnumerable<Person>> GetContactsAsync(ApplicationUser user)
        {
           return await _context.Person.Where( x => x.UserId == user.Id)
                                              .Include(person => person.Phones)
                                              .ToArrayAsync();
        }

        public async Task<bool> DeleteContact(Guid PersonId)
        {
            _context.Person.Remove(await _context.Person.FindAsync(PersonId));

            var deleteResult = await _context.SaveChangesAsync();

            return deleteResult == 1;
        }

        public async Task<Person> GetContactByIdAsync(Guid personId)
        {
            return await _context.Person.Where(x => x.Id == personId)
                                 .Include(x => x.Phones)
                                 .SingleAsync();
        }

        public async Task<bool> UpdateContactAsync(Person person, Guid personId)
        {
            var entity = await _context.Person.Where(x => x.Id == personId)
                                 .Include(x => x.Phones)
                                 .SingleAsync();
                
            entity.NickName = person.NickName;
            entity.FullName = person.FullName;
            entity.Address = person.Address;
            entity.Website = person.Website;
            entity.DateOfBirth = person.DateOfBirth;
            entity.Phones = person.Phones;
            try{
                await _context.SaveChangesAsync();
                return 1 == 1;
            }
            
            catch{
                return 2==1;
            }
            
        }
    }
}
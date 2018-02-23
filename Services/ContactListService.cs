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

        public async Task<IEnumerable<FullContactListViewModel>> GetContactsAsync(ApplicationUser user)
        {
            var entity = await _context.Person.Where( x => x.UserId == user.Id)
                                              .Include(person => person.Phones)
                                              .ToListAsync();
            
            List<FullContactListViewModel> fullContactViewModel = new List<FullContactListViewModel>();
            
            foreach(var en in entity)
            {
                List<PhoneNumber> phoneNumber = new List<PhoneNumber>();
                foreach(PhoneNumber phone in en.Phones)
                {
                    PhoneNumber number = new PhoneNumber
                    {
                        Id = phone.Id,
                        PersonId = phone.PersonId,
                        Phone = phone.Phone
                    };

                    phoneNumber.Add(number);
                }

                Person person = new Person
                {
                    Id = en.Id,
                    UserId = en.UserId,
                    NickName = en.NickName,
                    FullName = en.FullName,
                    Address = en.Address,
                    Website = en.Website,
                    DateOfBirth = en.DateOfBirth
                };
                
                FullContactListViewModel add = new FullContactListViewModel
                {
                    Person  = person,
                    PhoneNumbers = phoneNumber
                };

                fullContactViewModel.Add(add);
            }

            return fullContactViewModel;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ContactList.Data;
using ContactList.Models;
using Microsoft.EntityFrameworkCore;
using ContactList.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ContactList.Services
{
    public class ContactListService : IContactListService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ContactListService(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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

        public async Task<MemoryStream> BuildCsvString(AllContactListViewModel list)
        {
             string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"ContactList.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("ContactList");

                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("Full Name");
                row.CreateCell(1).SetCellValue("Nick Name");
                row.CreateCell(2).SetCellValue("Phone");
                row.CreateCell(3).SetCellValue("Address");
                row.CreateCell(4).SetCellValue("Website");
                row.CreateCell(5).SetCellValue("Birth Date");

                var i = 1;
                foreach(Person person in list.contactList)
                {
                    string phones = "";
                    foreach (var phone in person.Phones)
                    {
                        phones += phone.Phone.ToString()+" ";
                    }

                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(person.FullName);
                    row.CreateCell(1).SetCellValue(person.NickName);
                    row.CreateCell(2).SetCellValue(phones);
                    row.CreateCell(3).SetCellValue(person.Address);
                    row.CreateCell(4).SetCellValue(person.Website);
                    row.CreateCell(5).SetCellValue(person.DateOfBirth.Date.ToString("yyyy/MM/dd"));

                    i++;
                }
                
                workbook.Write(fs);


                using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

            }

            return memory;
        }
    }
}
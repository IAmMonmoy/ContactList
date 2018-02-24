using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ContactList.Models;

namespace ContactList.ViewModels
{
    public class AllContactListViewModel
    {
        public IEnumerable<Person> contactList { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ContactList.Models;

namespace ContactList.ViewModels
{
    public class FullContactListViewModel
    {
        public Person Person { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}
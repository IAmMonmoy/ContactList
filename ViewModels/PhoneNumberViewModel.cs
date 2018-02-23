using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.ViewModels
{
    public class PhoneNumberViewModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
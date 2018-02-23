using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.ViewModels
{
    public class PhoneNumber
    {
        [Required]
        public string Phone { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.ViewModels
{
    public class PhoneNumber
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
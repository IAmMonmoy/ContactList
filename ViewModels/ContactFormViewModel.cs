using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.ViewModels
{
    public class ContactFormViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string NickName { get; set; }

        public string Address { get; set; }
  
        public string Website { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

    }
}
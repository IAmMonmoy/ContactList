using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.ViewModels
{
    public class ContactFormViewModel
    {
        [Required]
        [StringLength(50,MinimumLength = 5)]
        public string FullName { get; set; }

        [Required]
        [StringLength(10,MinimumLength = 3)]
        public string NickName { get; set; }

        public string Address { get; set; }
  
        public string Website { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ContactList.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(10,MinimumLength = 3)]
        public string NickName { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 5)]
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<PhoneNumber> Phones { get; set; }

    }
}
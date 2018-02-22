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
        public string NickName { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<PhoneNumber> Phones { get; set; }

    }
}
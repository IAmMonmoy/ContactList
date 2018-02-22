using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class PhoneNumber
    {
        public Guid Id { get; set; }

        public string Phone { get; set; }

        public Person Person { get; set; }
        [Required]
        public Guid PersonId { get; set; }
    }
}
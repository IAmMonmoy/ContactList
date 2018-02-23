using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class PhoneNumber
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, MinimumLength = 5)]
        public string Phone { get; set; }

        public Person Person { get; set; }
        [Required]
        public Guid PersonId { get; set; }
    }
}
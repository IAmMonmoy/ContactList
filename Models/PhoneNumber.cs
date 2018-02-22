using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ContactList.Models
{
    public class PhoneNumber
    {
        public Guid Id { get; set; }

        [Required]
        public string Phone { get; set; }
      
        public Person Person { get; set; }

        [Required]
        public Guid PersonId { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityApi.Models
{
    public class User
    {

        [Key]
        public Guid IdUser { get; set; }

        [StringLength(100),Required]
        public string NameUser { get; set; }

        [StringLength(255), Required]
        public string PasswordUser { get; set; }

        [StringLength(128), Required, EmailAddress]
        public string EmailUser { get; set; }

        public bool Active { get; set; }
        public bool Online { get; set; }
        public byte[] Salt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

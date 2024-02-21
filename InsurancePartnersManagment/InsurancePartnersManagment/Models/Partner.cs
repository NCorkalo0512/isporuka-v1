using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InsurancePartnersManagment.Models
{
    public class Partner
    {
        public int IdPartner { get; set; }

        [Required, MinLength(2), MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(255)]
        public string LastName { get; set; }
        public string Address { get; set; }

        [Required, MinLength(10), MaxLength(20)]
        public string PartnerNumber { get; set; }

        [StringLength(11, MinimumLength = 11)]
        public string CroatianPIN { get; set; }

        [Required]
        public int PartnerTypeId { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        [Required, EmailAddress, MaxLength(255)]
        public string CreateByUser { get; set; }
        [Required]
        public bool IsForeign { get; set; }
        [MinLength(10), MaxLength(20)]
        public string ExternalCode { get; set; }
        [Required]
        public char Gender { get; set; }
    }
}
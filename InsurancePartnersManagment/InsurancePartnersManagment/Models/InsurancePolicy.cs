using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InsurancePartnersManagment.Models
{
    public class InsurancePolicy
    {
        public int IdPolicy { get; set; }
        public int PartnerId { get; set; }

        [Required, MinLength(10), MaxLength(15)]
        public string PolicyNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public virtual Partner Partner { get; set; }
    }
}
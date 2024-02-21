using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsurancePartnersManagment.Models
{
    public class PartnerListDTO
    {
        public int IdPartner { get; set; }
        public string PartnerNumber { get; set; }
        public int PartnerTypeId { get; set; }
        public bool IsForeign { get; set; }
        public char Gender { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
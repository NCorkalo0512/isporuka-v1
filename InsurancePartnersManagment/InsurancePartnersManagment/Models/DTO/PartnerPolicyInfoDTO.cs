using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsurancePartnersManagment.Models.DTO
{
    public class PartnerPolicyInfoDTO
    {
        public int IdPartner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PolicyCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
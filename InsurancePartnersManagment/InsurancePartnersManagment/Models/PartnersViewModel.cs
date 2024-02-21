using InsurancePartnersManagment.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsurancePartnersManagment.Models
{
    public class PartnersViewModel
    {
        public IEnumerable<PartnerListDTO> Partners { get; set; }
        public IEnumerable<PartnerPolicyInfoDTO> PartnerPolicyInfos { get; set; }
    }
}
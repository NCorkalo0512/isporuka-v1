using InsurancePartnersManagment.Models;
using InsurancePartnersManagment.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsurancePartnersManagment.Repositories
{
    public interface IPartnerRepository
    {
       Task<Partner> createPartner(Partner partner);
        Task<IEnumerable<PartnerListDTO>> GetAllPartners();
        Task<Partner> GetPartnerById(int id);
        Task<bool> UpdatePartner (Partner partner);
        Task<bool> DeletePartner(int id);
        Task<IEnumerable<PartnerPolicyInfoDTO>> GetPartnerPolicyInfo();
    }
}

using InsurancePartnersManagment.Models;
using InsurancePartnersManagment.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsurancePartnersManagment.Repositories
{
    public interface IInsurancePolicyRepository
    {
        Task<InsurancePolicy> createInsurancePolicy(InsurancePolicy insurancePolicy);
        Task<IEnumerable<InsurancePolicy>>GetAllInsurancePolicies();
        Task<InsurancePolicy> GetInsurancePolicyById(int id);

        Task<bool> UpdatePolicy(InsurancePolicy insurancePolicy);
        Task<bool> DeletePolicy(int id);

     
    }
}
using InsurancePartnersManagment.Repositories;
using System.Web.Mvc;
using System.Threading.Tasks;
using InsurancePartnersManagment.Models;
using System;

namespace InsurancePartnersManagment.Controllers
{
    public class PolicyController : Controller
    {
        private readonly IInsurancePolicyRepository _policyRepository;

        public PolicyController(IInsurancePolicyRepository policyRepository)
            =>_policyRepository = policyRepository;

        // GET: Policy
        public async Task<ActionResult>Index()
        {
            var policies= await _policyRepository.GetAllInsurancePolicies();
            return View(policies);
        }

        // GET: Policy/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Policy/Create
        [HttpPost]
        public async Task<ActionResult> Create(int partnerId, string policyNumber, decimal amount)
        {
            try
            {
                var insurancePolicy = new InsurancePolicy
                {
                    PartnerId = partnerId,
                    PolicyNumber = policyNumber,
                    Amount = amount
                };

                var createdPolicy = await _policyRepository.createInsurancePolicy(insurancePolicy);

                return Json(createdPolicy);
            }
            catch (Exception ex)
            {
               
                return Json(new { error = ex.Message });
            }
        }

        // GET: Policy/Details/
        public async Task<ActionResult> Details(int id)
        {
            var policy = await _policyRepository.GetInsurancePolicyById(id);
            if (policy == null)
            {
                throw new Exception("Policy not found!");
            }
            return View(policy);
        }

        // GET: Policy/Edit/
        public async Task<ActionResult> Edit(int id)
        {
            var policy = await _policyRepository.GetInsurancePolicyById(id);
            if (policy == null)
            {
                throw new Exception("Partner not found!");
            }
            return View(policy);
        }

        // POST: Policy/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, InsurancePolicy policy)
        {
            if (id != policy.IdPolicy)
            {
                throw new Exception("Partner not found!");
            }

            if (ModelState.IsValid)
            {
                await _policyRepository.UpdatePolicy(policy);
                return RedirectToAction(nameof(Index));
            }
            return View(policy);
        }

        // GET: Policy/Delete/
        public async Task<ActionResult> Delete(int id)
        {
            var policy = await _policyRepository.GetInsurancePolicyById(id);
            if (policy == null)
            {
                throw new Exception("Partner not found!");
            }
            return View(policy);
        }

        // POST: Partners/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _policyRepository.DeletePolicy(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

using InsurancePartnersManagment.Models;
using InsurancePartnersManagment.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InsurancePartnersManagment.Controllers
{
    public class PartnersController : Controller
    {
        private readonly    IPartnerRepository _partnerRepository;

        public PartnersController(IPartnerRepository partnerRepository)
            => _partnerRepository = partnerRepository;


        // GET: Partners
        public async Task<ActionResult> Index()
        {
            var partners = await _partnerRepository.GetAllPartners();
            var partnersWithPolicyInfo = await _partnerRepository.GetPartnerPolicyInfo();

            var viewModel = new PartnersViewModel
            {
                Partners = partners,
                PartnerPolicyInfos = partnersWithPolicyInfo
            };

            return View(viewModel);
        }

        // GET: Partner/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Partners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Partner model)
        {
            if (ModelState.IsValid)
            {
                await _partnerRepository.createPartner(model);
                TempData["CreatedPartnerId"] = model.IdPartner;
                return RedirectToAction("Index"); 
            }
            return View(model); 
        }

        // GET: Partners/Details/
        public async Task<ActionResult> Details(int id)
        {
            var partner = await _partnerRepository.GetPartnerById(id);
            if (partner == null)
            {
                return HttpNotFound("Partner not found!");
            }
        
            var result = new
            {
                FullName = partner.FirstName + " " + partner.LastName,
                partner.Address,
                partner.PartnerNumber,
                partner.PartnerTypeId,
                IsForeign = partner.IsForeign ? "Yes" : "No", 
                Gender = partner.Gender.ToString(),
                CreatedBy = partner.CreateByUser, 
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: Partners/Edit/
        public async Task<ActionResult> Edit(int id)
        {
            var partner = await _partnerRepository.GetPartnerById(id);
            if (partner == null)
            {
                throw new Exception("Partner not found!");
            }
            return View(partner);
        }

        // POST: Partners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Partner partner)
        {
            if (id != partner.IdPartner)
            {
                throw new Exception("Partner not found!");
            }

            if (ModelState.IsValid)
            {
                await _partnerRepository.UpdatePartner(partner);
                return RedirectToAction(nameof(Index));
            }
            return View(partner);
        }

        // GET: Partners/Delete/
        public async Task<ActionResult> Delete(int id)
        {
            var partner = await _partnerRepository.GetPartnerById(id);
            if (partner == null)
            {
                throw new Exception("Partner not found!");
            }
            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _partnerRepository.DeletePartner(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
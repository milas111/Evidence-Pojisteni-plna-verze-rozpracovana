using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidencePojisteni.Data;
using EvidencePojisteni.Models;

namespace EvidencePojisteni.Controllers
{
    public class AssignedInsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignedInsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssignedInsurances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AssignedInsurance.Include(a => a.Insurance).Include(a => a.Insured);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AssignedInsurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedInsurance = await _context.AssignedInsurance
                .Include(a => a.Insurance)
                .Include(a => a.Insured)
                .FirstOrDefaultAsync(m => m.AssignedInsuranceId == id);
            if (assignedInsurance == null)
            {
                return NotFound();
            }

            return View(assignedInsurance);
        }

        // GET: AssignedInsurances/Create
        public IActionResult Create(int insuredId)
        {
            var name = _context.Insured.Where(n => n.InsuredId == insuredId).FirstOrDefault();
            ViewBag.Name = name.FirstName + " " + name.SurName;
            ViewBag.InsuredId = insuredId;
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "InsuranceId", "InsuranceName");
            ViewData["InsuredId"] = new SelectList(_context.Insured, "InsuredId", "FirstName", insuredId);
            return View();
        }

        // POST: AssignedInsurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int insuredId, [Bind("AssignedInsuranceId,InsuranceId,InsuredId,Value,Issue,ValidFrom,ValidTo")] AssignedInsurance assignedInsurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignedInsurance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Insureds", new { id = insuredId });
            }
            var name = _context.Insured.Where(n => n.InsuredId == insuredId).FirstOrDefault();
            ViewBag.Name = name.FirstName + " " + name.SurName;
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "InsuranceId", "InsuranceName", assignedInsurance.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insured, "InsuredId", "FirstName", assignedInsurance.InsuredId);
            return View(assignedInsurance);
        }

        // GET: AssignedInsurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedInsurance = await _context.AssignedInsurance.FindAsync(id);
            if (assignedInsurance == null)
            {
                return NotFound();
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "InsuranceId", "InsuranceName", assignedInsurance.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insured, "InsuredId", "City", assignedInsurance.InsuredId);
            return View(assignedInsurance);
        }

        // POST: AssignedInsurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignedInsuranceId,InsuranceId,InsuredId,Value,Issue,ValidFrom,ValidTo")] AssignedInsurance assignedInsurance)
        {
            if (id != assignedInsurance.AssignedInsuranceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignedInsurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignedInsuranceExists(assignedInsurance.AssignedInsuranceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Insureds", new { id = assignedInsurance.InsuredId });
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "InsuranceId", "InsuranceName", assignedInsurance.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insured, "InsuredId", "City", assignedInsurance.InsuredId);
            return View(assignedInsurance);
        }

        // GET: AssignedInsurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedInsurance = await _context.AssignedInsurance
                .Include(a => a.Insurance)
                .Include(a => a.Insured)
                .FirstOrDefaultAsync(m => m.AssignedInsuranceId == id);
            if (assignedInsurance == null)
            {
                return NotFound();
            }

            return View(assignedInsurance);
        }

        // POST: AssignedInsurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignedInsurance = await _context.AssignedInsurance.FindAsync(id);
            _context.AssignedInsurance.Remove(assignedInsurance);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Insureds", new { id = assignedInsurance.InsuredId });
        }

        private bool AssignedInsuranceExists(int id)
        {
            return _context.AssignedInsurance.Any(e => e.AssignedInsuranceId == id);
        }
    }
}

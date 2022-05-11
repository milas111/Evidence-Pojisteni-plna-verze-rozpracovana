using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvidencePojisteni.Data;
using EvidencePojisteni.Models;
using X.PagedList;

namespace EvidencePojisteni.Controllers
{
    public class InsuredsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int pageSize = 3;

        public InsuredsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insureds
        public async Task<IActionResult> Index(int? page)
        {
            var insuredList = _context.Insured.ToPagedList(page ?? 1, pageSize);
            ViewBag.InsuredList = insuredList;
            return View(await insuredList.ToListAsync());
        }

        // GET: Insureds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await _context.Insured
                .FirstOrDefaultAsync(m => m.InsuredId == id);
            if (insured == null)
            {
                return NotFound();
            }

            return View(insured);
        }

        // GET: Insureds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insureds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuredId,FirstName,SurName,Email,Phone,Street,City,Zip")] Insured insured)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insured);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insured);
        }

        // GET: Insureds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await _context.Insured.FindAsync(id);
            if (insured == null)
            {
                return NotFound();
            }
            return View(insured);
        }

        // POST: Insureds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuredId,FirstName,SurName,Email,Phone,Street,City,Zip")] Insured insured)
        {
            if (id != insured.InsuredId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insured);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuredExists(insured.InsuredId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(insured);
        }

        // GET: Insureds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await _context.Insured
                .FirstOrDefaultAsync(m => m.InsuredId == id);
            if (insured == null)
            {
                return NotFound();
            }

            return View(insured);
        }

        // POST: Insureds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insured = await _context.Insured.FindAsync(id);
            _context.Insured.Remove(insured);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuredExists(int id)
        {
            return _context.Insured.Any(e => e.InsuredId == id);
        }
    }
}

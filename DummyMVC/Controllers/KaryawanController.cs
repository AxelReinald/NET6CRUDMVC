using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DummyMVC.Data;
using DummyMVC.Models;

namespace DummyMVC.Controllers
{
    public class KaryawanController : Controller
    {
        private readonly CompanyContext _context;

        public KaryawanController(CompanyContext context)
        {
            _context = context;
        }

        private List<SelectListItem> GetPageSizes(int selectedPageSize = 5)
        {
            var pagesSizes = new List<SelectListItem>();

            if (selectedPageSize == 5)
                pagesSizes.Add(new SelectListItem("5", "5", true));
            else
                pagesSizes.Add(new SelectListItem("5", "5"));

            for (int lp = 10; lp <= 100; lp += 10)
            {
                if (lp == selectedPageSize)
                { pagesSizes.Add(new SelectListItem(lp.ToString(), lp.ToString(), true)); }
                else
                    pagesSizes.Add(new SelectListItem(lp.ToString(), lp.ToString()));
            }

            return pagesSizes;
        }

        // GET: Karyawan
        public IActionResult Index(string SearchText = "", int pg = 1, int pageSize = 5)
        {
            var query = _context.Karyawan.AsQueryable();

            if (pg < 1) pg = 1;

            if (SearchText != "" && SearchText != null)
            {
                query = _context.Karyawan
                .Where(p => p.NamaKaryawan.Contains(SearchText))
                .AsQueryable();
            }

            int recsCount = query.Count();

            int recSkip = (pg - 1) * pageSize;

            List<Karyawan> retKar = query.Skip(recSkip).Take(pageSize).ToList();

            Pager SearchPager = new Pager(recsCount, pg, pageSize) { Action = "Index", Controller = "Karyawan", SearchText = SearchText };
            ViewBag.SearchPager = SearchPager;

            this.ViewBag.PageSizes = GetPageSizes(pageSize);

            return View(retKar.ToList());
        }

        // GET: Karyawan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Karyawans == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karyawan == null)
            {
                return NotFound();
            }

            return View(karyawan);
        }

        // GET: Karyawan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Karyawan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NoKaryawan,NamaKaryawan,TanggalLahir,Alamat,Email,Umur")] Karyawan karyawan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karyawan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            string errors = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            ModelState.AddModelError("", errors);

            return View(karyawan);
        }

        // GET: Karyawan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Karyawans == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawans.FindAsync(id);
            if (karyawan == null)
            {
                return NotFound();
            }
            return View(karyawan);
        }

        // POST: Karyawan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NoKaryawan,NamaKaryawan,TanggalLahir,Alamat,Email,Umur")] Karyawan karyawan)
        {
            if (id != karyawan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karyawan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KaryawanExists(karyawan.Id))
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
            return View(karyawan);
        }

        // GET: Karyawan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Karyawans == null)
            {
                return NotFound();
            }

            var karyawan = await _context.Karyawans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karyawan == null)
            {
                return NotFound();
            }

            return View(karyawan);
        }

        // POST: Karyawan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Karyawans == null)
            {
                return Problem("Entity set 'CompanyContext.Karyawans'  is null.");
            }
            var karyawan = await _context.Karyawans.FindAsync(id);
            if (karyawan != null)
            {
                _context.Karyawans.Remove(karyawan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KaryawanExists(int id)
        {
          return _context.Karyawans.Any(e => e.Id == id);
        }
    }
}

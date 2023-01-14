using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inviWebApp.Models;

namespace inviWebApp.Controllers
{
    public class ViviendumsController : Controller
    {
        private readonly inviContext _context;

        public ViviendumsController(inviContext context)
        {
            _context = context;
        }

        // GET: Viviendums
        public async Task<IActionResult> Index()
        {
            var inviContext = _context.Vivienda.Include(v => v.AlcaldiaNavigation);
            return View(await inviContext.ToListAsync());
        }

        // GET: Viviendums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vivienda == null)
            {
                return NotFound();
            }

            var viviendum = await _context.Vivienda
                .Include(v => v.AlcaldiaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viviendum == null)
            {
                return NotFound();
            }

            return View(viviendum);
        }

        // GET: Viviendums/Create
        public IActionResult Create()
        {
            ViewData["Alcaldia"] = new SelectList(_context.Alcaldia, "Nombre", "Nombre");
            return View();
        }

        // POST: Viviendums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dirreccion,Tipo,Alcaldia")] Viviendum viviendum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viviendum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Alcaldia"] = new SelectList(_context.Alcaldia, "Nombre", "Nombre", viviendum.Alcaldia);
            return View(viviendum);
        }

        // GET: Viviendums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vivienda == null)
            {
                return NotFound();
            }

            var viviendum = await _context.Vivienda.FindAsync(id);
            if (viviendum == null)
            {
                return NotFound();
            }
            ViewData["Alcaldia"] = new SelectList(_context.Alcaldia, "Nombre", "Nombre", viviendum.Alcaldia);
            return View(viviendum);
        }

        // POST: Viviendums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dirreccion,Tipo,Alcaldia")] Viviendum viviendum)
        {
            if (id != viviendum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viviendum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViviendumExists(viviendum.Id))
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
            ViewData["Alcaldia"] = new SelectList(_context.Alcaldia, "Nombre", "Nombre", viviendum.Alcaldia);
            return View(viviendum);
        }

        // GET: Viviendums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vivienda == null)
            {
                return NotFound();
            }

            var viviendum = await _context.Vivienda
                .Include(v => v.AlcaldiaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viviendum == null)
            {
                return NotFound();
            }

            return View(viviendum);
        }

        // POST: Viviendums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vivienda == null)
            {
                return Problem("Entity set 'inviContext.Vivienda'  is null.");
            }
            var viviendum = await _context.Vivienda.FindAsync(id);
            if (viviendum != null)
            {
                _context.Vivienda.Remove(viviendum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViviendumExists(int id)
        {
          return _context.Vivienda.Any(e => e.Id == id);
        }
    }
}

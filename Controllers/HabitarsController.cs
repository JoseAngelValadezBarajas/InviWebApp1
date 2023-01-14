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
    public class HabitarsController : Controller
    {
        private readonly inviContext _context;

        public HabitarsController(inviContext context)
        {
            _context = context;
        }

        // GET: Habitars
        public async Task<IActionResult> Index()
        {
            var inviContext = _context.Habitars.Include(h => h.CurpNavigation).Include(h => h.ViviendaNavigation);
            return View(await inviContext.ToListAsync());
        }

        // GET: Habitars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Habitars == null)
            {
                return NotFound();
            }

            var habitar = await _context.Habitars
                .Include(h => h.CurpNavigation)
                .Include(h => h.ViviendaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitar == null)
            {
                return NotFound();
            }

            return View(habitar);
        }

        // GET: Habitars/Create
        public IActionResult Create()
        {
            ViewData["Curp"] = new SelectList(_context.Propietarios, "Curp", "Curp");
            ViewData["Vivienda"] = new SelectList(_context.Vivienda, "Id", "Id");
            return View();
        }

        // POST: Habitars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Curp,Vivienda")] Habitar habitar)
        {
            String CurpTemp = habitar.Curp;
            

            if (ModelState.IsValid == false)
            {
                _context.Add(habitar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Curp"] = new SelectList(_context.Propietarios, "Curp", "Curp", habitar.Curp);
            ViewData["Vivienda"] = new SelectList(_context.Vivienda, "Id", "Id", habitar.Vivienda);
            return View(habitar);
        }

        // GET: Habitars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habitars == null)
            {
                return NotFound();
            }

            var habitar = await _context.Habitars.FindAsync(id);
            if (habitar == null)
            {
                return NotFound();
            }
            ViewData["Curp"] = new SelectList(_context.Propietarios, "Curp", "Curp", habitar.Curp);
            ViewData["Vivienda"] = new SelectList(_context.Vivienda, "Id", "Id", habitar.Vivienda);
            return View(habitar);
        }

        // POST: Habitars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Curp,Vivienda")] Habitar habitar)
        {
            if (id != habitar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid == false)
            {
                try
                {
                    _context.Update(habitar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitarExists(habitar.Id))
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
            ViewData["Curp"] = new SelectList(_context.Propietarios, "Curp", "Curp", habitar.Curp);
            ViewData["Vivienda"] = new SelectList(_context.Vivienda, "Id", "Id", habitar.Vivienda);
            return View(habitar);
        }

        // GET: Habitars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Habitars == null)
            {
                return NotFound();
            }

            var habitar = await _context.Habitars
                .Include(h => h.CurpNavigation)
                .Include(h => h.ViviendaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitar == null)
            {
                return NotFound();
            }

            return View(habitar);
        }

        // POST: Habitars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Habitars == null)
            {
                return Problem("Entity set 'inviContext.Habitars'  is null.");
            }
            var habitar = await _context.Habitars.FindAsync(id);
            if (habitar != null)
            {
                _context.Habitars.Remove(habitar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitarExists(int id)
        {
          return _context.Habitars.Any(e => e.Id == id);
        }
    }
}

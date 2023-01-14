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

    public class AlcaldiumsController : Controller
    {
        private readonly inviContext _context;

        public AlcaldiumsController(inviContext context)
        {
            _context = context;
        }

        // GET: Alcaldiums
        public async Task<IActionResult> Index()
        {
              return View(await _context.Alcaldia.ToListAsync());
        }

        // GET: Alcaldiums/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Alcaldia == null)
            {
                return NotFound();
            }

            var alcaldium = await _context.Alcaldia
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (alcaldium == null)
            {
                return NotFound();
            }

            return View(alcaldium);
        }

        // GET: Alcaldiums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alcaldiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Clave,Nombre,Estado,Poblacion")] Alcaldium alcaldium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alcaldium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alcaldium);
        }

        // GET: Alcaldiums/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Alcaldia == null)
            {
                return NotFound();
            }

            var alcaldium = await _context.Alcaldia.FindAsync(id);
            if (alcaldium == null)
            {
                return NotFound();
            }
            return View(alcaldium);
        }

        // POST: Alcaldiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Clave,Nombre,Estado,Poblacion")] Alcaldium alcaldium)
        {
            if (id != alcaldium.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alcaldium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlcaldiumExists(alcaldium.Nombre))
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
            return View(alcaldium);
        }

        // GET: Alcaldiums/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Alcaldia == null)
            {
                return NotFound();
            }

            var alcaldium = await _context.Alcaldia
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (alcaldium == null)
            {
                return NotFound();
            }

            return View(alcaldium);
        }

        // POST: Alcaldiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Alcaldia == null)
            {
                return Problem("Entity set 'inviContext.Alcaldia'  is null.");
            }
            var alcaldium = await _context.Alcaldia.FindAsync(id);
            if (alcaldium != null)
            {
                _context.Alcaldia.Remove(alcaldium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlcaldiumExists(string id)
        {
          return _context.Alcaldia.Any(e => e.Nombre == id);
        }
    }
}

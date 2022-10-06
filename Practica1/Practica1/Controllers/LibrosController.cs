using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practica1;
using Practica1.Models;

namespace Practica1.Controllers
{
    public class LibrosController : Controller
    {
        private readonly appDBcontext _context;

        public LibrosController(appDBcontext context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            var appDBcontext = _context.libros.Include(l => l.autor).Include(l => l.genero);
            return View(await appDBcontext.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.libros
                .Include(l => l.autor)
                .Include(l => l.genero)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["autorId"] = new SelectList(_context.autores, "ID", "apellido");
            ViewData["generoId"] = new SelectList(_context.generos, "id", "id");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,titulo,resumen,fechapubli,fotoportada,generoId,autorId")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["autorId"] = new SelectList(_context.autores, "ID", "apellido", libro.autorId);
            ViewData["generoId"] = new SelectList(_context.generos, "id", "id", libro.generoId);
            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["autorId"] = new SelectList(_context.autores, "ID", "apellido", libro.autorId);
            ViewData["generoId"] = new SelectList(_context.generos, "id", "id", libro.generoId);
            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,titulo,resumen,fechapubli,fotoportada,generoId,autorId")] Libro libro)
        {
            if (id != libro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.ID))
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
            ViewData["autorId"] = new SelectList(_context.autores, "ID", "apellido", libro.autorId);
            ViewData["generoId"] = new SelectList(_context.generos, "id", "id", libro.generoId);
            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.libros
                .Include(l => l.autor)
                .Include(l => l.genero)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.libros.FindAsync(id);
            _context.libros.Remove(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.libros.Any(e => e.ID == id);
        }
    }
}

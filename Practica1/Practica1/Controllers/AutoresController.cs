using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practica1.Models;

namespace Practica1.Controllers
{
    public class AutoresController : Controller
    {
        private readonly appDBcontext _context;
        private readonly IWebHostEnvironment env;

        public AutoresController(appDBcontext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Autores
        public async Task<IActionResult> Index()
        {
            return View(await _context.autores.ToListAsync());
        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,apellido,nombre,biografia,foto")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivofoto = archivos[0];

                    if (archivofoto.Length > 0)
                    {
                        var pathDestino = Path.Combine(env.WebRootPath, "images");
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivofoto.FileName);
                        var rutaDestino = Path.Combine(pathDestino, archivoDestino);

                        using (var filestream = new FileStream(rutaDestino, FileMode.Create))
                        {
                            archivofoto.CopyTo(filestream);
                            autor.foto = archivoDestino;
                        }
                    }
                }
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,apellido,nombre,biografia,foto")] Autor autor)
        {
            if (id != autor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivofoto = archivos[0];

                    if (archivofoto.Length > 0)
                    {
                        var pathDestino = Path.Combine(env.WebRootPath, "images");
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivofoto.FileName);
                        var rutaDestino = Path.Combine(pathDestino, archivoDestino);
                        string fotoAnterior = Path.Combine(pathDestino, autor.foto);
                        if (string.IsNullOrEmpty(autor.foto))
                        {
                            if (System.IO.File.Exists(fotoAnterior))
                                System.IO.File.Delete(fotoAnterior);
                        }

                        using (var filestream = new FileStream(rutaDestino, FileMode.Create))
                        {
                            archivofoto.CopyTo(filestream);
                            autor.foto = archivoDestino;
                        }
                    }
                }
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.ID))
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
            return View(autor);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.autores.FindAsync(id);
            _context.autores.Remove(autor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
            return _context.autores.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class CVsController : Controller
    {
        private readonly CVContext _context;

        public CVsController(CVContext context)
        {
            _context = context;
        }

        // GET: CVs
        public async Task<IActionResult> Index(string searchString)
        {
            var cv = from m in _context.Congviec
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                cv = cv.Where(s => s.Title.Contains(searchString));
            }

            return View(await cv.ToListAsync());
        }
   
        // GET: CVs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.Congviec
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // GET: CVs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CVs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenCV,NoiDungCV,Title")] CV cV)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cV);
        }

        // GET: CVs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.Congviec.FindAsync(id);
            if (cV == null)
            {
                return NotFound();
            }
            return View(cV);
        }

        // POST: CVs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenCV,NoiDungCV,Title")] CV cV)
        {
            if (id != cV.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CVExists(cV.ID))
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
            return View(cV);
        }

        // GET: CVs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.Congviec
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // POST: CVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cV = await _context.Congviec.FindAsync(id);
            _context.Congviec.Remove(cV);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CVExists(int id)
        {
            return _context.Congviec.Any(e => e.ID == id);
        }
    }
}

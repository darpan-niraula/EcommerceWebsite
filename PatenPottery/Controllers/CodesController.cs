using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatenPottery.Models;

namespace PatenPottery.Controllers
{
    [Authorize]
    public class CodesController : Controller
    {
        private readonly PatenPotteryContext _context;

        public CodesController(PatenPotteryContext context)
        {
            _context = context;
        }

        // GET: Codes
        public async Task<IActionResult> Index()
        {
              return _context.Codes != null ? 
                          View(await _context.Codes.ToListAsync()) :
                          Problem("Entity set 'PatenPotteryContext.Codes'  is null.");
        }

        // GET: Codes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Codes == null)
            {
                return NotFound();
            }

            var code = await _context.Codes
                .FirstOrDefaultAsync(m => m.CodeId == id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        // GET: Codes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Codes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeId,Value,Description,ParentCodeId")] Code code)
        {
            if (ModelState.IsValid)
            {
                _context.Add(code);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(code);
        }

        // GET: Codes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Codes == null)
            {
                return NotFound();
            }

            var code = await _context.Codes.FindAsync(id);
            if (code == null)
            {
                return NotFound();
            }
            return View(code);
        }

        // POST: Codes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodeId,Value,Description,ParentCodeId")] Code code)
        {
            if (id != code.CodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(code);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodeExists(code.CodeId))
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
            return View(code);
        }

        // GET: Codes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Codes == null)
            {
                return NotFound();
            }

            var code = await _context.Codes
                .FirstOrDefaultAsync(m => m.CodeId == id);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        // POST: Codes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Codes == null)
            {
                return Problem("Entity set 'PatenPotteryContext.Codes'  is null.");
            }
            var code = await _context.Codes.FindAsync(id);
            if (code != null)
            {
                _context.Codes.Remove(code);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CodeExists(int id)
        {
          return (_context.Codes?.Any(e => e.CodeId == id)).GetValueOrDefault();
        }
    }
}

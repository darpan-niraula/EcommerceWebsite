﻿using Microsoft.AspNetCore.Authorization;
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
            var parentCodes = await _context.Codes.Where(c => c.ParentCodeId == null).ToListAsync();
            ViewBag.ParentCodes = new SelectList(parentCodes, "CodeId", "Value");
            return View();
        }

        public async Task<JsonResult> GetChildCodes(int parentId)
        {
            var childCodes = await _context.Codes.Where(c => c.ParentCodeId == parentId).ToListAsync();
            return Json(childCodes);
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

            // Fetch the existing ParentCodeId values
            var existingParentCodeIds = _context.Codes
                                             .Where(c => c.ParentCodeId.HasValue)
                                             .Select(c => c.ParentCodeId.Value)
                                             .ToList();

            // Determine available numbers from 1 to 20 that aren't already assigned
            var availableParentCodeIds = Enumerable.Range(1, 20).Except(existingParentCodeIds).ToList();

            // Pass the availableParentCodeIds to the view using ViewBag
            ViewBag.AvailableParentCodeIds = availableParentCodeIds;

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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = await _context.Codes.FindAsync(id);
            if (code == null)
            {
                return NotFound();
            }
            var parentCodes = await _context.Codes.Where(c => c.ParentCodeId == null).ToListAsync();
            ViewBag.ParentCodes = new SelectList(parentCodes, "CodeId", "Description", code.ParentCodeId);

            return PartialView("_EditPartial", code);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CodeId,Value,Description,ParentCodeId")] Code code)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (code.CodeId > 0)
                    {
                        _context.Update(code);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Add(code);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodeExists(code.CodeId))
                    {
                        return Json(new { success = false, message = "Code not found." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Concurrency error occurred." });
                    }
                }
                return Json(new { success = true, message = "Code updated successfully." });
            }
            return Json(new { success = false, message = "Invalid model state." });
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

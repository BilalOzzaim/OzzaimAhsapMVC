using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OzzaimAhsap.Models;

namespace OzzaimAhsapMVC.Areas.Admin.Controllers;

[Area("Admin"), Authorize()]
public class GeridonuslersController : Controller
{
    private readonly AhsapContext _context;

    public GeridonuslersController()
    {
        _context = new AhsapContext();
    }

    // GET: Admin/Geridonuslers
    public async Task<IActionResult> Index()
    {
        return View(await _context.Geridonusler.ToListAsync());
    }

    // GET: Admin/Geridonuslers/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var geridonusler = await _context.Geridonusler
            .FirstOrDefaultAsync(m => m.id == id);
        if (geridonusler == null)
        {
            return NotFound();
        }

        return View(geridonusler);
    }

    // GET: Admin/Geridonuslers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Geridonuslers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,ad_soyad,telefon,email,mesaj")] Geridonusler geridonusler)
    {
        if (ModelState.IsValid)
        {
            _context.Add(geridonusler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(geridonusler);
    }

    // GET: Admin/Geridonuslers/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var geridonusler = await _context.Geridonusler.FindAsync(id);
        if (geridonusler == null)
        {
            return NotFound();
        }
        return View(geridonusler);
    }

    // POST: Admin/Geridonuslers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("id,ad_soyad,telefon,email,mesaj")] Geridonusler geridonusler)
    {
        if (id != geridonusler.id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(geridonusler);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeridonuslerExists(geridonusler.id))
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
        return View(geridonusler);
    }

    // GET: Admin/Geridonuslers/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var geridonusler = await _context.Geridonusler
            .FirstOrDefaultAsync(m => m.id == id);
        if (geridonusler == null)
        {
            return NotFound();
        }

        return View(geridonusler);
    }

    // POST: Admin/Geridonuslers/Delete/5
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var geridonusler = await _context.Geridonusler.FindAsync(id);
        if (geridonusler != null)
        {
            _context.Geridonusler.Remove(geridonusler);
            await _context.SaveChangesAsync();
        }

      return RedirectToAction(nameof(Index));
    }

    private bool GeridonuslerExists(int id)
    {
        return _context.Geridonusler.Any(e => e.id == id);
    }
}

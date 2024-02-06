using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaExamHAB.Data;
using AgendaExamHAB.Models;

namespace AgendaExamHAB.Controllers
{
    public class PersonContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonContacts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PersonContacts.Include(p => p.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PersonContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personContact = await _context.PersonContacts
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (personContact == null)
            {
                return NotFound();
            }

            return View(personContact);
        }

        // GET: PersonContacts/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id");
            return View();
        }

        // POST: PersonContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,PersonId,Type,ContactName,Value,Created,CreatedBy,Modified,ModifiedBy")] PersonContact personContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", personContact.PersonId);
            return View(personContact);
        }

        // GET: PersonContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personContact = await _context.PersonContacts.FindAsync(id);
            if (personContact == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", personContact.PersonId);
            return View(personContact);
        }

        // POST: PersonContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,PersonId,Type,ContactName,Value,Created,CreatedBy,Modified,ModifiedBy")] PersonContact personContact)
        {
            if (id != personContact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonContactExists(personContact.ContactId))
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
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", personContact.PersonId);
            return View(personContact);
        }

        // GET: PersonContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personContact = await _context.PersonContacts
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (personContact == null)
            {
                return NotFound();
            }

            return View(personContact);
        }

        // POST: PersonContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personContact = await _context.PersonContacts.FindAsync(id);
            if (personContact != null)
            {
                _context.PersonContacts.Remove(personContact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonContactExists(int id)
        {
            return _context.PersonContacts.Any(e => e.ContactId == id);
        }
    }
}

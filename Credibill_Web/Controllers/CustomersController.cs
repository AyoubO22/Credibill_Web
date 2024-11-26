using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrediBill_Web.Data;
using CrediBill_Web.Models;

namespace CrediBill_Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var customer = await _context.Customers
        .Include(c => c.Invoices) // Charger les factures associées
        .FirstOrDefaultAsync(m => m.Id == id);

    if (customer == null)
    {
        return NotFound();
    }

    return View(customer);
}

        // GET: Customers/Create
        public IActionResult Create() => View();

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var customer = await _context.Customers
        .Include(c => c.Invoices) // Charger les factures associées
        .FirstOrDefaultAsync(c => c.Id == id);

    if (customer == null)
    {
        return NotFound();
    }

    // Charger toutes les factures pour le dropdown
    ViewBag.AllInvoices = await _context.Invoices.ToListAsync();

    return View(customer);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Address,CreatedDate,Deleted,Invoices")] Customer customer)
{
    if (id != customer.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            // Gérer l'association des factures liées
            var selectedInvoices = Request.Form["Invoices"].ToString().Split(',').Select(int.Parse).ToList();
            customer.Invoices = await _context.Invoices
                .Where(i => selectedInvoices.Contains(i.Id))
                .ToListAsync();

            _context.Update(customer);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(customer.Id))
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

    // Charger toutes les factures pour le dropdown
    ViewBag.AllInvoices = await _context.Invoices.ToListAsync();

    return View(customer);
}

private bool CustomerExists(int id)
{
    return _context.Customers.Any(e => e.Id == id);
}

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return NotFound();

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentalQuotationApp.Data;
using RentalQuotationApp.Models;
using RentalQuotationApp.ViewModels;

[Authorize]
public class QuotationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuotationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var quotations = _context.Quotations.ToList();
        return View(quotations);
    }
    //[HttpGet("Quotations/Create")]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(QuotationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var quotation = new Quotation
            {
                Company = model.Company,
                Branch = model.Branch,
                CustomerCategory = model.CustomerCategory,
                CustomerName = model.CustomerName,
                QuotationExpiry = model.QuotationExpiry,
                RentalStartDate = model.RentalStartDate,
                RentalEndDate = model.RentalEndDate,
                QuotationNumber = model.QuotationNumber,
                NumberOfVehicles = model.NumberOfVehicles,
                TotalRentalSum = model.TotalRentalSum,
                TotalAdditionalCost = model.TotalAdditionalCost,
                ApprovalStatus = model.ApprovalStatus
            };

            // Add cost components
            foreach (var component in model.CostComponents)
            {
                quotation.CostComponents.Add(new CostComponent
                {
                    Description = component.Description,
                    Amount = component.Amount,
                    QuotationId = quotation.Id // This will automatically be set when the quotation is saved
                });
            }

            _context.Quotations.Add(quotation);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(model);
    }

    // GET: Quotations/Create
    //public IActionResult Create()
    //{
    //    var model = new QuotationViewModel();
    //    return View(model);
    //}
    //public IActionResult Create(Quotation quotation)
    //{
    //    Console.WriteLine("Received POST request to Create action.");

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(quotation.QuotationNumber))
    //            {
    //                quotation.QuotationNumber = GenerateQuotationNumber(); // Generate a unique quotation number
    //            }

    //            _context.Quotations.Add(quotation);
    //            _context.SaveChanges();

    //            Console.WriteLine("Data saved successfully.");
    //            return RedirectToAction("Index");
    //        }
    //        catch (DbUpdateException dbEx)
    //        {
    //            Console.WriteLine($"Database update exception: {dbEx.Message}");
    //            foreach (var entry in dbEx.Entries)
    //            {
    //                Console.WriteLine($"Entity of type {entry.Entity.GetType().Name} in state {entry.State} caused the error.");
    //            }
    //            ModelState.AddModelError("", "An error occurred while updating the database. Please try again.");
    //        }
    //        catch (Exception ex)
    //        {
    //            var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
    //            Console.WriteLine($"Error: {ex.Message}. Inner Exception: {innerExceptionMessage}");
    //            ModelState.AddModelError("", $"Error: {ex.Message}. Inner Exception: {innerExceptionMessage}");
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("Model state is invalid:");
    //        foreach (var key in ModelState.Keys)
    //        {
    //            var state = ModelState[key];
    //            foreach (var error in state.Errors)
    //            {
    //                Console.WriteLine($"Validation error on {key}: {error.ErrorMessage}");
    //            }
    //        }
    //    }
    //    return View(quotation);
    //}

    private string GenerateQuotationNumber()
    {
        return $"QTN-{DateTime.Now:ddyy}-{_context.Quotations.Count() + 1}";
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Approve(int id)
    {
        var quotation = _context.Quotations.Find(id);
        if (quotation != null)
        {
            quotation.ApprovalStatus = "Approved";
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Reject(int id)
    {
        var quotation = _context.Quotations.Find(id);
        if (quotation != null)
        {
            quotation.ApprovalStatus = "Rejected";
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var quotation = _context.Quotations.Include(q => q.CostComponents)
                                           .FirstOrDefault(q => q.Id == id);

        if (quotation == null)
        {
            return NotFound();
        }

        return View(quotation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Quotation quotation)
    {
        if (id != quotation.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(quotation);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quotations.Any(q => q.Id == id))
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
        return View(quotation);
    }


}
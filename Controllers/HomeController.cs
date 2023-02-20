using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Notes.Models;
using Notes.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Notes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    NotedbContext db = new NotedbContext();
    public async Task<IActionResult> Index()
    {
        var model = new IndexViewModel()
        {
            Notes = await db.Notes.OrderBy(x => x.Id).ToListAsync(),
        };
        return View(model);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Note note)
    {
        if (ModelState.IsValid)
        {
            db.Notes.Add(note);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var note = await db.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound();
        }

        db.Notes.Remove(note);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

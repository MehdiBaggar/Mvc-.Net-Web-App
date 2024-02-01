using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionVoiture.Models;
using GestionVoiture.Data;

namespace GestionVoiture.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var voitures = _dbContext.Voitures.OrderBy(o => Guid.NewGuid()).Take(3).ToList();
        return View(voitures);
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


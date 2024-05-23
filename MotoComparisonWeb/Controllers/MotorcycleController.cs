using Microsoft.AspNetCore.Mvc;

using MotoComparisonWeb.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MotorcycleController : Controller
{
    private readonly MotorcycleSpecService _motorcycleSpecService;

    public MotorcycleController(MotorcycleSpecService motorcycleSpecService)
    {
        _motorcycleSpecService = motorcycleSpecService;
    }

    public async Task<IActionResult> Index()
    {
        var manufacturers = await _motorcycleSpecService.GetManufacturers();
        return View(manufacturers);
    }

    [HttpPost]
    public async Task<IActionResult> Compare(string modelUrl1, string modelUrl2)
    {
        var specs1 = await _motorcycleSpecService.GetMotorcycleSpecs(modelUrl1);
        var specs2 = await _motorcycleSpecService.GetMotorcycleSpecs(modelUrl2);

        var comparisonList = _motorcycleSpecService.CompareMotorcycles(specs1, specs2);

        return View(comparisonList);
    }

    [HttpGet]
    public async Task<IActionResult> GetModelSuggestions(string manufacturerUrl, string term)
    {
        var models = await _motorcycleSpecService.GetMotorcycleModels(manufacturerUrl);
        var filteredModels = models.Where(m => m.Name.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        return PartialView("_ModelSuggestions", filteredModels);
    }
}

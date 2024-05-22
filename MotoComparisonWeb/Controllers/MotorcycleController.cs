using Microsoft.AspNetCore.Mvc;

using MotoComparisonWeb.Services;

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
    public async Task<IActionResult> GetModels(string manufacturerUrl)
    {
        var models = await _motorcycleSpecService.GetModels(manufacturerUrl);
        return Json(models);
    }
}

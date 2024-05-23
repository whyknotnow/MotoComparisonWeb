
using Microsoft.EntityFrameworkCore;

using MotoComparisonWeb.Models;

namespace MotoComparisonWeb.Services
{

    public class MotorcycleSpecService
    {
        private readonly MotorcycleContext _context;

        public MotorcycleSpecService(MotorcycleContext context)
        {
            _context = context;
        }
        public List<MotorcycleSpec> CompareMotorcycles(Dictionary<string, string> specs1, Dictionary<string, string> specs2)
        {
            var comparisonList = new List<MotorcycleSpec>();
            var allKeys = new HashSet<string>(specs1.Keys);
            allKeys.UnionWith(specs2.Keys);

            foreach (var key in allKeys)
            {
                comparisonList.Add(new MotorcycleSpec
                {
                    Specification = key,
                    ComparisonModelOne = specs1.ContainsKey(key) ? specs1[key] : "N/A",
                    ComparisonModelTwo = specs2.ContainsKey(key) ? specs2[key] : "N/A"
                });
            }

            return comparisonList;
        }




        public async Task<Dictionary<string, string>> GetMotorcycleSpecs(string url)
        {
            var specs = await _context.Specifications
                                      .Where(s => s.Model.Url == url)
                                      .ToDictionaryAsync(s => s.Key, s => s.Value);
            return specs;
        }

        public async Task<List<KeyValuePair<string, string>>> GetManufacturers()
        {
            var manufacturers = await _context.Manufacturers
                                              .Select(m => new KeyValuePair<string, string>(m.Name, m.Url))
                                              .ToListAsync();
            return manufacturers;
        }

        public async Task<List<Model>> GetMotorcycleModels(string manufacturerUrl)
        {
            var models = await _context.Models
                                       .Where(m => m.Manufacturer.Url == manufacturerUrl)
                                       .ToListAsync();
            return models;
        }

     
    }



}



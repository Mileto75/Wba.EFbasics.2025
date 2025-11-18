using Microsoft.AspNetCore.Mvc;
using Wba.EFbasics.Web.Data;
using Wba.EFbasics.Web.ViewModels;

namespace Wba.EFbasics.Web.Controllers
{
    public class RacesController : Controller
    {
        private readonly HorseDbContext _horseDbContext;

        public RacesController(HorseDbContext horseDbContext)
        {
            _horseDbContext = horseDbContext;
        }

        public IActionResult Index()
        {
            //get the races
            var races = _horseDbContext.Races.ToList();
            //put in viewmodel
            var racesIndexViewModel = new RacesIndexViewModel
            {
                Races = races.Select(r => new BaseViewModel
                {
                    Id = r.Id,
                    Value = r.Name
                })
            };
            //pass to the view
            return View(racesIndexViewModel);
        }
    }
}

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
        [HttpGet]
        public IActionResult Add()
        {
            //shows the form
            return View();
        }
        [HttpPost]
        public IActionResult Add(RacesAddViewModel racesAddViewModel)
        {
            //processes the formdata
            //Validate the data
                //use the ModelState.IsValid
                //if false => return to the View
            //create entity
            //store in database
            //redirect to index
        }
    }
}

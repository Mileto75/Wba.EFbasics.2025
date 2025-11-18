using Microsoft.AspNetCore.Mvc;
using Wba.EFbasics.Core.Entities;
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
            //processes the formdata//
            //Validate the data
            //use the ModelState.IsValid
            //if false => return to the View
            //check if name exists
            if(_horseDbContext.Races
                .Any(r => r.Name.ToUpper().Equals(racesAddViewModel.Name.ToUpper())))
            {
                ModelState.AddModelError("Name", "Name exists!");
            }
            if (!ModelState.IsValid)
            {
                return View(racesAddViewModel);
            }
            //create entity
            var newrace = new Race 
            {
                Name = racesAddViewModel.Name
            };
            //add to change tracker
            _horseDbContext.Races.Add(newrace);
            //store in database
            _horseDbContext.SaveChanges();
            //redirect to index
            return RedirectToAction(nameof(Index));
        }
    }
}

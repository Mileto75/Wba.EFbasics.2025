using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wba.EFbasics.Core.Entities;
using Wba.EFbasics.Web.Data;
using Wba.EFbasics.Web.ViewModels;

namespace Wba.EFbasics.Web.Controllers
{
    public class HorsesController : Controller
    {
        //inject(request) a horseDbContext
        private readonly HorseDbContext _horseDbContext;

        public HorsesController(HorseDbContext horseDbContext)
        {
            _horseDbContext = horseDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Use the DbContext to query the database
            //get the data
            //fill the model
            //pass to the view
            var horses = _horseDbContext.Horses.ToList();
            var horsesIndexViewModel = new HorsesIndexViewModel();
            horsesIndexViewModel.Horses = horses.Select(h =>
            new BaseViewModel
            {
                Id = h.Id,
                Value = h.Name
            });
            ViewBag.PageTitle = "Our horses";
            return View(horsesIndexViewModel);
        }
        [HttpGet]
        public IActionResult Info(int id)
        {
            //get the horse
            var horse = _horseDbContext
                .Horses
                .Include(h => h.Race)
                .Include(h =>h.Identification)
                .FirstOrDefault(h => h.Id == id);
            //check if null
            if (horse is null)
            {
                return NotFound();
            }
            //fill the model
            var horsesInfoViewModel =
                new HorsesInfoViewModel 
                {
                    Id = horse.Id,
                    Country = horse.Country,
                    DateOfBirth = horse.DateOfBirth.ToShortDateString(),
                    Race = new BaseViewModel 
                    {
                        Id = horse?.Race?.Id ?? 0,
                        Value = horse?.Race?.Name ?? "<NoRace>"
                    },
                    IdentificationCode = horse.Identification.IdentificationCode,
                    Value = horse.Name,
                    Weight = horse.Weight
                };
            //pass to the view
            return View(horsesInfoViewModel);
        }
        //Crud


        [HttpGet]
        public IActionResult Add()
        {
            //show the form
            //declare a viewmodel
            //fill the races
            var horsesAddViewModel = new HorsesAddViewModel
            {
                DateOfBirth = DateTime.Now,
                Races = _horseDbContext.Races.ToList().Select(r =>
                new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
            };
            return View(horsesAddViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(HorsesAddViewModel horsesAddViewModel)
        {
            //custom validatie
            //voor de Modelstate controle
            if(horsesAddViewModel.DateOfBirth >= DateTime.Now)
            {
                ModelState.AddModelError("DateOfBirth", "Horse must be born!");
            }
            //receive the form data
            if(!ModelState.IsValid)
            {
                //reload the list of races
                horsesAddViewModel.Races
                    = _horseDbContext.Races.ToList().Select(r =>
                new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                });
                return View(horsesAddViewModel);
            }
            //Add the horse to the database
            //create a horse object
            var horse = new Horse 
            {
                Name = horsesAddViewModel.Name,
                Country = horsesAddViewModel.Country,
                DateOfBirth = horsesAddViewModel.DateOfBirth,
                RaceId = horsesAddViewModel.RaceId,
                Identification = new Identification 
                {
                    IdentificationCode = horsesAddViewModel.IdentificationCode
                },
                Weight = horsesAddViewModel.Weight
            };
            //track the new horse
            _horseDbContext.Horses.Add(horse);
            //save to the database
            _horseDbContext.SaveChanges();
            //redirect to the index page
            return RedirectToAction(nameof(Index));

        }
        //Edit
        [HttpGet]
        public IActionResult Edit(int id)//shows the edit form
        {
            //get the horse to edit
            var horse = _horseDbContext.Horses
                .Include(h => h.Identification)
                .FirstOrDefault(h => h.Id == id);
            //check if null
            if(horse == null)
            {
                return NotFound();
            }
            //fill the viewmodel
            var horsesEditViewModel = new HorsesEditViewModel
            {
                Id = horse.Id,
                Name = horse.Name,
                IdentificationCode = horse.Identification.IdentificationCode,
                RaceId = horse.RaceId,
                DateOfBirth = horse.DateOfBirth,
                Country = horse.Country,
                Weight = horse.Weight,
                //load the list of races
                Races
                    = _horseDbContext.Races.ToList().Select(r =>
                new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
            };
            // pass to the view
            return View(horsesEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HorsesEditViewModel horsesEditViewModel)
        {
            //get the horse
            //another null check
            //Validate
            //update the horse
            //Save to the database
            return RedirectToAction(nameof(Index));
        }
    }
}
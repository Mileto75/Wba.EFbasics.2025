using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                        Id = horse.Race.Id,
                        Value = horse.Race.Name
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
            return View();
        }
        [HttpPost]
        public IActionResult Add(HorsesAddViewModel horsesAddViewModel)
        {
            //receive the form data
            return View();
        }
    }
}
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wba.EFbasics.Core.Entities;
using Wba.EFbasics.Web.Data;
using Wba.EFbasics.Web.ViewModels;

namespace Wba.EFbasics.Web.Controllers
{
    public class HorsesController : Controller
    {
        //inject(request) a horseDbContext
        private readonly HorseDbContext _horseDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HorsesController(HorseDbContext horseDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _horseDbContext = horseDbContext;
            _webHostEnvironment = webHostEnvironment;
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
                    Image = horse.ImageFilename,
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
        public async Task<IActionResult> Add(HorsesAddViewModel horsesAddViewModel)
        {
            //custom validatie
            //check if not in future
            if(horsesAddViewModel.DateOfBirth >= DateTime.Now)
            {
                ModelState.AddModelError("DateOfBirth", "Horse must be born!");
            }
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
            string newFilename = "https://placehold.co/600x400";
            if (horsesAddViewModel.Image is not null)
            {
                //Add the horse to the database
                //handle file upload
                //1 create unique filename
                newFilename = $"{Guid.NewGuid()}_{horsesAddViewModel.Image.FileName}";
                //2 build path to img folder
                var pathToImgFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                //3 check if path exists => if not, create directory
                if (!Directory.Exists(pathToImgFolder))
                {
                    Directory.CreateDirectory(pathToImgFolder);
                }
                //4 build full path to file
                var fullPathToFile = Path.Combine(pathToImgFolder, newFilename);
                //5 check if file exists
                if (Path.Exists(fullPathToFile))
                {
                    //fix later with exception
                    Console.WriteLine("FileExists");
                }
                //6 copy file from memory to filepath location
                using (FileStream fileStream = new(fullPathToFile, FileMode.Create))
                {
                    //copy from memory(iformfile) to fullPathtoFile
                    horsesAddViewModel.Image.CopyTo(fileStream);
                }
            }
            //create a horse object
            var horse = new Horse 
            {
                Name = horsesAddViewModel.Name,
                Country = horsesAddViewModel.Country,
                DateOfBirth = horsesAddViewModel.DateOfBirth,
                RaceId = horsesAddViewModel.RaceId,
                ImageFilename = newFilename,
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
            var editHorse = _horseDbContext.Horses
                .Include(h => h.Identification)
                .FirstOrDefault(h => h.Id == horsesEditViewModel.Id);
            //another null check
            if(editHorse is null)
            {
                //later change this to TempData message with redirect
                return NotFound();
            }
            //Validate
            //custom validatie
            //check if not in future
            if (horsesEditViewModel.DateOfBirth >= DateTime.Now)
            {
                ModelState.AddModelError("DateOfBirth", "Horse must be born!");
            }
            if (!ModelState.IsValid)
            {
                //reload the list of races
                horsesEditViewModel.Races
                    = _horseDbContext.Races.ToList().Select(r =>
                new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                });
                return View(horsesEditViewModel);
            }
            //update the horse
            //assign all the properties
            editHorse.Name = horsesEditViewModel.Name;
            editHorse.RaceId = horsesEditViewModel.RaceId;
            editHorse.Weight = horsesEditViewModel.Weight;
            editHorse.Identification.IdentificationCode = horsesEditViewModel.IdentificationCode;
            editHorse.Country = horsesEditViewModel.Country;
            editHorse.DateOfBirth = horsesEditViewModel.DateOfBirth;
            
            //Save to the database
            _horseDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //delete
        //confirm dialog
        public IActionResult ConfirmDelete(int id)
        {
            //get the horse to delete
            var deleteHorse = _horseDbContext.Horses
                .FirstOrDefault(h => h.Id == id);
            //check if  null
            if(deleteHorse is null)
            {
                return NotFound();
            }
            //pass id and name to the view
            var horsesConfirmDeleteViewModel = new HorsesConfirmDeleteViewModel
            {
                Id = deleteHorse.Id,
                Value = deleteHorse.Name
            };
            return View(horsesConfirmDeleteViewModel);
        }
        //the real delete method
        public IActionResult Delete(HorsesConfirmDeleteViewModel horsesConfirmDeleteViewModel)
        {
            //get the horse
            var deleteHorse = _horseDbContext.Horses
                .FirstOrDefault(h => h.Id == horsesConfirmDeleteViewModel.Id);
            //check if null
            if(deleteHorse is null)
            {
                //change later to use TempData in state management
                return NotFound();
            }
            //mark for deletion
            _horseDbContext.Horses.Remove(deleteHorse);
            //push changes to the database = delete statement
            _horseDbContext.SaveChanges();
            //redirect to index
            return RedirectToAction(nameof(Index));
        }
    }
}
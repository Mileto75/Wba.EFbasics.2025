using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wba.EFbasics.Core.Entities;
using Wba.EFbasics.Web.Data;
using Wba.EFbasics.Web.Services;
using Wba.EFbasics.Web.Services.Interfaces;
using Wba.EFbasics.Web.ViewModels;

namespace Wba.EFbasics.Web.Controllers
{
    public class HorsesController : Controller
    {
        //inject(request) a horseDbContext
        private readonly HorseDbContext _horseDbContext;
        private readonly IFormBuilderService _formBuilderService;
        private readonly IFileService _fileService;

        public HorsesController(HorseDbContext horseDbContext, IFormBuilderService formBuilderService, IFileService fileService)
        {
            _horseDbContext = horseDbContext;
            _formBuilderService = formBuilderService;
            _fileService = fileService;
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
        public async Task<IActionResult> Add()
        {
            //show the form
            //declare a viewmodel
            //fill the races
            var horsesAddViewModel = new HorsesAddViewModel
            {
                DateOfBirth = DateTime.Now,
                //fill the dropdown with races
                Races = await _formBuilderService.GetRaces()
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
                    = await _formBuilderService.GetRaces();
                return View(horsesAddViewModel);
            }
            string newFilename = "https://placehold.co/600x400";
            if (horsesAddViewModel.Image is not null)
            {
                newFilename = await _fileService.StoreFile(horsesAddViewModel.Image);
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
            try 
            {
                _horseDbContext.SaveChanges();
            }
            catch(DbUpdateException dbUpdateException)
            {
                //in production log the exception in database or file
                //create the user error message
                TempData["errorMessage"] = "Something went wrong!";
                return RedirectToAction(nameof(Index));
            }
            //create session with user message
            //HttpContext.Session.SetString("message", "Horse created successfully!");
            //use tempdata
            TempData["message"] = "Horse created successfully!";
            //redirect to the index page
            return RedirectToAction(nameof(Index));

        }
        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)//shows the edit form
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
                    = await _formBuilderService.GetRaces()
            };
            // pass to the view
            return View(horsesEditViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HorsesEditViewModel horsesEditViewModel)
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
                    = await _formBuilderService.GetRaces();
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
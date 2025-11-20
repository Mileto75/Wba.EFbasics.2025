using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Wba.EFbasics.Web.ViewModels
{
    public class HorsesAddViewModel
    {
        [Display(Name = "Horse name:")]
        [Required(ErrorMessage = "Please provide a name!")]
        public string Name { get; set; }
        [Display(Name = "Origin:")]
        [Required]
        public string Country { get; set; }
        [Display(Name = "Date of birth:")]
        [Required]
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Id code")]
        public string IdentificationCode { get; set; }
        [Required]
        [Display(Name = "Weight:")]
        
        public decimal Weight { get; set; }
        //datatypes for select dropdown in html
        
        public int? RaceId { get; set; }
        public IEnumerable<SelectListItem> Races { get; set; }
    }
}

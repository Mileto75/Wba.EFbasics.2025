using System.ComponentModel.DataAnnotations;

namespace Wba.EFbasics.Web.ViewModels
{
    public class RacesAddViewModel
    {
        [Required(ErrorMessage = "Please provide a name!")]
        [Display(Name="Race:")]
        public string Name { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wba.EFbasics.Web.Services.Interfaces
{
    public interface IFormBuilderService
    {
        //returns a list of races for the dropdown form
        Task<IEnumerable<SelectListItem>> GetRaces();
    }
}

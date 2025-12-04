using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wba.EFbasics.Web.Data;
using Wba.EFbasics.Web.Services.Interfaces;

namespace Wba.EFbasics.Web.Services
{
    public class FormBuilderService : IFormBuilderService
    {
        private readonly HorseDbContext _horseDbContext;

        public FormBuilderService(HorseDbContext horseDbContext)
        {
            //dependency injection
            _horseDbContext = horseDbContext;
        }

        public async Task<IEnumerable<SelectListItem>> GetRaces()
        {
            return await _horseDbContext.Races
                .Select(r =>
                new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }).ToListAsync();
        }
    }
}

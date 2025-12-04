using Microsoft.AspNetCore.Hosting;
using Wba.EFbasics.Web.Services.Interfaces;

namespace Wba.EFbasics.Web.Services
{
    public class FileServiceV2 : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileServiceV2(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> StoreFile(IFormFile file)
        {
            //Add the horse to the database
            //handle file upload
            //1 create unique filename
            var newFilename = $"{Guid.NewGuid()}_{file.FileName}";
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
                await file.CopyToAsync(fileStream);
            }
            return newFilename;
        }
    }
}

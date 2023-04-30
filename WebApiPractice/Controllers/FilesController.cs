using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebApiPractice.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        // setup the fileExtensionContentTypeProvider instance
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        // constructor for the injection of the FileExtensionContentTypeProvider
        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }
        
        
        
        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            var pathToFile = "openGIS.pdf";

            if(!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }


            // if it does not recognise the file extension, it will return "application/octet-stream"
            // "application/octet-stream" is a generic content type used for unknown file types.
            // It indicates that the file should be treated as a binary stream and that the receiving application
            // should determine the appropriate way to handle it.
            if (!_fileExtensionContentTypeProvider.TryGetContentType(
                pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(pathToFile);


            return File(bytes, contentType, Path.GetFileName(pathToFile)); // this acts as a wrapper for the FileResult class
                           // Once the PDF has been upladed, update othe properties for the file and "CopytoOutput Directory = Copy always"
                           // different content are used in the second argument 

            // we can also use a built in service called the "FileExtensionContentTypeProvider" and is defined in the Program/Startup.cs
            // builder.Services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
        }
    }
}

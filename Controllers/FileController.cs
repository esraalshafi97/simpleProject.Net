using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cityInfo.api.Controllers
{
    [ApiController]

    [Route("file")]
    
    public class FileController : ControllerBase
    {

        private readonly FileExtensionContentTypeProvider fileExtension;

        public FileController(FileExtensionContentTypeProvider _fileExtension)
        {
            this.fileExtension = _fileExtension ?? throw new System.ArgumentException(nameof(FileExtensionContentTypeProvider));
        }


        [HttpGet("{id}")]
      public ActionResult getFile(String id)
        {
            String path = "ADXWT2.pdf";
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            Console.Write("ladsflkajfalsjfal7777");

            if (!fileExtension.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            Console.Write("ladsflkajfalsjfal");
            Console.Write(contentType);

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/pdf", Path.GetFileName(path) ) ;
        }

        [HttpPost]
        public async Task<ActionResult> AddFile(IFormFile file)
        {
            if(file.ContentType!="application/pdf" || file.Length>29129019 || file.Length == 0)
            {
                return BadRequest("file error");
            }
            var path=Path.Combine(Directory.GetCurrentDirectory(), $"upload_file_{Guid.NewGuid}.pdf");
            using(var stream=new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok("done");



        }
        }
}


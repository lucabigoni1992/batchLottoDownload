using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryLotto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using libExcel;
using System.Collections;

namespace LottoWeb.ClientApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDispenserController : ControllerBase
    {

        private IWebHostEnvironment _hostingEnvironment;

        public FileDispenserController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file is null)
            {
                return NotFound();
            }
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "File");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.Length > 0)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(fileStream).ConfigureAwait(true);

            }
            return Ok();
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string file)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, @"File");
            var filePath = Path.Combine(uploads, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();
            var memory = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
                await stream.CopyToAsync(memory).ConfigureAwait(true);
            memory.Position = 0;


            return File(memory, GetContentType(filePath), file);
        }


        [HttpGet("MadeAndDownloadExcelLottoPalle")]
        public async Task<IActionResult> MadeAndDownloadExcelLottoPalle(string file, int idLotto)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, @"File\Excel\Creati");
            var filePath = Path.Combine(uploads, file + ".xlsx");

            if (System.IO.File.Exists(filePath))
                return await FileDownload(filePath).ConfigureAwait(true);

            new WriteExcel().WriteExcelFile(
                new ApiInterface()
                   .GetLottoDetailesFromId(idLotto)
                   , uploads
                   , file
                   );
            if (!System.IO.File.Exists(filePath))
                return NotFound();
            return await FileDownload(filePath).ConfigureAwait(true);

        }

        private async Task<IActionResult> FileDownload(string filePath)
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
                await stream.CopyToAsync(memory).ConfigureAwait(true);
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
        }

        [HttpGet]
        [Route("files")]
        public IActionResult Files()
        {
            var result = new List<string>();

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, @"File");
            if (Directory.Exists(uploads))
            {
                var provider = _hostingEnvironment.ContentRootFileProvider;
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    result.Add(fileInfo.Name);
                }
            }
            return Ok(result);
        }


        private static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
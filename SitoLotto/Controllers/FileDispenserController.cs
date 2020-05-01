using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryLotto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.Data;
using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace LottoWeb.ClientApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDispenserController : Controller
    {

        private IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<FileDispenserController> logger;

        public FileDispenserController(IWebHostEnvironment environment, ILogger<FileDispenserController> logger)
        {
            _hostingEnvironment = environment;
            this.logger = logger;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            };//in un ambiante professionale si deve gestire meglio
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string file)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            };//in un ambiante professionale si deve gestire meglio
        }


        [HttpGet("Made/excel/lotto")]
        public async Task<IActionResult> MadeAndDownloadExcelLottoPalle(string file, int idLotto)
        {
            try
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, @"File\Excel\Creati");
                var filePath = Path.Combine(uploads, file + ".xlsx");

                if (System.IO.File.Exists(filePath))
                    return await FileDownload(filePath).ConfigureAwait(true);

                //new WriteExcel().WriteExcelFile(
                //    new ApiInterface()
                //       .GetLottoPallefromId(idLotto)
                //            .Select(i => new { i.tipoPalla, i.nPalla })
                //            .ToList()
                //       , uploads
                //       , file
                //       );
                if (!System.IO.File.Exists(filePath))
                    return NotFound();
                return await FileDownload(filePath).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString()); return BadRequest(ex.ToString());
            };//in un ambiante professionale si deve gestire meglio
        }
        [HttpGet("Made/excel/lotto/detailes")]
        public async Task<IActionResult> MadeAndDownloadExcelLottoPalleDetailes(string file, int idLotto)
        {
            try
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, @"File\Excel\Creati");
                var filePath = Path.Combine(uploads, file + ".xlsx");

                if (System.IO.File.Exists(filePath))
                    return await FileDownload(filePath).ConfigureAwait(true);

                //new WriteExcel().WriteExcelFile(
                //    new ApiInterface()
                //       .GetLottoDetailesFromId(idLotto).results
                //            .Select(i => new
                //            {
                //                i.nEstrazione,
                //                i.anno,
                //                i.enumTipoVincita,
                //                i.valore,
                //                i.vincitori,
                //                i.premio
                //            })
                //            .ToList()
                //       , uploads
                //       , file
                //       );
                if (!System.IO.File.Exists(filePath))
                    return NotFound();
                return await FileDownload(filePath).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest();// ex.ToString()); 
            };//in un ambiante professionale si deve gestire meglio

        }


        [HttpGet]
        [Route("files")]
        public IActionResult Files()
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex.ToString()); return BadRequest(ex.ToString());
            };//in un ambiante professionale si deve gestire meglio

        }


        private async Task<IActionResult> FileDownload(string filePath)
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
                await stream.CopyToAsync(memory).ConfigureAwait(true);
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryLotto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using System.Data;
using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using libExcel;
using Newtonsoft.Json;
using lbControlWebPages;
using Newtonsoft.Json.Linq;
using libraryLotto.dlm;
using static libraryLotto.dlm.KendoResultDataSiteInputMapping;

namespace LottoWeb.ClientApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangedWebPageController : Controller
    {

        private IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ChangedWebPageController> logger;

        public ChangedWebPageController( IWebHostEnvironment environment, ILogger<ChangedWebPageController> logger)
        {
            _hostingEnvironment = environment;
             this.logger = logger;
        }

        [HttpGet]
        [Route("GetAllSite")]
        public string GetAllSite()
        {
            try
            {
                Response.StatusCode = 200;
                return JsonConvert.SerializeObject(InteractiveDB.GetAllSite());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                Response.StatusCode = 500;
                return "";
            };//in un ambiante professionale si deve gestire meglio
        }
        [HttpGet]
        [Route("AddSite/{SiteData}")]
        public IActionResult AddSite(string SiteData)
        {
            try
            {
                // return JsonConvert.SerializeObject(InteractiveDB.GetAllSite());
                SiteInputMapping newElem = JsonConvert.DeserializeObject<SiteInputMapping>(SiteData);
                return Ok(InteractiveDB.AddSite(newElem));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            };//in un ambiante professionale si deve gestire meglio
        }

    }
}
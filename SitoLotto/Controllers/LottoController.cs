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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace LottoWeb.ClientApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class LottoController : ControllerBase
    {
        private readonly ILogger<FileDispenserController> logger;

        public LottoController(ILogger<FileDispenserController> logger)
        {
            this.logger = logger;
        }

        ApiInterface a = new ApiInterface();

        // GET: api/Lotto
        [HttpGet]
        [ActionName("")]
        public string Get()
        {
            try
            {
                return JsonConvert.SerializeObject(a.GetLottoKendoQuery(""));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return "";
            };//in un ambiante professionale si deve gestire meglio
        }

        // GET: api/Lotto/ParamKendo

        [HttpGet("{ParamKendo}", Name = "Get All")]
        public string Get(string ParamKendo)
        {
            try
            {
                return JsonConvert.SerializeObject(a.GetLottoKendoQuery(ParamKendo));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return "";
            };//in un ambiante professionale si deve gestire meglio
        } // GET: api/Lotto/5

        [HttpGet("detailes/{id}", Name = "Palle")]
        public string GetPalle(int id)
        {
            try
            {
                return JsonConvert.SerializeObject(a.GetLottoPallefromId(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return "";
            };//in un ambiante professionale si deve gestire meglio
        }
        // GET: api/Lotto
        [HttpGet("active", Name = "active")]
        public string GetActive()
        {
            try
            {
                return JsonConvert.SerializeObject(DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return "";
            };//in un ambiante professionale si deve gestire meglio
        }

        [HttpGet("quote/{id}", Name = "Detailes")<]
        public string GetDetailes(int id)
        {
            try
            {
                return JsonConvert.SerializeObject(a.GetLottoDetailesFromId(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return "";
            };//in un ambiante professionale si deve gestire meglio
        }



        [HttpGet("statistics/Quote", Name = "Statistics Quote")]
        public string GetStatisticsQuote()
        {
            try
            {
                return JsonConvert.SerializeObject(a.GetLottoStatisticsQuote());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return "";
            };//in un ambiante professionale si deve gestire meglio
        }

        [HttpGet("statistics/Balls", Name = "Statistics Balls")]
        public string GetStatisticsBalls()
        {
            try
            {
                return JsonConvert.SerializeObject(a.GetLottoStatisticsBalls());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return "";
            };//in un ambiante professionale si deve gestire meglio
        }

        //// POST: api/Lotto
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //    return ;
        //}

        //// PUT: api/Lotto/5
        //[HttpPut("{id}")]
        //public void Put()
        //{
        //    return;
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete()
        //{
        //    return;
        //}
    }
}
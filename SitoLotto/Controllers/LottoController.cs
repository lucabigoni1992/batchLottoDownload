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

namespace LottoWeb.ClientApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class LottoController : ControllerBase
    {

        ApiInterface a = new ApiInterface();

        // GET: api/Lotto
        [HttpGet]
        [ActionName("")]
        public string Get()
        {
            return JsonConvert.SerializeObject(a.GetLottoKendoQuery(""));
        }

        // GET: api/Lotto/ParamKendo

        [HttpGet("{ParamKendo}", Name = "Get All")]
        public string Get(string ParamKendo)
        {
            return JsonConvert.SerializeObject(a.GetLottoKendoQuery(ParamKendo)); ;
        } // GET: api/Lotto/5

        [HttpGet("detailes/{id}", Name = "Palle")]
        public string GetPalle(int id)
        {
            return JsonConvert.SerializeObject(a.GetLottoPallefromId(id)); ;
        }

        [HttpGet("quote/{id}", Name = "Detailes")]
        public string GetDetailes(int id)
        {
            return JsonConvert.SerializeObject(a.GetLottoDetailesFromId(id)); 
        }



        [HttpGet("statistics/Quote", Name = "Statistics Quote")]
        public string GetStatisticsQuote()
        {
            return JsonConvert.SerializeObject(a.GetLottoStatisticsQuote());
        }

        [HttpGet("statistics/Balls", Name = "Statistics Balls")]
        public string GetStatisticsBalls()
        {
            return JsonConvert.SerializeObject(a.GetLottoStatisticsBalls());
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
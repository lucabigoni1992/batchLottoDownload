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
    public string Get()
    {
      return JsonConvert.SerializeObject(a.GetLotto());
    }

    // GET: api/Lotto/5
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id)
    {
      return "value";
    }

    // POST: api/Lotto
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT: api/Lotto/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
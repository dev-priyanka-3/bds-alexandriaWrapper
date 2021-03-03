using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AlexandriaWrapper.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using AlexandriaWrapper.Model;
using System.Net.Http.Json;
using System.Net;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;

namespace AlexandriaWrapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WrapperController : ControllerBase
    {
        public IApiHelper _apiHelper;
        public readonly IConfiguration _config;
        public readonly string apiKey;
        public WrapperController(IConfiguration config)
        {
            _apiHelper = new ApiHelper();
            _config = config;


            apiKey = config.GetSection("key").Value;
        }
        //[HttpPost("entity")]
        //public IActionResult PostEntity()
        //{

        //    return Ok();
        //}
        //[HttpGet("entity")]
        //public IActionResult GetEntity()
        //{

        //    return Ok();
        //}
        //[HttpPost("financials")]
        //public IActionResult PostFinancials()
        //{

        //    return Ok();
        //}
        //[HttpGet("financials")]
        //public IActionResult GetFinancials()
        //{

        //    return Ok();
        //}
        //[HttpPost("financials-coverage")]
        //public IActionResult PostFinancialsCoverage()
        //{

        //    return Ok();
        //}
        //[HttpGet("financials-coverage")]
        //public IActionResult GetFinancialsCoverage()
        //{

        //    return Ok();
        //}
        [HttpPost("FeedPostRequest")]
        public IActionResult FeedPostRequest(WrapperModel content)
        {
            HttpResponseMessage response = _apiHelper.POSTSpecial(content,apiKey);
            string contentVal = response.Content.ReadAsStringAsync().Result;
            dynamic val = JsonConvert.DeserializeObject<ExpandoObject>(contentVal, new ExpandoObjectConverter());
            return Ok(val);
        }

        [HttpGet("FeedGetRequest")]
        public IActionResult FeedGetRequest(string UrlPath)
        {
            UrlPath = UrlPath.Replace("\"", "");
            HttpResponseMessage response = _apiHelper.GETSpecial(UrlPath,apiKey);
            string contentVal = response.Content.ReadAsStringAsync().Result;
            dynamic val = JsonConvert.DeserializeObject<ExpandoObject>(contentVal, new ExpandoObjectConverter());
            return Ok(val);
        }
    }
}

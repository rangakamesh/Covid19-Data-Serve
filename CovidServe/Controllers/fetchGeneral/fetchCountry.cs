using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CovidServe.Models;
using CovidServe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidServe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class fetchCountry : ControllerBase
    {
        private readonly CountryService _countryService;


        public fetchCountry(CountryService countryService)
        {
            _countryService = countryService;
        }

        public class QueryParameters
        { 
            [Required] public string CountryName { get; set; }
        }

        [HttpGet]
        public ActionResult<CovidRecord[]> Get([FromQuery] QueryParameters parameters)
        {

            return _countryService.GetCountry(parameters.CountryName);
        }
    }
}

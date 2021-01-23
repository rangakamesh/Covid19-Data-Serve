using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CovidServe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidServe.Controllers.fetchField
{
    [Route("api/[controller]")]
    [ApiController]
    public class fetchCountryNewDeaths : ControllerBase
    {

        private readonly CountryService _countryService;

        public fetchCountryNewDeaths(CountryService countryService)
        {
            _countryService = countryService;
        }

        public class QueryParameters
        {
            [Required] public bool Ascending { get; set; }
            [Required] public string CountryName { get; set; }

        }

        [HttpGet]
        public ActionResult<object> Get([FromQuery] QueryParameters parameters)
        {
            return _countryService.GetCountryField(parameters.Ascending, "newdeaths", parameters.CountryName);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CovidServe.Models;
using CovidServe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidServe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class fetchTopDeathCount : ControllerBase
    {
        private readonly CountryService _countryService;

        public fetchTopDeathCount(CountryService countryService)
        {
            _countryService = countryService;
        }

        public class QueryParameter
        {

            [Required] public int Top { get; set; } = 10;

            [Range(2019, 3019, ErrorMessage = "Value for {0} is required and must be between {1} and {2}.")]
            [Required] public int Year { get; set; }

            [Range(01, 12, ErrorMessage = "Value for {0} is required and must be between {1} and {2}.")]
            [Required] public int Month { get; set; }

            [Range(01, 31, ErrorMessage = "Value for {0} is required and must be between {1} and {2}.")]
            [Required] public int Day { get; set; }

        }

        [HttpGet]
        public CovidRecord[] Get([FromQuery] QueryParameter parameters)
        {
            return _countryService.GetTopCount("deaths", parameters.Top, parameters.Year, parameters.Month, parameters.Day);
        }
    }
}

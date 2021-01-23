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
    public class fetchCountryToday : ControllerBase
    {
        private readonly CountryService _countryService;

        public fetchCountryToday(CountryService countryService)
        {
            _countryService = countryService;
        }

        public class QueryParameters
        {
            [Required] public string CountryName { get; set; }

        }

        [HttpGet]
        public CovidRecord Get([FromQuery] QueryParameters parameters)
        {
            DateTime today = DateTime.Now;
            return _countryService.GetCountryOnDate(parameters.CountryName, today.Year, today.Month, today.Day);
        }
    }
}

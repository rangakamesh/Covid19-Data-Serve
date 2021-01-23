using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidServe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidServe.Controllers.fetchGeneral
{
    [Route("api/[controller]")]
    [ApiController]
    public class fetchCountryNames : ControllerBase
    {
        private readonly CountryService _countryService;


        public fetchCountryNames(CountryService countryService)
        {
            _countryService = countryService;
        }


        [HttpGet]
        public ActionResult<object> Get()
        {

            return _countryService.GetCountryNames();
        }
    }
}

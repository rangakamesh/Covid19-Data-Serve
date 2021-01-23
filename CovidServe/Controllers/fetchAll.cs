using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidServe.Models;
using CovidServe.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidServe.Controllers
{
    [Route("/api/fetchAll")]
    [ApiController]
    public class fetchAll : ControllerBase
    {
        private readonly CountryService _countryService;

        public fetchAll(CountryService countryService)
        {
            _countryService = countryService;    
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> Get()
        {
            return _countryService.Get();
        }

    }
}

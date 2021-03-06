﻿using CovidServe.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;

namespace CovidServe.Controllers.fetchField
{
    [Route("api/[controller]")]
    [ApiController]
    public class fetchCountryConfirmed : ControllerBase
    {
        private readonly CountryService _countryService;

        public fetchCountryConfirmed(CountryService countryService)
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
            return _countryService.GetCountryField(parameters.Ascending, "confirmed", parameters.CountryName);
        }
    }
}

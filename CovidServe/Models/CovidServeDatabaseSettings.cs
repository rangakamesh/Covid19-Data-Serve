using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidServe.Models
{
    public class CovidServeDatabaseSettings : ICovidServeDatabaseSettings
    {
        public string CovidCollectionName { get; set; }
        //public string ConnectionString { get; set; } //It is good to use this in production instead of Env Var
        public string DatabaseName { get; set; }
    }

    public interface ICovidServeDatabaseSettings
    { 
        string CovidCollectionName { get; set; }
        //string ConnectionString { get; set; }//It is good to use this in production instead of Env Var
        string DatabaseName { get; set; }
    }
}

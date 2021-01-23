using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CovidServe.Models
{
    public class CovidRecord
    {
        public string Country_Region { get; set; }
        public DateTime Last_Update { get; set; }

        //[BsonElement("Lat")] /* Depreciated and appended to the Country object */
        //public double Latitude { get; set; }

        //[BsonElement("Long_")] /* Depreciated and appended to the Country object */
        //public double Longitude { get; set; }
        public int Confirmed { get; set; } 
        public double Deaths { get; set; }
        public double Recovered { get; set; }
        public double Active { get; set; }
        public double New_Cases { get; set; }
        public double New_Deaths { get; set; }
        public double New_Recoveries { get; set; }
        public double Incident_Rate { get; set; }
        public double Case_Fatality_Rate { get; set; }

        public override string ToString() => JsonSerializer.Serialize<CovidRecord>(this);
    }
}

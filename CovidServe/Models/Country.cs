using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CovidServe.Models
{
    public class Country
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("country")]
        public string CountryName { get; set; }

        [BsonElement("latitude")] 
        public double Latitude { get; set; }

        [BsonElement("longitude")] 
        public double Longitude { get; set; }

        [BsonElement("object")]
        public CovidRecord[] Records { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Country>(this);

    }
}

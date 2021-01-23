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
    public class ResponseMessage
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}

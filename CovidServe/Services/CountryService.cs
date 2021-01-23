using CovidServe.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidServe.Services
{
    public class CountryService
    {
        private readonly IMongoCollection<Country> _country;

        public CountryService(ICovidServeDatabaseSettings settings)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB"));
            var database = client.GetDatabase(settings.DatabaseName);

            _country = database.GetCollection<Country>(settings.CovidCollectionName);
        }

        public List<Country> Get() => _country.Find(countries => true).ToList();
        public object GetCountryNames()
        {

            var countryNames = _country.AsQueryable().Select(country => country.CountryName.ToLower());

            return new { countryNames };

        }

        public CovidRecord[] GetCountry(string CountryName) 
        {
            var result = _country.AsQueryable().Where(country => country.CountryName.ToLower() == CountryName.ToLower())
                .Select(country => country.Records).ToArray();

            if (result.Count() > 0)
                return result[0];
            else
                return new CovidRecord[0];
        }

        public CovidRecord GetCountryOnDate(string CountryName, int Year, int Month, int Day)
        {
            DateTime from = new DateTime(Year, Month, Day);
            TimeSpan oneDay = new TimeSpan(1,0,0,0);
            DateTime to = from + oneDay;

            var result = _country.AsQueryable().Where(country => country.CountryName == CountryName)
                .Select(country => new { arrayVal = country.Records.Where(cv => cv.Last_Update >= from & cv.Last_Update < to) });

            CovidRecord toRet = new CovidRecord();
            foreach (var x in result) {
                foreach (var y in x.arrayVal)
                {
                    toRet = y;
                }
            }
            return toRet;             
        }

        public CovidRecord[] GetOnDate(int Year, int Month, int Day)
        {
            DateTime from = new DateTime(Year, Month, Day);
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
            DateTime to = from + oneDay;

            var result = _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).ToArray();

            return result;
        }

        public CovidRecord[] GetTopCount(string OnField, int Top, int Year, int Month, int Day)
        {
            //no input can be 0

            DateTime from = new DateTime(Year, Month, Day,0,0,0);
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
            DateTime to = from + oneDay;

            Console.WriteLine("From : {0}",from);
            Console.WriteLine("To : {0}",to);

            CovidRecord[] result = (OnField.ToLower()) switch
            {
                "confirmed" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.Confirmed).Take(Top).ToArray(),
                "active" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.Active).Take(Top).ToArray(),
                "deaths" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.Deaths).Take(Top).ToArray(),
                "recovered" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.Recovered).Take(Top).ToArray(),
                "newcases" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.New_Cases).Take(Top).ToArray(),
                "newdeaths" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.New_Deaths).Take(Top).ToArray(),
                "newrecoveries" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.New_Recoveries).Take(Top).ToArray(),
                _ => new CovidRecord[0],
            };

            return result;
        }


        public CovidRecord[] GetSortedCount(bool Ascending,string OnField, int Top, int Year, int Month, int Day)
        {
            //no input can be 0

            DateTime from = new DateTime(Year, Month, Day);
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
            DateTime to = from + oneDay;

            CovidRecord[] result;

            if (Ascending)
            {
                result = (OnField.ToLower()) switch
                {
                    "confirmed" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderBy(rec => rec.Confirmed).Take(Top).ToArray(),
                    "active" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderBy(rec => rec.Active).Take(Top).ToArray(),
                    "deaths" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderBy(rec => rec.Deaths).Take(Top).ToArray(),
                    "recovered" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderBy(rec => rec.Recovered).Take(Top).ToArray(),
                    "newcases" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderBy(rec => rec.New_Cases).Take(Top).ToArray(),
                    "newdeaths" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderBy(rec => rec.New_Deaths).Take(Top).ToArray(),
                    "newrecoveries" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderBy(rec => rec.New_Recoveries).Take(Top).ToArray(),
                    _ => new CovidRecord[0],
                };
            }
            else
            {
                result = (OnField.ToLower()) switch
                {
                    "confirmed" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.Confirmed).Take(Top).ToArray(),
                    "active" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.Active).Take(Top).ToArray(),
                    "deaths" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.Deaths).Take(Top).ToArray(),
                    "recovered" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.Recovered).Take(Top).ToArray(),
                    "newcases" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.New_Cases).Take(Top).ToArray(),
                    "newdeaths" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.New_Deaths).Take(Top).ToArray(),
                    "newrecoveries" => _country.AsQueryable().SelectMany(x => x.Records).Distinct().Where(rec => rec.Last_Update >= from & rec.Last_Update < to).OrderByDescending(rec => rec.New_Recoveries).Take(Top).ToArray(),
                    _ => new CovidRecord[0],
                };
            }


            return result;
        }

        public object GetCountryField(bool Ascending, string OnField, string CountryName)
        {
            //no input can be 0
            Console.WriteLine("Country {0}, ONF {1}", CountryName, OnField);
            var result = _country.AsQueryable().Where(country => country.CountryName.ToLower() == CountryName.ToLower()).Select(country => country.Records).ToArray();
            
            object records;

            if (result.Count() > 0)
            {
                if (Ascending)
                {
                    records = (OnField.ToLower()) switch
                    {
                        "confirmed" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.Confirmed }).OrderBy(recs => recs.Last_Update).ToArray(),
                        "death" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.Deaths }).OrderBy(recs => recs.Last_Update).ToArray(),
                        "active" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.Active }).OrderBy(recs => recs.Last_Update).ToArray(),
                        "recovered" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.Recovered }).OrderBy(recs => recs.Last_Update).ToArray(),
                        "newcases" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.New_Cases }).OrderBy(recs => recs.Last_Update).ToArray(),
                        "newdeaths" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.New_Deaths }).OrderBy(recs => recs.Last_Update).ToArray(),
                        "newrecoveries" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.New_Recoveries }).OrderBy(recs => recs.Last_Update).ToArray(),
                        _ => new { },
                    };
                }
                else
                {
                    records = (OnField.ToLower()) switch
                    {
                        "confirmed" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.Confirmed }).OrderByDescending(recs => recs.Last_Update).ToArray(),
                        "death" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.Deaths }).OrderByDescending(recs => recs.Last_Update).ToArray(),
                        "active" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.Active }).OrderByDescending(recs => recs.Last_Update).ToArray(),
                        "recovered" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.Recovered }).OrderByDescending(recs => recs.Last_Update).ToArray(),
                        "newcases" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.New_Cases }).OrderByDescending(recs => recs.Last_Update).ToArray(),
                        "newdeaths" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.New_Deaths }).OrderByDescending(recs => recs.Last_Update).ToArray(),
                        "newrecoveries" => result[0].AsQueryable().Select(recs => new { recs.Country_Region, recs.Last_Update, recs.New_Recoveries }).OrderByDescending(recs => recs.Last_Update).ToArray(),
                        _ => new { },
                    };

                }
            }
            else
            { 
                records = new { };
            }

           return records;
        }
    }
}

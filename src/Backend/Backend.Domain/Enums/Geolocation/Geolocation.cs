using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Backend.Domain.Enums.Geolocation.Geolocation;

namespace Backend.Domain.Enums.Geolocation
{
    // I Know the best approach is use an API but theres no reason for that now, All I need is to make this work
    // I can create a issue for this later, meanwhile this will do the task so whatever
    public static class Geolocation
    {
        public class Country
        {
            public int CountryId { get; set; }
            public string CountryName { get; set; }
        }
        public class State
        {
            public int StateId { get; set; }
            public int CountryId { get; set; }
            public string StateName { get; set; }
        }
        public class City
        {
            public int CityId { get; set; }
            public int CountryId { get; set; }
            public int StateId { get; set; }
            public string CityName { get; set; }
        }

        public static class Countries
        {
            public static List<Country> List = new List<Country>() 
            { 
                new Country()
                {
                    CountryId = 1,
                    CountryName = "Brazil"
                },
            };
        }
        public static class States
        {
            public static List<State> List = new List<State>()
            {
                new State()
                {
                    CountryId = 1,
                    StateId = 1,
                    StateName = "Sao Paulo"
                },
                new State()
                {
                    CountryId = 1,
                    StateId = 2,
                    StateName = "Santa Catarina"
                }
            };
        }
        public static class Cities
        {
            public static List<City> List = new List<City>()
            {
                new City()
                {
                    CountryId = 1,
                    StateId = 1,
                    CityId = 1,
                    CityName = "Atibaia"
                },
                new City()
                {
                    CountryId = 1,
                    StateId = 2,
                    CityId = 2,
                    CityName = "Espirito Santo"
                }
            };
        }

        public static List<State> GetStatesBySelectedCountry(int countryId)
        {
            return States.List
                .Where(x => x.CountryId == countryId)
                .ToList();
        }

        public static List<City> GetCitiesBySelectedCountryAndState(int countryId, int stateId)
        {
            return Cities.List
                .Where(x => x.CountryId == countryId && 
                x.StateId == stateId)
                .ToList();
        }
    }
}

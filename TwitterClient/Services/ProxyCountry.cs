using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TwitterClient.Models;

namespace TwitterClient.Services
{
    class ProxyCountry : IProxyCountry
    {
        private RestClient _client;

        public ProxyCountry()
        {
            _client = new RestClient("https://restcountries.eu/rest/v2/all");
        }

        public List<Country.CountryObject> Countries()
        {
            var request = new RestRequest();
            var response = _client.Get<List<Country.CountryObject>>(request);
            return response.Data;
        }
    }
}

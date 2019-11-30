using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClient.Models;

namespace TwitterClient.Services
{
    interface IProxyCountry
    {
        List<Country.CountryObject> Countries();
    }
}

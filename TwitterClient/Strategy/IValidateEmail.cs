using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Strategy
{
    interface IValidateEmail
    {
        bool Validate(string email);
    }
}

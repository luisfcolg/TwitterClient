using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Strategy
{
    class ValidateEmailAt : IValidateEmail
    {
        public bool Validate(string email)
        {
            string[] parts = email.Split('@');

            if (parts.Length != 2)
                return false;

            if (parts[0].Length == 0 || parts[1].Length == 0)
                return false;

            return true;
        }
    }
}

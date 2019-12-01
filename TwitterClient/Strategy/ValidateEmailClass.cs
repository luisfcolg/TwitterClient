using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Strategy
{
    class ValidateEmailClass
    {
        private readonly IValidateEmail _strategy;
        private readonly string _email;

        public ValidateEmailClass(IValidateEmail strategy, string email)
        {
            _strategy = strategy;
            _email = email;
        }

        public bool Validate()
        {
            return _strategy.Validate(_email);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    abstract class OptionalFieldsDecorator : User
    {
        public abstract string GetPhone();
        public abstract string GetBio();
    }
}

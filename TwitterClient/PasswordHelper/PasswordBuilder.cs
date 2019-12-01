using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClient.Models;

namespace TwitterClient.PasswordHelper
{
    abstract class PasswordBuilder
    {
        protected Password _password;

        public Password GetPassword()
        {
            return _password;
        }

        public virtual void GenerateBytes() { }

        public virtual void GenerateHashBytes() { }

        public virtual void GenerateHashString() { }
    }
}

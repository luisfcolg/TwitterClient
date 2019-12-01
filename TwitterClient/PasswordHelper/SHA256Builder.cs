using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClient.Models;
using System.Security.Cryptography;

namespace TwitterClient.PasswordHelper
{
    class SHA256Builder : PasswordBuilder
    {
        public SHA256Builder(string password)
        {
            _password = new Password { PasswordString = password };
        }

        public override void GenerateBytes()
        {
            _password.Bytes = Encoding.Unicode.GetBytes(_password.PasswordString);
        }

        public override void GenerateHashBytes()
        {
            _password.HashBytes = HashAlgorithm.Create("SHA256").ComputeHash(_password.Bytes);
        }

        public override void GenerateHashString()
        {
            _password.HashString = Convert.ToBase64String(_password.HashBytes);
        }
    }
}

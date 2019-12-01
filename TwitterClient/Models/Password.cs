using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    class Password
    {
        public string PasswordString { get; set; }
        public byte[] Bytes { get; set; }
        public byte[] HashBytes { get; set; }
        public string HashString { get; set; }

        public Password()
        {

        }
    }
}

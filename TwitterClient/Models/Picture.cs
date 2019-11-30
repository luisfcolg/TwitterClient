using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
}

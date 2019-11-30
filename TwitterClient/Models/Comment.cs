﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int IdPost { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public DateTime Date { get; set; }
    }
}

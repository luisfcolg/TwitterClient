﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public Tweet Post { get; set; }
    }
}
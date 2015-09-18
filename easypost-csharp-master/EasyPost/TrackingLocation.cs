﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class TrackingLocation {
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikroszimulacio.Entities
{
    public class DeathProbabilities
    {
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public double P { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLL_Object
{
    public class BusLine
    {
        public int key { get; set; }//ID of the line.
        public List<int> stations { get; set; }//List of station IDs.
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UL_Processor_V2020
{
    public class Activity
    {
        public DateTime start;
        public DateTime end;

        public String type;
        public String szChildren;
        public String szAdults;

        public int children;
        public int teachers;
        public String line = "";
        //public List<String> children;
        //public List<String> teachers;
    }

    public class DetailActivity
    {
        public DateTime start;
        public DateTime end; 
        
        public String type;
        public String[] lineCols; 

        public List<String> childrenList = new List<string>();
        public List<String> adultsList = new List<string>();
    }

  
}

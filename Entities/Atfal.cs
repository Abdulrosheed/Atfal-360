using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atfal360.Entities
{
    public class Atfal
    {
        public int Id {get;set;}
       public string  Name {get;set;}
       public int Age {get;set;}
        public string Region {get;set;}
        public string State {get;set;}
        public string Dila {get;set;}
        public string Muqami {get;set;}
        public override string ToString()
        {
            return $"{Name}\t{Region}\t{State}\t{Dila}\t{Muqami}\t{Age}";
        }
    }

}
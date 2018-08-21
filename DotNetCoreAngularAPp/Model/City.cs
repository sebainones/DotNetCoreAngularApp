using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularApp.Model
{
    public class City
    {               
        public int CityId { get; set; }
        public string Name { get; set; }
        public int ZipCode  { get; set; }
    }
}

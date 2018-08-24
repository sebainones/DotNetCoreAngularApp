using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiUnsecure.Model
{
    public class Ticket
    {

        public long Id { get; set; }

        public string Concert { get; set; }

        public string Artist { get; set; }

        public bool Available { get; set; }
    }
}

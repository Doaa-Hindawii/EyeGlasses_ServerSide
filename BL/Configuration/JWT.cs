using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Configuration
{
    public class JWT
    {
        public string Secret_Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double Duration { get; set; }

    }
}

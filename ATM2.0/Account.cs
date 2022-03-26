using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM2._0
{
    public class Account
    {
        public int IdAccount { get; set; }

        public string Type { get; set; }

        public bool Status { get; set; }

        public decimal Balance { get; set; }

        public int IdUser { get; set; }
    }
}

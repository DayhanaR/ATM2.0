using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM2._0
{
    public class Transaction
    {
        public int IdTransaction { get; set; }

        public int IdUser { get; set; }

        public int IdAccount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}

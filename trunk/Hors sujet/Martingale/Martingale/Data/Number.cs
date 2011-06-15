using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Martingale
{
    class Number
    {
        public int N { get; set; }
        public int Occurrences { get; set; }

        public Number(int n)
        {
            N = n;
            Occurrences = 0;
        }
    }
}

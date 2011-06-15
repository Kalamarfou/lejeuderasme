using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Martingale
{
    public class ArrayListChiffres : ArrayList
    {
        public ArrayListChiffres()
        {
            for (int i = 0; i < 37; i++)
            {
                base.Add(new Number(i));
            }
        }

        public override int Add(object value)
        {
            foreach (Number n in this)
            {
                if (n.N == (int)value)
                {
                    n.Occurrences++;
                    return 1;
                }
            }
            return 0;
        }
    }
}

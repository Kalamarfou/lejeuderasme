using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace UltimateErasme.XP
{
    public class SuccesCollection : CollectionBase
    {
        public Succes this[SuccesEvents succes]
        {
            get
            {
                foreach (Succes item in List)
                {
                    if (item.SuccesEvent == succes)
                    {
                        return item;
                    }
                }
                return null;
            }
        }


        public void Add(Succes succes)
        {
            List.Add(succes);
        }
    }
}

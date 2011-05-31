using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimateErasme.MenuStates
{
    class OptionsFinales
    {
        private static OptionsFinales optionsFinales;

        public string Flou { get; set; }
        public string LangueDoublages { get; set; }

        public OptionsFinales()
        {
            LangueDoublages = "";
            Flou = "";
        }

        public static OptionsFinales getInstance()
        {
            if (optionsFinales == null)
            {
                optionsFinales = new OptionsFinales();
            }
            return optionsFinales;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace UltimateErasme.MenuStates
{
    class OptionMenu
    {
        public String option { get; set; }
        public Vector2 position { get; set; }
        public Boolean isSelected { get; set; }

        public OptionMenu(String option, Vector2 position, Boolean isSelected)
        {
            this.option = option;
            this.position = position;
            this.isSelected = isSelected;
        }
    }

    class ListeMenu
    {
        public ButtonMenu titreListe { get; set; }
        public List<OptionMenu> optionsListe { get; set; }
        public Color color { get; set; }
        public Color onSelectedColor { get; set; }

        public ListeMenu(ButtonMenu titreListe, List<OptionMenu> optionsListe, Color color, Color onSelectedColor)
        {
            this.titreListe = titreListe;
            this.optionsListe = optionsListe;
            this.color = color;
            this.onSelectedColor = onSelectedColor;
        }

        public void changeOption()
        {
            Boolean changeSelected = false;
            foreach (OptionMenu option in optionsListe)
            {
                if (changeSelected == true) {
                    option.isSelected = true;
                    changeSelected = false;
                } else if (option.isSelected)
                {
                    option.isSelected = false;
                    changeSelected = true;
                }
            }
            if (changeSelected == true)
            {
                optionsListe[0].isSelected = true;
            }
        }
    }
}

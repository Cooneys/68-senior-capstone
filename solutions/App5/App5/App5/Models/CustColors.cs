using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App5.Models
{
    class CustColors
    {
        public static Color grabColor(int i)
        {
            Color tempColor = Color.FromRgb(255, (55 + i), (55 + i));

            /*if ((i % 3) == 0)
            {
                tempColor = Color.FromRgb((255 - (i * 5)), 0, 0);
            }
            if ((i % 3) == 1)
            {
                tempColor = Color.FromRgb(0, (255 - (i * 5)), 0);
            }
            if ((i % 3) == 2)
            {
                tempColor = Color.FromRgb(0, 0, (255 - (i * 5)));
            }*/
            return tempColor;
        }
    }
}

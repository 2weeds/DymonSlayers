using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SgClient1
{
    public class PickupFactory
    {
        public Dictionary<string, Bitmap> dict = new Dictionary<string, Bitmap>();

        public Bitmap getPickup(Bitmap bitmap, string pickupName)
        {
            Bitmap pickUp = null;

            if (dict.ContainsKey(pickupName))
            {
                pickUp = dict[pickupName];
            }
            else
            {
                dict.Add(pickupName, bitmap);
                pickUp = dict[pickupName];
            }
            return pickUp;
        }
    }
}

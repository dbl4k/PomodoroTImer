using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroTimer.Utility
{
    public static class Desktop
    {
        public static double GetPrimaryWidth()
        {
            return GetBounds().Width;
        }

        public static double GetPrimaryHeight()
        {
            return GetBounds().Height;
        }

        public static Rectangle GetBounds(int screenIndex = -1)
        {
            if (screenIndex < 0)
                return Screen.PrimaryScreen.Bounds;
            else
                return Screen.AllScreens[screenIndex].Bounds;
        }


    }
}

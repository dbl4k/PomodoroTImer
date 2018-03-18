using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using PomodoroTimer.Enumerations;

namespace PomodoroTimer.Utility
{
    public static class Desktop
    {

        public static BoundaryTestResult IsWindowWithinPrimaryBounds(Window window)
        {
            var windowTop = window.Top;
            var windowLeft = window.Left;
            var windowBottom = window.Top + window.Height;
            var windowRight = window.Left = window.Width;

            // Outside tests.
            if (windowTop >= GetPrimaryBottom())
                return BoundaryTestResult.Outside;

            if (windowBottom <= GetPrimaryTop())
                return BoundaryTestResult.Outside;
       
            if (windowLeft >= GetPrimaryRight())
                return BoundaryTestResult.Outside;

            if (windowRight <= GetPrimaryLeft())
                return BoundaryTestResult.Outside;

            // Partially Inside Tests
            if (windowTop < GetPrimaryBottom() && windowBottom > GetPrimaryBottom())
                return BoundaryTestResult.PartiallyInside;

            if (windowTop < GetPrimaryTop() && windowBottom > GetPrimaryTop())
                return BoundaryTestResult.PartiallyInside;

            if (windowLeft < GetPrimaryLeft() && windowRight > GetPrimaryLeft())
                return BoundaryTestResult.PartiallyInside;

            if (windowLeft < GetPrimaryRight() && windowRight > GetPrimaryRight())
                return BoundaryTestResult.PartiallyInside;

            // Inside
            return BoundaryTestResult.Inside;
        }

        public static double GetPrimaryWidth()
        {
            return GetBounds().Width;
        }

        public static double GetPrimaryTop()
        {
            return GetBounds().Top;
        }

        public static double GetPrimaryLeft()
        {
            return GetBounds().Left;
        }

        public static double GetPrimaryRight()
        {
            return GetBounds().Right;
        }

        public static double GetPrimaryBottom()
        {
            return GetBounds().Right;
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

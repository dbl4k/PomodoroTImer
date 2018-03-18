using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace PomodoroTimer.Utility
{
    public static class Configuration
    {
        public static TimeSpan DefaultTimeSpan
        {
            get => Properties.Settings.Default.Timer_StartValue;
            set => Properties.Settings.Default.Timer_StartValue = value;
        }

        public static double MainWindowTop
        {
            get => Properties.Settings.Default.MainWindow_Top;
            set => Properties.Settings.Default.MainWindow_Top = value;
        }

        public static double MainWindowLeft
        {
            get => Properties.Settings.Default.MainWindow_Left;
            set => Properties.Settings.Default.MainWindow_Left = value;
        }

        public static void Save() => Properties.Settings.Default.Save();
    }
}

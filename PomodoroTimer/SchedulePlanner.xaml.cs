using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PomodoroTimer
{
    /// <summary>
    /// Interaction logic for SchedulePlanner.xaml
    /// </summary>
    public partial class SchedulePlanner : Window
    {
        public SchedulePlanner()
        {
            InitializeComponent();
        }
        
        #region "ui events"

        // draggable window support
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        #endregion

    }
}

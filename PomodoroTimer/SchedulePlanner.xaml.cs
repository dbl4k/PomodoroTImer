using PomodoroTimer.ViewModels;
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
using PomodoroTimer.Controllers;

namespace PomodoroTimer
{
    /// <summary>
    /// Interaction logic for SchedulePlanner.xaml
    /// </summary>
    public partial class SchedulePlanner : Window
    {
        private readonly ScheduleController _controller;

        #region "constructor(s)"

        public SchedulePlanner(ScheduleController controller) : this()
        {
            _controller = controller;
            lstScheduleItems.ItemsSource = _controller.GetObservableCollection();
        }

        public SchedulePlanner()
        {
            InitializeComponent();
        }

        #endregion

        #region "ui events"

        // draggable window support
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnStartSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (_controller.HasOpenScheduleItem())
            {
                _controller.CurrentScheduleItem = _controller.GetNextOpenScheduleItem();
            }

        }

        #endregion

        #region "methods"
        
        #endregion

    }
}

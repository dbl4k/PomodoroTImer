using PomodoroTimer.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
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
            _controller.InitializeNextOpenScheduleItem();
        }

        private void btnAddScheduleItem_Click(Button sender, RoutedEventArgs e)
        {
            var map = GetAddItemButtonValueMap();

            if (map.TryGetValue(sender.Name, out TimeSpan value))
                AddNewScheduleItemToEnd(value);
            else
                throw new InvalidEnumArgumentException($"no button/value map item exists for mapping ${sender}");
            
        }
        
        #endregion

        #region "methods"
        private void AddNewScheduleItemToEnd(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        private Dictionary<String, TimeSpan> GetAddItemButtonValueMap()
        {
            var map = new Dictionary<String, TimeSpan>
            {
                { btnAddItem15mins.Name, TimeSpan.FromMinutes(15) },
                { btnAddItem20mins.Name, TimeSpan.FromMinutes(20) },
                { btnAddItem25mins.Name, TimeSpan.FromMinutes(25) }
            };

            return map;
        }

        #endregion






    }
}

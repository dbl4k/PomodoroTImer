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
using PomodoroTimer.Models;

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

        private void btnAddScheduleItem_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                throw new InvalidEnumArgumentException($"Handler expects sender type of '${typeof(Button)}', but received '${sender.GetType()}'");    

            var map = GetAddItemButtonValueMap();

            if (map.TryGetValue(((Button)sender).Name, out TimeSpan value))
                AddNewScheduleItem(value);
            else
                throw new InvalidEnumArgumentException($"no button/value map item exists for mapping ${sender}");
            
        }

        private void btnRemoveItem_MouseDown(object sender, RoutedEventArgs e)
        {
            var image = (Image) sender;
            var parent = (StackPanel) image.Parent;
            var context = (ScheduleItem)parent.DataContext;

            _controller.RemoveScheduleItem(context);
        }

        #endregion

        #region "methods"
        private void AddNewScheduleItem(TimeSpan timeSpan)
        {
            String label = String.Empty;
            _controller.AddNewScheduleItem(label, timeSpan);
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

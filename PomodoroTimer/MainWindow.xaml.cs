using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PomodoroTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ManagedTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            RestoreConfiguredWindowPosition();

            timer = new ManagedTimer(
                (TimeSpan value) => SetContent(value)
            );

        }
        

        public void SetContent(TimeSpan value)
        {
            Action action = () => 
                txtTimeRemaining.Content =
                   value.Minutes.ToString("00")
                   + ":"
                   + value.Seconds.ToString("00");

            Dispatcher.Invoke(action);
            
        }
 
        #region "confguration getters/setters"

        private void RestoreConfiguredWindowPosition()
        {
            if (ConfiguredWindowPositionIsValid()) {
                this.Top = Properties.Settings.Default.MainWindow_Top;
                this.Left = Properties.Settings.Default.MainWindow_Left;
            }
        }

        private bool ConfiguredWindowPositionIsValid()
        {
            var top = Properties.Settings.Default.MainWindow_Top;
            var left = Properties.Settings.Default.MainWindow_Left;
            var width = this.Width;
            var height = this.Height;
            var right = left + width;
            var bottom = top + height;

            var bounds = Screen.PrimaryScreen.Bounds;
            var screenWidth = bounds.Width;
            var screenHeight = bounds.Height;

            return (right < screenWidth && bottom < screenHeight);
        }

        private void SaveConfiguredWindowPosition()
        {
            Properties.Settings.Default.MainWindow_Top = this.Top;
            Properties.Settings.Default.MainWindow_Left = this.Left;

            Properties.Settings.Default.Save();
        }

        #endregion

        #region "ui events"

        // draggable window support
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnPlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (timer.IsZero) {
                timer.Reset();
            }

            timer.Resume();
        }

        private void btnRefresh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Reset();
            SetContent(timer.TimeRemaining);
        }

        private void btnPause_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Pause();
        }

        private void btnReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveConfiguredWindowPosition();
        }

        #endregion
        
    }
}

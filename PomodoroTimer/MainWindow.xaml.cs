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
        private readonly Timer timer;
        private TimeSpan timeRemaining;
        private bool paused;

        public MainWindow()
        {
            InitializeComponent();

            timeRemaining = TimeSpan.FromSeconds(0);

            timer = new Timer()
            {
                Enabled = true,
                Interval = TimeSpan.FromSeconds(1).TotalMilliseconds
            };

            setTimer(TimeSpan.FromMinutes(25));

            timer.Elapsed += timer_Tick;

        }

        private void txtTimeRemaining_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!paused && !timerHasExpired())
            {
                pauseTimer();
            }
            else if (paused && !timerHasExpired())
            {
                resumeTimer();
            }
            else
            {
                setTimer(TimeSpan.FromMinutes(25));
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!paused && !timerHasExpired()) {
                timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(1));

                Action action = () => updateControl();
                Dispatcher.Invoke(action);

            }
        }
        
        public void updateControl() {
            txtTimeRemaining.Content = timeRemaining.Minutes.ToString("00") + ":" + timeRemaining.Seconds.ToString("00");
        }

        private void setTimer(TimeSpan time)
        {
            timeRemaining = time;
            resumeTimer();
        }

        private void resumeTimer()
        {
            paused = false;
        }

        private void pauseTimer()
        {
            paused = true;
        }

        private bool timerIsRunning()
        {
            return !timerHasExpired() || !paused;
        }

        private bool timerHasExpired()
        {
            return timeRemaining.TotalMilliseconds <= 0;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}

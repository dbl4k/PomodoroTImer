using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PomodoroTimer
{
    public class ManagedTimer
    {
        public delegate void OnTick(TimeSpan value);

        private OnTick _onTick;

        private readonly TimeSpan _configuredStartValue = Properties.Settings.Default.Timer_StartValue;

        private readonly Timer _timer;
        private bool _paused = true;
        private TimeSpan _timeRemaining;

        #region "constructor"

        public ManagedTimer(OnTick callback)
        {
            _timeRemaining = _configuredStartValue;

            _timer = new Timer()
            {
                Enabled = true,
                Interval = TimeSpan.FromSeconds(1).TotalMilliseconds
            };

            _onTick = callback;

            _timer.Elapsed += timer_Tick;
        }

        #endregion

        #region "timer events"

        private void timer_Tick(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                Decrement();
                _onTick(TimeRemaining);
            }
        }

        #endregion

        #region "properies"

        public bool IsRunning
        {
            get
            {
                return !IsPaused && !IsZero;
            }
        }

        public TimeSpan TimeRemaining
        {
            get
            {
                if (_timeRemaining.TotalSeconds < 0)
                {
                    return TimeSpan.FromSeconds(0);
                }
                else
                {
                    return _timeRemaining;
                }
            }

            set
            {
                _timeRemaining =
                    _timeRemaining.TotalSeconds > 0 ? value : TimeSpan.FromSeconds(0);
            }

        }

        public bool IsZero
        {
            get
            {
                return TimeRemaining.TotalSeconds <= 0;
            }
        }

        public bool IsPaused
        {
            get
            {
                return _paused;
            }
        }

        #endregion

        #region "methods"

        public void Decrement()
        {
            TimeRemaining =
                TimeRemaining.Subtract(TimeSpan.FromSeconds(1));
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }

        public void Reset()
        {
            TimeRemaining = _configuredStartValue;
        }

        #endregion
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PomodoroTimer.Controllers
{
    public class ManagedTimer
    {
        public event EventHandler TimesUp;
        private bool _timesUpEventFired = false;

        public delegate void OnTick(TimeSpan value);

        private readonly OnTick _onTick;
    
        private readonly TimeSpan _configuredStartValue = Properties.Settings.Default.Timer_StartValue;

        private readonly Timer _timer;
        private bool _paused = false;
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

            if (!IsPaused && IsZero)
            {
                if (!_timesUpEventFired)
                {
                    _timesUpEventFired = true;
                    TimesUp?.Invoke(this, e);
                }
                
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
                    value.TotalSeconds > 0 ? value : TimeSpan.FromSeconds(0);
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
            Reset(_configuredStartValue);
        }

        public void Reset(TimeSpan value)
        {
            _timer.Stop();
            TimeRemaining = value;
            _timesUpEventFired = false;
            _timer.Start();
        }

        #endregion
        
    }
}

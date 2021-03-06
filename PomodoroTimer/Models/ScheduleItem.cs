﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PomodoroTimer.Annotations;
using PomodoroTimer.Utility;

namespace PomodoroTimer.Models
{
    public class ScheduleItem : INotifyPropertyChanged
    {
        private static readonly TimeSpan DefaultTimeToSpend = Configuration.DefaultTimeSpan;

        #region "properties"

        private string _label = null;
        private TimeSpan _timeToSpend;
        private bool _completed = false;

        public String Label
        {
            get => _label;
            set
            {
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }

        public TimeSpan TimeToSpend
        {
            get => _timeToSpend;
            
            set
            {
                _timeToSpend = value;
                OnPropertyChanged(nameof(TimeToSpend));
            }
        }

        public bool Completed
        {
            get => _completed;
            set
            {
                _completed = value;
                OnPropertyChanged(nameof(Completed));
            }
        }

        #endregion

        #region "constructor(s)"

        public ScheduleItem() : this(String.Empty)
        {
        }

        public ScheduleItem(String label) : this(label, DefaultTimeToSpend)
        {
        }
        
        public ScheduleItem(String label, TimeSpan timeToSpend)
        {
            Label = label;
            TimeToSpend = timeToSpend;
        }

        #endregion

        #region "INotifyPropertyChanged implementation"
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomodoroTimer.Models
{
    public class ScheduleItem
    {
        private static readonly TimeSpan DEFAULT_TIME_TO_SPEND = TimeSpan.FromMinutes(25);

        #region "properties"
        
        public String Label { get; set; }
        public TimeSpan TimeToSpend { get; set; }
        public bool Completed { get; set; } = false;

        #endregion

        #region "constructor(s)"

        public ScheduleItem() : this(String.Empty)
        {
        }

        public ScheduleItem(String label) : this(label, DEFAULT_TIME_TO_SPEND)
        {
        }
        
        public ScheduleItem(String label, TimeSpan timeToSpend)
        {
            Label = label;
            TimeToSpend = timeToSpend;
        }

        #endregion
    }
}

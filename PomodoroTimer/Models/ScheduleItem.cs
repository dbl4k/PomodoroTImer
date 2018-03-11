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
        public String Label { get; set; }
        public TimeSpan TimeToSpend { get; set; }
        public bool Completed { get; set; }
    }
}

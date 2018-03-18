using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using PomodoroTimer.Controllers;
using PomodoroTimer.Models;

namespace PomodoroTimer.ViewModels
{
    public class ScheduleItems : ObservableCollection<ScheduleItem>
    {
        public ScheduleController _controller;
        public Schedule _schedule;

        public ScheduleItems() : base()
        {
            // only used by designer, not 
        }

        public ScheduleItems(ScheduleController controller) : base()
        {
            _controller = controller;
            _schedule = _controller.Schedule;

            _schedule.Items.ForEach((item) => Add(item));
        }

        public new void Remove(ScheduleItem scheduleItem)
        {
            base.Remove(scheduleItem);

            if (_schedule.Items.Contains(scheduleItem))
            {
                _schedule.Items.Remove(scheduleItem);
            }
        }

        public new void Add(ScheduleItem scheduleItem)
        {
            base.Add(scheduleItem);

            if (!_schedule.Items.Contains(scheduleItem))
            {
                _schedule.Items.Add(scheduleItem);
            }
        }

    }
}

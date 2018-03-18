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

        /// <summary>
        /// Only used by designer datatemplate, not for runtime.
        /// </summary>
        public ScheduleItems() : base()
        {
        }

        /// <summary>
        /// Only used by designer datatemplate, not for runtime.
        /// </summary>
        /// <param name="items">number of datatemplate items to create.</param>
        public ScheduleItems(int items) : base()
        {
            for (var i = 1; i <= items; i++)
            {
                this.Add(new ScheduleItem(String.Format("Activity ", i)));
            }
        }

        public ScheduleItems(ScheduleController controller) : base()
        {
            _controller = controller;
            _schedule = _controller.Schedule;

            _schedule.Items.ForEach((item) => Add(item));
        }

        public void Remove(ScheduleItem scheduleItem)
        {
            base.Remove(scheduleItem);

            if (_schedule != null && _schedule.Items.Contains(scheduleItem))
            {
                _schedule.Items.Remove(scheduleItem);
            }
        }

        public void Add(ScheduleItem scheduleItem)
        {
            base.Add(scheduleItem);

            if (_schedule != null && !_schedule.Items.Contains(scheduleItem))
            {
                _schedule.Items.Add(scheduleItem);
            }
        }

    }
}

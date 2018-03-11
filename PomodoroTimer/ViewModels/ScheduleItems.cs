using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PomodoroTimer.Controllers;
using PomodoroTimer.Models;

namespace PomodoroTimer.ViewModels
{
    public class ScheduleItems : ObservableCollection<ScheduleItem>
    {
        public ScheduleItems() : base()
        {
        }

        public ScheduleItems(int items) : base()
        {
            for (var i = 1; i <= items; i++)
            {
                this.Add(new ScheduleItem(String.Format("Activity ", i)));
            }
        }


        public ScheduleItems(ScheduleController controller) : base()
        {
            controller.Schedule.Items.ForEach((item) => Add(item));
        }
    }
}

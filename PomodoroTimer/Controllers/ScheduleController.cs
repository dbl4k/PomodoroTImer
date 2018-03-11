using PomodoroTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PomodoroTimer.ViewModels;

namespace PomodoroTimer.Controllers
{
    public class ScheduleController
    {

        private static String _fDefaultItemName = "Item {0}";

        #region "fields"

        public Schedule Schedule;
        
        #endregion

        #region "properties"

        public ScheduleItem CurrentScheduleItem { get; set; }

        #endregion

        #region "constructor(s)"

        public ScheduleController() : this(1) 
        {
        }

        public ScheduleController(int items)
        {
            Schedule = new Schedule();
            Schedule.Items = new List<ScheduleItem>();

            for(var i=1; i <= items; i++)
            {
                var name = GetDefaultItemName(i);
                Schedule.Items.Add(new ScheduleItem(name));
            }
        }
        
        #endregion

        #region "methods"

        private string GetDefaultItemName(int i)
        {
            return String.Format(_fDefaultItemName, i);
        }

        public ScheduleItems GetObservableCollection()
        {
            return new ScheduleItems(this);
        }

        public bool HasOpenScheduleItem() => !(GetNextOpenScheduleItem() == null);

        public ScheduleItem GetNextOpenScheduleItem()
        {
            ScheduleItem result = null;

            foreach (ScheduleItem item in Schedule.Items)
            {
                if (item.Completed.Equals(false))
                {
                    result = item;
                    break;
                }
            }

            return result;
        }

        #endregion

    }
}

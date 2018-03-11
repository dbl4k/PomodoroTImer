using PomodoroTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Controllers
{
    public class ScheduleController
    {

        private static String F_DEFAULT_ITEM_NAME = "Item {0}";

        #region "fields"

        public Schedule Schedule;
        
        #endregion

        #region "properties"

        // none yet..

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
            return String.Format(F_DEFAULT_ITEM_NAME, i);
        }

        #endregion

    }
}

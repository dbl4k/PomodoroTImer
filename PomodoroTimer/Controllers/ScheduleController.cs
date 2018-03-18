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

        private int _nextNewItemId = 0;

        public Schedule Schedule; // non-observable
        public ScheduleItems ScheduleItems; // observable view collection
        public ManagedTimer Timer;
        
        #endregion

        #region "properties"

        public ScheduleItem CurrentScheduleItem { get; set; }

        #endregion

        #region "constructor(s)"

        public ScheduleController(ManagedTimer timer) : this(timer, 1) 
        {
        }

        public ScheduleController(ManagedTimer timer, int items)
        {
            Timer = timer;

            Schedule = new Schedule();
            Schedule.Items = new List<ScheduleItem>();

            for(var i=1; i <= items; i++)
            {
                var name = GetDefaultItemName(GetNextNewItemId());
                Schedule.Items.Add(new ScheduleItem(name));
            }
        }
        
        #endregion

        #region "methods"

        public int GetNextNewItemId()
        {
            if (ScheduleItems != null)
                _nextNewItemId = ScheduleItems.Count;

            return _nextNewItemId += 1;
        }

        private string GetDefaultItemName(int itemId)
        {
            return String.Format(_fDefaultItemName, itemId);
        }

        public ScheduleItems GetObservableCollection()
        {
            return ScheduleItems ?? (ScheduleItems = new ScheduleItems(this));
        }

        public bool HasOpenScheduleItem() => GetNextOpenScheduleItem() != null;

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

        public void InitializeNextOpenScheduleItem()
        {
            if (HasOpenScheduleItem())
            {
                CurrentScheduleItem = GetNextOpenScheduleItem();
                Timer.Reset(CurrentScheduleItem.TimeToSpend);
            }
        }

        public void ClearCurrentScheduleItem()
        {
            CurrentScheduleItem = null;
        }

        internal void RemoveScheduleItem(ScheduleItem scheduleItem)
        {
            if (ScheduleItems.Contains(scheduleItem))
            {
                ScheduleItems.Remove(scheduleItem);
            }
        }

        public void CompleteCurrentScheduleItem()
        {
            if (CurrentScheduleItem != null)
            {
                CurrentScheduleItem.Completed = true;
            }
        }

        internal void AddNewScheduleItem(string label, TimeSpan timeToSpend)
        {
            if (String.IsNullOrEmpty(label))
                label = GetDefaultItemName(GetNextNewItemId());

            ScheduleItems.Add(new ScheduleItem(label, timeToSpend));
        }
        
        #endregion

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using PomodoroTimer.Controllers;
using PomodoroTimer.Enumerations;
using PomodoroTimer.Models;
using PomodoroTimer.Properties;
using PomodoroTimer.Utility;
using PomodoroTimer.ViewModels;
using Application = System.Windows.Application;

namespace PomodoroTimer
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ManagedTimer _timer;
        private ScheduleController _scheduleController;
        private SchedulePlanner _planner;

        public MainWindow()
        {
            InitializeComponent();

            RestoreConfiguredWindowPosition();

            SetupTimer();
        }

        public void SetupTimer()
        {
            _timer = new ManagedTimer(
                UpdateTimeRemainingLabel
            );

            _timer.OnTimerReachedZero += OnTimerReachedZero;
        }

        public void UpdateTimeRemainingLabel(TimeSpan value)
        {
            var timeLabelText = $"{value.Minutes:00}:{value.Seconds:00}";
            var scheduleItemText = HasCurrentScheduleItem() ? GetCurrentScheduleItem().Label : String.Empty;

            List<Action> actions = new List<Action>
            {
                () => txtTimeRemaining.Content = timeLabelText,
                () => txtCurrentScheduleItemLabel.Text = scheduleItemText
            };

            actions.ForEach((n) => Dispatcher.Invoke(n));
        }
        
        public void OnTimerReachedZero(object timer, EventArgs e)
        {
            _scheduleController.CompleteCurrentScheduleItem();

            Audio.PlayOnce(Utility.Resources.Audio.TimerBeep);

            if (_scheduleController.HasOpenScheduleItem())
                _scheduleController.InitializeNextOpenScheduleItem();
            else
                _scheduleController.ClearCurrentScheduleItem();
            
        }

        #region "confguration getters/setters"

        private void RestoreConfiguredWindowPosition()
        {
            if (Desktop.IsWindowWithinPrimaryBounds(this) == BoundaryTestResult.Inside)
            {
                Top = Configuration.MainWindowTop;
                Left = Configuration.MainWindowLeft;
            }
        }
        
        private void SaveConfiguredWindowPosition()
        {
            Configuration.MainWindowTop = Top;
            Configuration.MainWindowLeft = Left;

            Configuration.Save();
        }

        #endregion

        #region "ui events"

        // draggable window support
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void btnPlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_timer.IsZero) _timer.Reset();

            _timer.Resume();
        }

        private void btnRefresh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (HasCurrentScheduleItem())
                _timer.Reset(GetCurrentScheduleItem().TimeToSpend);
            else
                _timer.Reset();
            
            UpdateTimeRemainingLabel(_timer.TimeRemaining);
        }

        private void btnPause_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _timer.Pause();
        }

        private void btnReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSchedulePlanner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_scheduleController == null)
            {
                _scheduleController = new ScheduleController(_timer, 3);
            }

            if (_planner == null)
            {
                _planner = 
                    new SchedulePlanner(_scheduleController);
            }
            
            _planner.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveConfiguredWindowPosition();
        }

        #endregion

        #region "methods"

        private bool HasCurrentScheduleItem()
        {
            return GetCurrentScheduleItem() != null;
        }

        private ScheduleItem GetCurrentScheduleItem()
        {
            return _scheduleController?.CurrentScheduleItem;
        }

        #endregion
    }
}
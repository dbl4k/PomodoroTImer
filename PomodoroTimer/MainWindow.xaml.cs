﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using PomodoroTimer.Controllers;
using PomodoroTimer.Models;
using PomodoroTimer.Properties;
using PomodoroTimer.ViewModels;
using Application = System.Windows.Application;

namespace PomodoroTimer
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ManagedTimer _timer;
        private ScheduleController _scheduleController;
        private SchedulePlanner _planner;

        public MainWindow()
        {
            InitializeComponent();

            RestoreConfiguredWindowPosition();

            _timer = new ManagedTimer(
                UpdateTimeRemainingLabel
            );
        }
        
        public void UpdateTimeRemainingLabel(TimeSpan value)
        {
            void Action() =>
                txtTimeRemaining.Content = 
                    $"{value.Minutes:00}:{value.Seconds:00}";
            
            Dispatcher.Invoke(Action);
        }

        #region "confguration getters/setters"

        private void RestoreConfiguredWindowPosition()
        {
            if (ConfiguredWindowPositionIsValid())
            {
                Top = Settings.Default.MainWindow_Top;
                Left = Settings.Default.MainWindow_Left;
            }
        }

        private bool ConfiguredWindowPositionIsValid()
        {
            var top = Settings.Default.MainWindow_Top;
            var left = Settings.Default.MainWindow_Left;
            var width = Width;
            var height = Height;
            var right = left + width;
            var bottom = top + height;

            var bounds = Screen.PrimaryScreen.Bounds;
            var screenWidth = bounds.Width;
            var screenHeight = bounds.Height;

            return right < screenWidth && bottom < screenHeight;
        }

        private void SaveConfiguredWindowPosition()
        {
            Settings.Default.MainWindow_Top = Top;
            Settings.Default.MainWindow_Left = Left;

            Settings.Default.Save();
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
                _scheduleController = new ScheduleController(9);
            }

            if (_planner == null)
            {
                var observable = new ScheduleItems(_scheduleController);
                _planner = new SchedulePlanner(observable);
            }
            
            _planner.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveConfiguredWindowPosition();
        }

        #endregion
    }
}
﻿using Eddi;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace EddiMissionMonitor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ConfigurationWindow : UserControl
    {
        private MissionMonitor missionMonitor()
        {
            return (MissionMonitor)EDDI.Instance.ObtainMonitor("Mission monitor");
        }

        public ConfigurationWindow()
        {
            InitializeComponent();

            missionsData.ItemsSource = missionMonitor()?.missions;

            MissionMonitorConfiguration configuration = MissionMonitorConfiguration.FromFile();
            missionWarningInt.Text = configuration.missionWarning?.ToString(CultureInfo.InvariantCulture);
        }

        private void missionsUpdated(object sender, DataTransferEventArgs e)
        {
            // Update the mission monitor's information
            missionMonitor()?.writeMissions();
        }

        private void warningChanged(object sender, TextChangedEventArgs e)
        {
            MissionMonitorConfiguration configuration = MissionMonitorConfiguration.FromFile();
            try
            {
                int? warning = string.IsNullOrWhiteSpace(missionWarningInt.Text) ? 60 : Convert.ToInt32(missionWarningInt.Text, CultureInfo.InvariantCulture);
                missionMonitor().missionWarning = warning;
                configuration.missionWarning = warning;
                configuration.ToFile();
            }
            catch
            {
                // Bad user input; ignore it
            }
        }

        private void setRoute(object sender, RoutedEventArgs e)
        {

        }

        private void updateRoute(object sender, RoutedEventArgs e)
        {

        }

        private void clearRoute(object sender, RoutedEventArgs e)
        {

        }

        private void EnsureValidInteger(object sender, TextCompositionEventArgs e)
        {
            // Match valid characters
            Regex regex = new Regex(@"[0-9]");
            // Swallow the character doesn't match the regex
            e.Handled = !regex.IsMatch(e.Text);
        }

    }
}

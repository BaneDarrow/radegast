﻿/**
 * Radegast Metaverse Client
 * Copyright(c) 2021, Sjofn, LLC
 * All rights reserved.
 *  
 * Radegast is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program.If not, see<https://www.gnu.org/licenses/>.
 */

using System;

namespace Radegast
{
    public partial class ntfRegionRestart : Notification
    {
        private RadegastInstance instance;
        private string RegionName;
        private int CountdownSeconds;
        private System.Timers.Timer CountdownTimer;

        public ntfRegionRestart(RadegastInstance instance, string region_name, int countdown_seconds)
            : base(NotificationType.RegionRestart)
        {
            InitializeComponent();
            this.instance = instance;
            this.RegionName = region_name;
            this.CountdownSeconds = countdown_seconds;

            txtHead.BackColor = instance.MainForm.NotificationBackground;
            txtCountdownLabel.BackColor = instance.MainForm.NotificationBackground;
            txtCountdown.BackColor = instance.MainForm.NotificationBackground;
            btnHome.Focus();

            CountdownTimer = new System.Timers.Timer();
            CountdownTimer.Interval = 1000; // 1s
            CountdownTimer.Elapsed += OnCountdownTimerEvent;

            GUI.GuiHelpers.ApplyGuiFixes(this);
        }

        private void OnCountdownTimerEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                --CountdownSeconds;
                var ts = TimeSpan.FromSeconds(CountdownSeconds);
                txtCountdown.Text = ts.ToString(@"mm:ss");
            }));
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            instance.Client.Self.RequestTeleport(OpenMetaverse.UUID.Zero);
            instance.MainForm.RemoveNotification(this);
        }

        private void btnElsewhere_Click(object sender, EventArgs e)
        {
            // idk, this is silly and Second Life specific.
            instance.Client.Self.Teleport("Hippo Hollow", new OpenMetaverse.Vector3(180, 205, 44));
            instance.MainForm.RemoveNotification(this);
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            instance.MainForm.RemoveNotification(this);
        }
    }
}

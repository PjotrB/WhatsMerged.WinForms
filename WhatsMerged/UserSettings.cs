using System.Collections.Generic;
using WhatsMerged.Base.Models;

namespace WhatsMerged.WinForms
{
    public class UserSettings
    {
        public string[] Folders { get; set; }
        public Dictionary<string, Settings> SettingsPerProject { get; set; }

        public UserSettings(string[] folders)
        {
            Folders = folders;
            SettingsPerProject = new Dictionary<string, Settings>();
        }
    }
}
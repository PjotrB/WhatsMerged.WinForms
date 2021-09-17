using System;
using System.Windows.Forms;
using WhatsMerged.Base.Helpers;

namespace WhatsMerged.WinForms.Forms
{
    public partial class UserSettingsForm : Form
    {
        public string FolderText;

        public string TitleText;

        public bool HideCancelButton;

        public UserSettingsForm()
        {
            InitializeComponent();
        }

        private void UserSettingsForm_Load(object sender, EventArgs e)
        {
            txtFolders.Text = FolderText;
            txtFolders.Select(0, 0);

            Text = TitleText.HasValue() ? TitleText : "Set Folders";

            if (HideCancelButton)
            {
                btnCancel.Visible = false;
                (btnOK.Left, btnCancel.Left) = (btnCancel.Left, btnOK.Left);    // Swap Left-pos of both buttons using Tuple Assignment
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            FolderText = txtFolders.Text;

            if (HideCancelButton)
            {
                (btnOK.Left, btnCancel.Left) = (btnCancel.Left, btnOK.Left);    // Restore Left-pos of both buttons using Tuple Assignment
                btnCancel.Visible = true;
            }
        }
    }
}
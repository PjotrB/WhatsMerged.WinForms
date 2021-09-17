using System.Collections.Generic;
using System.Windows.Forms;

namespace WhatsMerged.WinForms.Forms
{
    public partial class CheckboxListForm : Form
    {
        public string Intro
        {
            get => lblIntro.Text;
            set => lblIntro.Text = value;
        }

        public List<string> Items { get; set; }

        public CheckboxListForm()
        {
            InitializeComponent();
        }

        private void MessageForm_Load(object sender, System.EventArgs e)
        {
            checkedListBox1.Items.Clear();
            foreach (var item in Items)
                checkedListBox1.Items.Add(item);

            var factor = Utils.GetScalingFactor();

            checkedListBox1.Height = Utils.Scale(8 + 18 * Items.Count, factor);

            // Re-position checkedListBox1 under variable-sized intro text
            checkedListBox1.Top = lblIntro.Top + lblIntro.Height + Utils.Scale(16, factor);

            // Adjust Form height to show all of checkedListBox1 + space below for button.
            Height = checkedListBox1.Top + checkedListBox1.Height + Utils.Scale(84, factor);

            // Adjust Form width to show all of variable-sized intro text, with margins left + right the same.
            Width = lblIntro.Left + lblIntro.Width + lblIntro.Left * 2;

            // Size takes its value from the Height and Width that we have just set
            MinimumSize = Size;
        }

        private void BtnOK_Click(object sender, System.EventArgs e)
        {
            Items = new List<string>();

            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                Items.Add(checkedListBox1.CheckedItems[i].ToString());
        }
    }
}
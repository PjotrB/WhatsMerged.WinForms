using System.Collections.Generic;
using System.Windows.Forms;

namespace WhatsMerged.WinForms.Forms
{
    public partial class CheckboxListForm : Form
    {
        public string Intro
        {
            get { return lblIntro.Text; }
            set { lblIntro.Text = value; }
        }

        public List<string> Items { get; set; }

        private void SetFormSize()
        {
            var size = lblIntro.Size;
            Size = new System.Drawing.Size(size.Width + 50, size.Height + 100);
        }

        public CheckboxListForm()
        {
            InitializeComponent();
        }

        private void MessageForm_Load(object sender, System.EventArgs e)
        {
            checkedListBox1.Items.Clear();

            foreach (var item in Items)
                checkedListBox1.Items.Add(item);

            checkedListBox1.Height = 8 + 18 * Items.Count;

            // Re-position checkedListBox1 under variable-sized intro text
            checkedListBox1.Top = lblIntro.Top + lblIntro.Height + 16;

            // Adjust Form height to show all of checkedListBox1 + space below for button.
            Height = checkedListBox1.Top + checkedListBox1.Height + 84;

            // Adjust Form width to show all of variable-sized intro text, with margins left + right the same.
            Width = lblIntro.Left + lblIntro.Width + lblIntro.Left * 2;
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
using System.Windows.Forms;

namespace WhatsMerged.WinForms.Forms
{
    public partial class MessageForm : Form
    {
        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; SetFormSize(); }
        }

        private void SetFormSize()
        {
            var size = lblMessage.Size;
            Size = new System.Drawing.Size(size.Width + 50, size.Height + 100);
        }

        public MessageForm()
        {
            InitializeComponent();
        }

        private void MessageForm_Load(object sender, System.EventArgs e)
        {
            Text = "Message";
        }
    }
}
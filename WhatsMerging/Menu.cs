using System;
using System.Windows.Forms;

namespace WhatsMerged.WinForms
{
    public static class Menu
    {
        public static ContextMenuStrip WithItem(this ContextMenuStrip menu, string text, Action action)
        {
            menu.Items.Add(CreateItem(text, action));
            return menu;
        }

        public static ContextMenuStrip WithSeparator(this ContextMenuStrip menu)
        {
            menu.Items.Add(new ToolStripSeparator());
            return menu;
        }

        public static ToolStripItem CreateItem(string text, Action action)
        {
            var menuItem = new ToolStripMenuItem { Text = text };
            menuItem.Click += (obj, eventArgs) => action();
            return menuItem;
        }
    }
}

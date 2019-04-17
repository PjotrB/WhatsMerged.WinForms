using System;
using System.Windows.Forms;

namespace WhatsMerged.WinForms
{
    public static class Menu
    {
        public static ContextMenuStrip WithItem(this ContextMenuStrip menu, string text, Action action)
        {
            var item = CreateItem(text, action);
            menu.Items.Add(item);
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
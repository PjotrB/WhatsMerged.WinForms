using System.Windows.Forms;

namespace WhatsMerged.WinForms
{
    public static class Utils
    {
        /// <summary>
        /// Empty the Windows event queue, processing all Paint events and ignoring/discarding all others.
        /// </summary>
        public static void PaintNow()
        {
            // Inspired by https://www.codeproject.com/Tips/513764/Repainting-WinForms-windows-safely-inside-a-proces

            //MessageFilter registration
            Application.AddMessageFilter(PaintMessageFilter.Instance);
            //Process messages in the queue
            Application.DoEvents();
            //MessageFilter desregistration
            Application.RemoveMessageFilter(PaintMessageFilter.Instance);
        }

        /// <summary>
        /// Private class for use in PaintNow()
        /// </summary>
        private class PaintMessageFilter : IMessageFilter
        {
            static public IMessageFilter Instance = new PaintMessageFilter();

            public bool PreFilterMessage(ref Message m)
            {
                return (m.Msg != 0x000F); //WM_PAINT -> we only let WM_PAINT messages through
            }
        }

        /// <summary>
        /// If strToRemove exists at the end of s, then chop it off. Else return s as-is.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="strToRemove"></param>
        /// <returns></returns>
        public static string RemoveAtEnd(this string s, string strToRemove)
        {
            return s.EndsWith(strToRemove)
                ? s.Substring(0, s.Length - strToRemove.Length)
                : s;
        }
    }
}
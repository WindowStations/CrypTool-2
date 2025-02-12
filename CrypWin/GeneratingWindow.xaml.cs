﻿using CrypTool.PluginBase.Attributes;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace CrypTool.CrypWin
{

    /// <summary>
    /// Interaction logic for GeneratingWindow.xaml
    /// </summary>
    [Localization("CrypTool.CrypWin.Properties.Resources")]
    public partial class GeneratingWindow : Window
    {
        private const int GwlStyle = -16;
        private const int WsSysmenu = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public GeneratingWindow()
        {
            InitializeComponent();
            SourceInitialized += GeneratingWindowSourceInitialized;
        }

        /// <summary>
        /// Hide close button of this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneratingWindowSourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper wih = new WindowInteropHelper(this);
            int style = GetWindowLong(wih.Handle, GwlStyle);
            SetWindowLong(wih.Handle, GwlStyle, style & ~WsSysmenu);
        }

        /// <summary>
        /// Sets the message of this window
        /// </summary>
        /// <param name="msg"></param>
        public void SetMessage(string msg)
        {
            Message.Content = msg;
        }
    }
}

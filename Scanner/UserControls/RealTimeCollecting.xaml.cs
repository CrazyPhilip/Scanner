using Emgu.CV;
using Emgu.CV.Structure;
using JiebaNet.Segmenter;
using Microsoft.Win32;
using Scanner.Model;
using Scanner.Threads;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Scanner.UserControls
{
    /// <summary>
    /// RealTimeCollecting.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimeCollecting : UserControl
    {
        public RealTimeCollecting()
        {
            InitializeComponent();
        }

    }
}

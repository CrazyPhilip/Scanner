using Emgu.CV;
using Emgu.CV.Structure;
using JiebaNet.Segmenter;
using Microsoft.Win32;
using Scanner.Model;
using Scanner.Threads;
using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Tesseract;
using Scanner.ViewModels;

namespace Scanner.UserControls
{
    /// <summary>
    /// Single.xaml 的交互逻辑
    /// </summary>
    public partial class Single : UserControl
    {
        public Single()
        {
            InitializeComponent();
            this.DataContext = new SingleViewModel();
        }
    }
}

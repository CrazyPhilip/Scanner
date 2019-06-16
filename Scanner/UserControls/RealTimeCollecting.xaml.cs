using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using JiebaNet.Segmenter;
using Microsoft.Win32;
using Scanner.Model;
using Scanner.Threads;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        public int selectedDeviceIndex = 0;
        String path = @"../../../collect_shots/";
        int count = 1;  //记录拍照数量

        public RealTimeCollecting()
        {
            InitializeComponent();
        }

        private void Reco_Click(object sender, RoutedEventArgs e)
        {

        }

        private void On_cam_Click(object sender, RoutedEventArgs e)
        {
            if (!videoSourcePlayer1.IsRunning)
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                selectedDeviceIndex = 0;
                videoSource = new VideoCaptureDevice(videoDevices[selectedDeviceIndex].MonikerString);//连接摄像头。
                videoSource.VideoResolution = videoSource.VideoCapabilities[selectedDeviceIndex];
                videoSourcePlayer1.VideoSource = videoSource;
                // set NewFrame event handler
                videoSourcePlayer1.Start();
            }
            else
            {
                videoSourcePlayer1.Stop();
                videoSourcePlayer1.Dispose();
            }
        }

        private void Shot_Click(object sender, RoutedEventArgs e)
        {
            if (videoSource == null)
                return;
            Bitmap bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            string fileName = "shot" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".jpg";

            bitmap.Save(path + fileName, ImageFormat.Jpeg);

            /*
            if ()
            {
                switch (count)
                {

                } 
            }
            else
            {
                MessageBox.Show("识别失败！请重新采集这一张！", "采集");
            }*/

            bitmap.Dispose();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

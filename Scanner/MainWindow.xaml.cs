using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Scanner.Model;
using Tesseract;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Scanner.Threads;
using JiebaNet.Segmenter;
using System.Windows.Controls;

namespace Scanner
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Photo photo = new Photo();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog { Filter = "图像文件|*.jpg;*.png;*.jpeg;*.bmp;*.gif|所有文件|*.*" };

            if ((bool)openfiledialog.ShowDialog())
            {
                Original.Source = new BitmapImage(new Uri(openfiledialog.FileName));
                photo.OriginalPhoto = CvInvoke.Imread(openfiledialog.FileName);  //读取图像
            }
            
        }

        private void SharpenButton_Click(object sender, RoutedEventArgs e)
        {
            Mat sharpened = new Mat();
            bool s = sharpen(photo.OriginalPhoto, sharpened);

            if (s)
            {
                photo.SharpenedPhoto = sharpened;
                Sharpened.Source = BitmapSourceConvert.ToBitmapSource(sharpened);
            }
        }
        
        //此部分代码功能是锐化图像，本质上是 扫描图像并访问相邻像素
        //sharpened_pixel=5*current-left-right-up-down
        byte saturateCast(double inValue)
        {
            /*
            if (inValue < 0)
                return 0;
            else if (inValue > 255)
                return 255;
            else
                return (byte)inValue;
                */

            if (inValue < 100)
                return 0;
            else if (inValue >= 100)
                return 255;
            else
                return (byte)inValue;
        }

        /*
        bool sharpen(Mat inImg, Mat outImg)
        {
            int nchannel = inImg.NumberOfChannels;
            int nrows = inImg.Rows;
            int ncols = inImg.Cols;

            if (nchannel == 1)
            {
                var tempImg = inImg.ToImage<Gray, byte>();
                var tempImg2 = tempImg.Clone();
                for (int i = 1; i < nrows - 1; ++i)
                {
                    for (int j = 1; j < ncols - 1; ++j)
                    {
                        tempImg2.Data[i, j, 0] = saturateCast(5 * tempImg.Data[i, j, 0] - tempImg.Data[i - 1, j, 0] - tempImg.Data[i, j - 1, 0] -
                        tempImg2.Data[i + 1, j, 0] - tempImg.Data[i, j + 1, 0]);
                    }
                }

                for (int i = 0; i < nrows; ++i)
                {
                    tempImg2.Data[i, 0, 0] = 0;
                    tempImg2.Data[i, ncols - 1, 0] = 0;
                }

                for (int j = 0; j < ncols; ++j)
                {
                    tempImg2.Data[0, j, 0] = 0;
                    tempImg2.Data[nrows - 1, j, 0] = 0;
                }
                tempImg2.Mat.CopyTo(outImg);
            }

            else if (nchannel == 3)
            {
                var tempImg = inImg.ToImage<Bgr, byte>();
                var tempImg2 = tempImg.Clone();
                for (int i = 1; i < nrows - 1; ++i)
                {
                    for (int j = 1; j < ncols - 1; ++j)
                    {
                        for (int channel = 0; channel < 3; channel++)
                        {

                            tempImg2.Data[i, j, channel] = saturateCast(5 * tempImg.Data[i, j, channel] - tempImg.Data[i - 1, j, channel] - tempImg.Data[i, j - 1, channel] -

                                tempImg.Data[i + 1, j, channel] - tempImg.Data[i, j + 1, channel]);
                        }
                    }
                }

                for (int i = 0; i < nrows; ++i)
                {
                    for (int channel = 0; channel < 3; channel++)
                    {
                        tempImg2.Data[i, 0, channel] = 0;

                        tempImg2.Data[i, ncols - 1, channel] = 0;
                    }
                }

                for (int j = 0; j < ncols; ++j)
                {
                    for (int channel = 0; channel < 3; channel++)
                    {

                        tempImg2.Data[0, j, channel] = 0;

                        tempImg2.Data[nrows - 1, j, channel] = 0;
                    }
                }

                tempImg2.Mat.CopyTo(outImg);
            }

            else
            {
                return false;
            }

            return true;
        }
        */

        bool sharpen(Mat inImg, Mat outImg)
        {
            int nchannel = inImg.NumberOfChannels;
            int nrows = inImg.Rows;
            int ncols = inImg.Cols;

            if (nchannel == 1)
            {
                var tempImg = inImg.ToImage<Gray, byte>();
                var tempImg2 = tempImg.Clone();
                for (int i = 1; i < nrows - 1; ++i)
                {
                    for (int j = 1; j < ncols - 1; ++j)
                    {
                        tempImg2.Data[i, j, 0] = saturateCast(tempImg.Data[i, j, 0]);
                    }
                }

                for (int i = 0; i < nrows; ++i)
                {
                    tempImg2.Data[i, 0, 0] = 0;
                    tempImg2.Data[i, ncols - 1, 0] = 0;
                }

                for (int j = 0; j < ncols; ++j)
                {
                    tempImg2.Data[0, j, 0] = 0;
                    tempImg2.Data[nrows - 1, j, 0] = 0;
                }
                tempImg2.Mat.CopyTo(outImg);

                tempImg.Dispose();
                tempImg2.Dispose();
            }

            else if (nchannel == 3)
            {
                var tempImg = inImg.ToImage<Bgr, byte>();
                var tempImg2 = tempImg.Clone();
                for (int i = 1; i < nrows - 1; ++i)
                {
                    for (int j = 1; j < ncols - 1; ++j)
                    {
                        for (int channel = 0; channel < 3; channel++)
                        {

                            tempImg2.Data[i, j, channel] = saturateCast(tempImg.Data[i, j, channel]);
                        }
                    }
                }

                for (int i = 0; i < nrows; ++i)
                {
                    for (int channel = 0; channel < 3; channel++)
                    {
                        tempImg2.Data[i, 0, channel] = 0;

                        tempImg2.Data[i, ncols - 1, channel] = 0;
                    }
                }

                for (int j = 0; j < ncols; ++j)
                {
                    for (int channel = 0; channel < 3; channel++)
                    {

                        tempImg2.Data[0, j, channel] = 0;

                        tempImg2.Data[nrows - 1, j, channel] = 0;
                    }
                }

                tempImg2.Mat.CopyTo(outImg);

                tempImg.Dispose();
                tempImg2.Dispose();
            }

            else
            {
                return false;
            }

            return true;
        }

        private void RecognizeButton_Click(object sender, RoutedEventArgs e)
        {
            //RecoText.Text = Recognize(photo.SharpenedPhoto);
            string RecoText = "";

            RecognizeThread thread = new RecognizeThread(photo.OriginalPhoto);  //识别原始图片
            ThreadStart childref = new ThreadStart(thread.Recognize);
            Thread childThread = new Thread(childref);
            childThread.Name = "TextRecognition";
            childThread.Start(); //启动线程
            childThread.Join();

            RecoText = thread.recoText;
            var segmenter = new JiebaSegmenter();
            var segments = segmenter.Cut(RecoText);

            foreach (var word in segments)
            {
                Button button = new Button();
                button.Height = 30;
                button.Margin = new Thickness(5);
                //button.Width = 

                button.Content = word;

                stack.Children.Add(button);

            }
        }
        
    }
}

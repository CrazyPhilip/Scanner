using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Scanner.Threads
{
    public class RecognizeThread
    {
        public Mat inImg = new Mat();
        public string recoText = "";

        public RecognizeThread(Mat img)
        {
            this.inImg = img;
        }

        public void Recognize()
        {
            try
            {
                string path = @"C:\Users\cf200\source\repos\Scanner\Scanner\bin\Debug\";
                TesseractEngine _ocr = new TesseractEngine(path, "chi_sim", EngineMode.Default);

                Bitmap bit = inImg.Bitmap;
                //bit = PreprocesImage(bit);//进行图像处理,如果识别率低可试试
                Page page = _ocr.Process(bit);
                recoText = page.GetText();//识别后的内容
                page.Dispose();
                _ocr.Dispose();

                Console.WriteLine(recoText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
    }
}

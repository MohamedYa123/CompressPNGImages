using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Accord.Video;
//using Accord.Video.FFMPEG;
namespace Quality_raiser
{
    public partial class raisevideo : Form
    {
        public raisevideo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of= new OpenFileDialog();
            of.ShowDialog();
            if (of.FileName == "")
            {
                return;
            }
            //VideoCapture cap= new VideoCapture(of.FileName);
            //Bitmap bit=new Bitmap(cap.Width,cap.Height);
            //List<Bitmap> arr=new List<Bitmap>();
            //cap.Start();
            //using (var vFReader = new VideoFileReader())
            //{
            //    vFReader.Open(of.FileName);
            //    for (int i = 0; i < vFReader.FrameCount; i++)
            //    {
            //        Bitmap bmpBaseOriginal = vFReader.ReadVideoFrame();
            //    }
            //    vFReader.Close();
            //}

        }
    }
}

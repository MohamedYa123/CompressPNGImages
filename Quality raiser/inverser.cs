using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quality_raiser
{
    public partial class inverser : Form
    {
        public inverser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of= new OpenFileDialog();
            of.ShowDialog();
            Bitmap bitmap=new Bitmap(10,10);
            if (of.FileName != "")
            {
                bitmap= new Bitmap(of.FileName);
            }
            of=new OpenFileDialog();
            of.ShowDialog();
            if (of.FileName != "")
            {
                Ruler rc= new Ruler();
                rc.loadfrombytes(of.FileName);
               pictureBox1.Image=(Image)rc.inverse(bitmap);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf= new SaveFileDialog();
            svf.ShowDialog();
            if (svf.FileName != "")
            {
                Bitmap bitmap = (Bitmap)pictureBox1.Image;
                bitmap.Save(svf.FileName);
            }
        }
    }
}

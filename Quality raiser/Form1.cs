using System.Runtime.Intrinsics.X86;

namespace Quality_raiser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Ruler r = new Ruler();
        bool done;
        Bitmap inversed;
        void study(Bitmap btmp2,Bitmap btmp,byte lowerrate,byte finder)
        {
            done= false;
            r.study(btmp2, btmp, lowerrate, finder);
            inversed= r.inverse(btmp2);
            done = true;
        }
        Bitmap gbtmp;
        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of=new OpenFileDialog();
            of.ShowDialog();
            if (of.FileName == "")
            {
                return;
            }
            Bitmap btmp = new Bitmap(of.FileName);
            byte lowerrate = Convert.ToByte(numericUpDown1.Value);
            Bitmap btmp2=new Bitmap(width:btmp.Width/lowerrate, height:btmp.Height/lowerrate);
            for(int i = 0; i < btmp.Width/lowerrate*lowerrate; i += 1)
            {
                Color newcolor= Color.FromArgb(255,0, 0, 0);
                int[] newc = new int[3];
                for (int i2=0;i2<btmp.Height/lowerrate*lowerrate; i2+=1)
                {
                    var c = btmp.GetPixel(i, i2);
                    if (true)
                    {
                        newc[0] += c.R;
                        newc[1] += c.G;
                        newc[2] += c.B;
                   }
                    
                    if ((i2 - 1) % lowerrate == 0)
                    {
                        newcolor = Color.FromArgb(255, newc[0] / lowerrate, newc[1] / lowerrate, newc[2] / lowerrate);
                        btmp2.SetPixel(i/lowerrate, i2/lowerrate, newcolor);
                        //btmp2.SetPixel(i / 2, i2 / 2, btmp.GetPixel(i, i2));
                        newc = new int[3];
                    }
                }
                
            }
          
            r = new Ruler();
            switch (comboBox1.SelectedIndex) {
                case 0:
                    r.mode=Ruler.keymode.square; break;
                case 1:
                    r.mode=Ruler.keymode.horizontal; break;
                case 2:
                    r.mode = Ruler.keymode.vertical;
                    break;
            }
            byte finder = Convert.ToByte(numericUpDown2.Value);
            done= false;
            await Task.Run(() => study(btmp2, btmp, lowerrate, finder));
            pictureBox1.Image = (Image)btmp;
            pictureBox2.Image = (Image)btmp2;//lowered
            gbtmp = btmp2;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmp=(Bitmap)pictureBox2.Image;
            SaveFileDialog sf = new SaveFileDialog();
            sf.ShowDialog();
            if (sf.FileName != "")
            {
                bitmp.Save(sf.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "mo files|*.mo";
            sf.ShowDialog();
            if (sf.FileName != "")
            {
                r.savebytes(sf.FileName);
                Ruler rc= new Ruler();
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bitmp = (Bitmap)pictureBox3.Image;
            SaveFileDialog sf = new SaveFileDialog();
            sf.ShowDialog();
            if (sf.FileName != "")
            {
                bitmp.Save(sf.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            inverser inv=new inverser();
            inv.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            raisevideo rs=new raisevideo();
            rs.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex= 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value =Convert.ToInt32(Math.Round( Convert.ToDouble( r.completion) / r.totalsize*100));

                if (done)
                {
                    pictureBox3.Image = inversed;//regenerated
                    done = false;
                    MessageBox.Show("Size of Ruler : " + r.getsize() / 1024.0 + " KB");
                 
                }
            }
            catch
            {

            }
        }
    }
}
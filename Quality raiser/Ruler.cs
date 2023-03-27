using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Quality_raiser
{
    [Serializable]
    public class Ruler
    {
        Dictionary<ruleC, Rule> rulesR = new Dictionary<ruleC, Rule>();
        Dictionary<ruleC, Rule> rulesG = new Dictionary<ruleC, Rule>();
        Dictionary<ruleC, Rule> rulesB = new Dictionary<ruleC, Rule>();
        byte finder = 10;
        byte pnum;
        public int completion = 0;
        public int totalsize=0;
        public void study(Bitmap small,Bitmap big,byte lowerrate,byte finder)
        {
            
            pnum = lowerrate;
            this.finder= finder;
            int left = 2;
            int right = 2;
            if (mode == keymode.horizontal)
            {
                left = 2;
                right = 1;
            }
            else if(mode == keymode.vertical) 
            {
                left = 1; right=2;
            }
            totalsize = small.Width/left * small.Height/right;
            for (int i=0;i<small.Width-1;i+=left)
            {
                for(int i2=0;i2< small.Height-1;i2+=right)
                {
                    completion++;
                    Color c1 = small.GetPixel(i, i2);
                    Color c2 = small.GetPixel(i+1, i2);
                    Color c3 = small.GetPixel(i, i2+1);
                    Color c4 = small.GetPixel(i+1, i2+1);
                    byte[] ruleR = null;
                    byte[] ruleG= null;
                    byte[] ruleB = null;
                    int[] newR = null;// new int[lowerrate * lowerrate * 4];
                    int[] newG = null;// new int[lowerrate * lowerrate * 4];
                    int[] newB = null;// new int[lowerrate * lowerrate * 4];
                    if (mode == keymode.square)
                    {
                        byte[] r = { Convert.ToByte(Math.Round( Convert.ToDouble( c1.R) / finder)), Convert.ToByte(Math.Round( Convert.ToDouble( c2.R) / finder)), Convert.ToByte(Math.Round( Convert.ToDouble(c3.R) / finder)), Convert.ToByte(Math.Round( Convert.ToDouble(c4.R) / finder)) };
                        byte[] g = { Convert.ToByte(Math.Round( Convert.ToDouble(c1.G) / finder)), Convert.ToByte(Math.Round( Convert.ToDouble(c2.G) / finder)), Convert.ToByte(Math.Round( Convert.ToDouble(c3.G) / finder)), Convert.ToByte(Math.Round( Convert.ToDouble(c4.G) / finder)) };
                        byte[] b = { Convert.ToByte(Math.Round(Convert.ToDouble(c1.B) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.B) / finder)), Convert.ToByte(Convert.ToDouble(c3.B)     / finder), Convert.ToByte(Convert.ToDouble(c4.B) / finder) };
                        ruleR = r;
                        ruleG=g;
                        ruleB = b;
                         newR = new int[lowerrate * lowerrate * 4];
                         newG = new int[lowerrate * lowerrate * 4];
                         newB = new int[lowerrate * lowerrate * 4];
                    }
                    else if(mode==keymode.horizontal)
                    {
                        byte[] r = { Convert.ToByte(Math.Round(Convert.ToDouble(c1.R) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.R) / finder)) };
                        byte[] g = { Convert.ToByte(Math.Round(Convert.ToDouble(c1.G) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.G) / finder)) };
                        byte[] b = { Convert.ToByte(Math.Round(Convert.ToDouble(c1.B) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.B) / finder)) };
                        ruleR = r;
                        ruleG = g;
                        ruleB = b;
                         newR = new int[lowerrate * lowerrate * 2];
                         newG = new int[lowerrate * lowerrate * 2];
                         newB = new int[lowerrate * lowerrate * 2];
                    }
                    else if (mode == keymode.vertical)
                    {
                        byte[] r = { Convert.ToByte(Math.Round(Convert.ToDouble(c1.R) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c3.R) / finder)) };
                        byte[] g = {Convert.ToByte(Math.Round(Convert.ToDouble(c1.G) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c3.G) / finder)) };
                        byte[] b = {Convert.ToByte(Math.Round(Convert.ToDouble(c1.B) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c3.B) / finder)) };
                        ruleR = r;
                        ruleG = g;
                        ruleB = b;
                        newR = new int[lowerrate * lowerrate * 2];
                        newG = new int[lowerrate * lowerrate * 2];
                        newB = new int[lowerrate * lowerrate * 2];
                    }
                    int counter = 0;
                    int a1 = 1;
                    int a2 = 1;
                    if (mode == keymode.square)
                    {
                        a1 = 2;
                        a2=2;
                    }
                    else if (mode == keymode.horizontal)
                    {
                        a1 = 2;a2 = 1;
                    }
                    else if (mode == keymode.vertical)
                    {
                        a1= 1;
                        a2 = 2;
                    }
                    for(int a = i * lowerrate; a < i * lowerrate + lowerrate* a1; a++)
                    {
                        for(int b = i2 * lowerrate; b < i2 * lowerrate + lowerrate* a2; b++)
                        {
                            Color c=big.GetPixel(a, b);
                            newR[counter] = c.R;
                            newG[counter] = c.G;
                            newB[counter] = c.B;
                            counter++;
                        }
                    }
                    Rule r1= new Rule();
                    r1.answers = newR;
                    Rule r2=new Rule();
                    r2.answers = newG;
                    Rule r3= new Rule();
                    r3.answers = newB;
                    if (!rulesR.ContainsKey(ruleC.fromint(ruleR)))
                    {
                       rulesR.Add(ruleC.fromint( ruleR), r1);
                    }
                    else
                    {
                        var s = rulesR[ruleC.fromint(ruleR)];
                        s.nums++;
                        for(int z = 0; z < s.answers.Length; z++)
                        {
                            s.answers[z] += r1.answers[z];
                        }
                    }
                    if(!rulesG.ContainsKey(ruleC.fromint(ruleG)))
                    {
                        rulesG.Add(ruleC.fromint(ruleG), r2);
                    }
                    else
                    {
                        var s = rulesG[ruleC.fromint(ruleG)];
                        s.nums++;
                        for (int z = 0; z < s.answers.Length; z++)
                        {
                            s.answers[z] += r2.answers[z];
                        }
                    }
                    if (!rulesB.ContainsKey(ruleC.fromint(ruleB)))
                    {
                        rulesB.Add(ruleC.fromint(ruleB), r3);
                    }
                    else
                    {
                        var s = rulesB[ruleC.fromint(ruleB)];
                        s.nums++;
                        for (int z = 0; z < s.answers.Length; z++)
                        {
                            s.answers[z] += r3.answers[z];
                        }
                    }
                }
            }
            for (int z = 0; z < 3; z++)
            {
                var h = rulesR;
                switch (z)
                {
                    case 0:
                        h = rulesR;
                        break;
                    case 1:
                        h = rulesG;
                        break;
                    case 2:
                        h = rulesB;
                        break;
                }
                foreach (var a in h)
                {
                    int i = 0;
                    for(int f=0;f<a.Value.answers.Length;f++)
                    {
                        a.Value.answers[f] /= a.Value.nums;
                    }
                }

            }
        }
        public Bitmap inverse(Bitmap small)
        {
            Bitmap big=new Bitmap(small.Width*pnum, small.Height*pnum);
            int lowerrate = pnum;
            int left = 2;
            int right = 2;
            if (mode == keymode.horizontal)
            {
                left = 2;
                right = 1;
            }
            else if (mode == keymode.vertical)
            {
                left = 1; right = 2;
            }
            for (int i = 0; i < small.Width - 1; i += left)
            {
                for (int i2 = 0; i2 < small.Height - 1; i2 += right)
                {
                    Color c1 = small.GetPixel(i, i2);
                    Color c2 = small.GetPixel(i + 1, i2);
                    Color c3 = small.GetPixel(i, i2 + 1);
                    Color c4 = small.GetPixel(i + 1, i2 + 1);
                    byte[] ruleR = null;
                    byte[] ruleG = null;
                    byte[] ruleB = null;
                    if (mode == keymode.square)
                    {
                        byte[] r = { Convert.ToByte(Math.Round(Convert.ToDouble(c1.R) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.R) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c3.R) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c4.R) / finder)) };
                        byte[] g = { Convert.ToByte(Math.Round(Convert.ToDouble(c1.G) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.G) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c3.G) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c4.G) / finder)) };
                        byte[] b = { Convert.ToByte(Math.Round(Convert.ToDouble(c1.B) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.B) / finder)), Convert.ToByte(Convert.ToDouble(c3.B) / finder), Convert.ToByte(Convert.ToDouble(c4.B) / finder) };
                        ruleR = r;
                        ruleG = g;
                        ruleB = b;
                    }
                    else if (mode == keymode.horizontal)
                    {
                        byte[] r = {Convert.ToByte(Math.Round(Convert.ToDouble(c1.R) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.R) / finder)) };
                        byte[] g = {Convert.ToByte(Math.Round(Convert.ToDouble(c1.G) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.G) / finder)) };
                        byte[] b = {Convert.ToByte(Math.Round(Convert.ToDouble(c1.B) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c2.B) / finder)) };
                        ruleR = r;
                        ruleG = g;
                        ruleB = b;
                    }
                    else if (mode == keymode.vertical)
                    {
                        byte[] r = {Convert.ToByte(Math.Round(Convert.ToDouble(c1.R) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c3.R) / finder)) };
                        byte[] g = {Convert.ToByte(Math.Round(Convert.ToDouble(c1.G) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c3.G) / finder)) };
                        byte[] b = {Convert.ToByte(Math.Round(Convert.ToDouble(c1.B) / finder)), Convert.ToByte(Math.Round(Convert.ToDouble(c3.B) / finder)) };
                        ruleR = r;
                        ruleG = g;
                        ruleB = b;
                     
                    }
                    int a1 = 1;
                    int a2 = 1;
                    if (mode == keymode.square)
                    {
                        a1 = 2;
                        a2 = 2;
                    }
                    else if (mode == keymode.horizontal)
                    {
                        a1 = 2; a2 = 1;
                    }
                    else if (mode == keymode.vertical)
                    {
                        a1 = 1;
                        a2 = 2;
                    }
                    int counter = 0;
                    var colorextractedR = rulesR[ruleC.fromint( ruleR)];
                    var colorextractedG = rulesG[ruleC.fromint(ruleG)];
                    var colorextractedB = rulesB[ruleC.fromint(ruleB)];
                    for (int a = i * lowerrate; a < i * lowerrate + lowerrate * a1; a++)
                    {
                        for (int b = i2 * lowerrate; b < i2 * lowerrate + lowerrate * a2; b++)
                        {
                            Color c = Color.FromArgb(255, colorextractedR.answers[counter], colorextractedG.answers[counter], colorextractedB.answers[counter]);
                            big.SetPixel(a, b, c);
                            counter++;
                        }
                    }
                }
            }
            return big;
        }
        public int getsize()
        {
            int size = 0;
            size += rulesR.Count + rulesB.Count + rulesG.Count;
            if (mode == keymode.square)
            {
                size *= pnum * pnum * 4 + 4;
            }
            else if (mode == keymode.horizontal||mode==keymode.vertical)
            {
                size *= pnum*pnum * 2 + 2;
            }
            
            return size+4+3*4;
        }
        [Obsolete]
        public void saveme(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream writerFileStream =
                 new FileStream(path, FileMode.Create, FileAccess.Write);
            // Save our dictionary of friends to file
            formatter.Serialize(writerFileStream, this);
            // Close the writerFileStream when we are done.
            writerFileStream.Close();
        }
        [Obsolete]
        public static Ruler load(string datapath)
        {
            Ruler tc;
            FileStream readerFileStream = new FileStream(datapath,
    FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
            // Reconstruct information of our friends from file.
            tc = (Ruler)formatter.Deserialize(readerFileStream);
            // Close the readerFileStream when we are done
            readerFileStream.Close();
            return tc;
        }
        byte[] getbytes(int n)
        {
            byte[] buffer = BitConverter.GetBytes(n);
            return buffer;
        }
        int getint(byte[] bytes)
        {
           // if (BitConverter.IsLittleEndian)
             //   Array.Reverse(bytes);
            int i = BitConverter.ToInt32(bytes, 0);
            return i;
        }
        public enum keymode { square,horizontal,vertical}
        public keymode mode;
        public byte version;
        public void savebytes(string path)
        {
            byte[]  allbytes= new byte[getsize()];
            allbytes[0] = pnum;
            allbytes[1] = finder;
            allbytes[2] = (byte)mode;
            allbytes[3] = (byte)version;
            var bytesR = getbytes(rulesR.Count);
            var bytesG=getbytes(rulesG.Count);
            var bytesB=getbytes(rulesB.Count);
            int tr = 0;
            for(int u=4;u<bytesR.Length+4;u++)
            {
                allbytes[u] = bytesR[tr];
                tr++;
            }
            int y = tr;
            tr = 0;
            for(int u=4+y;u<bytesG.Length+4+y;u++)
            {
                allbytes[u] = bytesG[tr];
                tr++;
            }
            y += tr;
            tr = 0;
            for(int u=4+y;u<bytesB.Length+4+y;u++)
            {
                allbytes[u] = bytesB[tr];
                tr++;
            }
            int i = 4+3*4;
            int q = -1;
            for (int z = 0; z < 3; z++)
            {
                var h = rulesR;
                switch (z)
                {
                    case 0:
                        h = rulesR;
                        break;
                    case 1:
                        h = rulesG;
                        break;
                    case 2:
                        h = rulesB;
                        break;
                }
                foreach (var a in h)
                {
                    q++;
                    allbytes[i] = a.Key.a1;
                    allbytes[i + 1] = a.Key.a2;
                    if (mode == keymode.square)
                    {
                        allbytes[i + 2] = a.Key.a3;
                        allbytes[i + 3] = a.Key.a4;
                    }
                    if (mode == keymode.square)
                    {
                        i += 4;
                    }
                    else
                    {
                        i += 2;
                    }
                    foreach (var b in a.Value.answers)
                    {
                        allbytes[i] = Convert.ToByte( b);
                        i++;
                    }
                }
                
            }
            File.WriteAllBytes(path, allbytes);
        }
        public void loadfrombytes(string path)
        {
            var allbytes=File.ReadAllBytes(path);
            mode = (keymode)allbytes[2];
            pnum = allbytes[0];
            int max = pnum * pnum * 2;
            
            finder = allbytes[1];
            if (version != allbytes[3])
            {
                throw new Exception("version mismatch !");
            }
           
            if (mode == keymode.square)
            {
                max = pnum * pnum * 4;
            }
            var bytesR = new byte[4];
            var bytesG = new byte[4];
            var bytesB = new byte[4];
            int pointera = 4;
            for(int i=0;i<3;i++)
            {
                for(int y=0;y<4;y++) { 
                switch(i)
                    {
                        case 0:
                            bytesR[y] = allbytes[4+i*4+y];
                            break;
                        case 1:
                            bytesG[y] = allbytes[4+i*4+y];
                            break;
                        case 2:
                            bytesB[y] = allbytes[4+i*4+y];
                            break;
                    }
                    pointera++;
                }
            }
            int rulesRnum = getint(bytesR);
            int rulesGnum = getint(bytesG);
            int rulesBnum = getint(bytesB);
            byte[] dash=new byte[4];
            bool record=true;
            int index = 0;
            ruleC rlc=new ruleC();
            int total = 0;
            Rule rl=new Rule();
            int pointer = 0;
            int colorh = 0;
            int colorlen = rulesRnum;
            int maxindx = 2;
            if (mode == keymode.square)
            {
                maxindx = 4;
            }
            for(int i=pointera;i<allbytes.Length;i++)
            {
                if (record)
                {
                    dash[index] = allbytes[i];
                    index++;
                    if (index == maxindx)
                    {
                        index= 0;
                        record= false;
                        rlc = ruleC.fromint(dash);
                        rl=new Rule();
                        rl.answers = new int[max];
                    }
                }
                else
                {
                    rl.answers[pointer] = allbytes[i];
                    pointer++;
                    if (pointer == max)
                    {
                        total++;
                        record= true;
                        pointer= 0;
                        switch (colorh)
                        {
                            case 0:
                                rulesR.Add(rlc, rl);
                                break;
                            case 1:
                                rulesG.Add(rlc, rl);
                                break;
                            case 2:
                                rulesB.Add(rlc, rl);
                                break;
                        }
                        if (total == colorlen)
                        {
                            colorh++;
                            total= 0;
                            switch (colorh)
                            {
                                case 0:
                                    colorlen = rulesRnum;
                                    break;
                                case 1:
                                    colorlen = rulesGnum;
                                    break;
                                case 2:
                                    colorlen = rulesBnum;
                                    break;
                            }
                        }

                    }
                }
            }
        }
    }
    [Serializable]
    public class Rule
    {
       // public byte[] Rules=new byte[4];
        public int[] answers = new int[16];
        public int nums = 1;
    }
    [Serializable]
    public struct ruleC
    {
       public byte a1;
       public byte a2;
       public byte a3;
       public byte a4;
        public override string ToString()
        {
            return $"{a1}:{a2}:{a3}:{a4}";
        }
        public static  ruleC fromint(byte[]b)
        {
            if (b.Length == 4)
            {
                ruleC r = new ruleC { a1 = b[0], a2 = b[1], a3 = b[2], a4 = b[3] };
                return r;
            }
            else
            {
                ruleC r = new ruleC { a1 = b[0], a2 = b[1] };
                return r;
            }
        }
    }
}

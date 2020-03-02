using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace rsa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int p = Convert.ToInt32(textBox1.Text);
            int q = Convert.ToInt32(textBox2.Text);
            int eukval = 0;
            int oeukval = 0;
            int n = p * q;
            int eee = (p-1) * (q-1);
            for (int i = 3; i < eee; i += 2) {
                if (gcf(i, eee) == 1)
                {
                    eukval = i;
                    break;
                }
            }
            for (int i = 2; i < eee; i += 1)
            {
                if (ogcf(i, eukval,eee) == 1)
                {
                    oeukval = i;
                    break;
                }
            }

            textBox3.Text = eukval.ToString() + "," + n.ToString();
            textBox4.Text = oeukval.ToString() + "," + n.ToString();

        }


        static int gcf(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        static int ogcf(int a, int b,int c)
        {
            if ((a * b) % c == 1)
            {
                return 1;
            }
            return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            char[] litery;
            string publiczny = textBox3.Text;
            string[] k = publiczny.Split(',');
            int[] publiczne = { Convert.ToInt32(k[0]), Convert.ToInt32(k[1]) };
            if (radioButton1.Checked)
            {
                BigInteger bigintiger;
                BigInteger dos = BigInteger.Parse(textBox6.Text);
                bigintiger = dos;
                for (int bi = 0; bi < publiczne[0] - 1; bi += 1)
                {
                    bigintiger = bigintiger * dos;
                }

                bigintiger = bigintiger % publiczne[1];
                textBox5.Text = bigintiger.ToString();
            }else if (radioButton2.Checked)
            {
                string s = "";
                byte[] ASCIIValues = Encoding.ASCII.GetBytes(textBox6.Text.ToUpper());
                foreach (byte b in ASCIIValues)
                {
                    //MessageBox.Show(Convert.ToInt32(b).ToString());
                    BigInteger bigintiger=0;
                    BigInteger dos= Convert.ToInt32(b);
                    bigintiger = dos;
                    for (int bi = 0; bi < publiczne[0] - 1; bi += 1)
                    {
                        //MessageBox.Show(Convert.ToInt32(b).ToString());
                        bigintiger = bigintiger * dos;
                    }

                    bigintiger = bigintiger % publiczne[1];
                    s += ","+bigintiger.ToString();
                    
                    //MessageBox.Show(Convert.ToInt32(b).ToString());
                    textBox5.Text = s.ToString();


                }


            }
            
           
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string prywatne = textBox4.Text;
            string[] k = prywatne.Split(',');
            int[] priv = { Convert.ToInt32(k[0]), Convert.ToInt32(k[1]) };
           
            if (radioButton1.Checked)
            {
                Dictionary<int, double> potegi2 = new Dictionary<int, double>();
                double dor = Convert.ToDouble(textBox5.Text);
                potegi2.Add(1, dor % priv[1]);
                do
                {
                    potegi2.Add(potegi2.Last().Key * 2, Math.Pow(potegi2.Last().Value, 2) % priv[1]);
                } while (potegi2.Last().Key < priv[0] / 2);


                BigInteger sum = 1;
                while (priv[0] != 0)
                {
                    if (priv[0] >= potegi2.Last().Key)
                    {
                        sum *= Convert.ToInt64(potegi2.Last().Value);
                        priv[0] -= potegi2.Last().Key;
                        //MessageBox.Show(Convert.ToString(sum));
                    }
                    else
                    {
                        potegi2.Remove(potegi2.Last().Key);
                    }
                }
                sum = sum % priv[1];
                textBox7.Text = sum.ToString();
            }
            else if (radioButton2.Checked)
            {
                string wyraz="";
                string[] sar;
                sar= textBox5.Text.Split(',');
                var slist=sar.ToList();
                slist.RemoveAt(0);
                sar = slist.ToArray();
                string s = sar.ToString();
                int temppriv0 = priv[0];
                foreach (string str in sar)
                {
                    priv[0] = temppriv0;
                    double strval = Convert.ToDouble(str);
                    Dictionary<int, double> potegi2 = new Dictionary<int, double>();
                    
                    potegi2.Add(1, strval % priv[1]);
                    do
                    {
                        potegi2.Add(potegi2.Last().Key * 2, Math.Pow(potegi2.Last().Value, 2) % priv[1]);
                    } while (potegi2.Last().Key < priv[0] / 2);


                    BigInteger sum = 1;
                    while (priv[0] != 0)
                    {
                        if (priv[0] >= potegi2.Last().Key)
                        {
                            sum *= Convert.ToInt64(potegi2.Last().Value);
                            priv[0] -= potegi2.Last().Key;
                            //MessageBox.Show(Convert.ToString(sum));
                        }
                        else
                        {
                            potegi2.Remove(potegi2.Last().Key);
                        }
                    }
                    sum = sum % priv[1];
                    char c = (char)sum;
                    //textBox7.Text = c.ToString();
                    wyraz += c.ToString();

                }

                textBox7.Text = wyraz;

            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

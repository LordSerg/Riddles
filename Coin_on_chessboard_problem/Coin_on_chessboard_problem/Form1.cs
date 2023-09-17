using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coin_on_chessboard_problem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Introduction f = new Introduction();
            f.ShowDialog();
            MessageBox.Show("Now your cellmate has already hidden the key...\nYou have to find it :)\n\n\\\\light circle - is for heads\n\\\\dark circle - is for tails", "Remark");
        }
        int correct_answer = -1;
        int[,] m = new int[8, 8];
        int[] corr = new int[6];
        int[] magic = new int[6];
        int[] out_num = new int[6];
        int k = 0;
        //int tries = 2;
        void f()
        {
            int temp = correct_answer;
            for (int i = 0; i < 6; i++)
            {
                corr[i] = temp % 2;
                temp /= 2;
            }
            int count = 0;
            for (int i = 0; i < 8; i ++)
                for (int j = 1; j < 8; j+=2)
                    count += m[i, j];
            magic[0] = count % 2;
            count = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 2; j < 8; j += 4)
                    count += m[i, j]+ m[i, j+1];
            magic[1] = count % 2;
            count = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 4; j < 8; j += 8)
                    count += m[i, j]+ m[i, j+1]+m[i, j+2]+ m[i, j+3];
            magic[2] = count % 2;
            count = 0;
            for (int i = 1; i < 8; i += 2)
                for (int j = 0; j < 8; j++)
                    count += m[i, j];
            magic[3] = count % 2;
            count = 0;
            for (int i = 2; i < 8; i += 4)
                for (int j = 0; j < 8; j++)
                    count += m[i, j]+m[i+1,j];
            magic[4] = count % 2;
            count = 0;
            for (int i = 4; i < 8; i += 8)
                for (int j = 0; j < 8; j++)
                    count += m[i, j]+m[i+1,j]+m[i+2,j]+m[i+3,j];
            magic[5] = count % 2;
            for (int i = 0; i < 6; i++)
            {
                out_num[i] = (corr[i] == magic[i]) ? 0 : 1;
            }
            k = 0;
            for (int i = 0; i < 6; i++)
            {
                k += (int)Math.Pow(2, i) * out_num[i];
            }
            m[k / 8, k % 8] = m[k / 8, k % 8] == 1 ? 0 : 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    m[i, j] = r.Next(0, 2);
                }
            }
            correct_answer = r.Next(0, 64);
            f();
            //m[correct_answer / 8, correct_answer % 8] = f(m[correct_answer / 8, correct_answer % 8]);
            /*
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                     s.Append(m[i, j] + " ");
                }
                s.Append("\n");
            }
            richTextBox1.Text = s.ToString();
            */
            Draw();
        }
        void Draw()
        {
            if (correct_answer == -1)
            {
                int w = pictureBox1.Width, h = pictureBox1.Height;
                if (w > 0 && h > 0)
                {
                    Bitmap bit = new Bitmap(w, h);
                    Graphics g = Graphics.FromImage(bit);
                    g.Clear(Color.White);
                    pictureBox1.Image = bit;
                }
            }
            else
            {
                int w = pictureBox1.Width, h = pictureBox1.Height;
                if (w > 0 && h > 0)
                {
                    Bitmap bit = new Bitmap(w, h);
                    Graphics g = Graphics.FromImage(bit);
                    g.Clear(Color.White);
                    int size;//one cell size
                    if (w > h)
                        size = h / 8;
                    else
                        size = w / 8;
                    Brush black = new SolidBrush(Color.FromArgb(152, 119, 101));
                    Brush white = new SolidBrush(Color.FromArgb(236, 214, 180));
                    Brush head = new SolidBrush(Color.FromArgb(153, 153, 153));
                    Brush tail = new SolidBrush(Color.FromArgb(47, 47, 47));

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if ((i + j) % 2 == 0)
                                g.FillRectangle(white, i * size, j * size, size, size);
                            else
                                g.FillRectangle(black, i * size, j * size, size, size);
                            g.DrawString((i + j * 8).ToString(), this.Font, Brushes.Black, i * size, j * size);
                            if (m[j, i] == 1)//head
                                g.FillEllipse(head, i * size + (size / 5), j * size + (size / 5), size - 2 * size / 5, size - 2 * size / 5);
                            else
                                g.FillEllipse(tail, i * size + (size / 5), j * size + (size / 5), size - 2 * size / 5, size - 2 * size / 5);
                        }
                    }

                    pictureBox1.Image = bit;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (correct_answer != -1)
            {
                if (int.Parse(textBox1.Text) == correct_answer)
                {
                    //MessageBox.Show("Correct!!!","Congratulations");
                    Oops f = new Oops();
                    f.ShowDialog();
                    Application.Exit();
                }
                else
                {
                    //if (tries == 0)
                    {
                        MessageBox.Show("You lost this issue :(\n\nTry another one", "Incorrect");
                        correct_answer = -1;
                        textBox1.Text = "";
                        //tries = 2;
                        Draw();
                    }
                    //else
                    //{
                    //    if (tries == 1)
                    //        MessageBox.Show("You have one attempt left for current issue", "Incorrect");
                    //    else if (tries == 2)
                    //        MessageBox.Show("You have two attempts left for current issue", "Incorrect");
                    //    tries--;
                    //    textBox1.Text = "";
                    //}
                    //MessageBox.Show("Inorrect (((\nthe correct = " + correct_answer +
                    //    "\ncorr = " + arr_to_txt(corr) +
                    //    "\nmagic = " + arr_to_txt(magic) +
                    //    "\nout = " + arr_to_txt(out_num) +
                    //    "\nk = " + k);
                }
            }
        }
        string arr_to_txt(int []a)
        {
            string answer = "";
            for (int i = 0; i < a.Length; i++)
            {
                answer += a[a.Length-1-i].ToString() + " ";
            }
            return answer;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (correct_answer >= 0)
            {
                int w = pictureBox1.Width, h = pictureBox1.Height;
                int size;//one cell size
                if (w > h)
                    size = h / 8;
                else
                    size = w / 8;
                int i = e.X / size;
                int j = e.Y / size;
                textBox1.Text = (i + j * 8).ToString();
            }
        }
    }
}

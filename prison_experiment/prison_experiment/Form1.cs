using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prison_experiment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int priz_num=100;//кількість в'язнів = кількість коробок
        int check_num=50;//кількість разів для перевірки
        int TIMES = 50000;//кількість експериментів
        int[] arr;
        Random r = new Random();
        int success_rnd = 0;
        int success_clvr = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            priz_num = (int)numericUpDown1.Value;
            check_num = (int)numericUpDown3.Value;
            TIMES = (int)numericUpDown2.Value;
            string s = "";
            success_clvr = 0;
            success_rnd = 0;
            for (int i = 1; i <= TIMES; i++)
            {
                init_boxes();
                success_rnd += do_rand_experement() ? 1 : 0;
                success_clvr += do_clever_experement() ? 1 : 0;
                s = "experement "+i.ToString() + "/" + TIMES.ToString()+":\n" +
                    "RANDOM:\n" +
                    "success = " + success_rnd + "\n" +
                    "failure = " + (i - success_rnd) + "\n" +
                    "probability = "+(Math.Round((double)success_rnd/(double)i,10))+"\n\n" +
                    "ALGORYTHM:\n" +
                    "success = " + success_clvr + "\n" +
                    "failure = " + (i - success_clvr) +"\n"+
                    "probability = " + (Math.Round((double)success_clvr / (double)i, 10));
                richTextBox1.Text = s;
                richTextBox1.Refresh();
            }
            //string array = "\n";
            //for (int i = 0; i < priz_num; i++) array+="a["+i+"] = "+arr[i].ToString()+"\n";
            //s += array;
            //s += find_circles();
            richTextBox1.Text = s;
        }
        void swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }
        void init_boxes()
        {
            arr = new int[priz_num];
            for (int i = 0; i < priz_num; i++) arr[i] = i;
            for (int i = 0; i < priz_num*50; i++) swap(ref arr[r.Next(priz_num)], ref arr[r.Next(priz_num)]);
        }
        bool do_rand_experement() 
        {//returns, if experement gone well for prizoners
            int k = 0;
            List<int> ch = new List<int>();
            bool is_managed = false;
            for (int i = 0; i < priz_num; i++)//prisoners
            {
                is_managed = false;
                for (int j = 0; j < check_num; j++)//boxes
                {
                    k = r.Next(priz_num);
                    while (ch.Contains(k))
                    {
                        k = r.Next(priz_num);
                    }
                    
                    if (arr[k] == i)//якщо номер в'язня(і) співпадає з випадковим номером, що у коробці(arr[k])
                    {//то в'язню пощастило
                        is_managed = true;
                        break;
                    }
                    ch.Add(k);
                }
                if (!is_managed)
                    return false;
                ch.Clear();
            }
            return true;
        }
        bool do_clever_experement()
        {//returns, if experement gone well for prizoners
            int k = -1;
            bool is_managed = false;
            for (int i = 0; i < priz_num; i++)//prisoners
            {
                is_managed = false;
                k = i;
                for (int j = 0; j < check_num; j++)//boxes
                {
                    if (arr[k] == i)//якщо номер в'язня(і) співпадає з номером, що у коробці(arr[k])
                    {
                        is_managed = true;
                        break;
                    }
                    k = arr[k];
                }
                if (!is_managed)
                    return false;
            }
            return true;
        }
        string find_circles()
        {
            string answer = "";
            bool[] flg = new bool[priz_num];
            for (int t = 0; t < priz_num; t++) flg[t] = true;
            List<int> cycle = new List<int>();
            int i = 0,count = 0;
            int k = 0;
            do
            {
                if (!flg[k])
                {
                    answer += "cycle #" + i + ": (length = "+cycle.Count+")\n";
                    foreach (int n in cycle) answer += "" + n + "->";
                    answer += cycle[0]+"\n";
                    cycle.Clear();
                    k = -1;
                    for (int t = 0; t < priz_num; t++)
                        if (flg[t])
                        {
                            k = t;
                            break;
                        }
                    i++;
                }
                if (k == -1)
                    break;
                cycle.Add(k);
                flg[k] = false;
                k = arr[k];
                count++;
            }
            while (true);//count < priz_num);
            return answer;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Maximum = numericUpDown1.Value;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://en.wikipedia.org/wiki/100_prisoners_problem");
        }
    }
}

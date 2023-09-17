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
    public partial class Oops : Form
    {
        public Oops()
        {
            InitializeComponent();
            Bitmap bit = new Bitmap(Properties.Resources.cat);
            this.Width = bit.Width;
            this.Height = bit.Height;
            pictureBox1.Image = bit;
            label1.Text = "Congratulations! You won this \"game\" and you totally deserve the cutest cat (⸝⸝ᵕᴗᵕ⸝⸝)♡";
        }
    }
}

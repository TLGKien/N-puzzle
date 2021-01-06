using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N_Puzzle_cs
{
    public partial class Image_Cell : PictureBox
    {
        private int value;
        public Image_Cell()
        {
            InitializeComponent();
        }

        public int Value { get => value; set => this.value = value; }
    }
}

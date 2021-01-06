using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Solution;

namespace N_Puzzle_cs
{
    public partial class Form1 : Form
    {
        // manager

        private List<Control> controls_list1;                       // controls list at start screen
        private List<Control> controls_list2;                       // controls list at playscreen
        private List< List<Image_Cell> > image_cell_list;           // list of images cell
        private List<int> sln;                                      // solution
        // value
        int[,] test;                                                // value is initialized for picture cells
        private int max=3;                                          // number of picture cells per a row
        private int[] null_cell_index = new int[2];                 // index of cell, where has value -1
        private Image image;                                        // save image
        private int move_count;                                     // save number of moved
        private DateTime start_time;                                // save time start play
        private string path;                                        // path saves location of image
        // other
        private Random random;
        private bool canMove;

        public Form1()
        {
            InitializeComponent();
            this.random = new Random();
            controls_list1 = new List<Control>();
            controls_list2 = new List<Control>();
            this.canMove = false;
            // add all controls at start screen to list1
            foreach (Control c in this.Controls)
            {
                c.BackColor = Color.Transparent;
                this.controls_list1.Add(c);
            }
            InitializePlayScreen();
            Hidden_Play_Screen();
        }


        //---------------------------------------------------
        //-------------------------------- xoá các cell phần chơi
        //---------------------------------------------------

        private void Remove_Image_Cells()
        {
            foreach (List<Image_Cell> l in image_cell_list) 
            {
                foreach (Image_Cell i in l)
                {
                    this.Controls.Remove(i);
                    this.controls_list2.Remove(i);
                }
            }
        }

        //----------------------------------------------
        //------------display start screen and hidden play screen
        //--------------------------------------------
        private void Show_Start_Hide_Play()
        {
            Remove_Image_Cells();
            Hidden_Play_Screen();
            Show_Start_Screen();
            this.canMove = false;
        }

        //----------------------------------------------
        //------------display start screen and hidden play screen
        //--------------------------------------------
        private void Show_Play_Hide_Start()
        {
            Hidden_Start_Screen();
            Show_Play_Screen();
            this.canMove = true;
        }

        //-----------------------------------------------------------------
        //-----Functions to display and hidden play screen and start screen
        //-----------------------------------------------------------------
        private void Show_Play_Screen()
        {
            foreach (Control c in this.controls_list2)
                c.Visible = true;
        }
        private void Hidden_Play_Screen()
        {
            foreach (Control c in this.controls_list2)
                c.Visible = false;
        }
        private void Show_Start_Screen()
        {
            foreach (Control c in this.controls_list1)
                c.Visible = true;
        }
        private void Hidden_Start_Screen()
        {
            foreach (Control c in this.controls_list1)
                c.Visible = false;
        }

        // -------------------------------------------------------------
        // -------------------------------------        EVENTS
        // -------------------------------------------------------------

        private void Cell_Click(object sender, EventArgs e)
        {
            int[] current_cell_index = new int[2];
            Image_Cell current_cell = (Image_Cell)sender;
            // find index of cell was clicked
            for (int i = 0; i < max; i++)
            {
                bool check = false;
                for (int j = 0; j < max; j++)
                {
                    if (image_cell_list[i][j] == current_cell)
                    {
                        current_cell_index[0] = i;
                        current_cell_index[1] = j;
                        check = true;
                        break;
                    }
                }
                if (check == true)
                    break;
            }
            // if cell clicked is near null cell ->> swap two cells and increase count of move
            if ((Math.Abs(current_cell_index[0] - null_cell_index[0]) + Math.Abs(current_cell_index[1] - null_cell_index[1]) == 1))
            {
                Swap_Two_Cell(ref current_cell, ref null_cell, current_cell_index);
            }
        }

        // ---------------------------------------------------
        // ------------------------Update time when timer tick
        // ---------------------------------------------------
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan time_delta = DateTime.Now.Subtract(start_time);
            this.time_label.Text = "TIME: " + time_delta.ToString(@"hh\:mm\:ss");
        }

        private void TimerAutoRun_Tick(object sender, EventArgs e)
        {
            AutoMove();
        }

        private void play_btn_Click(object sender, EventArgs e)
        {
            InitializeCellArray();
            Show_Play_Hide_Start();
        }

        private void Play_btn_MouseMove(object sender, MouseEventArgs e)
        {
            this.play_btn.Font = new Font(this.play_btn.Font.FontFamily, 22, FontStyle.Bold);
            this.play_btn.ForeColor = Color.Red;
        }

        private void Play_btn_MouseLeave(object sender, EventArgs e)
        {
            this.play_btn.Font = this.exit_btn.Font;
            this.play_btn.ForeColor = Color.Black;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Exit_btn_MouseMove(object sender, MouseEventArgs e)
        {
            this.exit_btn.Font = new Font(this.exit_btn.Font.FontFamily, 22, FontStyle.Bold);
            this.exit_btn.ForeColor = Color.Red;
        }

        private void Exit_btn_MouseLeave(object sender, EventArgs e)
        {
            this.exit_btn.Font = this.play_btn.Font;
            this.exit_btn.ForeColor = Color.Black;
        }

        private void Easy_btn_Click(object sender, EventArgs e)
        {
            max = 3;
        }

        private void Medium_btn_Click(object sender, EventArgs e)
        {
            max = 4;
        }

        private void Hard_btn_Click(object sender, EventArgs e)
        {
            max = 5;
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            Show_Start_Hide_Play();
        }

        private void Solution_btn_Click(object sender, EventArgs e)
        {
            Solving solving = new Solving(this.max, this.test);
            sln = solving.Resolve();            
            timerAutoRun.Start();
        }

        private void New_btn_Click(object sender, EventArgs e)
        {
            Remove_Image_Cells();
            InitializeCellArray();
        }

        // ----------------------------------------------
        // ------------------ catch event press arow keys
        //-----------------------------------------------
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                this.MoveUp();
                return true;
            }
            if (keyData == Keys.Down)
            {
                this.MoveDown();
                return true;
            }
            if (keyData == Keys.Left)
            {
                this.MoveLeft();
                return true;
            }
            if (keyData == Keys.Right)
            {
                this.MoveRight();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}

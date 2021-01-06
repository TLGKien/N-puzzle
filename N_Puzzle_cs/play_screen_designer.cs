using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Solution;

namespace N_Puzzle_cs
{
    partial class Form1
    {
        //------------------------------------------------------
        // -------------------------------------Draw play Screen
        //------------------------------------------------------
        private void InitializePlayScreen()
        {
            //
            // orginal image
            //
            this.orginal_image = new PictureBox();
            this.orginal_image.Location = new Point(this.Size.Width - CONST.SIZE_DOCK - CONST.SIZE_ORGINAL_IMAGE, CONST.SIZE_DOCK);
            this.orginal_image.Size = new Size(CONST.SIZE_ORGINAL_IMAGE, CONST.SIZE_ORGINAL_IMAGE);
            this.controls_list2.Add(this.orginal_image);
            //
            // time label
            //
            this.time_label = new Label();
            this.time_label.AutoSize = true;
          //  this.time_label.Size = new Size(180, 30);
            this.time_label.Location = new Point(CONST.SIZE_DOCK, CONST.SIZE_DOCK + CONST.SIZE_ORGINAL_IMAGE + 30);
            this.time_label.Font = new Font("Rockwell", 16, FontStyle.Bold);
            this.time_label.BackColor = Color.Red;
            this.controls_list2.Add(this.time_label);
            //
            // move label
            //
            this.move_label = new Label();
            this.move_label.AutoSize = true;
          // this.move_label.Size = this.time_label.Size;
            this.move_label.Location = new Point(CONST.SIZE_DOCK, CONST.SIZE_DOCK + CONST.SIZE_ORGINAL_IMAGE + 100);
            this.move_label.Font = this.time_label.Font;
            this.move_label.BackColor = Color.Red;
            this.controls_list2.Add(this.move_label);
            //
            // solution_btn
            //
            this.solution_btn = new Button();
            this.solution_btn.Text = "SOLUTION";
            this.solution_btn.Size = new Size(120, 50);
            this.solution_btn.Location = new Point(this.time_label.Location.X + this.time_label.Size.Width + (this.Size.Width - CONST.SIZE_DOCK - this.time_label.Size.Width - this.solution_btn.Size.Width * 3) / 4, this.time_label.Location.Y + 25);
            this.solution_btn.Font = new Font("Ravie", 10, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.controls_list2.Add(this.solution_btn);
            this.solution_btn.Click += Solution_btn_Click;
            this.solution_btn.Enabled = true;
            //
            // new button
            //
            this.new_btn = new Button();
            this.new_btn.Text = "NEW";
            this.new_btn.Size = new Size(120, 50);
            this.new_btn.Location = new Point(this.solution_btn.Location.X + this.solution_btn.Size.Width + (this.Size.Width - CONST.SIZE_DOCK - this.time_label.Size.Width - this.solution_btn.Size.Width * 3) / 4, this.time_label.Location.Y + 25);
            this.new_btn.Font = new Font("Ravie", 10, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.controls_list2.Add(this.new_btn);
            this.new_btn.Click += New_btn_Click;
            //
            // back button
            //
            this.back_btn = new Button();
            this.back_btn.Text = "BACK";
            this.back_btn.Size = new Size(120, 50);
            this.back_btn.Location = new Point(this.new_btn.Location.X + this.solution_btn.Size.Width + (this.Size.Width - CONST.SIZE_DOCK - this.time_label.Size.Width - this.solution_btn.Size.Width * 3) / 4, this.time_label.Location.Y + 25);
            this.back_btn.Font = new Font("Ravie", 10, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.controls_list2.Add(this.back_btn);
            this.back_btn.Click += Back_btn_Click;
            //
            // timer
            //
            this.timer = new Timer();
            this.timer.Tick += Timer_Tick;
            this.timer.Interval = 1000;

            this.timerAutoRun = new Timer();
            this.timerAutoRun.Tick += TimerAutoRun_Tick;
            this.timerAutoRun.Interval = 100;
            // add controls to This.Controls
            this.Controls.Add(this.orginal_image);
            this.Controls.Add(this.time_label);
            this.Controls.Add(this.move_label);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.solution_btn);
            this.Controls.Add(this.new_btn);
        }

        //--------------------------------------------------
        //--------------------------------------------------
        //--------------------------------------------------
        private void InitializeCellArray()
        {
            timerAutoRun.Stop();
            Random_For_Test();
            // assign file from path to image
            image = Image.FromFile(path);
            int size_cell = CONST.SIZE_ORGINAL_IMAGE / max;
            Bitmap bmp = image as Bitmap;
            this.image_cell_list = new List<List<Image_Cell>>();
            this.move_count = 0;
            //
            // cells
            //
            Image_Cell tmp = new Image_Cell();
            for (int i = 0; i < max; i++)
            {
                List<Image_Cell> image_cell_row = new List<Image_Cell>();
                for (int j = 0; j < max; j++)
                {
                    // assign the value for cell
                    tmp = new Image_Cell();
                    tmp.Value = test[i, j];

                    // save index of null cell
                    if (tmp.Value == -1)
                    {
                        null_cell_index[0] = i;
                        null_cell_index[1] = j;
                        null_cell = tmp;
                    }
                    // cut the image and assign for cell 
                    else
                    {
                        int loca_x = (tmp.Value - 1) % max;
                        int loca_y = Convert.ToInt32((tmp.Value - 1) / max);
                        tmp.Image = bmp.Clone(new Rectangle(size_cell * loca_x, size_cell * loca_y, size_cell, size_cell), bmp.PixelFormat);
                    }
                    tmp.BorderStyle = BorderStyle.Fixed3D;
                    tmp.Size = new Size(CONST.SIZE_ORGINAL_IMAGE / max, CONST.SIZE_ORGINAL_IMAGE / max);
                    tmp.Click += new System.EventHandler(Cell_Click);
                    controls_list2.Add(tmp);
                    image_cell_row.Add(tmp);


                    // set location for cell
                    if (i == 0)
                    {
                        if (j == 0)
                            tmp.Location = new Point(CONST.SIZE_DOCK, CONST.SIZE_DOCK);
                        else
                            tmp.Location = new Point(image_cell_row[j - 1].Location.X + CONST.SIZE_ORGINAL_IMAGE / max, image_cell_row[j - 1].Location.Y);
                    }
                    else
                    {
                        tmp.Location = new Point(image_cell_list[i - 1][j].Location.X, image_cell_list[i - 1][j].Location.Y + CONST.SIZE_ORGINAL_IMAGE / max);
                    }
                }
                image_cell_list.Add(image_cell_row);
            }
            image_cell_list[max - 1][max - 1].Image = null;

            // add to this.controls
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    this.Controls.Add(image_cell_list[i][j]);
                }
            }

            // refresh some attritbute
            this.orginal_image.Image = image;
            this.move_label.Text = "MOVED: " + move_count.ToString();
            this.time_label.Text = "TIME: 00:00:00";
            this.timer.Start();
            this.start_time = DateTime.Now;
        }

        // control
        private PictureBox orginal_image;                           // orginal picture
        private Image_Cell null_cell;                               // image has value -1
        private Timer timer;
        private Timer timerAutoRun;
        private Label time_label;
        private Label move_label;
        private Button back_btn;
        private Button solution_btn;
        private Button new_btn;
    }
}

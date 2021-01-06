using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Drawing;
using System.Windows.Forms;

using Solution;
using System.Reflection;

namespace N_Puzzle_cs
{
    partial class Form1
    {
        //------------------------------------------------------------
        // --------------------------check possibility of test's value
        // --------------------------------- if max%2 == 1, count%2==0
        // --------------------------------- else if -1 on even row, count%2==0, else count%2==1
        // ------------------------- with count is number of inverse numbers pairs
        //-------------------------------------------------------------
        private bool Check_Test()
        {
            // convert array 2D to array 1D
            int counts = 0;
            List<int> list_tmp = new List<int>();
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    if (i != (max - 1) || j != (max - 1))
                    {
                        list_tmp.Add(test[i, j]);
                    }
                }
            }

            // count inverse number pairs
            for (int i = 0; i < list_tmp.Count; i++)
            {
                for (int j = i + 1; j < list_tmp.Count; j++)
                {
                    if (list_tmp[i] > list_tmp[j])
                    {
                        counts++;
                    }
                }
            }
            return (counts % 2 == 0);
        }

        // ---------------------------------------------
        //------------ random for value of picture cells
        //-----------------------------------------------
        private void Random_For_Test()
        {
            // random for path image
            path = Application.StartupPath + @"/Sources/play/" + random.Next(11).ToString() + ".png";

            // check array: tick the value was used
            bool[] check = new bool[max * max];
            for (int i = 1; i < max * max; i++)
                check[i] = true;

            test = new int[max, max];
            test[max - 1, max - 1] = -1;
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    if (i == max - 1 && j == max - 1) { }
                    else
                    {
                        // random from 1 to max*max-1, value is unique
                        int result_of_random = random.Next(1, max * max);
                        while (check[result_of_random] == false)
                        {
                            result_of_random = random.Next(1, max * max); ;
                        }
                        test[i, j] = result_of_random;
                        check[result_of_random] = false;
                    }

                }
            }

            // if values is impossible -> try again
            if (Check_Test() == false)
            {
                Random_For_Test();
            }
        }

        //------------------------------------------------------
        //-----------------------Function to check win condition
        //------------------------------------------------------
        private bool Check_Win()
        {
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    if (i == max - 1 && j == max - 1)
                        return true;
                    if (image_cell_list[i][j].Value != (i * max + j + 1))
                        return false;
                }
            }
            return true;
        }

        // ----------------------------------------------------------
        // ---------------------Function to swap location of two cell
        // ----------------------------------------------------------
        private void Swap_Two_Cell(ref Image_Cell current_cell, ref Image_Cell null_cell, int[] current_cell_index)
        {
            // swap test tuple
            int tmp = this.test[null_cell_index[0], null_cell_index[1]];
            this.test[null_cell_index[0], null_cell_index[1]] = this.test[current_cell_index[0], current_cell_index[1]];
            this.test[current_cell_index[0], current_cell_index[1]] = tmp;
                // swap cell list
            null_cell.Image = current_cell.Image;
            current_cell.Image = null;
            null_cell.Value = current_cell.Value;
            current_cell.Value = -1;
            null_cell = current_cell;
            null_cell_index[0] = current_cell_index[0];
            null_cell_index[1] = current_cell_index[1];
            this.move_count++;
            this.move_label.Text = "MOVED: " + this.move_count.ToString();
            // when resolve the puzzle ->> you win
            if (Check_Win() == true)
            {
                timer.Stop();
                timerAutoRun.Stop();
                MessageBox.Show("You are Winner!");
                Show_Start_Hide_Play();
            }
        }

        // --------------------------------------
        // -------------move when press arow keys
        // --------------------------------------
        private void MoveUp()
        {
            if (this.canMove == true)
            {
                if (null_cell_index[0] < this.max - 1)
                {
                    int[] current_cell_index = new int[2];
                    current_cell_index[0] = null_cell_index[0] + 1;
                    current_cell_index[1] = null_cell_index[1];
                    Image_Cell current_cell = image_cell_list[current_cell_index[0]][current_cell_index[1]];
                    Swap_Two_Cell(ref current_cell, ref null_cell, current_cell_index);
                }
            }
        }
        private void MoveDown()
        {
            if (this.canMove == true)
            {
                if (null_cell_index[0] > 0)
                {
                    int[] current_cell_index = new int[2];
                    current_cell_index[0] = null_cell_index[0] - 1;
                    current_cell_index[1] = null_cell_index[1];
                    Image_Cell current_cell = image_cell_list[current_cell_index[0]][current_cell_index[1]];
                    Swap_Two_Cell(ref current_cell, ref null_cell, current_cell_index);
                }
            }
        }
        private void MoveLeft()
        {
            if (this.canMove == true)
            {
                if (null_cell_index[1] < this.max - 1)
                {
                    int[] current_cell_index = new int[2];
                    current_cell_index[0] = null_cell_index[0];
                    current_cell_index[1] = null_cell_index[1] + 1;
                    Image_Cell current_cell = image_cell_list[current_cell_index[0]][current_cell_index[1]];
                    Swap_Two_Cell(ref current_cell, ref null_cell, current_cell_index);
                }
            }
        }
        private void MoveRight()
        {
            if (this.canMove == true)
            {
                if (null_cell_index[1] > 0)
                {
                    int[] current_cell_index = new int[2];
                    current_cell_index[0] = null_cell_index[0];
                    current_cell_index[1] = null_cell_index[1] - 1; 
                    Image_Cell current_cell = image_cell_list[current_cell_index[0]][current_cell_index[1]];
                    Swap_Two_Cell(ref current_cell, ref null_cell, current_cell_index);
                }
            }
        }

        //------------------------------------
        //-------------------------- Resolving
        //------------------------------------
        private void AutoMove()
        {
            if (sln.Count > 0)
            {
                int i = sln[0];
                sln.Remove(i);
                if (i == ARROW.UP)
                {
                    MoveUp();
                }
                else if (i == ARROW.DOWN)
                {
                    MoveDown();
                }
                else if (i == ARROW.LEFT)
                {
                    MoveLeft();
                }
                else if (i == ARROW.RIGHT)
                {
                    MoveRight();
                }
            }
            else
            {
                this.timerAutoRun.Stop();
            }
        }
    }
}

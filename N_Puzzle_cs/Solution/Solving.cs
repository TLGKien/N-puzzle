using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    public struct Node
    {
        private int[,] test;
        private List<int> path;
        private int[] null_cell_index;
        private int g;
        private int h;
        
        public int[,] Test { get => test; set => test = value; }
        public List<int> Path { get => path; set => path = value; }
        public int[] Null_cell_index { get => null_cell_index; set => null_cell_index = value; }
        public int G { get => g; set => g = value; }
        public int H { get => h; set => h = value; }

        public Node(Node a)
        {
            this.test = new int[Solving.max,Solving.max];
            Array.Copy(a.test, this.test, Solving.max*Solving.max);
            this.path = new List<int>(a.path);
            this.null_cell_index = new int[2] { a.null_cell_index[0], a.null_cell_index[1] };
            this.g = 0;
            this.h = 0;
        }


        public int heuristic()
        {
            int nb_misplaced = 0;
            int manhattan_dist = 0;
            for (int i = 0; i < Solving.max; i++)
            {
                for (int j = 0; j < Solving.max; j++)
                {
                    if (this.test[i,j] == -1)
                    {
                        continue;
                    }
                    if (this.Test[i, j] != (i * Solving.max + j + 1))
                    {
                        nb_misplaced++;
                        int true_i = (this.Test[i,j] - 1) / Solving.max;
                        int true_j = (this.Test[i,j] - 1) % Solving.max;
                        manhattan_dist += (Math.Abs(true_i - i) + Math.Abs(true_j - j));
                    }
                }
            }
            return manhattan_dist + Convert.ToInt32(Math.Sqrt(nb_misplaced));
        }

    }
    public class Solving
    {
        public static int max;
        private List<int[,]> close;
        private List<Node> open;
        private Node src;
        private int g;

        public Solving(int amax, int[,] test)
        {
            max = amax;
            this.g = 0;
            this.src.Test = test;
            this.src.Null_cell_index = FindNullCell(this.src);
            this.src.Path = new List<int>();
            this.src.G = this.g;
            this.src.H = src.heuristic();
            open = new List<Node>();
            close = new List<int[,]>();          
        }

        private int[] FindNullCell(Node cell)
        {
            for(int i = 0; i < max; i++)
            {
                for(int j = 0; j < max; j++)
                {
                    if (cell.Test[i,j] == -1)
                    {
                        return new int[2] { i, j };
                    }
                }
            }
            return new int[2];
        }
        private bool CanMoveUp(Node cell)
        {
            if (cell.Null_cell_index[0] < max - 1)
            {
                return true;
            }
            return false;
        }
        private bool CanMoveDown(Node cell)
        {
            if (cell.Null_cell_index[0] > 0)
            {
                return true;
            }
            return false;
        }
        private bool CanMoveLeft(Node cell)
        {
            if (cell.Null_cell_index[1] < max - 1)
            {
                return true;
            }
            return false;
        }
        private bool CanMoveRight(Node cell)
        {
            if (cell.Null_cell_index[1] > 0)
            {
                return true;
            }
            return false;
        }
        private void SwapTwoCell(ref Node cell, int[] current_cell_index)
        {
            cell.Test[cell.Null_cell_index[0],cell.Null_cell_index[1]] = cell.Test[current_cell_index[0],current_cell_index[1]];
            cell.Null_cell_index[0] = current_cell_index[0];
            cell.Null_cell_index[1] = current_cell_index[1];
            cell.Test[cell.Null_cell_index[0],cell.Null_cell_index[1]] = -1;
        }
        private Node MoveUp(Node cell)
        {
            Node tmp = new Node(cell);
            if (CanMoveUp(cell) == true)
            {
                int[] current_cell_index = new int[2];
                current_cell_index[0] = tmp.Null_cell_index[0] + 1;
                current_cell_index[1] = tmp.Null_cell_index[1];
                SwapTwoCell(ref tmp, current_cell_index);
                tmp.Path.Add(ARROW.UP);
                tmp.G = cell.G + 1;
                tmp.H = tmp.heuristic();
                return tmp;
            }
            return tmp;
        }
        private Node MoveDown(Node cell)
        {
            Node tmp = new Node(cell);
            if (CanMoveDown(cell) == true)
            {
                int[] current_cell_index = new int[2];
                current_cell_index[0] = tmp.Null_cell_index[0] - 1;
                current_cell_index[1] = tmp.Null_cell_index[1];
                SwapTwoCell(ref tmp, current_cell_index);
                tmp.Path.Add(ARROW.DOWN);
                tmp.G = cell.G + 1;
                tmp.H = tmp.heuristic();
                return tmp;
            }
            return tmp;
        }
        private Node MoveLeft(Node cell)
        {
            Node tmp = new Node(cell);
            if (CanMoveLeft(cell) == true)
            {
                int[] current_cell_index = new int[2];
                current_cell_index[0] = tmp.Null_cell_index[0];
                current_cell_index[1] = tmp.Null_cell_index[1] + 1;
                SwapTwoCell(ref tmp, current_cell_index);
                tmp.Path.Add(ARROW.LEFT);
                tmp.G = cell.G + 1;
                tmp.H = tmp.heuristic();
                return tmp;
            }
            return tmp;
        }
        private Node MoveRight(Node cell)
        {
            Node tmp = new Node(cell);
            if (CanMoveRight(cell) == true)
            {
                int[] current_cell_index = new int[2];
                current_cell_index[0] = tmp.Null_cell_index[0];
                current_cell_index[1] = tmp.Null_cell_index[1] - 1;
                SwapTwoCell(ref tmp, current_cell_index);
                tmp.Path.Add(ARROW.RIGHT);
                tmp.G = cell.G + 1;
                tmp.H = tmp.heuristic();
                return tmp;
            }
            return tmp;
        }

        private bool CompareTwoNode(int[,] node1, int[,] node2)
        {
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                    if (node1[i, j] != node2[i, j])
                        return false;
            }
            return true;
        }
        private bool checkNodeInList(int[,] node, List<int[,]> close)
        {
            for (int i = 0; i < close.Count; i++)
            {
                if (CompareTwoNode(node, close[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private int IndexMin3()
        {
            if (open.Count != 0)
            {
                Node min = new Node();
                min = open[0];
                int index = 0;
                for(int i = 1; i < open.Count; i++)
                {
                    if((min.H + min.G) > (open[i].H + open[i].G))
                    {
                        min = open[i];
                        index = i;
                    }
                    else if ((min.H + min.G) > (open[i].H + open[i].G))
                    {
                        if (min.G > open[i].G)
                        {
                            min = open[i];
                            index = i;
                        }
                    }
                }
                return index;
            }
            return 0;
        }

        private int IndexMin45()
        {
            if (open.Count != 0)
            {
                Node min = new Node();
                min = open[0];
                int index = 0;
                for (int i = 1; i < open.Count; i++)
                {
                    if ((min.H) > (open[i].H))
                    {
                        min = open[i];
                        index = i;
                    }
                    else if ((min.H) == (open[i].H))
                    {
                        if (min.G > open[i].G)
                        {
                            min = open[i];
                            index = i;
                        }
                    }
                }
                return index;
            }
            return 0;
        }

        public List<int> Resolve()
        {
            open.Add(this.src);
            close.Add(this.src.Test);
            Node current, tmp;
            while(this.open.Count > 0)
            {
                if(max == 3 )
                {
                    current = open[IndexMin3()];
                }
                else
                {
                    current = open[IndexMin45()];
                }
                open.Remove(current);
                if (current.heuristic() == 0)
                {
                    return current.Path;
                }

                tmp = MoveUp(current);
                if (checkNodeInList(tmp.Test,close) == false)
                {
                    open.Add(tmp);
                    close.Add(tmp.Test);
                }

                tmp = MoveDown(current);
                if (checkNodeInList(tmp.Test, close) == false)
                {
                    open.Add(tmp);
                    close.Add(tmp.Test);
                }

                tmp = MoveLeft(current);
                if (checkNodeInList(tmp.Test, close) == false)
                {
                    open.Add(tmp);
                    close.Add(tmp.Test);
                }

                tmp = MoveRight(current);
                if (checkNodeInList(tmp.Test, close) == false)
                {
                    open.Add(tmp);
                    close.Add(tmp.Test);
                }


            }
            return new List<int>();
        }
    }
}

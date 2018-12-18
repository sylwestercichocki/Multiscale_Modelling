using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiscale_Modeling.Logic
{

    public static class IListExtensions
    {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    class MC
    {
        List<int[]> t;
        Random rnd;
        public int N;
        //public Cellular_automata_space cas;
        public MC()
        {
            rnd = new Random();
            N = 0;
            Simple_grain_growth.cas = new Cellular_automata_space();
            t = new List<int[]>();
            for(int i = 1; i < Simple_grain_growth.cas.size-1; ++i)
            {
                for (int j = 1; j< Simple_grain_growth.cas.size-1; ++j)
                {
                    int[] x = new int[2];
                    x[0] = j;
                    x[1] = i;
                    t.Add(x);
                }
            }
            

        }

        private List<uint> neighboors(int x, int y)
        {
            List<uint> temp = new List<uint>();
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (Simple_grain_growth.cas.lattice[x + i, y + j].Cell_Type == CELL_TYPE.EMPTY ||
                        Simple_grain_growth.cas.lattice[x + i, y + j].Cell_Type == CELL_TYPE.GRAIN)

                    if (!temp.Contains(Simple_grain_growth.cas.lattice[x + i, y + j].ID)) temp.Add(Simple_grain_growth.cas.lattice[x + i, y + j].ID);
                }
            }

            return temp;
        }

        private int calcEnergy(int x, int y)
        {
            int en = 0;
            uint id = Simple_grain_growth.cas.lattice[x,y].ID;
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (id != Simple_grain_growth.cas.lattice[x + i,y + j].ID) en++;
                }
            }
            return en;
        }

        private void MCupdate(int x, int y)
        {
            uint id = Simple_grain_growth.cas.lattice[x,y].ID;
            int old = calcEnergy(x, y);
            //int r = rnd.Next(1,N+1);
            List<uint> n = neighboors(x, y);
            int r = rnd.Next(0, n.Count);
            Simple_grain_growth.cas.lattice[x, y].ID = n[r];
            int nnew = calcEnergy(x, y);
            if (nnew <= old)
            {
                //Simple_grain_growth.cas.lattice[x, y].Cell_Type = CELL_TYPE.GRAIN;
                
                    Simple_grain_growth.cas.lattice[x, y].ID = (uint)n[r];
            }
            else
            {
                Simple_grain_growth.cas.lattice[x, y].ID = id;
                //Simple_grain_growth.cas.lattice[x, y].Cell_Type = CELL_TYPE.GRAIN;
            }
        }

        public void update(int iter)
        {
            for (int i = 0; i < Simple_grain_growth.cas.size; ++i)
            {
                for (int j = 0; j < Simple_grain_growth.cas.size; ++j)
                {
                    if (i == 0)
                    {
                        Simple_grain_growth.cas.lattice[i, j].ID = Simple_grain_growth.cas.lattice[i + 1, j].ID;
                    }
                    if (j == 0)
                    {
                        Simple_grain_growth.cas.lattice[i, j].ID = Simple_grain_growth.cas.lattice[i, j+1].ID;
                    }
                    if( i == (Simple_grain_growth.cas.size - 1))
                    {
                        Simple_grain_growth.cas.lattice[i, j].ID = Simple_grain_growth.cas.lattice[i - 1, j].ID;
                    }
                    if( j == (Simple_grain_growth.cas.size - 1))
                    {
                        Simple_grain_growth.cas.lattice[i, j].ID = Simple_grain_growth.cas.lattice[i, j - 1].ID;
                    }
                }
            }


            t.Shuffle();
            t.Shuffle();
            for (int i = 0; i < iter; ++i)
            {
                for (int j = 0; j<t.Count; ++j)
                {
                    if(Simple_grain_growth.cas.lattice[t[j][0],t[j][1]].Cell_Type == CELL_TYPE.EMPTY ||
                        Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].Cell_Type == CELL_TYPE.GRAIN)
                    {
                        MCupdate(t[j][0], t[j][1]);
                    }
                }
                //while (t.Count > 0)
                //{
                //    int r = rnd.Next(0, t.Count);
                //    MCupdate(t[r][0], t[r][1]);
                //    t.RemoveAt(r);
                //}

                //for(int k = 0; k<temp.Count; ++k)
                //{
                //    t.Add(temp[k]);
                //}
            }
        }

        public void generateMC(int n)
        {
            Simple_grain_growth.current_id += (uint)n;
            N = n;
            for (int i = 0; i < Simple_grain_growth.cas.size - 0; ++i)
            {
                for (int j = 0; j < Simple_grain_growth.cas.size - 0; ++j)
                {
                    if (Simple_grain_growth.cas.lattice[i, j].Cell_Type == CELL_TYPE.EMPTY
                        || Simple_grain_growth.cas.lattice[i, j].Cell_Type == CELL_TYPE.GRAIN
                        || Simple_grain_growth.cas.lattice[i, j].Cell_Type == CELL_TYPE.MC)
                    {
                        int o = rnd.Next(2, N + 2);
                        Simple_grain_growth.cas.lattice[i, j].Cell_Type = CELL_TYPE.GRAIN;
                        Simple_grain_growth.cas.lattice[i, j].ID = (uint)o;
                    }
                }
            }
        }

    }
}

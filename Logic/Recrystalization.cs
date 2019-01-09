using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiscale_Modeling.Logic
{
    class Recrystalization
    {
        List<int[]> t;
        Random rnd;
        public int N;
        public uint currentRecID;
        public int highestEnergy;
        public Recrystalization()
        {
            rnd = new Random();
            N = 0;
            currentRecID = 1;
            Simple_grain_growth.cas = new Cellular_automata_space();
            t = new List<int[]>();
            for (int i = 1; i < Simple_grain_growth.cas.size - 1; ++i)
            {
                for (int j = 1; j < Simple_grain_growth.cas.size - 1; ++j)
                {
                    int[] x = new int[2];
                    x[0] = j;
                    x[1] = i;
                    t.Add(x);
                }
            }
        }

        private bool boundary(int x, int y)
        {
            int id = (int)Simple_grain_growth.cas.lattice[x, y].ID;
            for(int i = -1; i < 1; ++i)
            {
                for(int j = -1; j < 1; ++j)
                {
                    if (Simple_grain_growth.cas.lattice[x + i, y + j].ID != id) return true;
                }
            }
            return false;
        }


        public void distEnergy(int h_lower, int h_higher = 0)
        {
            highestEnergy = h_lower;
            if (h_higher > highestEnergy) highestEnergy = h_higher;
            for(int i = 1; i < Simple_grain_growth.cas.size - 1; ++i)
            {
                for (int j = 1; j < Simple_grain_growth.cas.size - 1; ++j)
                {
                    Simple_grain_growth.cas.lattice[i, j].H = h_lower;
                }
            }
            if (h_higher > 0)
            {
                for (int i = 1; i < Simple_grain_growth.cas.size - 1; ++i)
                {
                    for (int j = 1; j < Simple_grain_growth.cas.size - 1; ++j)
                    {
                        if (boundary(i, j))
                            Simple_grain_growth.cas.lattice[i, j].H = h_higher;
                    }
                }
            }
        }

        public void addNewNuclei(int n, bool onBoundaries=false)
        {
            int j = 0;
            Random rnd = new Random();
            for (int i = 0; i < n; ++i)
            {
                int x, y;
                do
                {
                    ++j;
                    if (onBoundaries)
                    {
                        while (true)
                        {
                            x = rnd.Next(1, checked((int)Simple_grain_growth.cas.size - 1));
                            y = rnd.Next(1, checked((int)Simple_grain_growth.cas.size - 1));
                            if (boundary(x, y)) break;
                        }
                    }
                    else
                    {
                        x = rnd.Next(1, checked((int)Simple_grain_growth.cas.size - 1));
                        y = rnd.Next(1, checked((int)Simple_grain_growth.cas.size - 1));
                    }
                    if (j > 5000) break;
                } while (Simple_grain_growth.cas.lattice[x, y].recrystalized);



                if (!Simple_grain_growth.cas.lattice[x, y].recrystalized)
                {
                    Simple_grain_growth.cas.lattice[x, y].recrystalized = true;
                    Simple_grain_growth.cas.lattice[x, y].H = 0;
                    Simple_grain_growth.cas.lattice[x, y].rec_ID = currentRecID;
                    int newid = rnd.Next(0, (int)Simple_grain_growth.current_id);
                    //Simple_grain_growth.cas.lattice[x, y].ID = (uint)newid;
                    ++currentRecID;
                }
            }
        }

        private int calcEnergy(int x, int y, bool first)
        {
            int en = 0;
            uint id = Simple_grain_growth.cas.lattice[x, y].ID;
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (id != Simple_grain_growth.cas.lattice[x + i, y + j].ID) en++;
                }
            }
            if (first) en += Simple_grain_growth.cas.lattice[x, y].H;
            return en;
        }

        private List<int[]> rec_neighboor(int x, int y)
        {
            List<int[]> temp = new List<int[]>();
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (Simple_grain_growth.cas.lattice[x + i, y + j].recrystalized)
                    {
                        int[] tt = new int[2];
                        tt[0] = x + i;
                        tt[1] = y + j;
                        temp.Add(tt);
                    }
                }
            }
            return temp;
        }

        public void compute(int n, int nNuclei, bool onBeginning, bool onBoundaries, bool increasing)
        {
            bool beg = true;
            if (onBeginning && beg) { addNewNuclei(nNuclei, onBoundaries); beg = false; }
            for(int i = 0; i < n; ++i)
            {
                t.Shuffle();
                t.Shuffle();
                if (!onBeginning) addNewNuclei(nNuclei, onBoundaries);
                if (increasing) nNuclei *= 2;
                for(int j = 0; j < t.Count; ++j)
                {
                    //if (Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].recrystalized) continue;
                    List<int[]> recNeighboorsID = rec_neighboor(t[j][0], t[j][1]);
                    if (recNeighboorsID.Count == 0) continue;
                    int r = rnd.Next(0, recNeighboorsID.Count);
                    int oldEnergy = calcEnergy(t[j][0], t[j][1], true);
                    uint oldID = Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].ID;

                    //int newid = rnd.Next(0, (int)Simple_grain_growth.current_id);
                    Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].ID =
                        Simple_grain_growth.cas.lattice[recNeighboorsID[r][0], recNeighboorsID[r][1]].ID;
                    //Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].ID = (uint)newid;
                    int energy = calcEnergy(t[j][0], t[j][1], false);

                    if (energy < oldEnergy)
                    {
                        Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].H = 0;
                        Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].recrystalized = true;
                        Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].rec_ID =
                        Simple_grain_growth.cas.lattice[recNeighboorsID[r][0], recNeighboorsID[r][1]].rec_ID;
                    }
                    else
                    {
                        Simple_grain_growth.cas.lattice[t[j][0], t[j][1]].ID = oldID;
                    }
                }
            }
        }
    }
}

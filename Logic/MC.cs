using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiscale_Modeling.Logic
{
    class MC
    {
        List<int[]> t;
        List<int[]> temp;
        Random rnd;
        public int N;
        public Cellular_automata_space cas;
        public MC()
        {
            rnd = new Random();
            N = 0;
            cas = new Cellular_automata_space();
            t = new List<int[]>();
            temp = new List<int[]>();
            for(int i = 1; i < cas.size-1; ++i)
            {
                for (int j = 1; j<cas.size-1; ++j)
                {
                    int[] x = new int[2];
                    x[0] = j;
                    x[1] = i;
                    t.Add(x);
                    temp.Add(x);
                }
            }
        }

        private int calcEnergy(int x, int y)
        {
            int en = 0;
            uint id = cas.lattice[x,y].ID;
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (id != cas.lattice[x + i,y + j].ID) en++;
                }
            }
            return en;
        }

        private void MCupdate(int x, int y)
        {

            uint id = cas.lattice[x,y].ID;
            int old = calcEnergy(x, y);
            int r = rnd.Next(1,N+1);
            cas.lattice[x, y].ID = (uint)r;
            int nnew = calcEnergy(x, y);
            if (nnew < old)
            {
                //cas.lattice[x, y].Cell_Type = CELL_TYPE.GRAIN;
                cas.lattice[x, y].ID = (uint)r;
            }
            else
            {
                cas.lattice[x, y].ID = id;
                //cas.lattice[x, y].Cell_Type = CELL_TYPE.GRAIN;
            }
        }

        public void update(int iter)
        {
            for (int i = 0; i < iter; ++i)
            {
                while (t.Count > 0)
                {
                    int r = rnd.Next(0, t.Count);
                    MCupdate(t[r][0], t[r][1]);
                    t.RemoveAt(r);
                }

                for(int k = 0; k<temp.Count; ++k)
                {
                    t.Add(temp[k]);
                }
            }
        }

        public void generateMC(int n)
        {
            N = n;
            for(int i = 1; i < cas.size - 1; ++i)
            {
                for(int j = 1; j < cas.size - 1; ++j)
                {
                    int o = rnd.Next(1,N+1);
                    cas.lattice[i, j].Cell_Type = CELL_TYPE.MC;
                    cas.lattice[i, j].ID = (uint)o;
                }
            }
        }
    }
}

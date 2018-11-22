using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiscale_Modeling
{
    class Cellular_automata_space
    {
        public uint xsize;
        public uint ysize;
        public uint size;
        public int numOfEmptyCells;
        public NEIGHBORHOOD_TYPE NEIGHBORHOOD_TYPE;
        public BOUNDARY_CONDITION BOUNDARY_CONDITION;
        //private List<List<Cell>> lattice;
        public Cell[,] lattice;
        //private List<List<Cell>> lattice_temp;
        public Cell[,] lattice_temp;

        public Cellular_automata_space()
        {
            
            size = 302;
            numOfEmptyCells = (int)((size-2)*(size-2));
            NEIGHBORHOOD_TYPE = NEIGHBORHOOD_TYPE.NEUMANN;
            BOUNDARY_CONDITION = BOUNDARY_CONDITION.ABSORBING;
            lattice = new Cell[size,size];
            lattice_temp = new Cell[size, size];
            //lattice = new List<List<Cell>>();
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    lattice[i, j] = new Cell();
                    lattice_temp[i, j] = new Cell();
                }
            }

            //lattice_temp = new List<List<Cell>>();
            //for (int i = 0; i < size; ++i)
            //{
            //    lattice_temp.Add(new List<Cell>());
            //    for (int j = 0; j < size; ++j)
            //    {
            //        lattice_temp[i].Add(new Cell());
            //    }
            //}
        }
        public Cellular_automata_space(uint s)
        {
            numOfEmptyCells = (int)(s*s);
            size = s+2;
            NEIGHBORHOOD_TYPE = NEIGHBORHOOD_TYPE.NEUMANN;
            BOUNDARY_CONDITION = BOUNDARY_CONDITION.ABSORBING;
            lattice = new Cell[size, size];
            lattice_temp = new Cell[size, size];
            //lattice = new List<List<Cell>>();
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    lattice[i, j] = new Cell();
                    lattice_temp[i, j] = new Cell();
                }
            }
        }
        //returns id of most common neighbor
        uint Neumann(int x, int y)
        {
            uint[] id = new uint[4];
            uint[] n = new uint[4];
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (i == -1 && j == -1) continue;
                    if (i == -1 && j == 1) continue;
                    if (i == 1 && j == -1) continue;
                    if (i == 1 && j == 1) continue;
                    if (lattice[x + i, y + j].Cell_Type == CELL_TYPE.EMPTY) continue;
                    if (lattice[x + i, y + j].Cell_Type == CELL_TYPE.SAVED) continue;

                    for (int w = 0; w < 4; ++w)
                    {
                        if (id[w] == lattice[x + i, y + j].ID)
                        {
                            n[w]++;
                            break;
                        }
                        else
                        {
                            if (id[w] == 0)
                            {
                                id[w] = lattice[x + i, y + j].ID;
                                n[w]++;
                                break;
                            }
                        }
                    }
                }
            }

            uint max = n.Max();
            List<uint> nn = new List<uint>();
            int q = 0;
            for (int i = 0; i < 4; ++i)
            {
                if (n[i] == max)
                {
                    nn.Add(id[i]);
                    q++;
                }
            }

            if (q == 1)
                return nn[0];
            else
            {
                Random rnd = new Random();
                int o = rnd.Next(nn.Count);
                return nn[o];
            }
        }
        
        public void updateNeumann()
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    lattice_temp[i,j].copy(lattice[i,j]);
                }
            }
            //sw.Stop();
            //Console.WriteLine(sw.Elapsed);
            //lattice_temp = lattice.Clone() as Cell[,];

            //sw.Start();

            for (int i = 1; i < size - 1; ++i)
            {
                for (int j = 1; j < size - 1; ++j)
                {
                    if (lattice[i, j].Cell_Type == CELL_TYPE.EMPTY)
                    {
                        
                        uint t = Neumann(i, j);
                        
                        if (t > 0)
                        {
                            lattice_temp[i, j].ID = t;
                            lattice_temp[i, j].Cell_Type = CELL_TYPE.GRAIN;
                            //lattice_temp[i][j].set(true, t);
                            //lattice_temp[i][j].setColor(colors[t - 1]);
                            --numOfEmptyCells;

                            //std::vector < unsigned int> xy;
                            //xy.push_back(i);
                            //xy.push_back(j);
                            //updated.push_back(xy);
                        }
                    }
                }
            }

            //sw.Stop();
            //Console.WriteLine(sw.Elapsed);

            ////Lattice = Lattice_temp.Clone() as Cell[,];

            //sw.Start();
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    lattice[i,j].copy(lattice_temp[i,j]);
                }
            }

            //sw.Stop();
            //Console.WriteLine(sw.Elapsed);
            //lattice.Clear();
            //lattice = new List<List<Cell>>(lattice_temp);
        }

        uint MooreRule1(int x, int y)
        {

            uint[] id = new uint[8];
            uint[] n = new uint[8];
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (lattice[x + i, y + j].Cell_Type == CELL_TYPE.EMPTY) continue;

                    for (int w = 0; w < 8; ++w)
                    {
                        if (id[w] == lattice[x + i, y + j].ID)
                        {
                            n[w]++;
                            break;
                        }
                        else
                        {
                            if (id[w] == 0)
                            {
                                id[w] = lattice[x + i, y + j].ID;
                                n[w]++;
                                break;
                            }
                        }
                    }
                }
            }

            uint max = n.Max();
            List<uint> nn = new List<uint>();
            int q = 0;
            for (int i = 0; i < 8; ++i)
            {
                if (n[i] == max)
                {
                    if (max > 4)
                    {
                        nn.Add(id[i]);
                        q++;
                    }
                }
            }

            if (q == 1)
                return nn[0];
            else
            {
                return 0;
            }
        }

        uint MooreRule2(int x, int y)
        {
            uint[] id = new uint[4];
            uint[] n = new uint[4];
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (i == -1 && j == -1) continue;
                    if (i == -1 && j == 1) continue;
                    if (i == 1 && j == -1) continue;
                    if (i == 1 && j == 1) continue;
                    if (lattice[x + i, y + j].Cell_Type == CELL_TYPE.EMPTY) continue;

                    for (int w = 0; w < 4; ++w)
                    {
                        if (id[w] == lattice[x + i, y + j].ID)
                        {
                            n[w]++;
                            break;
                        }
                        else
                        {
                            if (id[w] == 0)
                            {
                                id[w] = lattice[x + i, y + j].ID;
                                n[w]++;
                                break;
                            }
                        }
                    }
                }
            }

            uint max = n.Max();
            List<uint> nn = new List<uint>();
            int q = 0;
            for (int i = 0; i < 4; ++i)
            {
                if (n[i] == max)
                {
                    if (max > 2)
                    {
                        nn.Add(id[i]);
                        q++;
                    }
                }
            }

            if (q == 1)
                return nn[0];
            else
            {
                return 0;
            }
        }

        uint MooreRule3(int x, int y)
        {
            uint[] id = new uint[4];
            uint[] n = new uint[4];
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (i == 0 && j == -1) continue;
                    if (i == -1 && j == 0) continue;
                    if (i == 1 && j == 0) continue;
                    if (i == 0 && j == -1) continue;
                    if (lattice[x + i, y + j].Cell_Type == CELL_TYPE.EMPTY) continue;

                    for (int w = 0; w < 4; ++w)
                    {
                        if (id[w] == lattice[x + i, y + j].ID)
                        {
                            n[w]++;
                            break;
                        }
                        else
                        {
                            if (id[w] == 0)
                            {
                                id[w] = lattice[x + i, y + j].ID;
                                n[w]++;
                                break;
                            }
                        }
                    }
                }
            }

            uint max = n.Max();
            List<uint> nn = new List<uint>();
            int q = 0;
            for (int i = 0; i < 4; ++i)
            {
                if (n[i] == max)
                {
                    if (max > 2)
                    {
                        nn.Add(id[i]);
                        q++;
                    }
                }
            }

            if (q == 1)
                return nn[0];
            else
            {
                return 0;
            }
        }

        uint MooreRule4(int x, int y, int propability)
        {
            uint[] id = new uint[8];
            uint[] n = new uint[8];
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (lattice[x + i, y + j].Cell_Type == CELL_TYPE.EMPTY) continue;

                    for (int w = 0; w < 8; ++w)
                    {
                        if (id[w] == lattice[x + i, y + j].ID)
                        {
                            n[w]++;
                            break;
                        }
                        else
                        {
                            if (id[w] == 0)
                            {
                                id[w] = lattice[x + i, y + j].ID;
                                n[w]++;
                                break;
                            }
                        }
                    }
                }
            }

            uint max = n.Max();
            List<uint> nn = new List<uint>();
            int q = 0;
            for (int i = 0; i < 8; ++i)
            {
                if (n[i] == max)
                {
                        nn.Add(id[i]);
                        q++;
                }
            }

            if (q == 1)
            {
                Random rnd = new Random();
                int o = rnd.Next(100);
                if (o < propability)
                {
                    return nn[0];
                }
                else return 0;
            }
            else
            {
                Random rnd = new Random();
                int o = rnd.Next(nn.Count);
                int oo = rnd.Next(100);
                if (oo < propability)
                {
                    return nn[o];
                }
                else return 0;
            }
        }
        
        public void updateAdvancedMoore(int propability)
        {
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    lattice_temp[i,j].copy(lattice[i,j]);
                }
            }
            //Console.WriteLine(sw.Elapsed);

            //sw.Start();

            for (int i = 1; i < size - 1; ++i)
            {
                for (int j = 1; j < size - 1; ++j)
                {
                    if (lattice[i, j].Cell_Type == CELL_TYPE.EMPTY)
                    {

                        uint r1 = MooreRule1(i, j);
                        if (r1 > 0)
                        {
                            lattice_temp[i, j].ID = r1;
                            lattice_temp[i, j].Cell_Type = CELL_TYPE.GRAIN;
                            --numOfEmptyCells;
                            continue;
                        }
                        uint r2 = MooreRule2(i, j);
                        if (r2 > 0)
                        {
                            lattice_temp[i, j].ID = r2;
                            lattice_temp[i, j].Cell_Type = CELL_TYPE.GRAIN;
                            --numOfEmptyCells;
                            continue;
                        }
                        uint r3 = MooreRule3(i, j);
                        if ( r3 > 0)
                        {
                            lattice_temp[i, j].ID = r3;
                            lattice_temp[i, j].Cell_Type = CELL_TYPE.GRAIN;
                            --numOfEmptyCells;
                            continue;
                        }
                        uint r4 = MooreRule4(i, j, propability);
                        if(r4 > 0)
                        {
                            lattice_temp[i, j].ID = r4;
                            lattice_temp[i, j].Cell_Type = CELL_TYPE.GRAIN;
                            --numOfEmptyCells;
                            continue;
                        }
                    }
                }
            }

            //sw.Stop();
            //Console.WriteLine(sw.Elapsed);


            //sw.Start();
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    lattice[i,j].copy(lattice_temp[i,j]);
                }
            }

            //sw.Stop();
            //Console.WriteLine(sw.Elapsed);
        }
        //public uint Size { get => size; set => size = value; }
        //internal NEIGHBORHOOD_TYPE NEIGHBORHOOD_TYPE1 { get => NEIGHBORHOOD_TYPE; set => NEIGHBORHOOD_TYPE = value; }
        //internal BOUNDARY_CONDITION BOUNDARY_CONDITION1 { get => BOUNDARY_CONDITION; set => BOUNDARY_CONDITION = value; }
        //internal Cell[,] Lattice_temp { get => lattice_temp; set => lattice_temp = value; }
        //internal Cell[,] Lattice { get => lattice; set => lattice = value; }
        //internal List<List<Cell>> Lattice { get => lattice; set => lattice = value; }
        //internal List<List<Cell>> Lattice_temp { get => lattice_temp; set => lattice_temp = value; }
    }
}

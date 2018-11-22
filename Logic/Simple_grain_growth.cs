using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiscale_Modeling.Logic
{
    class Simple_grain_growth
    {
        public uint xsize;
        public uint ysize;
        public uint size;
        public Cellular_automata_space cas;
        public uint current_id;
        public List<uint> selectedGrainsId;
        public List<int[]> boundaryCoord;

        public Simple_grain_growth()
        {
            cas = new Cellular_automata_space();
            size = cas.size;
            current_id = 1;
            selectedGrainsId = new List<uint>();
            boundaryCoord = new List<int[]>();
        }
        public Simple_grain_growth(uint s)
        {
            cas = new Cellular_automata_space(s);
            size = s;
            current_id = 1;
            selectedGrainsId = new List<uint>();
        }

        public void SetSeeds(int numOfSeeds)
        {
            Random rnd = new Random();
            for (int i = 0; i < numOfSeeds; ++i)
            {
                int x = rnd.Next(1, checked((int)size - 1));
                int y = rnd.Next(1, checked((int)size - 1));
                if(cas.lattice[x,y].Cell_Type == CELL_TYPE.EMPTY)
                {
                    cas.lattice[x, y].Cell_Type = CELL_TYPE.GRAIN;
                    cas.lattice[x, y].ID = current_id;
                    ++current_id;
                    --cas.numOfEmptyCells;
                }
                //else
                //{
                //    do
                //    {
                //        x = rnd.Next(1, checked((int)size - 1));
                //        y = rnd.Next(1, checked((int)size - 1));
                //    } while (cas.lattice[x, y].Cell_Type == CELL_TYPE.EMPTY);
                //    cas.lattice[x, y].Cell_Type = CELL_TYPE.GRAIN;
                //    cas.lattice[x, y].ID = current_id;
                //    ++current_id;
                //    --cas.numOfEmptyCells;
                //}
            }
        }

        public void GenerateInclusions(int num, int sizeOfInclusion, int inclusionType)
        {
            Random rnd = new Random();

            if(inclusionType == 1)
            {
                if (cas.numOfEmptyCells > 0)
                {
                    for (int i = 0; i < num; ++i)
                    {

                        int x = 0;
                        int y = 0;


                        bool b = true;
                        while (b)
                        {
                            x = rnd.Next(1, checked((int)size - sizeOfInclusion));
                            y = rnd.Next(1, checked((int)size - sizeOfInclusion));
                            int bb = 0;
                            for (int ix = 0; ix < sizeOfInclusion; ++ix)
                            {
                                for (int iy = 0; iy < sizeOfInclusion; ++iy)
                                {
                                    if (cas.lattice[x + ix, y + iy].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                }
                            }
                            if (bb == 0) b = false;
                        }


                        for (int ix = 0; ix < sizeOfInclusion; ++ix)
                        {
                            for (int iy = 0; iy < sizeOfInclusion; ++iy)
                            {
                                cas.lattice[x + ix, y + iy].Cell_Type = CELL_TYPE.INCLUSION;
                                --cas.numOfEmptyCells;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < num; ++i)
                    {
                        int x = 0;
                        int y = 0;

                        bool b = true;
                        while (b)
                        {
                            x = rnd.Next(1, checked((int)size - sizeOfInclusion));
                            y = rnd.Next(1, checked((int)size - sizeOfInclusion));
                            int bb = 0;
                            for (int ix = 0; ix < sizeOfInclusion; ++ix)
                            {
                                for (int iy = 0; iy < sizeOfInclusion; ++iy)
                                {
                                    if (cas.lattice[x + ix, y + iy].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                }
                            }
                            int first = (int)cas.lattice[(x + sizeOfInclusion / 2), (y + sizeOfInclusion / 2)].ID; 
                            int boundary = 0;
                            for(int ib = -1; ib < 1; ++ib)
                            {
                                for (int jb = -1; jb < 1; ++jb)
                                {
                                    if (ib == 0 && jb == 0) continue;
                                    if (cas.lattice[(x + sizeOfInclusion / 2) + ib, (y + sizeOfInclusion / 2) + jb].ID != first) ++boundary;
                                }
                            }

                            if ((bb == 0) && (boundary > 0)) b = false;

                        }


                        for (int ix = 0; ix < sizeOfInclusion; ++ix)
                        {
                            for (int iy = 0; iy < sizeOfInclusion; ++iy)
                            {
                                cas.lattice[x + ix, y + iy].Cell_Type = CELL_TYPE.INCLUSION;
                                --cas.numOfEmptyCells;
                            }
                        }
                    }
                }
            }

            if(inclusionType == 2)
            {
                if (cas.numOfEmptyCells > 0)
                {
                    for (int i = 0; i < num; ++i)
                    {
                        int x = 0;
                        int y = 0;
                        bool b = true;
                        while (b)
                        {
                            x = rnd.Next(1 + sizeOfInclusion, checked((int)size - sizeOfInclusion));
                            y = rnd.Next(1 + sizeOfInclusion, checked((int)size - sizeOfInclusion));
                            int bb = 0;
                            for (int ix = x - sizeOfInclusion; ix <= x; ix++)
                            {
                                for (int iy = y - sizeOfInclusion; iy <= y; iy++)
                                {
                                    if ((ix - x) * (ix - x) + (iy - y) * (iy - y) <= sizeOfInclusion * sizeOfInclusion)
                                    {
                                        int xSym = x - (ix - x);
                                        int ySym = y - (iy - y);
                                        if (cas.lattice[ix, iy].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                        if (cas.lattice[ix, ySym].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                        if (cas.lattice[xSym, iy].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                        if (cas.lattice[xSym, ySym].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                    }
                                }
                            }
                            if (bb == 0) b = false;
                        }
                        for (int ix = x - sizeOfInclusion; ix <= x; ix++)
                        {
                            for (int iy = y - sizeOfInclusion; iy <= y; iy++)
                            {
                                if ((ix - x) * (ix - x) + (iy - y) * (iy - y) <= sizeOfInclusion * sizeOfInclusion)
                                {
                                    int xSym = x - (ix - x);
                                    int ySym = y - (iy - y);
                                    cas.lattice[ix, iy].Cell_Type = CELL_TYPE.INCLUSION;
                                    cas.lattice[ix, ySym].Cell_Type = CELL_TYPE.INCLUSION;
                                    cas.lattice[xSym, iy].Cell_Type = CELL_TYPE.INCLUSION;
                                    cas.lattice[xSym, ySym].Cell_Type = CELL_TYPE.INCLUSION;
                                    cas.numOfEmptyCells -= 4;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < num; ++i)
                    {
                        int x = 0;
                        int y = 0;
                        bool b = true;
                        while (b)
                        {
                            x = rnd.Next(1 + sizeOfInclusion, checked((int)size - sizeOfInclusion));
                            y = rnd.Next(1 + sizeOfInclusion, checked((int)size - sizeOfInclusion));
                            int bb = 0;
                            for (int ix = x - sizeOfInclusion; ix <= x; ix++)
                            {
                                for (int iy = y - sizeOfInclusion; iy <= y; iy++)
                                {
                                    if ((ix - x) * (ix - x) + (iy - y) * (iy - y) <= sizeOfInclusion * sizeOfInclusion)
                                    {
                                        int xSym = x - (ix - x);
                                        int ySym = y - (iy - y);
                                        if (cas.lattice[ix, iy].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                        if (cas.lattice[ix, ySym].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                        if (cas.lattice[xSym, iy].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                        if (cas.lattice[xSym, ySym].Cell_Type == CELL_TYPE.INCLUSION) ++bb;
                                    }
                                }
                            }


                            int first = (int)cas.lattice[x, y].ID;
                            int boundary = 0;
                            for (int ib = -1; ib < 1; ++ib)
                            {
                                for (int jb = -1; jb < 1; ++jb)
                                {
                                    if (ib == 0 && jb == 0) continue;
                                    if (cas.lattice[x + ib, y + jb].ID != first) ++boundary;
                                }
                            }

                            if ((bb == 0) && (boundary > 0)) b = false;


                        }
                        for (int ix = x - sizeOfInclusion; ix <= x; ix++)
                        {
                            for (int iy = y - sizeOfInclusion; iy <= y; iy++)
                            {
                                if ((ix - x) * (ix - x) + (iy - y) * (iy - y) <= sizeOfInclusion * sizeOfInclusion)
                                {
                                    int xSym = x - (ix - x);
                                    int ySym = y - (iy - y);
                                    cas.lattice[ix, iy].Cell_Type = CELL_TYPE.INCLUSION;
                                    cas.lattice[ix, ySym].Cell_Type = CELL_TYPE.INCLUSION;
                                    cas.lattice[xSym, iy].Cell_Type = CELL_TYPE.INCLUSION;
                                    cas.lattice[xSym, ySym].Cell_Type = CELL_TYPE.INCLUSION;
                                    cas.numOfEmptyCells -= 4;
                                }
                            }
                        }
                    }
                }
            }


            
        }
        public void update(int NeighborhoodType, int propability)
        {
            if (NeighborhoodType == 1)
            {
                if (cas.numOfEmptyCells > 0)
                {
                    int i = 0;
                    while (cas.numOfEmptyCells > 0)
                    {
                        //Stopwatch sw = new Stopwatch();
                        //sw.Start();
                        cas.updateNeumann();
                        ++i;
                        if (i > 500) break;
                        //sw.Stop();
                        //Console.WriteLine(sw.Elapsed);
                    }
                    Console.WriteLine(i);
                }
            }
            if (NeighborhoodType == 2)
            {
                if (cas.numOfEmptyCells > 0)
                {
                    int i = 0;
                    while (cas.numOfEmptyCells > 0)
                    {
                        //Stopwatch sw = new Stopwatch();
                        //sw.Start();
                        cas.updateAdvancedMoore(propability);
                        ++i;
                        //sw.Stop();
                        //Console.WriteLine(sw.Elapsed);
                    }
                    Console.WriteLine(i);
                }
            }

        }

        public void SelectGrains(int x, int y)
        {
            if(cas.lattice[x,y].Cell_Type == CELL_TYPE.GRAIN || cas.lattice[x,y].Cell_Type == CELL_TYPE.SAVED || cas.lattice[x,y].Cell_Type == CELL_TYPE.INCLUSION)
            {
                bool removed = false;
                for(int i =0; i<selectedGrainsId.Count; ++i)
                {
                    if (selectedGrainsId[i] == cas.lattice[x, y].ID)
                    {
                        selectedGrainsId.RemoveAt(i);
                        removed = true;
                        Console.WriteLine("Unselected grain id: " + cas.lattice[x, y].ID);
                    }
                }
                if (!removed)
                {
                    selectedGrainsId.Add(cas.lattice[x, y].ID);
                    Console.WriteLine("Selected grain id: " + cas.lattice[x, y].ID);
                }
            }



        }

        public void substructure()
        {
            //int tempId = 1;
            //for (int i = 0; i < selectedGrainsId.Count; ++i)
            //{

            //}
            cas.numOfEmptyCells = (int)((size - 2) * (size - 2));

            for (int i = 1; i < size; ++i)
            {
                for (int j = 1; j < size; ++j)
                {
                    if (selectedGrainsId.Contains(cas.lattice[i, j].ID))
                    {
                        cas.lattice[i, j].Cell_Type = CELL_TYPE.SAVED;
                        --cas.numOfEmptyCells;
                        continue;
                    }
                    cas.lattice[i, j].ID = 0;
                    cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                    //++cas.numOfEmptyCells;
                }
            }
        }

        public void dualPhase()
        {
            cas.numOfEmptyCells = (int)((size - 2) * (size - 2));

            for (int i = 1; i < size; ++i)
            {
                for (int j = 1; j < size; ++j)
                {
                    if (selectedGrainsId.Contains(cas.lattice[i, j].ID))
                    {
                        cas.lattice[i, j].ID = 1;
                        cas.lattice[i, j].Cell_Type = CELL_TYPE.SAVED;
                        --cas.numOfEmptyCells;
                        continue;
                    }
                    cas.lattice[i, j].ID = 0;
                    cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                    //++cas.numOfEmptyCells;
                }
            }
        }

        public void singleGrain()
        {
            cas.numOfEmptyCells = (int)((size - 2) * (size - 2));
            boundaryCoord.Clear();
            for (int i = 1; i < size; ++i)
            {
                for (int j = 1; j < size; ++j)
                {
                    if (selectedGrainsId.Contains(cas.lattice[i, j].ID))
                    {
                        if (boundary(i, j))
                        {
                            int[] coord = new int[2];
                            coord[0] = i;
                            coord[1] = j;
                            boundaryCoord.Add(coord);
                            //cas.lattice[i, j].Cell_Type = CELL_TYPE.INCLUSION;
                            --cas.numOfEmptyCells;
                            continue;
                        }
                        //cas.lattice[i, j].ID = 0;
                        //cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                    }
                    //cas.lattice[i, j].ID = 0;
                    //cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                    //++cas.numOfEmptyCells;
                }
            }

            for (int i = 1; i < size; ++i)
            {
                for (int j = 1; j < size; ++j)
                {
                    cas.lattice[i, j].ID = 0;
                    cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                }
            }

            for (int i = 0; i < boundaryCoord.Count; ++i)
            {
                int x = boundaryCoord[i][0];
                int y = boundaryCoord[i][1];

                cas.lattice[x, y].Cell_Type = CELL_TYPE.INCLUSION;
            }

            //for (int i = 1; i < size; ++i)
            //{
            //    for (int j = 1; j < size; ++j)
            //    {
            //        if (cas.lattice[i, j].Cell_Type != CELL_TYPE.INCLUSION)
            //        {
            //            cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
            //            cas.lattice[i, j].ID = 0;
            //        }
            //        else
            //        {
            //            //cas.lattice[i, j].ID = 0;
            //        }
            //    }
            //}
        }

        public void allGrains()
        {
            cas.numOfEmptyCells = (int)((size - 2) * (size - 2));
            boundaryCoord.Clear();

            for (int i = 1; i < size-1; ++i)
            {
                for (int j = 1; j < size-1; ++j)
                {
                    if (boundary(i, j))
                    {
                        int[] coord = new int[2];
                        coord[0] = i;
                        coord[1] = j;
                        boundaryCoord.Add(coord);
                        //cas.lattice[i, j].Cell_Type = CELL_TYPE.INCLUSION;
                        //cas.lattice[i, j].ID = 0;
                        //--cas.numOfEmptyCells;
                        continue;
                    }
                    //cas.lattice[i, j].ID = 0;
                    //cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                    //cas.lattice[i, j].ID = 0;
                    //cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                    //++cas.numOfEmptyCells;
                }
            }

            for (int i = 1; i < size-1; ++i)
            {
                for (int j = 1; j < size-1; ++j)
                {
                    cas.lattice[i, j].ID = 0;
                    cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                }
            }

            for(int i = 0; i<boundaryCoord.Count; ++i)
            {
                int x = boundaryCoord[i][0];
                int y = boundaryCoord[i][1];

                cas.lattice[x, y].Cell_Type = CELL_TYPE.INCLUSION;
                --cas.numOfEmptyCells;
            }
        }

        public bool boundary(int x, int y)
        {
            int tempId = (int)cas.lattice[x, y].ID;
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (cas.lattice[x + i, y + j].Cell_Type == CELL_TYPE.EMPTY) continue;
                    if (tempId != cas.lattice[x + i, y + j].ID) return true;
                    
                }
            }
            return false;
        }

        public void clear()
        {
            cas = new Cellular_automata_space();
            size = cas.size;
            current_id = 1;
            selectedGrainsId.Clear();
        }
        //public uint Size { get => size; set => size = value; }
        //internal Cellular_automata_space Cas { get => cas; set => cas = value; }
    }
}

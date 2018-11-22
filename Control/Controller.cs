using Multiscale_Modeling.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiscale_Modeling.Control
{
    class Controller
    {
        public Bitmap bm;
        private Graphics gr;
        private Brush brush;
        public Simple_grain_growth simple;
        public List<Color> colors;
        private Random rnd = new Random();
        private uint pixel_size;

        public Controller()
        {
            bm = new Bitmap(600, 600);
            gr = Graphics.FromImage(bm);
            brush = new SolidBrush(Color.Black);
            simple = new Simple_grain_growth();
            gr.FillRectangle(brush, 0, 0, 600, 600);
            pixel_size = 600 / (simple.size - 2);
            colors = new List<Color>();
            colors.Add(Color.Black);
        }
        public void setSize(uint xsize, uint ysize)
        {
        }
        public Bitmap GenerateSeeds(int num)
        {
            simple.SetSeeds(num);
            for(int i = 0; i < simple.current_id; ++i)
            {
                colors.Add(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
            }
            
            for (uint i = 1; i < simple.size; ++i)
            {
                for (uint j = 1;j < simple.size; ++j)
                {
                    if (simple.cas.lattice[i,j].Cell_Type != CELL_TYPE.INCLUSION)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                    
                }
            }
            return bm;
        }
        public Bitmap GenerateInclusions(int num, int sizeOfInclusion, int inclusionType)
        {
            simple.GenerateInclusions(num, sizeOfInclusion, inclusionType);
            for (uint i = 1; i < simple.size; ++i)
            {
                for (uint j = 1; j < simple.size; ++j)
                {
                    if (simple.cas.lattice[i,j].Cell_Type == CELL_TYPE.INCLUSION)
                    {
                        gr.FillRectangle(new SolidBrush(Color.White), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                }
            }
            return bm;
        }
        public Bitmap update(int NeighborhoodType, int propability)
        {
            //simple.cas.updateNeumann();
            simple.update(NeighborhoodType, propability);
            for (uint i = 1; i < simple.size; ++i)
            {
                for (uint j = 1; j < simple.size; ++j)
                {
                    if (simple.cas.lattice[i, j].Cell_Type != CELL_TYPE.INCLUSION)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                }
            }
            return bm;
        }
        public void saveTxt(string filePath)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine((simple.size-2) + " " + (simple.size-2));

                for(int i = 1; i < simple.size - 1; ++i)
                {
                    for(int j = 1; j < simple.size - 1; ++j)
                    {
                        streamWriter.WriteLine(simple.cas.lattice[i, j].Cell_Type + " " + simple.cas.lattice[i, j].ID);
                    }
                }
            }
        }

        public void loadTxt(string filePath)
        {
            
            using (StreamReader readtext = new StreamReader(filePath))
            {
                string[] str = readtext.ReadLine().Split(' ');
                simple.size = UInt32.Parse(str[0]);
                simple.size += 2;
                simple.cas.numOfEmptyCells = (int)((simple.size-2)*(simple.size - 2));
                for (int i = 0; i < simple.size; ++i)
                {
                    for (int j = 0; j < simple.size; ++j)
                    {
                        simple.cas.lattice[i, j].Cell_Type = CELL_TYPE.EMPTY;
                        simple.cas.lattice[i, j].ID = 0;
                        
                    }
                }
                uint maxID = 0;
                for (int i = 1; i < simple.size - 1; ++i)
                {
                    for (int j = 1; j < simple.size - 1; ++j)
                    {
                        string[] str2 = readtext.ReadLine().Split(' ');
                        Enum.TryParse(str2[0], out simple.cas.lattice[i, j].Cell_Type);
                        simple.cas.lattice[i, j].ID = UInt32.Parse(str2[1]);
                        if (simple.cas.lattice[i, j].ID > 0) --simple.cas.numOfEmptyCells;
                        maxID = Math.Max(maxID, simple.cas.lattice[i, j].ID);
                    }
                }

                colors.Clear();
                colors.Add(Color.Black);
                for (int i = 1; i < maxID+1; ++i)
                {
                    colors.Add(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
                }

                for (uint i = 1; i < simple.size; ++i)
                {
                    for (uint j = 1; j < simple.size; ++j)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                }
            }
        }

        public void loadBmp(string filePath)
        {
            clear();
            bm = (Bitmap)Image.FromFile(filePath);
            for(int i =1; i < bm.Height/2+1; ++i)
            {
                for(int j = 1; j < bm.Width/2+1; ++j)
                {
                    Color clr = bm.GetPixel((i-1)*2, (j-1)*2);
                    if (clr == Color.Black)
                    {

                        continue;
                    }
                    if (clr == Color.White)
                    {
                        simple.cas.lattice[i, j].Cell_Type = CELL_TYPE.INCLUSION;
                        simple.cas.numOfEmptyCells--;
                        continue;
                    }

                    if (colors.Contains(clr))
                    {
                        var index = 0;
                        for (int c = 0; c < colors.Count; ++c)
                        {
                            if (colors[c] == clr)
                            {
                                index = c;
                                break;
                            }
                        }
                        simple.cas.lattice[i, j].Cell_Type = CELL_TYPE.GRAIN;
                        simple.cas.lattice[i, j].ID = (uint)index;
                        simple.cas.numOfEmptyCells--;
                    }
                    else
                    {
                        colors.Add(clr);
                        var index = colors.Count - 1;
                        simple.cas.lattice[i, j].Cell_Type = CELL_TYPE.GRAIN;
                        simple.cas.lattice[i, j].ID = (uint)index;
                        simple.cas.numOfEmptyCells--;
                    }
                }
            }

            bm = new Bitmap(600, 600);
            gr = Graphics.FromImage(bm);
            brush = new SolidBrush(Color.Black);

            for (uint i = 1; i < simple.size; ++i)
            {
                for (uint j = 1; j < simple.size; ++j)
                {
                    if (simple.cas.lattice[i, j].Cell_Type != CELL_TYPE.INCLUSION)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                    if (simple.cas.lattice[i, j].Cell_Type != CELL_TYPE.INCLUSION)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                }
            }

            Console.WriteLine("a");
        }

        public void selectGrains(Point coordinates)
        {
            simple.SelectGrains(coordinates.X / 2, coordinates.Y / 2);
        }

        public void substructure()
        {
            simple.substructure();
            for (uint i = 1; i < simple.size; ++i)
            {
                for (uint j = 1; j < simple.size; ++j)
                {
                    if (simple.cas.lattice[i, j].Cell_Type != CELL_TYPE.INCLUSION)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                }
            }
        }

        public void dualPhase()
        {
            simple.dualPhase();
            for (uint i = 1; i < simple.size; ++i)
            {
                for (uint j = 1; j < simple.size; ++j)
                {
                    if (simple.cas.lattice[i, j].Cell_Type != CELL_TYPE.INCLUSION)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                }
            }
        }

        public void singleGrain()
        {
            simple.singleGrain();
            for (uint i = 1; i < simple.size; ++i)
            {
                for (uint j = 1; j < simple.size; ++j)
                {
                    if (simple.cas.lattice[i, j].Cell_Type != CELL_TYPE.INCLUSION)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                    if (simple.cas.lattice[i, j].Cell_Type == CELL_TYPE.INCLUSION)
                    {
                        gr.FillRectangle(new SolidBrush(Color.White), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                }
            }
        }

        public void allGrains()
        {
            simple.allGrains();
            for (uint i = 1; i < simple.size; ++i)
            {
                for (uint j = 1; j < simple.size; ++j)
                {
                    if (simple.cas.lattice[i, j].Cell_Type != CELL_TYPE.INCLUSION)
                    {
                        uint id = simple.cas.lattice[i, j].ID;
                        gr.FillRectangle(new SolidBrush(colors[(int)id]), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                    if (simple.cas.lattice[i, j].Cell_Type == CELL_TYPE.INCLUSION)
                    {
                        gr.FillRectangle(new SolidBrush(Color.White), (i - 1) * pixel_size, (j - 1) * pixel_size, pixel_size, pixel_size);
                    }
                }
            }
        }

        public void clear()
        {
            simple.clear();
            bm = new Bitmap(600, 600);
            gr = Graphics.FromImage(bm);
            brush = new SolidBrush(Color.Black);
            colors = new List<Color>();
            colors.Add(Color.Black);
            gr.FillRectangle(brush, 0, 0, 600, 600);
        }
    }
}

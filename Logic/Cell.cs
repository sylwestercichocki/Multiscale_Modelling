using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiscale_Modeling
{
    class Cell
    {
        public uint ID;
        public int H;
        public uint rec_ID;
        public bool recrystalized;
        public CELL_TYPE Cell_Type;

        public Cell()
        {
            ID = 0;
            H = 0;
            rec_ID = 0;
            recrystalized = false;
            Cell_Type = CELL_TYPE.EMPTY;
        }

        public void copy(Cell cell)
        {
            ID = cell.ID;
            Cell_Type = cell.Cell_Type;
            H = cell.H;
            rec_ID = cell.rec_ID;
            recrystalized = cell.recrystalized;
        }
        //public uint Id { get => ID; set => ID = value; }
        //internal CELL_TYPE Cell_type { get => Cell_Type; set => Cell_Type = value; }
    }
}

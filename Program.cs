using Multiscale_Modeling.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiscale_Modeling
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //uint size = 15;
            //Simple_grain_growth simple = new Simple_grain_growth(size);
            
            //simple.SetSeeds(2);
            //for(int i = 0; i< size; ++i)
            //{
            //    for (int j = 0; j< size; ++j)
            //    {
            //        System.Console.Write(simple.cas.lattice[i, j].ID);
            //    }
            //    System.Console.WriteLine();
            //}
            //System.Console.WriteLine();

            //for(int k = 0; k<10; ++k)
            //{
            //    simple.cas.updateNeumann();

            //    for (int i = 0; i < size; ++i)
            //    {
            //        for (int j = 0; j < size; ++j)
            //        {
            //            System.Console.Write(simple.cas.lattice[i, j].ID);
            //        }
            //        System.Console.WriteLine();
            //    }
            //    System.Console.WriteLine();
            //}
            


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

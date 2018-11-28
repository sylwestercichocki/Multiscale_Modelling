using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiscale_Modeling
{
    enum BOUNDARY_CONDITION
    {
        ABSORBING,
        PERIODIC
    };

    enum NEIGHBORHOOD_TYPE
    {
        MOORE,
        NEUMANN
    };

    enum CELL_TYPE
    {
        EMPTY,
        GRAIN,
        INCLUSION,
        SAVED,
        MC
    };
}

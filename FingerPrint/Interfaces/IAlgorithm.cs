using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Interfaces
{
    interface IAlgorithm
    {
        Picture Apply(Picture picture);
    }
}

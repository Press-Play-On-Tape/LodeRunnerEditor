using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LodeRunner
{
    enum EncryptionType : int {
        RLE_Row,
        RLE_Col,
        Grid
    }

    enum LevelElement : int {

        Blank,       // 0
        Brick,       // 1
        Solid,       // 2
        Ladder,      // 3
        Rail,        // 4
        FallThrough, // 5
        Gold,        // 6
        Level_Ladder,

    };

    class Enums
    {
    }
}

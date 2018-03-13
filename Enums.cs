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
        Brick_1,     // 7
        Brick_2,     // 8
        Brick_3,     // 9
        Brick_4,     // 10
        Brick_Transition,  // 11
        Brick_Close_1,  // 12
        Brick_Close_2,  // 13
        Brick_Close_3,  // 14
        Brick_Close_4,  // 15

    };

    class Enums
    {
    }
}

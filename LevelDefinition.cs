using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LodeRunner {
    
    class LevelDefinition {

        String levelName = "";
//        int[] data;
        CoordinateSet player = new CoordinateSet();
        List<CoordinateSet> enemies = new List<CoordinateSet>();
        List<CoordinateSet> ladders = new List<CoordinateSet>();
        EncryptionType encryptionType = EncryptionType.RLE_Row;
        LevelElement[,] grid = new LevelElement[16, 28];


        public string LevelName { get => levelName; set => levelName = value; }
        public CoordinateSet Player { get => player; set => player = value; }
        public List<CoordinateSet> Enemies { get => enemies; set => enemies = value; }
 //       public int[] Data { get => data; set => data = value; }
        public List<CoordinateSet> Ladders { get => ladders; set => ladders = value; }
        public EncryptionType EncryptionType { get => encryptionType; set => encryptionType = value; }
        public LevelElement[,] Grid { get => grid; set => grid = value; }

    }

}

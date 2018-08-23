using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LodeRunner {

    class LevelUtils {

        public static String getLevelText(ListViewItem item, LevelDefinition levelDefinition, Int16 levelId) {

            String rleRow = RLE_Row(levelDefinition);
            String rleCol = RLE_Col(levelDefinition);
            String rleNorm = RLE_Norm(levelDefinition);

            if (rleCol.Length < rleRow.Length) {
                rleRow = rleCol;
            }

            if (rleNorm.Length < rleRow.Length) {
                rleRow = rleNorm;
            }

            return "const uint8_t PROGMEM level" + levelId + "[] = {\n" + rleRow + "\n};\n\n";

        }

        private static String RLE_Col(LevelDefinition levelDefinition) {

            StringBuilder ret = new StringBuilder();

            int iCount = 0;
            LevelElement currentCell = levelDefinition.Grid[0, 0];

            ret.Append(Common(levelDefinition));
            ret.Append("0x01, ");


            // Map data ..

            for (int x = 0; x < 28; x++) {

                for (int y = 0; y < 16; y++) {

                    if (x != 0 || y != 0) {

                        //                        if ((levelDefinition.Grid[y, x] != LevelElement.Level_Ladder && levelDefinition.Grid[y, x] != currentCell) || iCount == 30) {
                        if ((levelDefinition.Grid[y, x] == LevelElement.Level_Ladder && currentCell != LevelElement.Blank) || 
                            (levelDefinition.Grid[y, x] != LevelElement.Level_Ladder && levelDefinition.Grid[y, x] != currentCell) || iCount == 30) {
                            ret.Append("0x" + (((int)currentCell * 32) + iCount + 1).ToString("X2"));
                            ret.Append(", ");
                            iCount = 0;
                            currentCell = levelDefinition.Grid[y, x];

                        }
                        else {
                            iCount++;
                        }

                    }

                }

            }

            ret.Append("0x" + (((int)currentCell * 32) + iCount + 1).ToString("X2"));
            ret.Append(", 0x00");

            return ret.ToString();

        }

        private static String RLE_Row(LevelDefinition levelDefinition) {

            StringBuilder ret = new StringBuilder();

            int iCount = 0;
            LevelElement currentCell = levelDefinition.Grid[0, 0];

            ret.Append(Common(levelDefinition));
            ret.Append("0x00, ");


            // Map data ..

            for (int y = 0; y < 16; y++) {

                for (int x = 0; x < 28; x++) {

                    if (x != 0 || y != 0) {

                        //                        if ((levelDefinition.Grid[y, x] != LevelElement.Level_Ladder && levelDefinition.Grid[y, x] != currentCell) || iCount == 30) {
                        if ((levelDefinition.Grid[y, x] == LevelElement.Level_Ladder && currentCell != LevelElement.Blank) ||
                            (levelDefinition.Grid[y, x] != LevelElement.Level_Ladder && levelDefinition.Grid[y, x] != currentCell) || iCount == 30) {

                            ret.Append("0x" + (((int)currentCell * 32) + iCount + 1).ToString("X2"));
                            ret.Append(", ");
                            iCount = 0;
                            currentCell = levelDefinition.Grid[y, x];

                        }
                        else {
                            iCount++;
                        }

                    }

                }

            }

            ret.Append("0x" + (((int)currentCell * 32) + iCount + 1).ToString("X2"));
            ret.Append(", 0x00");

            return ret.ToString();

        }

        private static String RLE_Norm(LevelDefinition levelDefinition) {

            StringBuilder ret = new StringBuilder();

            ret.Append(Common(levelDefinition));
            ret.Append("0x02, ");

            for (int y = 0; y < 16; y++) {

                for (int x = 0; x < 28; x += 2) {

                    int left = (int)levelDefinition.Grid[y, x]; if (left == 7) left = 0;
                    int right = (int)levelDefinition.Grid[y, x + 1]; if (right == 7) right = 0;

                    ret.Append("0x" + ((left * 16) + right).ToString("X2"));
                    ret.Append(", ");

                }

            }

            return ret.ToString();

        }


        private static String Common(LevelDefinition levelDefinition) {

            StringBuilder common = new StringBuilder();


            // Player position ..

            common.Append("0x" + levelDefinition.Player.X.ToString("X2"));
            common.Append(", ");
            common.Append("0x" + levelDefinition.Player.Y.ToString("X2"));
            common.Append(", ");


            // Number of Enemies ..

            common.Append("0x" + levelDefinition.Enemies.Count.ToString("X2"));
            common.Append(", ");

            for (int x = 0; x < levelDefinition.Enemies.Count; x++) {

                common.Append("0x" + levelDefinition.Enemies[x].X.ToString("X2"));
                common.Append(", ");
                common.Append("0x" + levelDefinition.Enemies[x].Y.ToString("X2"));
                common.Append(", ");

            }


            // Level ladders ..

            int count = 0;
            for (int y = 0; y < 16; y++) {

                for (int x = 0; x < 28; x++) {

                    if (levelDefinition.Grid[y, x] == LevelElement.Level_Ladder) {
                        count++;
                    }

                }

            }

            common.Append("0x" + count.ToString("X2"));
            common.Append(", ");

            for (int y = 0; y < 16; y++) {

                for (int x = 0; x < 28; x++) {

                    if (levelDefinition.Grid[y, x] == LevelElement.Level_Ladder) {
                        common.Append("0x" + x.ToString("X2"));
                        common.Append(", ");
                        common.Append("0x" + y.ToString("X2"));
                        common.Append(", ");
                    }

                }

            }


            // Reentry points ..

            for (int x = 0; x < 4; x++) {

                common.Append("0x" + levelDefinition.ReentryPoints[x].X.ToString("X2"));
                common.Append(", ");
                common.Append("0x" + levelDefinition.ReentryPoints[x].Y.ToString("X2"));
                common.Append(", ");

            }

            return common.ToString();

        }

    }

}

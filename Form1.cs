using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LodeRunner
{
    public partial class Form1 : Form
    {

        private Panel[,] pictureBoxes;

        private const int WIDTH = 14;
        private const int HEIGHT = 16;
        private const int ENCRYPTION_TYPE_RLE_ROW = 0;
        private const int ENCRYPTION_TYPE_RLE_COL = 1;
        private const int ENCRYPTION_TYPE_GRID = 2;

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


        public Form1() {
            InitializeComponent();

            pictureBoxes = new Panel[16, 28] {
                { pic000, pic010, pic020, pic030, pic040, pic050, pic060, pic070, pic080, pic090, pic0A0, pic0B0, pic0C0, pic0D0, pic0E0, pic0F0, pic100, pic110, pic120, pic130, pic140, pic150, pic160, pic170, pic180, pic190, pic1A0, pic1B0 },
                { pic001, pic011, pic021, pic031, pic041, pic051, pic061, pic071, pic081, pic091, pic0A1, pic0B1, pic0C1, pic0D1, pic0E1, pic0F1, pic101, pic111, pic121, pic131, pic141, pic151, pic161, pic171, pic181, pic191, pic1A1, pic1B1 },
                { pic002, pic012, pic022, pic032, pic042, pic052, pic062, pic072, pic082, pic092, pic0A2, pic0B2, pic0C2, pic0D2, pic0E2, pic0F2, pic102, pic112, pic122, pic132, pic142, pic152, pic162, pic172, pic182, pic192, pic1A2, pic1B2 },
                { pic003, pic013, pic023, pic033, pic043, pic053, pic063, pic073, pic083, pic093, pic0A3, pic0B3, pic0C3, pic0D3, pic0E3, pic0F3, pic103, pic113, pic123, pic133, pic143, pic153, pic163, pic173, pic183, pic193, pic1A3, pic1B3 },
                { pic004, pic014, pic024, pic034, pic044, pic054, pic064, pic074, pic084, pic094, pic0A4, pic0B4, pic0C4, pic0D4, pic0E4, pic0F4, pic104, pic114, pic124, pic134, pic144, pic154, pic164, pic174, pic184, pic194, pic1A4, pic1B4 },
                { pic005, pic015, pic025, pic035, pic045, pic055, pic065, pic075, pic085, pic095, pic0A5, pic0B5, pic0C5, pic0D5, pic0E5, pic0F5, pic105, pic115, pic125, pic135, pic145, pic155, pic165, pic175, pic185, pic195, pic1A5, pic1B5 },
                { pic006, pic016, pic026, pic036, pic046, pic056, pic066, pic076, pic086, pic096, pic0A6, pic0B6, pic0C6, pic0D6, pic0E6, pic0F6, pic106, pic116, pic126, pic136, pic146, pic156, pic166, pic176, pic186, pic196, pic1A6, pic1B6 },
                { pic007, pic017, pic027, pic037, pic047, pic057, pic067, pic077, pic087, pic097, pic0A7, pic0B7, pic0C7, pic0D7, pic0E7, pic0F7, pic107, pic117, pic127, pic137, pic147, pic157, pic167, pic177, pic187, pic197, pic1A7, pic1B7 },
                { pic008, pic018, pic028, pic038, pic048, pic058, pic068, pic078, pic088, pic098, pic0A8, pic0B8, pic0C8, pic0D8, pic0E8, pic0F8, pic108, pic118, pic128, pic138, pic148, pic158, pic168, pic178, pic188, pic198, pic1A8, pic1B8 },
                { pic009, pic019, pic029, pic039, pic049, pic059, pic069, pic079, pic089, pic099, pic0A9, pic0B9, pic0C9, pic0D9, pic0E9, pic0F9, pic109, pic119, pic129, pic139, pic149, pic159, pic169, pic179, pic189, pic199, pic1A9, pic1B9 },
                { pic00A, pic01A, pic02A, pic03A, pic04A, pic05A, pic06A, pic07A, pic08A, pic09A, pic0AA, pic0BA, pic0CA, pic0DA, pic0EA, pic0FA, pic10A, pic11A, pic12A, pic13A, pic14A, pic15A, pic16A, pic17A, pic18A, pic19A, pic1AA, pic1BA },
                { pic00B, pic01B, pic02B, pic03B, pic04B, pic05B, pic06B, pic07B, pic08B, pic09B, pic0AB, pic0BB, pic0CB, pic0DB, pic0EB, pic0FB, pic10B, pic11B, pic12B, pic13B, pic14B, pic15B, pic16B, pic17B, pic18B, pic19B, pic1AB, pic1BB },
                { pic00C, pic01C, pic02C, pic03C, pic04C, pic05C, pic06C, pic07C, pic08C, pic09C, pic0AC, pic0BC, pic0CC, pic0DC, pic0EC, pic0FC, pic10C, pic11C, pic12C, pic13C, pic14C, pic15C, pic16C, pic17C, pic18C, pic19C, pic1AC, pic1BC },
                { pic00D, pic01D, pic02D, pic03D, pic04D, pic05D, pic06D, pic07D, pic08D, pic09D, pic0AD, pic0BD, pic0CD, pic0DD, pic0ED, pic0FD, pic10D, pic11D, pic12D, pic13D, pic14D, pic15D, pic16D, pic17D, pic18D, pic19D, pic1AD, pic1BD },
                { pic00E, pic01E, pic02E, pic03E, pic04E, pic05E, pic06E, pic07E, pic08E, pic09E, pic0AE, pic0BE, pic0CE, pic0DE, pic0EE, pic0FE, pic10E, pic11E, pic12E, pic13E, pic14E, pic15E, pic16E, pic17E, pic18E, pic19E, pic1AE, pic1BE },
                { pic00F, pic01F, pic02F, pic03F, pic04F, pic05F, pic06F, pic07F, pic08F, pic09F, pic0AF, pic0BF, pic0CF, pic0DF, pic0EF, pic0FF, pic10F, pic11F, pic12F, pic13F, pic14F, pic15F, pic16F, pic17F, pic18F, pic19F, pic1AF, pic1BF }
            };

            initControlsRecursive(panel1.Controls);
        }

        //private void HookEvents() {
        //    foreach (Control ctl in this.Controls) {
        //        ctl.MouseClick += new MouseEventHandler(Form1_MouseClick);
        //    }
        //}

        void initControlsRecursive(Control.ControlCollection coll) {
            foreach (Control control in coll) {

                control.MouseClick += ControlOnMouseClick;

                Padding margin = control.Margin;
                margin.Left = 0;
                margin.Right = 0;
                margin.Bottom = 0;
                margin.Top = 0;
                control.Margin = margin;
                control.BackColor = Color.Black;
                ((Panel)control).BorderStyle = BorderStyle.None;
                ((Panel)control).BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                if (control.HasChildren)
                    initControlsRecursive(control.Controls);
            }
        }

        private void ControlOnMouseClick(object sender, MouseEventArgs args) {
            if (args.Button != MouseButtons.Left)
                return;

            String name = ((Panel)sender).Name;

            int x = int.Parse(name.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            int y = int.Parse(name.Substring(5, 1), System.Globalization.NumberStyles.HexNumber);

            pictureBoxes[y, x].BackgroundImage = imgBlocks.Images[0];

        }
        /*
        private Control FindControlAtPoint(Control container, Point pos) {
            Control child;
            foreach (Control c in container.Controls) {
                if (c.Visible && c.Bounds.Contains(pos)) {
                    child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
                    if (child == null) return c;
                    else return child;
                }
            }
            return null;
        }

        private Control FindControlAtCursor(Form form) {
            Point pos = Cursor.Position;
            if (form.Bounds.Contains(pos))
                return FindControlAtPoint(form, form.PointToClient(pos));
            return null;
        }
        */
        private void Form1_Load(object sender, EventArgs e) {

            loadLevel(new int[] { 0x00, 11, 39, 20, 101, 36, 18, 98, 7, 34, 16, 33, 97, 2, 65, 3, 65, 2, 34, 15, 33, 97, 1, 67, 1, 67, 1, 34, 15, 33, 97, 1, 67, 1, 67, 1, 34, 15, 33, 97, 1, 67, 1, 67, 1, 34, 15, 33, 97, 2, 65, 3, 65, 2, 34, 15, 33, 97, 4, 193, 4, 34, 16, 33, 97, 3, 193, 3, 34, 8, 97, 137, 98, 5, 34, 136, 98, 137, 98, 1, 33, 1, 33, 1, 34, 136, 98, 2, 193, 2, 193, 2, 193, 98, 1, 33, 1, 33, 1, 34, 2, 193, 2, 193, 2, 98, 9, 98, 1, 33, 1, 33, 1, 34, 8, 98, 9, 98, 1, 97, 1, 97, 1, 34, 8, 97, 76, 38, 0x4A, 0x00 });
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {

        //    Control control = FindControlAtCursor(this);

         //   control = control;


            String f = string.Format("{0} {1} ({2}) \n ", DateTime.Now.TimeOfDay.ToString(), e, sender.GetType().Name);
            f = f;
        }

        private int leftValue(int val) {

            return val >> 4;

        }

        private int rightValue(int val) {

            return val & 0x0F;

        }

        private void loadLevel(int[] data) {

            int dataOffset = 0;
            int goldLeft = 0;

            /*
            // Load player starting position ..

            int playerX = data[dataOffset++];
            int playerY = data[dataOffset++];


            // Load enemies ..

            int numberOfEnemies = data[dataOffset++];

            for (int x = 0; x < numberOfEnemies; x++) {

                //Enemy* enemy = &enemies[x];

                //enemy->setId(x);

                if (x < numberOfEnemies) {

                    int xRebirth = data[dataOffset++];
                    int yRebirth = data[dataOffset++];

                }

            }


            // Load level ladder points ..

            int levelLadderElementCount = data[dataOffset++];

            for (int x = 0; x < levelLadderElementCount; x++) {

                int xLadder = data[dataOffset++];
                int yLadder = data[dataOffset++];

            }
            */
            int encryptionType = data[dataOffset++];

            if (encryptionType == ENCRYPTION_TYPE_GRID) {

                for (int y = 0; y < HEIGHT; y++) {

                    for (int x = 0; x < WIDTH; x++) {

                        int element = data[dataOffset++];

                        if (leftValue(element) == ((int)LevelElement.Gold)) { goldLeft++; }
                        if (rightValue(element) == ((int)LevelElement.Gold)) { goldLeft++; }

                        pictureBoxes[y, x].BackgroundImage = imgBlocks.Images[leftValue(element)];
                        pictureBoxes[y, x].Tag = leftValue(element);
                        pictureBoxes[y, x + 1].BackgroundImage = imgBlocks.Images[rightValue(element)];
                        pictureBoxes[y, x + 1].Tag = rightValue(element);

                    }

                }

            }
            else {

                int cursor = 0;

                while (true) {

                    int element = data[dataOffset];
                    int block = (element & 0xE0) >> 5;
                    int run = element & 0x1F;

                    if (block == ((int)LevelElement.Gold)) { goldLeft++; }

                    if (run > 0) {

                        dataOffset++;

                        for (int x = 0; x < run; x++) {

                            if (encryptionType == ENCRYPTION_TYPE_RLE_ROW) {

                                int row = cursor / (WIDTH * 2);
                                int col = (cursor % (WIDTH * 2));

                                pictureBoxes[row, col].BackgroundImage = imgBlocks.Images[block];
                                pictureBoxes[row, col].Tag = leftValue(block);

                            }
                            else {

                                int col = cursor / HEIGHT;
                                int row = cursor % HEIGHT;

                                pictureBoxes[row, col].BackgroundImage = imgBlocks.Images[block];
                                pictureBoxes[row, col].Tag = leftValue(block);

                            }

                            cursor++;

                        }

                    }
                    else {

                        break;

                    }

                }

            }


        }

    }

}


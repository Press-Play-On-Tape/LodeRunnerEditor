using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LodeRunner
{
    public partial class frmMain : Form {

        private Panel[,] pictureBoxes;
        private PictureBox[] picEnemies;
        private PictureBox[] picReentryPoints;
        private ToolStripMenuItem[] mnuLevelElements;
        private ToolStripButton[] tsElements;

        private String gameNumber = "";
        private String numberOfGames = "";
        private Int16 gameNumberId = 0;
        private List<Int16> gamesPerLevels = new List<Int16>();

        private LevelElement selectedElementIndex = LevelElement.Brick;

        private const int WIDTH = 14;
        private const int HEIGHT = 16;

        public frmMain() {

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

            picEnemies = new PictureBox[6] { picEnemy1, picEnemy2, picEnemy3, picEnemy4, picEnemy5, picEnemy6 };
            picReentryPoints = new PictureBox[4] { picReentry1, picReentry2, picReentry3, picReentry4 };
            mnuLevelElements = new ToolStripMenuItem[10] { mnuLevelItem_00, mnuLevelItem_01, mnuLevelItem_02, mnuLevelItem_03, mnuLevelItem_04, mnuLevelItem_05, mnuLevelItem_06, mnuLevelItem_07, mnuLevelItem_08, mnuLevelItem_09 };
            tsElements = new ToolStripButton[11] { tsElement0, tsElement1, tsElement2, tsElement3, tsElement4, tsElement5, tsElement6, tsElement7, tsElement8, tsElement9, tsElement10 };

            initControlsRecursive(pnlLevel.Controls);
            mnuElementSelect.Tag = 1;

            for (int x = 0; x < tsElements.Length; x++) {
                tsElements[x].Paint += tsElement_Paint;
                tsElements[x].Click += tsElement_Click;
            }

            for (int x = 0; x < picReentryPoints.Length; x++) {
                picReentryPoints[x].Click += picReentryPoints_Click;
            }

        }

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

                if (control.HasChildren) initControlsRecursive(control.Controls);

            }


            foreach (Control control in picEnemies) {

                control.MouseClick += ControlOnMouseClick;

            }

        }

        private int leftValue(int val) {

            return val >> 4;

        }

        private int rightValue(int val) {

            return val & 0x0F;

        }

        private void loadLevel(LevelDefinition levelDefinition, int[] data) {

            int dataOffset = 0;
            int goldLeft = 0;

            if (levelDefinition.EncryptionType == EncryptionType.Grid) {

                for (int y = 0; y < HEIGHT; y++) {

                    for (int x = 0; x < WIDTH; x++) {

                        int element = data[dataOffset++];

                        if (leftValue(element) == ((int)LevelElement.Gold)) { goldLeft++; }
                        if (rightValue(element) == ((int)LevelElement.Gold)) { goldLeft++; }

                        levelDefinition.Grid[y, x * 2] = (LevelElement)leftValue(element);
                        levelDefinition.Grid[y, (x * 2) + 1] = (LevelElement)rightValue(element);

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

                            if (levelDefinition.EncryptionType == EncryptionType.RLE_Row) {

                                int row = cursor / (WIDTH * 2);
                                int col = (cursor % (WIDTH * 2));

                                levelDefinition.Grid[row, col] = (LevelElement)block;

                            }
                            else {

                                int col = cursor / HEIGHT;
                                int row = cursor % HEIGHT;

                                levelDefinition.Grid[row, col] = (LevelElement)block;

                            }

                            cursor++;

                        }

                    }
                    else {

                        break;

                    }

                }

            }


            // Map level ladders to grid ..

            foreach (CoordinateSet levelLadder in levelDefinition.Ladders) {

                levelDefinition.Grid[levelLadder.Y, levelLadder.X] = LevelElement.Level_Ladder;

            }

        }

        private void mnuLoad_Click(object sender, EventArgs e) {

            clearLevel();
            cmdSave.Enabled = false;
            cmdClear.Enabled = false;
            cmdReset.Enabled = false;

            lstLevels.Items.Clear();

            bool inLevel = false;

            if (dgOpenMapData.ShowDialog() == DialogResult.OK) {

                mnuSave.Enabled = true;
                mnuSaveAs.Enabled = true;

                const string userRoot = "HKEY_CURRENT_USER";
                const string subkey = "LodeRunner";
                const string keyName = userRoot + "\\" + subkey;
                Registry.SetValue(keyName, "PathName", Path.GetDirectoryName(dgOpenMapData.FileName));

                //                String[] lines = System.IO.File.ReadAllLines(dgOpenMapData.FileName);

                using (FileStream fs = File.Open(dgOpenMapData.FileName, FileMode.Open))
                using (BufferedStream bs = new BufferedStream(fs, System.Text.ASCIIEncoding.Unicode.GetByteCount("g")))
                using (StreamReader sr = new StreamReader(bs)) {

                    LevelDefinition levelDefinition = null;
                    string line;
                    while ((line = sr.ReadLine()) != null) {

                        if (line.StartsWith("#define GAME_NUMBER")) {

                            gameNumber = line.Substring(@"#define GAME_NUMBER".Length + 1).Trim();

                        }
                        else if (line.StartsWith("#define NUMBER_OF_GAMES")) {

                            numberOfGames = line.Substring(@"#define NUMBER_OF_GAMES".Length + 1).Trim();

                        }
                        else if (line.StartsWith("#if GAME_NUMBER")) {

                            gameNumberId = Int16.Parse(line.Substring(line.IndexOf("==") + 3).Trim());

                        }
                        else if (line.StartsWith("  #define LEVEL_COUNT")) {

                            Int16 gamesPerLevel = Int16.Parse(line.Substring(@"  #define LEVEL_COUNT".Length + 1).Trim());
                            gamesPerLevels.Add(gamesPerLevel);
                            dgExport.Rows.Add(dgExport.Rows.Count, gamesPerLevel);

                        }
                        else if (line.StartsWith("const uint8_t PROGMEM")) {

                            levelDefinition = new LevelDefinition();
                            levelDefinition.LevelName = line.Substring(line.IndexOf("PROGMEM ") + 8, line.Length - line.IndexOf("PROGMEM ") - 14);
                            inLevel = true;
                        }
                        else if (inLevel) {

                            if (line == "};") {

                                inLevel = false;
                                ListViewItem item = new ListViewItem();
                                item.Text = levelDefinition.LevelName;
                                item.Tag = levelDefinition;
                                lstLevels.Items.Add(item);

                            }
                            else {

                                char[] splitchar = { ',' };
                                String[] strData = line.Trim().Split(splitchar);

                                int cursor = 0;


                                // Player start pos ..

                                CoordinateSet player = new CoordinateSet();
                                player.X = Convert.ToInt32(strData[cursor++].Trim(), 16);
                                player.Y = Convert.ToInt32(strData[cursor++].Trim(), 16);
                                levelDefinition.Player = player;


                                // Enemies

                                int numberOfEnemies = Convert.ToInt32(strData[cursor++].Trim(), 16);

                                for (int x = 0; x < numberOfEnemies; x++) {

                                    CoordinateSet enemy = new CoordinateSet();
                                    enemy.X = Convert.ToInt32(strData[cursor++].Trim(), 16);
                                    enemy.Y = Convert.ToInt32(strData[cursor++].Trim(), 16);
                                    levelDefinition.Enemies.Add(enemy);

                                }


                                // Level Ladders

                                int numberOfLevelLadders = Convert.ToInt32(strData[cursor++].Trim(), 16);

                                for (int x = 0; x < numberOfLevelLadders; x++) {

                                    CoordinateSet levelLadder = new CoordinateSet();
                                    levelLadder.X = Convert.ToInt32(strData[cursor++].Trim(), 16);
                                    levelLadder.Y = Convert.ToInt32(strData[cursor++].Trim(), 16);
                                    levelDefinition.Ladders.Add(levelLadder);

                                }


                                // Reentry points

                                for (int x = 0; x < 4; x++) {

                                    CoordinateSet reentryPoint = new CoordinateSet();
                                    reentryPoint.X = Convert.ToInt32(strData[cursor++].Trim(), 16);
                                    reentryPoint.Y = Convert.ToInt32(strData[cursor++].Trim(), 16);
                                    levelDefinition.ReentryPoints.Add(reentryPoint);

                                }

                                // Encryption type ..

                                levelDefinition.EncryptionType = (EncryptionType)Convert.ToInt32(strData[cursor++].Trim(), 16);
                                int[] data = new int[strData.Length - cursor];

                                for (int count = cursor; count <= strData.Length - 2; count++) {
                                    data[count - cursor] = Convert.ToInt32(strData[count].Trim(), 16);
                                }

                                loadLevel(levelDefinition, data);

                            }

                        }

                    }

                }

            }

            foreach (ListViewItem item in lstLevels.Items) {
                validate(item, (LevelDefinition)item.Tag);
            }


            MessageBox.Show("Level data loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void lstLevels_SelectedIndexChanged(object sender, EventArgs e) {

            if (lstLevels.SelectedItems.Count > 0 && lstLevels.Tag != lstLevels.SelectedItems[0]) {

                lstLevels.Tag = lstLevels.SelectedItems[0];

                if (lstLevels.SelectedItems.Count > 0) {

                    cmdLevelDelete.Enabled = true;
                    LevelDefinition levelDefinition = (LevelDefinition)lstLevels.SelectedItems[0].Tag;
                    loadLevel(levelDefinition);

                }
                else {

                    cmdLevelDelete.Enabled = false;

                }

                cmdLevelUp.ForeColor = (lstLevels.SelectedItems.Count > 0 && lstLevels.SelectedItems[0].Index > 0 ? SystemColors.ControlText : SystemColors.ControlDark);
                cmdLevelUp.Enabled = (lstLevels.SelectedItems.Count > 0 && lstLevels.SelectedItems[0].Index > 0);
                cmdLevelDown.ForeColor = (lstLevels.SelectedItems.Count > 0 && lstLevels.SelectedItems[0].Index < lstLevels.Items.Count ? SystemColors.ControlText : SystemColors.ControlDark);
                cmdLevelDown.Enabled = (lstLevels.SelectedItems.Count > 0 && lstLevels.SelectedItems[0].Index < lstLevels.Items.Count);

            }

        }

        private void ControlOnMouseClick(object sender, MouseEventArgs args) {

            String name = "";
            int x = -1;
            int y = -1;

            if (lstLevels.SelectedItems.Count > 0) {

                if (args.Button == MouseButtons.Left) {

                    if (sender is Panel) {

                        name = ((Panel)sender).Name;
                        x = int.Parse(name.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                        y = int.Parse(name.Substring(5, 1), System.Globalization.NumberStyles.HexNumber);

                    }
                    else if (sender is PictureBox) {

                        name = ((PictureBox)sender).Name;
                        CoordinateSet enemyCoordiantes = (CoordinateSet)((PictureBox)sender).Tag;
                        x = enemyCoordiantes.X;
                        y = enemyCoordiantes.Y;

                    }

                    switch (selectedElementIndex) {

                        case LevelElement.ReentryPoint:

                            int reentryPointCount = 0;
                            foreach (PictureBox picReentryPoint in picReentryPoints) {

                                if (picReentryPoint.Tag != null) { reentryPointCount++; }

                            }

                            if (reentryPointCount == 4) {
                                MessageBox.Show("A maximum of 4 re-entry points can be added per level.", "Level Design Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            foreach (PictureBox picReentryPoint in picReentryPoints) {

                                if (picReentryPoint.Tag == null) {

                                    picReentryPoint.Parent = pictureBoxes[y, x];
                                    picReentryPoint.BackColor = Color.Transparent;
                                    picReentryPoint.Location = new System.Drawing.Point(0, 0);
                                    picReentryPoint.Visible = true;

                                    CoordinateSet reentryCoords = new CoordinateSet();
                                    reentryCoords.X = x;
                                    reentryCoords.Y = y;
                                    picReentryPoint.Tag = reentryCoords;
                                    return;

                                }

                            }

                            break;

                        case LevelElement.Level_Ladder:

                            if (getCountOfLevelElements(LevelElement.Level_Ladder) > 18) {
                                MessageBox.Show("A maximum of 18 escape ladders can be added per level.", "Level Design Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            break;

                        case LevelElement.Enemy:

                            int count = 0;

                            foreach (PictureBox picEnemy in picEnemies) {

                                if (picEnemy.Tag != null) {

                                    count++;

                                    CoordinateSet enemyPosition = (CoordinateSet)picEnemy.Tag;

                                    if (enemyPosition.X == x && enemyPosition.Y == y) {

                                        picEnemy.Tag = null;
                                        picEnemy.Visible = false;
                                        return;

                                    }

                                }

                            }

                            if (count == 6) {
                                MessageBox.Show("A maximum of 6 enemies can be added per level.", "Level Design Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;

                            }

                            foreach (PictureBox picEnemy in picEnemies) {

                                if (picEnemy.Tag == null) {

                                    picEnemy.Parent = pictureBoxes[y, x];
                                    picEnemy.BackColor = Color.Transparent;
                                    picEnemy.Location = new System.Drawing.Point(0, 0);
                                    picEnemy.Visible = true;

                                    CoordinateSet enemyCoords = new CoordinateSet();
                                    enemyCoords.X = x;
                                    enemyCoords.Y = y;
                                    picEnemy.Tag = enemyCoords;
                                    return;

                                }

                            }

                            return;

                        case LevelElement.Player:

                            picPlayer.Parent = pictureBoxes[y, x];
                            picPlayer.BackColor = Color.Transparent;
                            picPlayer.Location = new System.Drawing.Point(0, 0);
                            picPlayer.Visible = true;

                            CoordinateSet playerCoords = new CoordinateSet();
                            playerCoords.X = x;
                            playerCoords.Y = y;
                            picPlayer.Tag = playerCoords;

                            return;

                    }


                    pictureBoxes[y, x].BackgroundImage = imgBlocks.Images[(int)selectedElementIndex];
                    pictureBoxes[y, x].Tag = (int)selectedElementIndex;

                }
                else {

                    mnuElementSelect.Show(Cursor.Position);

                }

            }
            
        }
        
        private void mnuLevelItem_Click(ToolStripMenuItem selectedItem, int index) {

            foreach (ToolStripMenuItem item in mnuLevelElements) {

                item.Checked = false;

            }

            selectedItem.Checked = true;
            selectedElementIndex = (LevelElement)index;

        }

        private void mnuLevelItem_00_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 0);
        }

        private void mnuLevelItem_01_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 1);
        }

        private void mnuLevelItem_02_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 2);
        }

        private void mnuLevelItem_03_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 3);
        }

        private void mnuLevelItem_04_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 4);
        }

        private void mnuLevelItem_05_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 5);
        }

        private void mnuLevelItem_06_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 6);
        }

        private void mnuLevelItem_07_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 7);
        }
        
        private void mnuLevelItem_08_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 8);
        }

        private void mnuLevelItem_09_Click(object sender, EventArgs e) {
            mnuLevelItem_Click((ToolStripMenuItem)sender, 9);
        }

        int getCountOfLevelElements(LevelElement levelElement) {

            int count = 0;

            for (int y = 0; y < 16; y++) {

                for (int x = 0; x < 28; x++) {

                    if ((int)pictureBoxes[y, x].Tag == (int)levelElement) count++;

                }

            }

            return count;

        }

        int getCountOfLevelElements(LevelDefinition levelDefinition, LevelElement levelElement) {

            int count = 0;

            for (int y = 0; y < 16; y++) {

                for (int x = 0; x < 28; x++) {

                    if (levelDefinition.Grid[y,x] == levelElement) count++;

                }

            }

            return count;

        }

        private void tsElement_Paint(object sender, PaintEventArgs e) {

            Rectangle picRectangle = new Rectangle(0, 0, 22, 22);

            if (selectedElementIndex == (LevelElement)Int16.Parse( (String)((ToolStripButton)sender).Tag) ) {
                ControlPaint.DrawBorder(e.Graphics, picRectangle, Color.Red, ButtonBorderStyle.Solid);
            }
            else {
                ControlPaint.DrawBorder(e.Graphics, picRectangle, SystemColors.ControlLight, ButtonBorderStyle.Solid);
            }
        }

        private void tsElement_Click(object sender, EventArgs e) {

            if (lstLevels.SelectedItems.Count > 0) {

                selectedElementIndex = (LevelElement)Int16.Parse((String)((ToolStripButton)sender).Tag);
                tsTopMenu.Refresh();

            }

        }

        private void picReentryPoints_Click(object sender, EventArgs e) {

            if (lstLevels.SelectedItems.Count > 0) {

                ((PictureBox)sender).Visible = false;
                ((PictureBox)sender).Tag = null;

            }

        }

        private void cmdClear_Click(object sender, EventArgs e) {

            clearLevel();
            lstLevels.Select();

        }

        private void clearLevel() { 

            for (int x = 0; x < picEnemies.Length; x++) {

                picEnemies[x].Tag = null;
                picEnemies[x].Visible = false;

            }

            for (int x = 0; x < picReentryPoints.Length; x++) {

                picReentryPoints[x].Tag = null;
                picReentryPoints[x].Visible = false;

            }

            for (int y = 0; y < 16; y++) {

                for (int x = 0; x < 28; x++) {

                    pictureBoxes[y, x].BackgroundImage = imgBlocks.Images[(int)LevelElement.Blank];
                    pictureBoxes[y, x].Tag = (int)LevelElement.Blank;

                }

            }

            picPlayer.Tag = null;
            picPlayer.Visible = false;

        }

        private void cmdReset_Click(object sender, EventArgs e) {

            if (lstLevels.SelectedItems.Count > 0) {

                LevelDefinition levelDefinition = (LevelDefinition)lstLevels.SelectedItems[0].Tag;
                loadLevel(levelDefinition);
                lstLevels.Select();

            }

        }

        private void loadLevel(LevelDefinition levelDefinition) {


            for (int y = 0; y < 16; y++) {

                for (int x = 0; x < 28; x++) {

                    pictureBoxes[y, x].BackgroundImage = imgBlocks.Images[(int)levelDefinition.Grid[y, x]];
                    pictureBoxes[y, x].Tag = (int)levelDefinition.Grid[y, x];

                }

            }


            // Enemies ..

            for (int x = 0; x < 6; x++) {

                PictureBox picEnemy = picEnemies[x];

                if (x < levelDefinition.Enemies.Count) {

                    CoordinateSet enemyCoords = levelDefinition.Enemies[x];
                    picEnemy.Parent = pictureBoxes[enemyCoords.Y, enemyCoords.X];
                    picEnemy.BackColor = Color.Transparent;
                    picEnemy.Location = new System.Drawing.Point(0, 0);
                    picEnemy.Visible = true;
                    picEnemy.Tag = enemyCoords;

                }
                else {

                    picEnemy.Visible = false;
                    picEnemy.Tag = null;

                }

            }


            // Reentry Points ..

            for (int x = 0; x < 4; x++) {

                PictureBox picReentryPoint = picReentryPoints[x];

                if (x < levelDefinition.ReentryPoints.Count) {

                    CoordinateSet reentryPointyCoords = levelDefinition.ReentryPoints[x];
                    picReentryPoint.Parent = pictureBoxes[reentryPointyCoords.Y, reentryPointyCoords.X];
                    picReentryPoint.BackColor = Color.Transparent;
                    picReentryPoint.Location = new System.Drawing.Point(0, 0);
                    picReentryPoint.Visible = true;
                    picReentryPoint.Tag = reentryPointyCoords;

                }
                else {

                    picReentryPoint.Visible = false;
                    picReentryPoint.Tag = null;

                }

            }


            // Player starting point

            CoordinateSet playerCoords = levelDefinition.Player;
            picPlayer.Visible = false;

            if (playerCoords != null) {

                picPlayer.Parent = pictureBoxes[playerCoords.Y, playerCoords.X];
                picPlayer.BackColor = Color.Transparent;
                picPlayer.Location = new System.Drawing.Point(0, 0);
                picPlayer.Visible = true;
                picPlayer.Tag = playerCoords;

            }


            // Enable buttons ..

            cmdSave.Enabled = true;
            cmdReset.Enabled = true;
            cmdClear.Enabled = true;

        }

        private void cmdSave_Click(object sender, EventArgs e) {

            if (lstLevels.SelectedItems.Count > 0) {

                LevelDefinition levelDefinition = (LevelDefinition)lstLevels.SelectedItems[0].Tag;


                // Enemies ..

                levelDefinition.Enemies.Clear();
                for (int x = 0; x < picEnemies.Length; x++) {

                    if (picEnemies[x].Visible) {
                        CoordinateSet coordinate = (CoordinateSet)picEnemies[x].Tag;
                        levelDefinition.Enemies.Add(coordinate);
                    }

                }


                // Reentry points ..

                levelDefinition.ReentryPoints.Clear();
                for (int x = 0; x < picReentryPoints.Length; x++) {

                    if (picReentryPoints[x].Visible) {
                        CoordinateSet coordinate = (CoordinateSet)picReentryPoints[x].Tag;
                        levelDefinition.ReentryPoints.Add(coordinate);
                    }

                }

                for (int y = 0; y < 16; y++) {

                    for (int x = 0; x < 28; x++) {

                        levelDefinition.Grid[y,x] = (LevelElement)pictureBoxes[y, x].Tag;

                    }

                }

                levelDefinition.Player = (CoordinateSet)picPlayer.Tag;
                validate(lstLevels.SelectedItems[0], levelDefinition);
                lstLevels.Select();

            }

        }

        private void cmdLevelAdd_Click(object sender, EventArgs e) {

            ListViewItem item = new ListViewItem();
            item.Text = "level" + lstLevels.Items.Count;
            item.Tag = new LevelDefinition();
            item.ImageIndex = 1;
            lstLevels.Items.Add(item);
            lstLevels.SelectedItems.Clear();

            item.Selected = true;
            item.EnsureVisible();

            validate(item, (LevelDefinition)item.Tag);

        }

        private void validate(ListViewItem item, LevelDefinition levelDefinition) {

            bool inError = false;
            ArrayList lstErrors = new ArrayList();

            if (levelDefinition.Player == null) { inError = true; lstErrors.Add("A player must be placed on the level."); }

            if (levelDefinition.ReentryPoints.Count < 4) { inError = true; lstErrors.Add("Each level must contain exactly four re-entry points."); }
            if (levelDefinition.Enemies.Count == 0) { inError = true; lstErrors.Add("At least one enemy should be placed in a level."); }
            if (getCountOfLevelElements(levelDefinition, LevelElement.Gold) == 0) { inError = true; lstErrors.Add("At least one gold piece should be placed in a level."); }
            if (getCountOfLevelElements(levelDefinition, LevelElement.Level_Ladder) == 0) { inError = true; lstErrors.Add("At least one level ladder should be placed in a level."); }


            if (inError) {

                lstLevels.ShowItemToolTips = true;
                item.ImageIndex = 1;

                String toolTip = "";
                foreach (String error in lstErrors) {
                    toolTip = toolTip + error + "\n";
                }

                item.ToolTipText = toolTip;

            }
            else {

                lstLevels.ShowItemToolTips = false;
                item.ImageIndex = 0;

            }

        }

        private void cmdLevelDelete_Click(object sender, EventArgs e) {

            DialogResult result = MessageBox.Show("Delete selected the level?", "Confirm Deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            
            if (result == DialogResult.OK) {

                if (lstLevels.SelectedItems.Count > 0) {
                    lstLevels.SelectedItems[0].Remove();
                    clearLevel();
                    cmdClear.Enabled = false;
                    cmdReset.Enabled = false;
                    cmdSave.Enabled = false;
                }

            }

            if (lstLevels.Items.Count == 0) { mnuSave.Enabled = false; mnuSaveAs.Enabled = false; }

        }


        private void dgExport_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            e.Control.KeyPress -= new KeyPressEventHandler(colNumberOfGames_KeyPress);
            if (dgExport.CurrentCell.ColumnIndex == 1) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null) {
                    tb.KeyPress += new KeyPressEventHandler(colNumberOfGames_KeyPress);
                }
            }
        }
        
        private void colNumberOfGames_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void dgExport_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e) {
            e.Row.Cells[0].Value = dgExport.Rows.Count;
        }

        private void dgExport_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {

            Int16 rowId = 1;
            for (Int16 x = 0; x < dgExport.Rows.Count - 1; x++) {
                DataGridViewRow row = dgExport.Rows[x];
                row.Cells[0].Value = rowId;
                rowId++;
            }

        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e) {

            if (tabMain.SelectedIndex == 0) {
                lstLevels.Select();
            }
        }

        private void writeFile(String filename) {

            using (StreamWriter writer = new StreamWriter(filename)) {

                writer.Write("#pragma once\n\n#include \"../utils/Arduboy2Ext.h\"\n#include \"../utils/Utils.h\"\n#include \"../utils/Enums.h\"\n\n");
                writer.Write("#define GAME_NUMBER 1\n");
                writer.Write("#define NUMBER_OF_GAMES " + (dgExport.Rows.Count - 1) + "\n\n");

                int offset = 0;
                for (int x = 0; x < (dgExport.Rows.Count - 1); x++) {

                    DataGridViewRow dgRow = dgExport.Rows[x];
                    writer.Write("#if GAME_NUMBER == " + (x + 1) + "\n");
                    writer.Write("  #define LEVEL_COUNT         " + dgRow.Cells[1].Value + "\n");
                    writer.Write("  #define LEVEL_OFFSET        " + offset + "\n");
                    writer.Write("#endif\n");

                    offset = offset + Convert.ToInt16(dgRow.Cells[1].Value);

                }

                writer.Write("\n\n");

                for (Int16 x = 0; x < lstLevels.Items.Count; x++) {

                    ListViewItem item = lstLevels.Items[x];
                    LevelDefinition levelDefinition = (LevelDefinition)item.Tag;
                    writer.Write(LevelUtils.getLevelText(item, levelDefinition, x));

                }

                offset = 0;
                for (int y = 0; y < (dgExport.Rows.Count - 1); y++) {

                    DataGridViewRow dgRow = dgExport.Rows[y];
                    int maxPerLevel = Convert.ToInt16(dgRow.Cells[1].Value);

                    writer.Write("#if GAME_NUMBER == " + (y + 1) + "\n");
                    writer.Write("const uint8_t *levels[] =    { nullptr, \n");
                    writer.Write("                               ");

                    int z = 0;
                    for (int x = offset; x < offset + maxPerLevel; x++) {

                        z++;
                        writer.Write(lstLevels.Items[x].Text + ", ");

                        if (z % 10 == 0) {
                            writer.Write("\n                               ");
                        }

                    }
                    writer.Write("};\n#endif\n");

                    offset = offset + Convert.ToInt16(dgRow.Cells[1].Value);

                }

            }

        }

        private void cmdLevelUp_Click(object sender, EventArgs e) {

            if (lstLevels.SelectedItems[0].Index > 0) {

                int index = lstLevels.SelectedItems[0].Index - 1;
                ListViewItem lvi = lstLevels.SelectedItems[0];
                lstLevels.Items.RemoveAt(lvi.Index);
                lstLevels.Items.Insert(index, lvi);

            }

        }

        private void cmdLevelDown_Click(object sender, EventArgs e) {

            if (lstLevels.SelectedItems[0].Index < lstLevels.Items.Count) {

                int index = lstLevels.SelectedItems[0].Index + 1;
                ListViewItem lvi = lstLevels.SelectedItems[0];

                lstLevels.Items.RemoveAt(lvi.Index);
                lstLevels.Items.Insert(index, lvi);

            }

        }

        private void mnuSave_Click(object sender, EventArgs e) {

            int numberOfLevels = 0;


            for (int y = 0; y < (dgExport.Rows.Count - 1); y++) {

                DataGridViewRow dgRow = dgExport.Rows[y];
                int maxPerLevel = Convert.ToInt16(dgRow.Cells[1].Value);

                numberOfLevels = numberOfLevels + maxPerLevel;

            }

            if (lstLevels.Items.Count != numberOfLevels) {

                MessageBox.Show("The overall number of levels does not match the game breakdown.\n\nThe game breakdown can bet set on the 'Export' tab.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            writeFile(dgOpenMapData.FileName);

            MessageBox.Show("Level data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void mnuSaveAs_Click(object sender, EventArgs e) {

            dgSaveMapData.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (dgSaveMapData.FileName != "") {

                const string userRoot = "HKEY_CURRENT_USER";
                const string subkey = "LodeRunner";
                const string keyName = userRoot + "\\" + subkey;
                Registry.SetValue(keyName, "PathName", Path.GetDirectoryName(dgSaveMapData.FileName));
                writeFile(dgSaveMapData.FileName);

                MessageBox.Show("Level data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

    }

}



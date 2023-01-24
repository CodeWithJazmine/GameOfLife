using System;
using System.Drawing;
using System.Windows.Forms;

namespace GOLProject
{
    public partial class Form1 : Form
    {
        #region Variables
        // The universe array
        bool[,] universe = new bool[20, 20];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        // Default seed
        int seed = 70122;


        bool isHUDVisible = true;
        bool isGridVisible = true;
        bool isNeighborCountVisible = true;

        bool isToroidal = false;

        #endregion

        #region Form
        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }
        #endregion

        #region Next Generation
        // Calculate the next generation of cells
        private void NextGeneration()
        {
            // The scratchpad array
            bool[,] scratchpad = new bool[universe.GetLength(0), universe.GetLength(1)];


            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int count;
                    if (isToroidal)
                    {
                        count = CountNeighborsToroidal(x, y);
                    }
                    else
                    {
                        count = CountNeighborsFinite(x, y);
                    }

                    if (universe[x, y] == true && count < 2)
                    {
                        scratchpad[x, y] = universe[x, y];
                        scratchpad[x, y] = false;
                    }
                    if (universe[x, y] == true && count > 3)
                    {
                        scratchpad[x, y] = universe[x, y];
                        scratchpad[x, y] = false;
                    }
                    if (universe[x, y] == true && count == 2 || count == 3)
                    {
                        scratchpad[x, y] = universe[x, y];
                        scratchpad[x, y] = true;
                    }
                    if (universe[x, y] == false && count == 3)
                    {
                        scratchpad[x, y] = universe[x, y];
                        scratchpad[x, y] = true;
                    }
                    if (universe[x, y] == false && count != 3)
                    {
                        scratchpad[x, y] = false;
                    }


                }
            }

            bool[,] temp = universe;
            universe = scratchpad;
            scratchpad = temp;


            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabelAlive.Text = "Alive= " + CellCount().ToString();
            graphicsPanel1.Invalidate();
        }

        #endregion

        #region Timer
        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        #endregion

        #region Paint Graphic's Panel
        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {

            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Font for neighbor count & HUD

            Font font = new Font("Courier New", 8f);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Font hudFont = new Font("Century Gothic", 15f);
            StringFormat hudFormat = new StringFormat();
            hudFormat.Alignment = StringAlignment.Near;
            hudFormat.LineAlignment = StringAlignment.Far;

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {

                    int count;
                    string boundaryType;
                    if (isToroidal)
                    {
                        count = CountNeighborsToroidal(x, y);
                        boundaryType = "Toroidal";
                    }
                    else
                    {
                        count = CountNeighborsFinite(x, y);
                        boundaryType = "Finite";
                    }

                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    if (isGridVisible == true)
                    {
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }
                    else
                    { continue; }

                    // Write neighbor count
                    if (isNeighborCountVisible == true && count != 0)
                    {
                        e.Graphics.DrawString(count.ToString(), font, Brushes.Black, cellRect, stringFormat);
                    }

                    // Display HUD
                    string hud = $"Generation: {generations}\nCell count: {CellCount()}\nBoundary Type: {boundaryType}\nUniverse Size:[Width: {universe.GetLength(0)} Height:{universe.GetLength(1)}]";
                    if (isHUDVisible == true)
                    {
                        e.Graphics.DrawString(hud, hudFont, Brushes.ForestGreen, graphicsPanel1.ClientRectangle, hudFormat);
                    }
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
                float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                float x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                float y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[(int)x, (int)y] = !universe[(int)x, (int)y];

                // Tell Windows you need to repaint
                toolStripStatusLabelAlive.Text = "Alive= " + CellCount().ToString();
                graphicsPanel1.Invalidate();
            }
        }

        #endregion

        #region Count Neighbors

        // Count Neighbors Finite
        private int CountNeighborsFinite(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0)
                        continue;
                    // if xCheck is less than 0 then continue
                    if (xCheck < 0)
                        continue;
                    // if yCheck is less than 0 then continue
                    if (yCheck < 0)
                        continue;
                    // if xCheck is greater than or equal too xLen then continue
                    if (xCheck >= xLen)
                        continue;
                    // if yCheck is greater than or equal too yLen then continue
                    if (yCheck >= yLen)
                        continue;

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }
            return count;

        }

        // Count Neighbors Toroidal
        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;
                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0)
                        continue;
                    // if xCheck is less than 0 then set to xLen - 1
                    if (xCheck < 0)
                        xCheck = xLen - 1;
                    // if yCheck is less than 0 then set to yLen - 1
                    if (yCheck < 0)
                        yCheck = yLen - 1;
                    // if xCheck is greater than or equal too xLen then set to 0
                    if (xCheck >= xLen)
                        xCheck = 0;
                    // if yCheck is greater than or equal too yLen then set to 0
                    if (yCheck >= yLen)
                        yCheck = 0;
                    if (universe[xCheck, yCheck] == true)
                        count++;
                }
            }
            return count;
        }

        // Alive cell count
        private int CellCount()
        {
            int count = 0;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (universe[x, y] == true)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        #endregion

        #region Run Menu Items & New File Menu Item

        // Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Start
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            graphicsPanel1.Invalidate();
        }

        // Pause
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            graphicsPanel1.Invalidate();
        }

        // Next Generation
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            NextGeneration();
            graphicsPanel1.Invalidate();
        }

        // New/Reset
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }

            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabelAlive.Text = "Alive= " + CellCount().ToString();
            graphicsPanel1.Invalidate();
        }

        #endregion

        #region Color Menu Items
        // Background Color
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == colorDialog.ShowDialog())
            {
                graphicsPanel1.BackColor = colorDialog.Color;
                graphicsPanel1.Invalidate();
            }

        }
        // Cell Color
        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = cellColor;

            if (DialogResult.OK == colorDialog.ShowDialog())
            {
                cellColor = colorDialog.Color;
                graphicsPanel1.Invalidate();
            }
        }

        // Grid Color
        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = gridColor;

            if (DialogResult.OK == colorDialog.ShowDialog())
            {
                gridColor = colorDialog.Color;
                graphicsPanel1.Invalidate();
            }
        }

        #endregion

        #region Options Menu
        // Options Menu
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Options opt = new Options();
            opt.Interval = timer.Interval;
            opt.UniverseWidth = universe.GetLength(0);
            opt.UniverseHeight = universe.GetLength(1);

            if (DialogResult.OK == opt.ShowDialog())
            {
                timer.Interval = opt.Interval;
                if (opt.UniverseWidth != universe.GetLength(0) || opt.UniverseHeight != universe.GetLength(1))
                {
                    timer.Enabled = false;

                    bool[,] scratchpad = new bool[opt.UniverseWidth, opt.UniverseHeight];
                    bool[,] temp = universe;
                    universe = scratchpad;
                    scratchpad = temp;

                    generations = 0;
                    toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
                }
            }
            toolStripStatusLabelInterval.Text = "Interval = " + timer.Interval.ToString();

            graphicsPanel1.Invalidate();
        }

        #endregion

        #region Randomize Menu

        // Randomize() --- Default Constructor
        private void Randomize()
        {
            Random rnd = new Random();

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int num = rnd.Next(0, 2);
                    if (num == 0)
                    {
                        universe[x, y] = true;
                    }
                    else
                    {
                        universe[x, y] = false;
                    }
                }
            }
            graphicsPanel1.Invalidate();
        }

        //Randomize(Int32 seed) 
        public void Randomize(Int32 seed)
        {

            Random rnd = new Random(seed);

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int num = rnd.Next(0, 2);
                    if (num == 0)
                    {
                        universe[x, y] = true;
                    }
                    else
                    {
                        universe[x, y] = false;
                    }
                }
            }

            graphicsPanel1.Invalidate();
        }

        // Randomize From Seed 
        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;

            FromSeed fromSeed = new FromSeed();
            fromSeed.Seed = seed;
            if (DialogResult.OK == fromSeed.ShowDialog())
            {
                seed = fromSeed.Seed;
                Randomize(seed);
            }

            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabelSeed.Text = "Seed = " + seed.ToString();
            toolStripStatusLabelAlive.Text = "Alive= " + CellCount().ToString();
            graphicsPanel1.Invalidate();
        }

        // Randomize From Time
        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            Randomize();
            generations = 0;
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            toolStripStatusLabelAlive.Text = "Alive= " + CellCount().ToString();
            graphicsPanel1.Invalidate();

        }
        #endregion

        #region View Menu
        //Enable Finite Count Neighbors
        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            isToroidal = false;
            graphicsPanel1.Invalidate();
        }

        //Enable Toroidal Count Neighbors
        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            isToroidal = true;
            graphicsPanel1.Invalidate();
        }

        // Toggle Grid 
        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isGridVisible == true)
            {
                isGridVisible = false;
            }
            else
            {
                isGridVisible = true;
            }
            graphicsPanel1.Invalidate();
        }

        // Toggle Neighbor Count
        private void neighborCountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isNeighborCountVisible == true)
            {
                isNeighborCountVisible = false;
            }
            else
            {
                isNeighborCountVisible = true;
            }
            graphicsPanel1.Invalidate();
        }

        // Toggle HUD
        private void hUDToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (isHUDVisible == true)
            {
                isHUDVisible = false;
            }
            else
            {
                isHUDVisible = true;
            }
            graphicsPanel1.Invalidate();
        }
    }

    #endregion
}

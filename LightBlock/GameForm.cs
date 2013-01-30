using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace LightBlock
{
    /// <summary>
    /// The main GUI class for handling the Windows Form
    /// </summary>
    public partial class GameForm : Form
    {
        /// <summary>
        /// Constructor for the Windows Form
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            ShowSetupGUI();
        }

        /// <summary>
        /// Shows the Setup GUI, which asks the user how large they want to make the game board.
        /// </summary>
        private void ShowSetupGUI()
        {
            gameboard.Hide();
            panel_buttons.Hide();
            panel_setup.Show();

            //Change the form size so that we only show what we need to
            this.MaximumSize = panel_setup.ClientSize;
            this.MinimumSize = panel_setup.ClientSize;
        }

        /// <summary>
        /// Constructs the Game board GUI
        /// </summary>
        /// <param name="maxX">The width of the board</param>
        /// <param name="maxY">The height of the board</param>
        /// <param name="size">The size in pixels (both width and height) of each square</param>
        private void ConstructBoardGUI(int maxX, int maxY, int size)
        {
            panel_setup.Hide();

            panel_buttons.Show();
            gameboard.Show();

            gameboard.Controls.Clear();

            //Set up a GUI so that mechanics can talk to this form
            GUI.GetInstance(this);

            //Change the form size to fit the entirety of the gameboard plus the buttons.
            gameboard.Location = new Point(0, 0);
            Size s = new Size(
                maxX * size + 40 >= panel_buttons.Width? maxX * size + 40 : panel_buttons.Width + 40, 
                maxY * size + 40 + panel_buttons.ClientSize.Height
                );
            gameboard.Size = new Size(maxX * size + 40,  maxY * size + 40);
            this.MaximumSize = s;
            this.MinimumSize = s;

            //Change the buttons so that they exist underneath the gameboard and not overlapping.
            panel_buttons.Location = new Point((this.Width/2) - (panel_buttons.Width/2), this.Height - panel_buttons.Height);

            //Now create every square on the board and set it's color to default "dark"
            for(int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    PictureBox pb = new PictureBox();

                    //Naming the squares needs to be done in a specific way so that we can find them dynamically.
                    //Therefore the grid reference is included in the name, and to avoid confusion (12/0 and 1/20 would both be box120)
                    //We add a 0 to the number if it's under 10, i.e: 05 and 02.
                    pb.Name = "box" + (x < 10 ? x.ToString() : "0" + x) + (y < 10 ? y.ToString() : "0" + y);

                    pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    pb.Location = new System.Drawing.Point(10 + x * size, 10 + y * size);
                    pb.Size = new System.Drawing.Size(size, size);
                    gameboard.Controls.Add(pb);
                    SetBlockColor(SquareState.Dark, new Location(x, y));
                }
            }
        }

        /// <summary>
        /// Event processing for user key presses
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;

            //Check the keys to see if we care about any key that was pressed, and do the appropriate actions if so.
            switch (keyData)
            {
                case Keys.Right:
                    LightBlockEngine.player.Move(LightBlockEngine.player.location + LightBlock.Location.Right);
                    bHandled = true;
                    break;
                case Keys.Left:
                    LightBlockEngine.player.Move(LightBlockEngine.player.location + LightBlock.Location.Left);
                    bHandled = true;
                    break;
                case Keys.Up:
                    LightBlockEngine.player.Move(LightBlockEngine.player.location + LightBlock.Location.Up);
                    bHandled = true;
                    break;
                case Keys.Down:
                    LightBlockEngine.player.Move(LightBlockEngine.player.location + LightBlock.Location.Down);
                    bHandled = true;
                    break;
            }
            return bHandled;
        }

        /// <summary>
        /// Changes the color of a block on the GUI.
        /// </summary>
        /// <param name="s">The state of the block to change color based on</param>
        /// <param name="l">The block's location in the grid</param>
        public void SetBlockColor(SquareState s, Location l)
        {
            Color light = Color.Red;
            Color dark = Color.RosyBrown;
            Color block = Color.Gray;
            Color player = Color.Crimson;

            //Find the picturebox to be changed based on it's name.  i.e.: Location 1/20 would show up as "box0120"
            PictureBox pb = (PictureBox)gameboard.Controls.Find(
                "box" + (l.x < 10 ? l.x.ToString() : "0" + l.x) + (l.y < 10 ? l.y.ToString() : "0" + l.y), false)[0];
            switch (s)
            {
                case SquareState.Dark:
                    pb.BackColor = dark;
                    break;
                case SquareState.Light:
                    pb.BackColor = light;
                    break;
                case SquareState.Block:
                    pb.BackColor = block;
                    break;
                case SquareState.Player:
                    pb.BackColor = player;
                    break;
            }
        }

        /// <summary>
        /// Shows a Message Box with a string passed in
        /// </summary>
        /// <param name="message">Message to be shown</param>
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Starts a completely new game by removing the gameboard and showing the setup GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_new_Click(object sender, EventArgs e)
        {
            ShowSetupGUI();
        }

        /// <summary>
        /// Restarts the game without changing any of it's original parameters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_restart_Click(object sender, EventArgs e)
        {
            ConstructBoardGUI(LightBlockEngine.maxX, LightBlockEngine.maxY, LightBlockEngine.blockSize);
            LightBlockEngine.ResetGame();
        }

        /// <summary>
        /// Starts a new game with the parameters set in the Setup GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_play_Click(object sender, EventArgs e)
        {
            int x = (int)num_height.Value;
            int y = (int)num_width.Value;
            int size = 400/(x >= y ? x : y);

            ConstructBoardGUI(x, y, size);

            LightBlockEngine.NewGame(x, y, size);
        }
    }

    /// <summary>
    /// This class is for communicating with the GUI from outside.
    /// </summary>
    public class GUI
    {
        #region singleton
        //Singleton pattern to make sure we only have one of these objects at a time
        private static GUI _Instance;

        public static GUI GetInstance(GameForm gui)
        {
            if (_Instance == null)
            {
                _Instance = new GUI(gui);
            }
            return _Instance;
        }

        public static GUI GetInstance() { return _Instance; }

        private GUI(GameForm gf)
        {
            this.gui = gf;
        }
        #endregion

        GameForm gui;

        /// <summary>
        /// Updates the block color in the GUI to what the block is currently set to.
        /// </summary>
        /// <param name="s">The Square to be updated in GUI</param>
        public void UpdateBlockColor(Square s)
        {
            gui.SetBlockColor(s.currentState, s.location);
        }

        /// <summary>
        /// Shows a message box with the string passed
        /// </summary>
        /// <param name="message">Message to be shown</param>
        public void MessageBox(string message)
        {
            gui.ShowMessageBox(message);
        }
    }
}

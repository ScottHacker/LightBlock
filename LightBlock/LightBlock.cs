using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace LightBlock
{
    /// <summary>
    /// The main class for controling and interacting with the puzzle engine from the outside.
    /// </summary>
    public class LightBlockEngine
    {
        public static Grid grid;
        public static Player player;
        public static GUI gui = GUI.GetInstance();

        private static Location origPlayerLoc;

        public static int maxX;
        public static int maxY;
        public static int blockSize;

        /// <summary>
        /// Creates a new game with the passed in elements
        /// </summary>
        /// <param name="x">Width of board</param>
        /// <param name="y">Height of board</param>
        /// <param name="size">Size in pixels of each block</param>
        public static void NewGame(int x, int y, int size)
        {
            //Make sure that the GUI has been set before getting here, or there'll be problems.
            if (gui == null)
            {
                Debug.WriteLine("ERROR! GUI not found!");
                return;
            }

            maxX = x;
            maxY = y;
            blockSize = size;
            grid = new Grid();
            player = new Player();
            origPlayerLoc = player.location;
        }

        /// <summary>
        /// Resets the game without changing any of the parameters
        /// </summary>
        public static void ResetGame()
        {
            grid.ResetGrid();
            player.PlacePlayer(origPlayerLoc);
        }

        /// <summary>
        /// Ends the game with either a win or lose condition.
        /// </summary>
        /// <param name="playerVictory">True if player won, false if player lost.</param>
        public static void EndGame(bool playerVictory)
        {
            if (playerVictory)
                gui.MessageBox("You won!");
            else
                gui.MessageBox("You lost.");
        }
    }

    /// <summary>
    /// Class that controls Player location and movement
    /// </summary>
    public class Player
    {
        public Location location;

        /// <summary>
        /// Creates a new player and places it randomly on the grid.
        /// </summary>
        public Player()
        {
            this.location = PlacePlayer();
            LightBlockEngine.grid.ChangeState(location, SquareState.Player);
        }

        /// <summary>
        /// Places the player on a random dark block point on the grid
        /// </summary>
        /// <returns>The players location</returns>
        private Location PlacePlayer()
        {
            //Loop to keep getting random squares until a valid location for player can be found.
            Square randomSquare = LightBlockEngine.grid.GetSquare();
            while (randomSquare.currentState != SquareState.Dark)
                randomSquare = LightBlockEngine.grid.GetSquare();
            return randomSquare.location;
        }

        /// <summary>
        /// Places the player at a specific point on the grid passed in.
        /// </summary>
        /// <param name="l">The location to move the player to</param>
        public void PlacePlayer(Location l)
        {
            this.location = l;
            LightBlockEngine.grid.ChangeState(location, SquareState.Player);
        }

        /// <summary>
        /// Attempts to move the player to a location given, making sure that location would be valid
        /// </summary>
        /// <param name="l">Location to move to</param>
        public void Move(Location l)
        {
            if (LightBlockEngine.grid.LocationValid(l) && 
                LightBlockEngine.grid.Board[l.x, l.y].currentState == SquareState.Dark)
            {
                //Since we're using a different color for the player block, 
                //we need to change it back to the normal color after the player moves
                LightBlockEngine.grid.ChangeState(location, SquareState.Light);
                this.location = l;
                LightBlockEngine.grid.ChangeState(l, SquareState.Player);
                CheckIfStuck(l);
            }
        }

        /// <summary>
        /// Checks if the player can make any valid moves from their location, if not, then the game is over.
        /// </summary>
        /// <param name="l">Location to check</param>
        private void CheckIfStuck(Location l)
        {
            //Look at all the possible moves a player could make, and see if any of them would be valid moves
            //If there is a valid move, then we return to break out of the function entirely.
            foreach (Location direction in Location.Directions)
            {
                if (LightBlockEngine.grid.LocationValid(l + direction) &&
                    LightBlockEngine.grid.GetSquare(l + direction).currentState == SquareState.Dark)
                    return;
            }

            //If we've gotten here, then there are no valid moves.  Let's see if the player won or lost.
            if (CheckCompletion())
                LightBlockEngine.EndGame(true);
            else
                LightBlockEngine.EndGame(false);
        }

        /// <summary>
        /// Checks if there are any dark squares left on the board, if there aren't, then the puzzle is complete.
        /// </summary>
        /// <returns>Returns true if there are no dark spaces, false if there are.</returns>
        private bool CheckCompletion()
        {
            foreach (Square s in LightBlockEngine.grid.Board)
            {
                if (s.currentState == SquareState.Dark)
                    return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Class that manages the game board mechanics
    /// </summary>
    public class Grid
    {
        private Square[,] board;
        public Square[,] Board { get { return board; } }

        /// <summary>
        /// Checks if a Location given is a valid location on the grid.
        /// </summary>
        /// <param name="l">Location given</param>
        /// <returns>True if valid, false if invalid</returns>
        public bool LocationValid(Location l) { return l.x >= 0 & l.y >= 0 & l.x < LightBlockEngine.maxX & l.y < LightBlockEngine.maxY; }

        /// <summary>
        /// Takes a location and returns a Square object at that location on the board.
        /// </summary>
        /// <param name="l">Location of Square</param>
        /// <returns>Square object reference</returns>
        public Square GetSquare(Location l) { return board[l.x, l.y]; }

        /// <summary>
        /// Gets a random square on the board
        /// </summary>
        /// <returns>Square object reference</returns>
        public Square GetSquare()
        {
            Random r = new Random();
            Location randomLoc = new Location(r.Next(0, LightBlockEngine.maxX), r.Next(0, LightBlockEngine.maxY));
            return GetSquare(randomLoc);
        }

        /// <summary>
        /// Creates new grid based on parameters passed in.
        /// </summary>
        public Grid()
        {
            board = new Square[LightBlockEngine.maxX, LightBlockEngine.maxY];

            for (int x = 0; x < LightBlockEngine.maxX; x++)
            {
                for (int y = 0; y < LightBlockEngine.maxY; y++)
                {
                    Square s = new Square(x, y, LightBlockEngine.blockSize);
                    board[x, y] = s;
                }
            }
            //This will add one block for every twelve squares of grid space (rounded down).
            int numberOfBlocks = (LightBlockEngine.maxX * LightBlockEngine.maxY) / 12;
            AddBlocks(numberOfBlocks);
        }

        /// <summary>
        /// Resets grid to what it was before the player started making their moves.
        /// </summary>
        public void ResetGrid()
        {
            //Resets the board by going through each square and every lit one and the player square to dark 
            //(we'll reset the player square later)
            foreach (Square s in board)
            {
                if (s.currentState == SquareState.Light || s.currentState == SquareState.Player)
                    s.currentState = SquareState.Dark;
                ChangeState(s.location, s.currentState);
            }
        }

        /// <summary>
        /// Adds n amount of blocks randomly to the grid.
        /// </summary>
        /// <param name="numberOfBlocks">Number of blocks to be created</param>
        private void AddBlocks(int numberOfBlocks)
        {
            for (int i = 0; i < numberOfBlocks; i++)
            {
                //Keeps randomly getting squares until it finds a valid one.
                Square randomSquare = GetSquare();
                while (!FindValidBlockLocation(randomSquare.location))
                {
                    randomSquare = GetSquare();
                }
                board[randomSquare.location.x, randomSquare.location.y].currentState = SquareState.Block;
                ChangeState(randomSquare.location, SquareState.Block);
            }
        }

        /// <summary>
        /// Finds a location on the grid where a block could exist without making the puzzle impossible to beat
        /// </summary>
        /// <param name="l">Location on grid for block to be placed</param>
        /// <returns>True if location is valid, false if invalid</returns>
        private bool FindValidBlockLocation(Location l)
        {
            //If the square state isn't dark, then it plain isn't valid.  Move on.
            if (GetSquare(l).currentState != SquareState.Dark)
                return false;

            //Set up a full cardinal set of directions.  
            //In addition to up, down, left, right, we also add Up-right, up-left, down-right, down-left.
            List<Location> fullDirections = new List<Location>();
            fullDirections.AddRange(Location.Directions);
            fullDirections.AddRange(
                new Location[] { new Location(1, 1), new Location(1, -1), new Location(-1, 1), new Location(-1, -1) });

            //Goes through each direction and sees what's there.  If it's off the board or has a block there, then we add 1 to an int.
            //Once it does, it checks the "score" of that location, if it's over 3, then we don't allow a block to be placed there.
            //This keeps from situations where you have a block formation that you can enter but not exit, one of these are okay
            //but two would make for an impossible puzzle.
            int blocks = 0;
            foreach (Location direction in fullDirections)
            {
                if (!LocationValid(l + direction) ||
                    GetSquare(l + direction).currentState == SquareState.Block)
                    blocks++;
            }
            if (blocks >= 3)
                return false;

            //If we managed to get through all that without triggering a return, then the location must be valid.
            return true;
        }

        /// <summary>
        /// Changes the state of a block, including a call to update GUI.
        /// </summary>
        /// <param name="l">Location of block to be changed</param>
        /// <param name="s">State to change to</param>
        public void ChangeState(Location l, SquareState s)
        {
            board[l.x, l.y].currentState = s;
            LightBlockEngine.gui.UpdateBlockColor(board[l.x, l.y]);
        }
    }

    /// <summary>
    /// Object for each block on the grid
    /// </summary>
    public class Square
    {
        //Each square has a location and a state, nothing else.
        public Location location;
        public SquareState currentState;

        public Square(int x, int y, int size)
        {
            this.currentState = SquareState.Dark;
            this.location = new Location(x, y);
        }
    }

    /// <summary>
    /// Enum for valid states of a block
    /// </summary>
    public enum SquareState { Light, Dark, Block, Player }

    /// <summary>
    /// Custom type to manage a "location" variable.
    /// </summary>
    public struct Location
    {
        //Some basic directions to make things easier.
        public static Location Left = new Location(-1, 0);
        public static Location Right = new Location(1, 0);
        public static Location Up = new Location(0, -1);
        public static Location Down = new Location(0, 1);
        public static List<Location> Directions = new List<Location>() { Left, Right, Up, Down };

        //We store the location data in an array, with 0 as X and 1 as Y
        int[] loc;

        /// <summary>
        /// Get the location's X
        /// </summary>
        public int x
        {
            get { return loc[0]; }
        }

        /// <summary>
        /// Get the location's Y
        /// </summary>
        public int y
        {
            get { return loc[1]; }
        }

        /// <summary>
        /// Create new location
        /// </summary>
        /// <param name="x">X or width</param>
        /// <param name="y">Y or height</param>
        public Location(int x, int y)
        {
            loc = new int[] { x, y };
        }

        /// <summary>
        /// Override the plus operator so we can add location types without fuss.
        /// </summary>
        /// <param name="l1">Location 1</param>
        /// <param name="l2">Location 2</param>
        /// <returns>The locations added together</returns>
        public static Location operator +(Location l1, Location l2)
        {
            return new Location(l1.x + l2.x, l1.y + l2.y);
        }

        /// <summary>
        /// Override ToString() so we can return a clean and formatted location string.
        /// </summary>
        /// <returns>Formatted location string</returns>
        public override string ToString()
        {
            return "X: " + x + ", Y: " + y;
        }
    }
}

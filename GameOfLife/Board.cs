using System.Collections.Generic;

namespace GameOfLife
{
    public delegate void Rule(BoardController controller, Vector cell);
    public sealed class Board
    {
        internal Board(Game game)
        {
            mCells = new HashSet<Vector>();
            Game = game;
            Rules = new List<Rule>();
            BasicRules.RegisterRules(Rules);
        }
        public void Load(IEnumerable<Vector> state)
        {
            mCells.Clear();
            foreach (Vector cell in state)
            {
                mCells.Add(cell);
            }
        }
        public void LoadConfig()
        {
            Rules.Clear();
            BasicRules.RegisterRules(Rules);
            Rules.AddRange(Config.AdditionalRules);
            Load(Config.InitialState);
        }
        public (Vector min, Vector max) GetDimensions()
        {
            (Vector min, Vector max) dims = new()
            {
                min = (0, 0),
                max = (1, 1)
            };
            foreach (Vector cell in mCells)
            {
                if (cell.X < dims.min.X)
                {
                    dims.min.X = cell.X;
                }
                else if (cell.X >= dims.max.X)
                {
                    dims.max.X = cell.X + 1;
                }
                if (cell.Y < dims.min.Y)
                {
                    dims.min.Y = cell.Y;
                }
                else if (cell.Y >= dims.max.Y)
                {
                    dims.max.Y = cell.Y + 1;
                }
            }
            return dims;
        }
        public int GetNeighborCount(Vector cell)
        {
            int neighborCount = 0;
            var offsets = new List<Vector>
            {
                (-1, -1), ( 0, -1), ( 1, -1),
                (-1,  0),           ( 1,  0),
                (-1,  1), ( 0,  1), ( 1,  1)
            };
            foreach (var offset in offsets)
            {
                if (mCells.Contains(cell + offset))
                {
                    neighborCount++;
                }
            }
            return neighborCount;
        }
        public bool IsCellAlive(Vector cell)
        {
            return mCells.Contains(cell);
        }
        internal void Update()
        {
            var controller = new BoardController(this);
            var (min, max) = GetDimensions();
            // this extends to a margin of 1 cell, to allow screen growth
            for (int x = min.X - 1; x < max.X + 1; x++)
            {
                for (int y = min.Y - 1; y < max.Y + 1; y++)
                {
                    foreach (Rule rule in Rules)
                    {
                        rule(controller, (x, y));
                    }
                }
            }
            mCells = new HashSet<Vector>(controller.Cells);
        }
        public Game Game { get; private set; }
        public List<Rule> Rules { get; private set; }
        public HashSet<Vector> Cells => new(mCells);
        private HashSet<Vector> mCells;
    }
    public struct BoardController
    {
        internal BoardController(Board board)
        {
            Board = board;
            Cells = new HashSet<Vector>(board.Cells);
        }
        public bool AddCell(Vector cell)
        {
            return Cells.Add(cell);
        }
        public bool KillCell(Vector cell)
        {
            return Cells.Remove(cell);
        }
        public Board Board { get; private set; }
        internal HashSet<Vector> Cells { get; private set; }
    }
}

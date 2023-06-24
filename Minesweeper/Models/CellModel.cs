namespace Minesweeper.Models
{
    public class CellModel
    {
        private bool visited = false;
        public bool Visited { get { return visited; } set { this.visited = value; } }
        private bool live = false;
        public bool Live { get { return live; } set { this.live = value; } }

        private int liveNeighbors = 0;
        public int LiveNeighbors { get { return liveNeighbors; } set { this.liveNeighbors = value; } }

        private bool flagged = false;
        public bool Flagged { get { return flagged; } set { this.flagged = value; } }

        private int row = -1;
        public int Row { get { return row; } set { this.row = value; } }

        private int column = -1;
        public int Column { get { return column; } set { this.column = value; } }
        public CellModel()
        {
        }
        public override string ToString()
        {
            string v = Visited ? "1" : "0";
            string l = Live ? "1" : "0";
            string f = Flagged ? "1" : "0";
            return "" +v +"," + l +","+ LiveNeighbors +"," + f +"," + Row+"," + Column;
        }
    }
}

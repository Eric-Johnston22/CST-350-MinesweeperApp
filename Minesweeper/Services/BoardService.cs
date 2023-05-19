using Minesweeper.Models;
using System.Drawing;

namespace Minesweeper.Services
{
    public class BoardService
    {
        private CellModel[] bombs = new CellModel[25];
        public CellModel[] Bombs { get { return bombs; } }
        private decimal difficulty;
        public decimal Difficulty { get { return difficulty; } set { this.difficulty = value; } }

        public bool gameWon = false;
        public bool GameWon { get { return gameWon; } set { this.gameWon = value; } }

        private CellModel[,] grid = new CellModel[0,0];
        public CellModel[,] Grid { get { return grid; } set { grid = value; } }

        private int numberOfBombs;
        public int NumberOfBombs { get { return numberOfBombs; } set { this.numberOfBombs = value; } }

        private int remainingCells;
        public int RemainingCells { get { return remainingCells; } set { this.remainingCells = value; } }

        private int size;
        public int Size { get { return size; } set { this.size = value; } }


        public BoardService(int gridSize)
        {
            this.Size = gridSize;
            this.Grid = new CellModel[gridSize, gridSize];
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    Grid[i, j] = new CellModel();
                    Grid[i, j].Row = i;
                    Grid[i, j].Column = j;
                }
            }
            this.RemainingCells = this.Size * this.Size;
            this.Difficulty = 10;
            this.setupLiveNeighbors();
            this.calculateLiveNeighbors();
        }
        public void setupLiveNeighbors()
        {
            int numberOfBombs = (int)((this.Size * this.Size) * (Difficulty / 100));
            this.NumberOfBombs = numberOfBombs;
            this.bombs = new CellModel[numberOfBombs];
            Random random = new Random();
            for (int i = 0; i < numberOfBombs; i++)
            {
                bool chosen = false;
                while (!chosen)
                {
                    int x = random.Next(this.Size);
                    int y = random.Next(this.Size);
                    if (this.Grid[x, y].Live != true)
                    {
                        Grid[x, y].Live = true;
                        bombs[i] = this.Grid[x, y];
                        chosen = true;
                    }
                }
            }
        }
        public void calculateLiveNeighbors()
        {
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    int total;
                    if (i - 1 >= 0 && i + 1 < this.Size && j - 1 >= 0 && j + 1 < this.Size)
                    {
                        //Middle neighbors calculated
                        int one = this.Grid[i - 1, j - 1].Live ? 1 : 0;
                        int two = this.Grid[i - 1, j].Live ? 1 : 0;
                        int three = this.Grid[i - 1, j + 1].Live ? 1 : 0;
                        int four = this.Grid[i, j + 1].Live ? 1 : 0;
                        int five = this.Grid[i + 1, j + 1].Live ? 1 : 0;
                        int six = this.Grid[i + 1, j].Live ? 1 : 0;
                        int seven = this.Grid[i + 1, j - 1].Live ? 1 : 0;
                        int eight = this.Grid[i, j - 1].Live ? 1 : 0;
                        total = one + two + three + four + five + six + seven + eight;
                    }
                    else
                    {
                        //Corner neighbors calculated
                        if (i == 0 && j == 0)
                        {
                            int four = this.Grid[i, j + 1].Live ? 1 : 0;
                            int five = this.Grid[i + 1, j + 1].Live ? 1 : 0;
                            int six = this.Grid[i + 1, j].Live ? 1 : 0;
                            total = four + five + six;
                        }
                        else if (i == 0 && j == this.Size - 1)
                        {
                            int six = this.Grid[i + 1, j].Live ? 1 : 0;
                            int seven = this.Grid[i + 1, j - 1].Live ? 1 : 0;
                            int eight = this.Grid[i, j - 1].Live ? 1 : 0;
                            total = six + seven + eight;
                        }
                        else if (i == this.Size - 1 && j == 0)
                        {
                            int two = this.Grid[i - 1, j].Live ? 1 : 0;
                            int three = this.Grid[i - 1, j + 1].Live ? 1 : 0;
                            int four = this.Grid[i, j + 1].Live ? 1 : 0;
                            total = two + three + four;
                        }
                        else if (i == this.Size - 1 && j == this.Size - 1)
                        {
                            int one = this.Grid[i - 1, j - 1].Live ? 1 : 0;
                            int two = this.Grid[i - 1, j].Live ? 1 : 0;
                            int eight = this.Grid[i, j - 1].Live ? 1 : 0;
                            total = one + two + eight;
                        }
                        else
                        {
                            //Edge neighbors calculated
                            if (i == 0)
                            {
                                int four = this.Grid[i, j + 1].Live ? 1 : 0;
                                int five = this.Grid[i + 1, j + 1].Live ? 1 : 0;
                                int six = this.Grid[i + 1, j].Live ? 1 : 0;
                                int seven = this.Grid[i + 1, j - 1].Live ? 1 : 0;
                                int eight = this.Grid[i, j - 1].Live ? 1 : 0;
                                total = four + five + six + seven + eight;
                            }
                            else if (j == 0)
                            {
                                int two = this.Grid[i - 1, j].Live ? 1 : 0;
                                int three = this.Grid[i - 1, j + 1].Live ? 1 : 0;
                                int four = this.Grid[i, j + 1].Live ? 1 : 0;
                                int five = this.Grid[i + 1, j + 1].Live ? 1 : 0;
                                int six = this.Grid[i + 1, j].Live ? 1 : 0;
                                total = two + three + four + five + six;
                            }
                            else if (i == this.Size - 1)
                            {
                                int eight = this.Grid[i, j - 1].Live ? 1 : 0;
                                int one = this.Grid[i - 1, j - 1].Live ? 1 : 0;
                                int two = this.Grid[i - 1, j].Live ? 1 : 0;
                                int three = this.Grid[i - 1, j + 1].Live ? 1 : 0;
                                int four = this.Grid[i, j + 1].Live ? 1 : 0;
                                total = one + two + three + four + eight;
                            }
                            else
                            {
                                int one = this.Grid[i - 1, j - 1].Live ? 1 : 0;
                                int two = this.Grid[i - 1, j].Live ? 1 : 0;
                                int six = this.Grid[i + 1, j].Live ? 1 : 0;
                                int seven = this.Grid[i + 1, j - 1].Live ? 1 : 0;
                                int eight = this.Grid[i, j - 1].Live ? 1 : 0;
                                total = one + two + six + seven + eight;
                            }
                        }
                    }
                    this.Grid[i, j].LiveNeighbors = total;
                }
            }
        }

        public void floodFill(int row, int col)
        {
            //Visit this cell
            if (this.Grid[row, col].Visited != true)
            {
                this.Grid[row, col].Visited = true;
                this.RemainingCells--;
            }

            //If this cell has no live neighbors then
            //spread
            if (this.Grid[row, col].LiveNeighbors == 0)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        //If it is on the board and not a bomb
                        if (row + i >= 0 && row + i < this.Size &&
                           col + j >= 0 && col + j < this.Size &&
                           this.Grid[row + i, col + j].Live != true)
                        {
                            //Check if cell is visited and if not then visit it
                            //Decrement remaining available cell count
                            if (this.Grid[row + i, col + j].Visited != true)
                            {
                                //Flood fill neighbors
                                floodFill(row + i, col + j);
                            }
                        }
                    }
                }
            }
        }
    }
}

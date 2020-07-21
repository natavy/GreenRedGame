namespace GreenRedGame
{
    public class CellGrid
    {
        private readonly int xPosition; // x
        private readonly int yPosition; // y
        private readonly Cell[,] grid;

        //constructor
        public CellGrid(int x, int y, Cell[,] gen0)
        {
            xPosition = x;
            yPosition = y;
            grid = gen0;
        }

        //How many generation  until target generation is some color
        public int CountTargetGenerationsByColor(ICellPosition target, CellColor color, int targetGeneration)
        {
            var count = 0;

            for (int i = 0; i <= targetGeneration; i++)
            {
                if (grid[target.Row, target.Col].Color == color)
                {
                    count += 1;
                }

                ProcessNextGeneration();
            }

            return count;
        }

        //create next generation and aplly rules
        private void ProcessNextGeneration()
        {
            var generationSnapshot = CreateGenerationSnapshot();

            // Loop through every cell 
            for (int row = 0; row < yPosition; row++)
            {
                for (int col = 0; col < xPosition; col++)
                {
                    var cell = generationSnapshot[row, col];
                    //find green neighbours
                    var neighboursCount = GetGreenNeighboursCountFromSnapshot(cell, generationSnapshot);
                    //Aplly rules
                    switch (cell.Color)
                    {
                        case CellColor.Red when (neighboursCount == 3 || neighboursCount == 6):
                            grid[row, col].Color = CellColor.Green;
                            continue;
                        case CellColor.Green
                            when (neighboursCount != 2 && neighboursCount != 3 && neighboursCount != 6):
                            grid[row, col].Color = CellColor.Red;
                            continue;
                    }
                }
            }
        }

        //create current grid
        private Cell[,] CreateGenerationSnapshot()
        {
            var snapshot = new Cell[yPosition, xPosition];

            for (int row = 0; row < yPosition; row++)
            {
                for (int col = 0; col < xPosition; col++)
                {
                    var cell = (Cell)grid[row, col].Clone();
                    snapshot[row, col] = cell;
                }
            }

            return snapshot;
        }

        //get green neighbours and count it
        //surrounded by 8 cells
        private int GetGreenNeighboursCountFromSnapshot(Cell cell, Cell[,] snapshot)
        {
            var neighbours = 0;

            // ?**
            // *X*
            // ***
            if (cell.Row - 1 >= 0 && cell.Col - 1 >= 0 && snapshot[cell.Row - 1, cell.Col - 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            // *?*
            // *X*
            // ***
            if (cell.Row - 1 >= 0 && snapshot[cell.Row - 1, cell.Col].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            // **?
            // *X*
            // ***
            if (cell.Row - 1 >= 0 && cell.Col + 1 < xPosition &&
                snapshot[cell.Row - 1, cell.Col + 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            // ***
            // ?X*
            // ***
            if (cell.Col - 1 >= 0 && snapshot[cell.Row, cell.Col - 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            // ***
            // *X?
            // ***
            if (cell.Col + 1 < xPosition && snapshot[cell.Row, cell.Col + 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            // ***
            // *X*
            // ?**
            if (cell.Row + 1 < yPosition && cell.Col - 1 >= 0 &&
                snapshot[cell.Row + 1, cell.Col - 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            // ***
            // *X*
            // *?*
            if (cell.Row + 1 < yPosition && snapshot[cell.Row + 1, cell.Col].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            // ***
            // *X*
            // **?
            if (cell.Row + 1 < yPosition && cell.Col + 1 < xPosition &&
                snapshot[cell.Row + 1, cell.Col + 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            return neighbours;
        }
    }
}

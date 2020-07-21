namespace GreenRed.Console
{
    using GreenRedGame;
    using System;
    using System.Linq;


    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to Green vs. Red Game!");
            System.Console.WriteLine();
            System.Console.WriteLine("Enter size grid (height and width)separarted by ',' e.g. 3,3 or 5,5");
            var xyInput = System.Console.ReadLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Enter the values of row without separation e.g. 000");

            //Splits a string into substrings based on the characters in an array.
            var xy = xyInput?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            //Rows and cols
            var x = xy[0];
            var y = xy[1];
            Cell[,] gen0 = new Cell[x, y];

            //fill the grid
            for (int row = 0; row < y; row++)
            {
                System.Console.WriteLine($"Enter the values of row {row + 1}");
                var rowString = System.Console.ReadLine();

                //copy the characters in this instance to a Unicode character array.
                //and convert string to an Enum 
                var cellsInput = rowString?.ToCharArray()
                    .Select(value => (CellColor)Enum.Parse<CellColor>(value.ToString())).ToArray();

                for (int col = 0; col < cellsInput?.Length; col++)
                {

                    var color = cellsInput[col];

                    //if the value is not 1(green) or 0(red) will throw Exeption
                    if (color != CellColor.Green && color != CellColor.Red)
                    {
                        throw new FormatException($"Invalid color for cell {row}:{col}");
                    }

                    gen0[row, col] = new Cell(row, col, color);
                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Enter position(x & y) and number of turns separarted by ',' e.g. 1,0,10");
            var targetInput = System.Console.ReadLine();

            //Splits a string into substrings based on the characters in an array.
            var targetNumbers = targetInput?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            // Invert parameters for target cell
            // x (width) is first argument = col
            // y (height) is second argument = row
            var target = new Cell(targetNumbers[1], targetNumbers[0]);
            //thrird argument
            var targetGeneration = targetNumbers[2];
            var grid = new CellGrid(x, y, gen0);
            var targetGenerations = grid.CountTargetGenerationsByColor(target, CellColor.Green, targetGeneration);

            System.Console.WriteLine();
            System.Console.WriteLine($"Expected result is: {targetGenerations}");
        }
    }
}
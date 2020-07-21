namespace GreenRedGame
{
    using System;

    
    public class Cell : ICellPosition, ICloneable
    {
        //implement ICellPosition interface
        public int Row { get; } 
        public int Col { get; }

        public CellColor Color { get; set; }

        //constructor
        public Cell(int row, int col, CellColor color = CellColor.Green)
        {
            Row=row;
            Col=col;
            Color = color;
        }

        //Type safe Clone
        public override string ToString()
        {
            return $"{Row}:{Col} {Color}";
        }
        
        //implement ICloneable interface creates to copy the exisiting object 
        public object Clone()
        {
            return new Cell(Row, Col, Color);
        }
    }
   
}

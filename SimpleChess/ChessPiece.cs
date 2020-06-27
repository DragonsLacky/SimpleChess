using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SimpleChess
{
    public enum ChessColor { BLACK, WHITE }
    public class ChessPosition
    {
        public char X { get; set; }
        public int Y { get; set; }
        public ChessPosition(Char x, int y)
        {
            X = x;
            Y = y;
        }

    }
    abstract public class ChessPiece
    {
        public ChessPosition Position { get; set; }
        public ChessColor Color { get; set; }
        public PictureBox Piece { get; set; }
        public abstract void MovePiece(int x, int y);
        public abstract void getValidMoves();
        protected ChessPiece(char x, int y, ChessColor color, PictureBox piece)
        {
            Position = new ChessPosition(x, y);
            Color = color;
            Piece = piece;
        }

        
    }
}

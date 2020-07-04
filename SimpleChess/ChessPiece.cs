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
        public static bool operator==(ChessPosition p1, ChessPosition p2)
        {
            if (p1.X == p2.X && p1.Y == p2.Y)
            {
                return true;
            }
            else return false;
        }
        public static bool operator !=(ChessPosition p1, ChessPosition p2)
        {
            return !(p1 == p2);
        }
        public ChessPosition(Char x, int y)
        {
            X = x;
            Y = y;
        }

    }
    public class positionInfo
    {
        public bool ocupied { get; set; }
        public ChessPiece piece { get; set; }
        public positionInfo(ChessPiece p, bool oc)
        {
            ocupied = oc;
            piece = p;
        }
    }
    abstract public class ChessPiece
    {
        public ChessPosition Position { get; set; }
        public ChessColor Color { get; set; }
        public PictureBox Piece { get; set; }
        public PieceType Type { get; set; }
        public abstract void MovePiece(char x, int y);
        public abstract List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions);
        public abstract bool checkValidMove(ChessPosition position, List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions);
        virtual public  List<ChessPosition> getTakeMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions) { return getValidMoves(white, black, piecePositions); }
        public abstract string getType();
        virtual public bool victoryCondition(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> Occupied) { return false; }
        protected ChessPiece(char x, int y, ChessColor color, PictureBox piece)
        {
            Position = new ChessPosition(x, y);
            Color = color;
            Piece = piece;
        }

        
    }
}

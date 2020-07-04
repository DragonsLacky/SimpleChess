using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SimpleChess.Pieces
{
    public class Knight : ChessPiece
    {
        public Knight(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { Type = PieceType.KNIGHT; }

        public override bool checkValidMove(ChessPosition position, List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            List<ChessPosition> validPositions = getValidMoves(white, black, piecePositions);
            foreach (ChessPosition pos in validPositions)
            {
                if (position == pos)
                {
                    return true;
                }
            }
            return false;
        }

        public override List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            List<ChessPosition> validPositions = new List<ChessPosition>();
            validPositions.Add(new ChessPosition((char)(Position.X + 2), Position.Y + 1));
            validPositions.Add(new ChessPosition((char)(Position.X + 2), Position.Y - 1));
            validPositions.Add(new ChessPosition((char)(Position.X + 1), Position.Y + 2));
            validPositions.Add(new ChessPosition((char)(Position.X - 1), Position.Y + 2));
            validPositions.Add(new ChessPosition((char)(Position.X - 2), Position.Y + 1));
            validPositions.Add(new ChessPosition((char)(Position.X - 2), Position.Y - 1));
            validPositions.Add(new ChessPosition((char)(Position.X + 1), Position.Y - 2));
            validPositions.Add(new ChessPosition((char)(Position.X - 1), Position.Y - 2));

            for (int i = 0; i < validPositions.Count; i++)
            {
                if (validPositions[i].X < 'A' || validPositions[i].X > 'H' || validPositions[i].Y < 1 || validPositions[i].Y > 8)
                {
                    validPositions.RemoveAt(i);
                    i--;
                }
            }


            return validPositions;
        }
        
        public override string getType()
        {
            return "Knight";
        }

        public override void MovePiece(char x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}

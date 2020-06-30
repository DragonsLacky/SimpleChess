using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChess.Pieces
{
    public class Queen : ChessPiece
    {
        public Queen(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { Type = PieceType.QUEEN; }

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
            ChessPiece rook = new Rook(Position.X, Position.Y,Color, Piece);
            ChessPiece bishop = new Bishop(Position.X, Position.Y, Color, Piece);
            List<ChessPosition> validPositions = new List<ChessPosition>();
            validPositions.AddRange(rook.getValidMoves(white, black, piecePositions));
            validPositions.AddRange(bishop.getValidMoves(white, black, piecePositions));

            return validPositions;

        }

        public override void MovePiece(char x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}

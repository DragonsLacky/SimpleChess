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
        public Queen(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { }

        public override bool checkValidMove(ChessPosition position, List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            throw new NotImplementedException();
        }

        public override List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            return new List<ChessPosition>();
        }

        public override void MovePiece(char x, int y)
        {
            throw new NotImplementedException();
        }
    }
}

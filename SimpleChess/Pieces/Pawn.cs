using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChess.Pieces
{
    public class Pawn : ChessPiece
    {
        public Pawn(Char x, int y, ChessColor color): base(x,y,color)
        {
            
        }
        public override void getValidMoves()
        {
            throw new NotImplementedException();
        }

        public override void MovePiece(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}

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
        public Knight(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { }
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

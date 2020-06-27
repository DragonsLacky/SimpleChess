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

        //lista na naredni validni potezi
        private List<ChessPosition> validNextPositions;

        private override void checkValidMove(ChessPosition position,Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            char tempCoordinateX=position.X;
            int tempCoordinateY=position.Y;
            ChessPosition tempPosition = new ChessPosition(tempCoordinateX-1,tempCoordinateY-1);
         //1 Case
            if((Enumerable.Range('A','H').Contains(tempCoordinateX-1)) && (Enumerable.Range(1,8).Contains(tempCoordinateY+2)))
                {
                                
                }

            throw new NotImplementedException();
        }

        public override List<ChessPosition> getValidMoves(ChessPosition position,Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            checkValidMove(position, Occupied);
            return validNextPositions;

            throw new NotImplementedException();
        }

        public override ChessPosition MovePiece(ChessPosition position)
        {
            this.x=position.X;
            this.y=position.Y;

            throw new NotImplementedException();
        }
    }
}

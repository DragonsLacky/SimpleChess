using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChess
{
    class Moves
    {
        public ChessPiece chessPieceMoved { get; set; }
        public char  startX { get; set; }
        public int startY { get; set; }
        public char destiny_X { get; set; }
        public int destiny_Y { get; set; }
        public override string ToString()
        {

            return string.Format("{0} - {1}{2} - {3}{4}",chessPieceMoved.Color==ChessColor.WHITE? "WHITE":"BLACK",startX,startY,destiny_X,destiny_Y);
        }
        public Moves(ChessPiece chessPieceMoved, char startX,int startY,char destiny_X,int destiny_Y)
        {
            this.chessPieceMoved = chessPieceMoved;
            this.startX = startX;
            this.startY = startY;
            this.destiny_X = destiny_X;
            this.destiny_Y = destiny_Y;

        }

    }
}

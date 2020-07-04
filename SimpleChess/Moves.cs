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
        public bool PieceTaken { get; set; }
        public string TakenType { get; set; }
        public bool init { get; set; }
        public override string ToString()
        {
            if(init)
            {
                return string.Format("{0}\t{1}\t{2} - {3}\t{4}", "Player", "Piece", "from", "to", "Taken Piece");
            }
            if(!PieceTaken)
            {
                return string.Format("{0}\t{1}\t{2}{3} - {4}{5}", chessPieceMoved.Color == ChessColor.WHITE ? "White" : "Black", chessPieceMoved.getType(), startX, startY, destiny_X, destiny_Y);
            }
            return string.Format("{0}\t{1}\t{2}{3} - {4}{5}\t{6} {7}", chessPieceMoved.Color == ChessColor.WHITE ? "White" : "Black", chessPieceMoved.getType(), startX, startY, destiny_X, destiny_Y, chessPieceMoved.Color == ChessColor.WHITE ? "Black" : "White", TakenType);
        }

        public Moves(ChessPiece chessPieceMoved, char startX,int startY,char destiny_X,int destiny_Y)
        {
            this.chessPieceMoved = chessPieceMoved;
            this.startX = startX;
            this.startY = startY;
            this.destiny_X = destiny_X;
            this.destiny_Y = destiny_Y;
            PieceTaken = false;
            init = false;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SimpleChess.Pieces
{
    public class King : ChessPiece
    {
        public King(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { }

        public override bool checkValidMove(ChessPosition position)
        {
            throw new NotImplementedException();
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

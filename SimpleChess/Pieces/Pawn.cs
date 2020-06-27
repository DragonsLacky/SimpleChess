using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SimpleChess.Pieces
{
    public class Pawn : ChessPiece
    {
        bool startingPos;
        public Pawn(char x, int y, ChessColor color, PictureBox piece): base(x, y, color, piece) { startingPos = true; }

        public override bool checkValidMove(ChessPosition position)
        {
            throw new NotImplementedException();
        }

        public override List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black)
        {
            List<ChessPosition> movablePos = new List<ChessPosition>();
            if(Color == ChessColor.WHITE)
            {
                movablePos.Add(new ChessPosition(Position.X, Position.Y + 1));
                if(startingPos)
                {
                    movablePos.Add(new ChessPosition(Position.X, Position.Y + 2));
                }
            }
            else
            {
                movablePos.Add(new ChessPosition(Position.X, Position.Y - 1));
                if (startingPos)
                {
                    movablePos.Add(new ChessPosition(Position.X, Position.Y - 2));
                }
            }

            foreach (ChessPiece piece in white)
            {
                for (int i = 0; i < movablePos.Count; i++)
                {
                    if (movablePos[i].X == piece.Position.X && movablePos[i].Y == piece.Position.Y)
                    {
                        movablePos.RemoveAt(i);
                    }
                    if (piece.Color != Color)
                    {
                        if (Position.X - 1 == piece.Position.X && Position.Y - 1 == piece.Position.Y)
                        {
                            movablePos.Add(piece.Position);
                        }
                        if (Position.X + 1 == piece.Position.X && Position.Y - 1 == piece.Position.Y)
                        {
                            movablePos.Add(piece.Position);
                        }
                    }
                }
            }
            foreach (ChessPiece piece in white)
            {
                for (int i = 0; i < movablePos.Count; i++)
                {
                    if (movablePos[i].X == piece.Position.X && movablePos[i].Y == piece.Position.Y)
                    {
                        movablePos.RemoveAt(i);
                    }
                    if (piece.Color != Color)
                    {
                        if (Position.X - 1 == piece.Position.X && Position.Y + 1 == piece.Position.Y)
                        {
                            movablePos.Add(piece.Position);
                        }
                        if (Position.X + 1 == piece.Position.X && Position.Y + 1 == piece.Position.Y)
                        {
                            movablePos.Add(piece.Position);
                        }
                    }
                }
            }

            return movablePos;
            
        }

        public override void MovePiece(int x, int y)
        {
            if(startingPos)
            {

            }
            startingPos = false;
            throw new NotImplementedException();
        }
    }
}

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
        public Pawn(char x, int y, ChessColor color, PictureBox piece): base(x, y, color, piece) { startingPos = true; Type = PieceType.PAWN; }

        public override bool checkValidMove(ChessPosition position, List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            List<ChessPosition> validPositions = getValidMoves(white, black, piecePositions);
            foreach (ChessPosition pos in validPositions)
            {
                if(position == pos)
                {
                    return true;
                }
            }
            return false;
        }

        public override List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            List<ChessPosition> movablePos = new List<ChessPosition>();
            if(Color == ChessColor.WHITE)
            {
                movablePos.Add(new ChessPosition(Position.X, Position.Y + 1));
                if(startingPos && !piecePositions[Position.X][Position.Y + 1].ocupied)
                {
                    movablePos.Add(new ChessPosition(Position.X, Position.Y + 2));
                }
            }
            else
            {
                movablePos.Add(new ChessPosition(Position.X, Position.Y - 1));
                if (startingPos && !piecePositions[Position.X][Position.Y - 1].ocupied)
                {
                    movablePos.Add(new ChessPosition(Position.X, Position.Y - 2));
                }
            }
            for(int i = 0; i < movablePos.Count; i++)
            {
                if (movablePos[i].X < 'A' || movablePos[i].X > 'H' || movablePos[i].Y < 1 || movablePos[i].Y > 8)
                {
                    movablePos.RemoveAt(i);
                    i--;
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
            foreach (ChessPiece piece in black)
            {
                for (int i = 0; i < movablePos.Count; i++)
                {
                    if (movablePos[i].X == piece.Position.X && movablePos[i].Y == piece.Position.Y)
                    {
                        movablePos.RemoveAt(i);
                    }
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

            return movablePos;
            
        }

        public override List<ChessPosition> getTakeMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            List<ChessPosition> movablePos = new List<ChessPosition>();
            foreach (ChessPiece piece in white)
            {
                if (piece.Color != Color)
                {
                    movablePos.Add(new ChessPosition((char)(Position.X - 1), Position.Y - 1));
                    movablePos.Add(new ChessPosition((char)(Position.X + 1), Position.Y - 1));
                }
            }
            foreach (ChessPiece piece in black)
            {
                if (piece.Color != Color)
                {

                    movablePos.Add(new ChessPosition((char)(Position.X - 1), Position.Y + 1));
                    movablePos.Add(new ChessPosition((char)(Position.X + 1), Position.Y + 1));
                   
                }
            }
            return movablePos;
        }

        public override void MovePiece(char x, int y)
        {
            Position.X = x;
            Position.Y = y;
            startingPos = false;
        }
    }
}

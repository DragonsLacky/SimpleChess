using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SimpleChess.Pieces
{
    public class King : ChessPiece
    {
        public King(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { Type = PieceType.KING; }

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


        public override List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            List<ChessPosition> validPositions = new List<ChessPosition>();
            validPositions.Add(new ChessPosition((char)(Position.X + 1), Position.Y + 1));
            validPositions.Add(new ChessPosition((char)(Position.X + 1), Position.Y - 1));
            validPositions.Add(new ChessPosition((char)(Position.X - 1), Position.Y + 1));
            validPositions.Add(new ChessPosition((char)(Position.X - 1), Position.Y - 1));
            validPositions.Add(new ChessPosition(Position.X, Position.Y + 1));
            validPositions.Add(new ChessPosition(Position.X, Position.Y - 1));
            validPositions.Add(new ChessPosition((char)(Position.X + 1), Position.Y));
            validPositions.Add(new ChessPosition((char)(Position.X - 1), Position.Y));

            for (int i = 0; i < validPositions.Count; i++)
            {
                if (validPositions[i].X < 'A' || validPositions[i].X > 'H' || validPositions[i].Y < 1 || validPositions[i].Y > 8)
                {
                    validPositions.RemoveAt(i);
                    i--;
                }
            }

            if (this.Color == ChessColor.BLACK)
                foreach (ChessPiece p in white)
                {
                    if (p.Type == PieceType.KING)
                        continue;
                    List<ChessPosition> chessPositions = p.getTakeMoves(white, black, Occupied);
                    foreach (ChessPosition pos in chessPositions)
                    {
                        if (pos == Position)
                            continue;
                        for (int i = 0; i < validPositions.Count; i++)
                        {
                            if (validPositions[i] == pos)
                            {
                                
                                validPositions.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }

            if (this.Color == ChessColor.WHITE)
                foreach (ChessPiece p in black)
                {
                    if (p.Type == PieceType.KING)
                        continue;
                    List<ChessPosition> chessPositions = p.getTakeMoves(white, black, Occupied);
                    foreach (ChessPosition pos in chessPositions)
                    {
                        for (int i = 0; i < validPositions.Count; i++)
                        {
                            if (validPositions[i] == pos)
                            {
                                validPositions.RemoveAt(i);
                            }
                        }
                    }
                }

            return validPositions;
        }

        public override void MovePiece(char x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChess.Pieces
{
    public class Rook : ChessPiece
    {
        public Rook(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { }

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


        public override List<ChessPosition> getValidMoves(List<ChessPiece> white, List<ChessPiece> black, Dictionary<char, Dictionary<int, positionInfo>> piecePositions)
        {
            List<ChessPosition> validPositions = new List<ChessPosition>();
            int ypos = (Position.Y + 1);
            while (true)
            {
                if (ypos <= 8 && ypos >= 1)
                {
                    if (piecePositions[Position.X][ypos].ocupied && piecePositions[Position.X][ypos].piece.Color != Color)
                    {
                        validPositions.Add(new ChessPosition(Position.X, ypos));
                        break;
                    }
                    if(piecePositions[Position.X][ypos].ocupied)
                    {
                        break;
                    }
                    validPositions.Add(new ChessPosition(Position.X, ypos));
                    ypos += 1;
                }
                else break;
            }
            ypos = Position.Y - 1;
            while (true)
            {
                if (ypos <= 8 && ypos >= 1)
                {
                    if (piecePositions[Position.X][ypos].ocupied && piecePositions[Position.X][ypos].piece.Color != Color)
                    {
                        validPositions.Add(new ChessPosition(Position.X, ypos));
                        break;
                    }
                    if (piecePositions[Position.X][ypos].ocupied)
                    {
                        break;
                    }
                    validPositions.Add(new ChessPosition(Position.X, ypos));
                    ypos -= 1;
                }
                else break;
            }
            char xpos = (char) (Position.X + 1);
            while (true)
            {
                if (xpos <= 'H' && xpos >= 'A')
                {
                    if (piecePositions[xpos][Position.Y].ocupied && piecePositions[xpos][Position.Y].piece.Color != Color)
                    {
                        validPositions.Add(new ChessPosition(xpos, Position.Y));
                    }
                    if(piecePositions[xpos][Position.Y].ocupied)
                    {
                        break;
                    }
                    validPositions.Add(new ChessPosition(xpos, Position.Y));
                    xpos = (char)(xpos + 1);
                }
                else break;
            }
            xpos = (char)(Position.X - 1);
            while (true)
            {
                if (xpos <= 'H' && xpos >= 'A')
                {
                    if (piecePositions[xpos][Position.Y].ocupied && piecePositions[xpos][Position.Y].piece.Color != Color)
                    {
                        validPositions.Add(new ChessPosition(xpos, Position.Y));
                    }
                    if (piecePositions[xpos][Position.Y].ocupied)
                    {
                        break;
                    }
                    validPositions.Add(new ChessPosition(xpos, Position.Y));
                    xpos = (char)(xpos - 1);
                }
                else break;
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

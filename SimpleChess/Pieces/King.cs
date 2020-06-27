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
        public King(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { }

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
            List<ChessPosition> validNextPositions = new List<ChessPosition>();
            char tempCoordinateX = Position.X;
            int tempCoordinateY = Position.Y;
            ChessPosition tempPosition = new ChessPosition((char)(tempCoordinateX - 1), tempCoordinateY - 1);

            //1 Case
            if ((Enumerable.Range('A', 'H').Contains(tempCoordinateX - 1)) && (Enumerable.Range(1, 8).Contains(tempCoordinateY - 1)))
            {
                if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY - 1].ocupied == false)
                {
                    ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX - 1), tempCoordinateY - 1);
                    validNextPositions.Add(nextValidPosition);
                }
                if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY - 1].ocupied == true)
                {
                    if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY - 1].piece.Color != this.Color)
                    {
                        ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX - 1), tempCoordinateY - 1);
                        validNextPositions.Add(nextValidPosition);
                    }
                }

            }
            //2 Case 
            if (Enumerable.Range(1, 8).Contains(tempCoordinateY - 1))
            {
                if (Occupied[tempCoordinateX][tempCoordinateY - 1].ocupied == false)
                {
                    ChessPosition nextValidPosition = new ChessPosition(tempCoordinateX, tempCoordinateY - 1);
                    validNextPositions.Add(nextValidPosition);
                }
                if (Occupied[tempCoordinateX][tempCoordinateY - 1].ocupied == true)
                {
                    if (Occupied[tempCoordinateX][tempCoordinateY - 1].piece.Color != this.Color)
                    {
                        ChessPosition nextValidPosition = new ChessPosition(tempCoordinateX, tempCoordinateY - 1);
                        validNextPositions.Add(nextValidPosition);
                    }
                }

            }
            //3 Case 
            if ((Enumerable.Range('A', 'H').Contains(tempCoordinateX + 1)) && (Enumerable.Range(1, 8).Contains(tempCoordinateY - 1)))
            {
                if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY - 1].ocupied == false)
                {
                    ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX + 1), tempCoordinateY - 1);
                    validNextPositions.Add(nextValidPosition);
                }
                if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY - 1].ocupied == true)
                {
                    if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY - 1].piece.Color != this.Color)
                    {
                        ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX + 1), tempCoordinateY - 1);
                        validNextPositions.Add(nextValidPosition);
                    }
                }

            }
            //4 Case 
            if (Enumerable.Range('A', 'H').Contains(tempCoordinateX + 1))
            {
                if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY].ocupied == false)
                {
                    ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX + 1), tempCoordinateY);
                    validNextPositions.Add(nextValidPosition);
                }
                if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY].ocupied == true)
                {
                    if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY].piece.Color != this.Color)
                    {
                        ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX + 1), tempCoordinateY);
                        validNextPositions.Add(nextValidPosition);
                    }
                }

            }
            //5 Case 
            if ((Enumerable.Range('A', 'H').Contains(tempCoordinateX + 1)) && (Enumerable.Range(1, 8).Contains(tempCoordinateY + 1)))
            {
                if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY + 1].ocupied == false)
                {
                    ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX + 1), tempCoordinateY + 1);
                    validNextPositions.Add(nextValidPosition);
                }
                if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY + 1].ocupied == true)
                {
                    if (Occupied[(char)(tempCoordinateX + 1)][tempCoordinateY + 1].piece.Color != this.Color)
                    {
                        ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX + 1), tempCoordinateY + 1);
                        validNextPositions.Add(nextValidPosition);
                    }
                }

            }
            //6 Case 
            if (Enumerable.Range(1, 8).Contains(tempCoordinateY + 1))
            {
                if (Occupied[tempCoordinateX][tempCoordinateY + 1].ocupied == false)
                {
                    ChessPosition nextValidPosition = new ChessPosition(tempCoordinateX, tempCoordinateY + 1);
                    validNextPositions.Add(nextValidPosition);
                }
                if (Occupied[tempCoordinateX][tempCoordinateY + 1].ocupied == true)
                {
                    if (Occupied[tempCoordinateX][tempCoordinateY + 1].piece.Color != this.Color)
                    {
                        ChessPosition nextValidPosition = new ChessPosition(tempCoordinateX, tempCoordinateY + 1);
                        validNextPositions.Add(nextValidPosition);
                    }
                }

            }
            //7 Case 
            if ((Enumerable.Range('A', 'H').Contains(tempCoordinateX - 1)) && (Enumerable.Range(1, 8).Contains(tempCoordinateY + 1)))
            {
                if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY + 1].ocupied == false)
                {
                    ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX - 1), tempCoordinateY + 1);
                    validNextPositions.Add(nextValidPosition);
                }
                if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY + 1].ocupied == true)
                {
                    if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY + 1].piece.Color != this.Color)
                    {
                        ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX - 1), tempCoordinateY + 1);
                        validNextPositions.Add(nextValidPosition);
                    }
                }

            }
            //8 Case
            if (Enumerable.Range('A', 'H').Contains(tempCoordinateX - 1))
            {
                if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY].ocupied == false)
                {
                    ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX - 1), tempCoordinateY);
                    validNextPositions.Add(nextValidPosition);
                }
                if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY].ocupied == true)
                {
                    if (Occupied[(char)(tempCoordinateX - 1)][tempCoordinateY].piece.Color != this.Color)
                    {
                        ChessPosition nextValidPosition = new ChessPosition((char)(tempCoordinateX - 1), tempCoordinateY);
                        validNextPositions.Add(nextValidPosition);
                    }
                }

            }

            foreach (ChessPiece p in white)
            {
                List<ChessPosition> chessPositions = p.getValidMoves(white, black, Occupied);
                foreach (ChessPosition pos in chessPositions)
                {
                    for (int i = 0; i < validNextPositions.Count; i++)
                    {
                        if(validNextPositions[i] == pos)
                        {
                            validNextPositions.RemoveAt(i);
                        }
                    }
                }
            }

            foreach (ChessPiece p in black)
            {
                List<ChessPosition> chessPositions = p.getValidMoves(white, black, Occupied);
                foreach (ChessPosition pos in chessPositions)
                {
                    for (int i = 0; i < validNextPositions.Count; i++)
                    {
                        if (validNextPositions[i] == pos)
                        {
                            validNextPositions.RemoveAt(i);
                        }
                    }
                }
            }

            return validNextPositions;
        }

        public override void MovePiece(char x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}

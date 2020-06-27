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
        //lista na naredni validni potezi
        private List<ChessPosition> validNextPositions;

        public King(char x, int y, ChessColor color, PictureBox piece) : base(x, y, color, piece) { }
        //
        private override void checkValidMove(ChessPosition position,Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            char tempCoordinateX=position.X;
            int tempCoordinateY=position.Y;
            ChessPosition tempPosition = new ChessPosition(tempCoordinateX-1,tempCoordinateY-1);

          //1 Case
                if((Enumerable.Range('A','H').Contains(tempCoordinateX-1)) && (Enumerable.Range(1,8).Contains(tempCoordinateY-1)))
                {
                    if(Occupied[tempCoordinateX-1][tempCoordinateY-1].ocupied==false)
                    {
                      ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX-1,tempCoordinateY-1);
                      validNextPositions.Add(nextValidPosition);  
                    }
                    if(Occupied[tempCoordinateX-1][tempCoordinateY-1].ocupied==true)
                    {
                      if(Occupied[tempCoordinateX-1][tempCoordinateY-1].piece.Color!=this.Color)
                        {
                           ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX-1,tempCoordinateY-1);
                           validNextPositions.Add(nextValidPosition);  
                        }
                    }

                }
          //2 Case 
                if(Enumerable.Range(1,8).Contains(tempCoordinateY-1))
                {
                    if(Occupied[tempCoordinateX][tempCoordinateY-1].ocupied==false)
                    {
                      ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX,tempCoordinateY-1);
                      validNextPositions.Add(nextValidPosition);  
                    }
                    if(Occupied[tempCoordinateX][tempCoordinateY-1].ocupied==true)
                    {
                      if(Occupied[tempCoordinateX][tempCoordinateY-1].piece.Color!=this.Color)
                        {
                           ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX,tempCoordinateY-1);
                           validNextPositions.Add(nextValidPosition);  
                        }
                    }

                }
          //3 Case 
                if((Enumerable.Range('A','H').Contains(tempCoordinateX+1)) && (Enumerable.Range(1,8).Contains(tempCoordinateY-1)))
                {
                    if(Occupied[tempCoordinateX+1][tempCoordinateY-1].ocupied==false)
                    {
                      ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX+1,tempCoordinateY-1);
                      validNextPositions.Add(nextValidPosition);  
                    }
                    if(Occupied[tempCoordinateX+1][tempCoordinateY-1].ocupied==true)
                    {
                      if(Occupied[tempCoordinateX+1][tempCoordinateY-1].piece.Color!=this.Color)
                        {
                           ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX+1,tempCoordinateY-1);
                           validNextPositions.Add(nextValidPosition);  
                        }
                    }

                }
          //4 Case 
                if(Enumerable.Range('A','H').Contains(tempCoordinateX+1))
                {
                    if(Occupied[tempCoordinateX+1][tempCoordinateY].ocupied==false)
                    {
                      ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX+1,tempCoordinateY);
                      validNextPositions.Add(nextValidPosition);  
                    }
                    if(Occupied[tempCoordinateX+1][tempCoordinateY].ocupied==true)
                    {
                      if(Occupied[tempCoordinateX+1][tempCoordinateY].piece.Color!=this.Color)
                        {
                           ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX+1,tempCoordinateY);
                           validNextPositions.Add(nextValidPosition);  
                        }
                    }

                }
          //5 Case 
                if((Enumerable.Range('A','H').Contains(tempCoordinateX+1)) && (Enumerable.Range(1,8).Contains(tempCoordinateY+1)))
                {
                    if(Occupied[tempCoordinateX+1][tempCoordinateY+1].ocupied==false)
                    {
                      ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX+1,tempCoordinateY+1);
                      validNextPositions.Add(nextValidPosition);  
                    }
                    if(Occupied[tempCoordinateX+1][tempCoordinateY+1].ocupied==true)
                    {
                      if(Occupied[tempCoordinateX+1][tempCoordinateY+1].piece.Color!=this.Color)
                        {
                           ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX+1,tempCoordinateY+1);
                           validNextPositions.Add(nextValidPosition);  
                        }
                    }

                }
          //6 Case 
                if(Enumerable.Range(1,8).Contains(tempCoordinateY+1) )
                {
                    if(Occupied[tempCoordinateX][tempCoordinateY+1].ocupied==false)
                    {
                      ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX,tempCoordinateY+1);
                      validNextPositions.Add(nextValidPosition);  
                    }
                    if(Occupied[tempCoordinateX][tempCoordinateY+1].ocupied==true)
                    {
                      if(Occupied[tempCoordinateX][tempCoordinateY+1].piece.Color!=this.Color)
                        {
                           ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX,tempCoordinateY+1);
                           validNextPositions.Add(nextValidPosition);  
                        }
                    }

                }
          //7 Case 
                if((Enumerable.Range('A','H').Contains(tempCoordinateX-1)) && (Enumerable.Range(1,8).Contains(tempCoordinateY+1)))
                {
                    if(Occupied[tempCoordinateX-1][tempCoordinateY+1].ocupied==false)
                    {
                      ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX-1,tempCoordinateY+1);
                      validNextPositions.Add(nextValidPosition);  
                    }
                    if(Occupied[tempCoordinateX-1][tempCoordinateY+1].ocupied==true)
                    {
                      if(Occupied[tempCoordinateX-1][tempCoordinateY+1].piece.Color!=this.Color)
                        {
                           ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX-1,tempCoordinateY+1);
                           validNextPositions.Add(nextValidPosition);  
                        }
                    }

                }
         //8 Case
              if(Enumerable.Range('A','H').Contains(tempCoordinateX-1))
                {
                    if(Occupied[tempCoordinateX-1][tempCoordinateY].ocupied==false)
                    {
                      ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX-1,tempCoordinateY);
                      validNextPositions.Add(nextValidPosition);  
                    }
                    if(Occupied[tempCoordinateX-1][tempCoordinateY].ocupied==true)
                    {
                      if(Occupied[tempCoordinateX-1][tempCoordinateY].piece.Color!=this.Color)
                        {
                           ChessPosition nextValidPosition=new ChessPosition(tempCoordinateX-1,tempCoordinateY);
                           validNextPositions.Add(nextValidPosition);  
                        }
                    }

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
//lista na naredni validni potezi
        private List<ChessPosition> validNextPositions;

        private override void checkValidMove(ChessPosition position,Dictionary<char, Dictionary<int, positionInfo>> Occupied)
        {
            char tempCoordinateX=position.X;
            int tempCoordinateY=position.Y;
            ChessPosition tempPosition = new ChessPosition(tempCoordinateX-1,tempCoordinateY-1);

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

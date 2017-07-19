using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessGame.Models
{
    public enum MoveResult
    {
        Success,
        NoMove,
        MoveFromOutside,
        MoveToOutside,
        EmptySpot,
        WrongTurn,
        MoveOnOwn,
        MovePatternErr,
        MovePathErr,
        Strike,
    }



    public enum Piece
    {
        King,
        Queen,
        Rook,
        Bishop,
        Knight,
        Pawn,
        Empty,
    }

    public enum Color
    {
        Black,
        White,
        None,
    }

    public enum PresentPiece
    {
        blackKing,
        blackQueen,
        blackRook,
        blackBishop,
        blackKnight,
        blackPawn,
        whiteKing,
        whiteQueen,
        whiteRook,
        whiteBishop,
        whiteKnight,
        whitePawn,
        emptySpot,
    }

    public enum SquareColor
    {
        Black,
        White,
    }

    public struct ChessSquare
    {
        public SquareColor squareColor;
        public Piece piece;
        public Color color;
        public PresentPiece presentPiece;
    }

    public class ChessBoard
    {
        public ChessSquare[,] chessSquare { get; set; }
        public Color nextPlayer { get; set; }

        public ChessBoard()
        {
            chessSquare = new ChessSquare[8, 8];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    chessSquare[i * 2, j * 2].squareColor = SquareColor.Black;
                    chessSquare[i * 2, j * 2 + 1].squareColor = SquareColor.White;

                    chessSquare[i * 2 + 1, j * 2].squareColor = SquareColor.White;
                    chessSquare[i * 2 + 1, j * 2 + 1].squareColor = SquareColor.Black;

                }
            }

            for (int i = 0; i < 8; i++)
            {
                chessSquare[0, i].color = Color.White;

                chessSquare[1, i].piece = Piece.Pawn;
                chessSquare[1, i].color = Color.White;
                chessSquare[1, i].presentPiece = PresentPiece.whitePawn;

                chessSquare[2, i].piece = Piece.Empty;
                chessSquare[2, i].color = Color.None;
                chessSquare[2, i].presentPiece = PresentPiece.emptySpot;
                
                chessSquare[3, i].piece = Piece.Empty;
                chessSquare[3, i].color = Color.None;
                chessSquare[3, i].presentPiece = PresentPiece.emptySpot;
                
                chessSquare[4, i].piece = Piece.Empty;
                chessSquare[4, i].color = Color.None;
                chessSquare[4, i].presentPiece = PresentPiece.emptySpot;
                
                chessSquare[5, i].piece = Piece.Empty;
                chessSquare[5, i].color = Color.None;
                chessSquare[5, i].presentPiece = PresentPiece.emptySpot;

                chessSquare[6, i].piece = Piece.Pawn;
                chessSquare[6, i].color = Color.Black;
                chessSquare[6, i].presentPiece = PresentPiece.blackPawn;

                chessSquare[7, i].color = Color.Black;
            }

            chessSquare[0, 0].piece = Piece.Rook;
            chessSquare[0, 0].presentPiece = PresentPiece.whiteRook;
            chessSquare[0, 1].piece = Piece.Knight;
            chessSquare[0, 1].presentPiece = PresentPiece.whiteKnight;
            chessSquare[0, 2].piece = Piece.Bishop;
            chessSquare[0, 2].presentPiece = PresentPiece.whiteBishop;
            chessSquare[0, 3].piece = Piece.Queen;
            chessSquare[0, 3].presentPiece = PresentPiece.whiteQueen;
            chessSquare[0, 4].piece = Piece.King;
            chessSquare[0, 4].presentPiece = PresentPiece.whiteKing;
            chessSquare[0, 5].piece = Piece.Bishop;
            chessSquare[0, 5].presentPiece = PresentPiece.whiteBishop;
            chessSquare[0, 6].piece = Piece.Knight;
            chessSquare[0, 6].presentPiece = PresentPiece.whiteKnight;
            chessSquare[0, 7].piece = Piece.Rook;
            chessSquare[0, 7].presentPiece = PresentPiece.whiteRook;

            chessSquare[7, 0].piece = Piece.Rook;
            chessSquare[7, 0].presentPiece = PresentPiece.blackRook;
            chessSquare[7, 1].piece = Piece.Knight;
            chessSquare[7, 1].presentPiece = PresentPiece.blackKnight;
            chessSquare[7, 2].piece = Piece.Bishop;
            chessSquare[7, 2].presentPiece = PresentPiece.blackBishop;
            chessSquare[7, 3].piece = Piece.Queen;
            chessSquare[7, 3].presentPiece = PresentPiece.blackQueen;
            chessSquare[7, 4].piece = Piece.King;
            chessSquare[7, 4].presentPiece = PresentPiece.blackKing;
            chessSquare[7, 5].piece = Piece.Bishop;
            chessSquare[7, 5].presentPiece = PresentPiece.blackBishop;
            chessSquare[7, 6].piece = Piece.Knight;
            chessSquare[7, 6].presentPiece = PresentPiece.blackKnight;
            chessSquare[7, 7].piece = Piece.Rook;
            chessSquare[7, 7].presentPiece = PresentPiece.blackRook;

            nextPlayer = Color.White;
        }
        

        public MoveResult MovePiece(int fromRow, int fromCol, int toRow, int toCol)
        {
            var qResult = QualifyBoardMove(fromRow, fromCol, toRow, toCol);
            if (qResult != MoveResult.Success)
                return qResult;

            qResult = QualifyPiece(fromRow, fromCol);
            if (qResult != MoveResult.Success)
                return qResult;

            qResult = QualifyFreeMove(fromRow, fromCol, toRow, toCol);
            if (qResult != MoveResult.Success)
                return qResult;

            qResult = QualifyPath(fromRow, fromCol, toRow, toCol);
            if (qResult != MoveResult.Success)
                return qResult;

            qResult = QualifyDest(toRow, toCol);
            if (qResult != MoveResult.Success)
                return qResult;

            chessSquare[toRow, toCol].piece = chessSquare[fromRow, fromCol].piece;
            chessSquare[toRow, toCol].color = chessSquare[fromRow, fromCol].color;
            chessSquare[toRow, toCol].presentPiece = chessSquare[fromRow, fromCol].presentPiece;
            chessSquare[fromRow, fromCol].piece = Piece.Empty;
            chessSquare[fromRow, fromCol].color = Color.None;
            chessSquare[fromRow, fromCol].presentPiece = PresentPiece.emptySpot;

            if (nextPlayer == Color.White)
            {
                nextPlayer = Color.Black;
            }
            else
            {
                nextPlayer = Color.White;
            }

            return MoveResult.Success;
        }

        private MoveResult QualifyBoardMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            // Check against moving from outside the chess board
            if (fromRow < 0 || fromRow > 7 || fromCol < 0 || fromCol > 7)
                return MoveResult.MoveFromOutside;

            // Check against moving outside the chess board
            if (toRow < 0 || toRow > 7 || toCol < 0 || toCol > 7)
                return MoveResult.MoveToOutside;

            // Check for actual move
            if (fromRow == toRow && fromCol == toCol)
                return MoveResult.NoMove;

            return MoveResult.Success;
        }

        private MoveResult QualifyPiece(int fromRow, int fromCol)
        {
            // Check that concerned square has a piece
            if (chessSquare[fromRow, fromCol].piece == Piece.Empty)
                return MoveResult.EmptySpot;

            // Check that the piece is of correct color
            if (chessSquare[fromRow, fromCol].color != nextPlayer)
                return MoveResult.WrongTurn;

            return MoveResult.Success;
        }

        private MoveResult QualifyFreeMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            MoveResult result = MoveResult.Success;

            switch (chessSquare[fromRow, fromCol].piece)
            {
                case Piece.King:
                    if ((Math.Abs(toCol-fromCol) > 1) || (Math.Abs(toRow - fromRow) > 1))
                    {
                        result = MoveResult.MovePatternErr;
                    }
                    break;
                case Piece.Queen:
                    if ((Math.Abs(toCol - fromCol) > 0) && (Math.Abs(toRow - fromRow) > 0) &&
                        (Math.Abs(toCol - fromCol) != Math.Abs(toRow - fromRow)))
                    {
                        result = MoveResult.MovePatternErr;
                    }
                    break;
                case Piece.Rook:
                    if ((Math.Abs(toCol - fromCol) > 0) && (Math.Abs(toRow - fromRow) > 0))
                    {
                        result = MoveResult.MovePatternErr;
                    }
                    break;
                case Piece.Bishop:
                    if (Math.Abs(toCol - fromCol) != Math.Abs(toRow - fromRow))
                    {
                        result = MoveResult.MovePatternErr;
                    }
                    break;
                case Piece.Knight:
                    if ((Math.Abs(toCol - fromCol) == 0) || (Math.Abs(toRow - fromRow) == 0) ||
                        (Math.Abs(toCol - fromCol) == 1) && (Math.Abs(toRow - fromRow) != 2) ||
                        (Math.Abs(toCol - fromCol) == 2) && (Math.Abs(toRow - fromRow) != 1) ||
                        (Math.Abs(toCol - fromCol) > 2) || (Math.Abs(toRow - fromRow) > 2))
                    {
                        result = MoveResult.MovePatternErr;
                    }
                    break;
                case Piece.Pawn:
                    if ((Math.Abs(toCol - fromCol) > 1) || (Math.Abs(toRow - fromRow) > 1))
                    {
                        result = MoveResult.MovePatternErr;
                        if ((chessSquare[fromRow, fromCol].color == Color.White) &&
                            (toRow - fromRow == 2) && (toCol == fromCol))
                        {
                            result = MoveResult.Success;
                        }
                        if ((chessSquare[fromRow, fromCol].color == Color.Black) &&
                            (fromRow - toRow == 2) && (toCol == fromCol))
                        {
                            result = MoveResult.Success;
                        }
                    }
                    else
                    {
                        if (chessSquare[fromRow, fromCol].color == Color.White)
                        {
                            if (toRow <= fromRow)
                            {
                                result = MoveResult.MovePatternErr;
                            }
                        }
                        else
                        {
                            if (toRow >= fromRow)
                            {
                                result = MoveResult.MovePatternErr;
                            }
                        }
                    }
                    break;
            }
            return result;
        }

        private MoveResult QualifyPath(int fromRow, int fromCol, int toRow, int toCol)
        {
            MoveResult result = MoveResult.Success;

            switch (chessSquare[fromRow, fromCol].piece)
            {
                case Piece.King:
                    // The king has no path move limitations
                    break;
                case Piece.Queen:
                    if ((Math.Abs(toCol - fromCol) > 1) && (Math.Abs(toRow - fromRow) > 1))
                    {

                        result = MoveResult.MovePathErr;
                    }
                    break;
                case Piece.Rook:
                    if (Math.Abs(toCol - fromCol) > 1)
                    {
                        if (fromCol > toCol)
                        {
                            for (int testCol = toCol+1; testCol < fromCol; testCol++)
                            {
                                if (chessSquare[fromRow, testCol].piece != Piece.Empty)
                                {
                                    result = MoveResult.MovePathErr;
                                }
                            }
                        }
                        else
                        {
                            for (int testCol = fromCol + 1; testCol < toCol; testCol++)
                            {
                                if (chessSquare[fromRow, testCol].piece != Piece.Empty)
                                {
                                    result = MoveResult.MovePathErr;
                                }
                            }
                        }
                    }
                    if (Math.Abs(toRow - fromRow) > 1)
                    {
                        if (fromRow > toRow)
                        {
                            for (int testRow = toRow + 1; testRow < fromRow; testRow++)
                            {
                                if (chessSquare[fromRow, testRow].piece != Piece.Empty)
                                {
                                    result = MoveResult.MovePathErr;
                                }
                            }
                        }
                        else
                        {
                            for (int testRow = fromRow + 1; testRow < toRow; testRow++)
                            {
                                if (chessSquare[fromRow, testRow].piece != Piece.Empty)
                                {
                                    result = MoveResult.MovePathErr;
                                }
                            }
                        }
                    }
                    break;
                case Piece.Bishop:
                    break;
                case Piece.Knight:
                    // The knight has no path move limitations
                    break;
                case Piece.Pawn:
                    // The pawn has only path move limitations at starting position

                    break;
            }

            return result;
        }

        private MoveResult QualifyDest(int toRow, int toCol)
        {
            // Check that concerned square has no own piece
            if (chessSquare[toRow, toCol].color == nextPlayer)
                return MoveResult.MoveOnOwn;

            return MoveResult.Success;
        }
    }
}
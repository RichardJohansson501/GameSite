using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChessGame.Models;

namespace ChessGame.Controllers
{
    public class PlayController : Controller
    {
        //public ChessBoard chessBoard { get; set; }
        static private ChessBoard chessBoard;


        // GET: Play
        public ActionResult Index()
        {
            if (chessBoard == null)
            {
                chessBoard = new ChessBoard();
            }

            //ViewBag.Message = "Play game controller";

            
            return View(chessBoard);
           
        }

        public ActionResult Move(string from, string to)
        {
            // "from" and "to" parameters are expected to be in format XN, e.g. A1, C3 etc
            from = from.ToUpper();
            to = to.ToUpper(); 
            var fromRow = (int) (from.First() - 'A');
            var fromCol = (int)(from.Last() - '1');
            var toRow = (int)(to.First() - 'A');
            var toCol = (int)(to.Last() - '1');


            var moveResult = chessBoard.MovePiece(fromRow, fromCol, toRow, toCol);

            if (moveResult == MoveResult.Success)
            {
                ViewBag.LatestMove = "From: " + from + " To: " + to;
                ViewBag.MoveProgress = "Successfull move";
                
            }
            else
            {
                ViewBag.LatestMove = "From: <non>  To: <none>";
                ViewBag.MoveProgress = moveResult;
            }

            return View("Index", chessBoard);
        }
    }
}
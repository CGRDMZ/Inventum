using System.Collections.Generic;

namespace MvcApp.Models
{
    public class BoardViewModel
    {
        public List<BoardModel> Boards { get; init; }
        public BoardModel CreateBoardModel {get; init; }

    }
}
using System.Collections.Generic;

namespace MvcApp.Models
{
    public class BoardViewModel
    {
        public List<BoardMetadataModel> Boards { get; init; }
        public BoardMetadataModel CreateBoardModel {get; init; }

    }
}
using System.Collections.Generic;
using Application.Models;

namespace MvcApp.Models
{
    public class BoardViewModel
    {
        public List<BoardMetadataModel> Boards { get; init; }
        public BoardMetadataModel CreateBoardModel { get; init; }
        public List<InvitationDto> Invitations { get; init; }
        public AcceptInvitationModel acceptInvitationModel { get; init; }




    }
}
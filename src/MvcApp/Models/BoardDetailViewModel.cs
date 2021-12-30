using Application.Models;

namespace MvcApp.Models
{
    public class BoardDetailViewModel
    {
        public BoardDto Board { get; init; }

        public CreateCardGroupModel CreateCardGroupModel { get; init; }

        public CreateNewCardModel CreateNewCardModel { get; init; }
        
        public InviteUserModel InviteUserModel { get; init; }

    }
}
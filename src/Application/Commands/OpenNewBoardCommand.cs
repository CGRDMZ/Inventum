using Application.Interfaces;

namespace Application.Commands
{
    public class OpenNewBoardCommand : ICommand
    {

        public string UserId { get; }
        public string Name { get; }

        public OpenNewBoardCommand(string userId, string name, string backgroundColorRGB)
        {
            UserId = userId;
        }

    }
}

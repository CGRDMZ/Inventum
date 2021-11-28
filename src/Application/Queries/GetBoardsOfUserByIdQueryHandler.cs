using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;

namespace Application.Queries
{
    public class GetBoardsOfUserByIdQueryHandler : IRequestHandler<GetBoardsOfUserByIdQuery, List<BoardDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetBoardsOfUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<BoardDto>> Handle(GetBoardsOfUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(Guid.Parse(request.UserId));

            if (user is null) {
                throw new Exception("User not found.");
            }

            var boards = user.Boards;

            Console.WriteLine("wtf: " + boards.Count);


            var boardDtoList = new List<BoardDto>();

            foreach (var board in boards)
            {
                boardDtoList.Add(new BoardDto() {
                    BoardId = board.BoardId.ToString(),
                    Name = board.Name,
                    BgColor = board.BgColor.ToString()
                });
            }

            return boardDtoList;

        }
    }
}
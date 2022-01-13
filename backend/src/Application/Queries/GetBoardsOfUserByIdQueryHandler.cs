using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;

namespace Application.Queries
{
    public class GetBoardsOfUserByIdQueryHandler : IRequestHandler<GetBoardsOfUserByIdQuery, List<BoardDetailsDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetBoardsOfUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<BoardDetailsDto>> Handle(GetBoardsOfUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(Guid.Parse(request.UserId));

            if (user == null) {
                throw new Exception("User not found.");
            }

            var boards = user.Boards;

            Console.WriteLine("wtf: " + boards.Count);


            var boardDtoList = new List<BoardDetailsDto>();

            foreach (var board in boards)
            {
                boardDtoList.Add(new BoardDetailsDto() {
                    BoardId = board.BoardId.ToString(),
                    Name = board.Name,
                    BgColor = board.BgColor.ToString()
                });
            }

            return boardDtoList;

        }
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using Domain;
using MediatR;

namespace Application.Queries;


public class GetBoardActivitiesByIdQueryHandler : IRequestHandler<GetBoardActivitiesByIdQuery, ActivityDto[]>
{
    private readonly IBoardRepository _boardRepository;

    public GetBoardActivitiesByIdQueryHandler(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<ActivityDto[]> Handle(GetBoardActivitiesByIdQuery request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.BoardId, out Guid boardId))
        {
            throw new Exception("Invalid board id.");
        }
        var board = await _boardRepository.FindByIdAsync(Guid.Parse(request.BoardId));

        if (!board.IsAccessiableBy(Guid.Parse(request.UserId)))
        {
            throw new UserNotAuthorizedException();
        }


        int page = request.Page < 1 ? 1 : request.Page;
        int limit = request.Limit < 1 ? 1 : request.Limit;
        int offset = (page - 1) * limit;


        return board.Activities.OrderByDescending(a => a.OccuredOn)
                                .Skip(offset)
                                .Take(limit)
                                .Select(a => new ActivityDto
                                {
                                    OccuredOn = a.OccuredOn,
                                    DoneByUser = a.DoneBy.Username,
                                    Message = a.Message
                                })
                                .ToArray();
    }
}


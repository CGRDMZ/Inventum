using Application.Models;
using MediatR;

namespace Application.Queries;

public class GetBoardActivitiesByIdQuery : IRequest<ActivityDto[]>
{
    public string BoardId { get; set; }
    public string UserId { get; set; }
    public int Limit { get; set; }
    public int Page { get; set; }
}
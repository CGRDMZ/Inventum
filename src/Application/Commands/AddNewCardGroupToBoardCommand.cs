using System;
using Application.Interfaces;
using MediatR;

namespace Application.Commands
{
    public class AddNewCardGroupToBoardCommand : ICommand, IRequest
    {
        public string OwnerUserId { get; init; }
        public string BoardId { get; init; }
        public string BoardName { get; init; }
    }
}
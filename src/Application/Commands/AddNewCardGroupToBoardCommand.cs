using System;
using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.Commands
{
    public class AddNewCardGroupToBoardCommand : ICommand, IRequest<ResultWrapper<CardGroupDto>>
    {
        public string OwnerUserId { get; init; }
        public string BoardId { get; init; }
        public string CardGroupName { get; init; }
    }
}
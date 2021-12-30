using System;
using Application.Interfaces;
using MediatR;

namespace Application.Commands
{
   public class RemoveBoardCommand : IRequest<ResultWrapper<Unit>>
   {
       
        public string UserId { get; init; }
        public string BoardId { get; init; }

   }
   
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces
{
    public interface ICardService
    {
        /// <exception cref="Application.UserNotAuthorizedException">
        ///     Thrown when the user is not authorized to access to cards.
        /// </exception>
        /// <exception cref="Application.ResourceNotFoundException">
        ///     Thrown when the resource does not exist.
        /// </exception>
        IEnumerable<Card> GetCards(Board board, Guid userId, Guid cardGroupId, IEnumerable<Guid> cardIds);
    }
}
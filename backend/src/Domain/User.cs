using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        private List<Board> boards = new List<Board>();

        public IReadOnlyCollection<Board> Boards => boards.AsReadOnly();


        private List<Invitation> invitations = new List<Invitation>();
        public IReadOnlyCollection<Invitation> Invitations => invitations.AsReadOnly();

        public User() {}

        public User(Guid userId, string username) {
            if (username.Length < 5) {
                throw new ArgumentException("Username cannot be smaller than 5 letters.");
            }

            UserId = userId;
            Username = username;
        }

        public void ReceiveInvitation(Invitation invitation) {
            if (invitation == null) {
                throw new ArgumentNullException(nameof(Invitation));
            }

            var alreadyOwner = boards.Find(b => b == invitation.InvitedTo) != null;
            if (alreadyOwner) {
                throw new DomainException("User is already a owner of this board.");
            }

            var alreadyInvited = invitations.Find(i => i.InvitedTo == invitation.InvitedTo) != null;
            if (alreadyInvited) {
                throw new DomainException("User has already received an invitation to this board.");
            }


            invitations.Add(invitation);
        }

        public void HandleInvitation(Invitation invitation, InvitationResult result) {
            var inv = invitations.Single(i => i == invitation);

            inv.Handle(result);
        }




        public void OpenNewBoard() {
            Board board = Board.CreateEmptyBoard(this);

            boards.Add(board);
        }

        public static User New(Guid id, string username) {
            return new User(id, username);
        }
    }
}
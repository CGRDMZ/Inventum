using System;

namespace Domain
{
    public enum InvitationResult
    {
        ACCEPT,
        REJECT
    }
    public class Invitation
    {
        private Invitation()
        {
        }

        public Invitation(Board invitedTo)
        {
            InvitedTo = invitedTo;
            IsDeleted = false;
        }

        public Guid InvitationId { get; private set; }
        public Board InvitedTo { get; private set; }
        public User InvitedUser { get; private set; }
        public bool IsDeleted { get; private set; }

        public void Handle(InvitationResult res)
        {
            IsDeleted = true;

            switch (res)
            {
                case InvitationResult.ACCEPT:
                    InvitedTo.AddNewOwner(InvitedUser);
                    break;
                case InvitationResult.REJECT:
                    break;
            }
        }

        public static Invitation New(Board invitedTo)
        {
            return new Invitation(invitedTo);
        }

    }
}
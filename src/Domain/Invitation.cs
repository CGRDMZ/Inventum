using System;

namespace Domain
{
    public class Invitation
    {
        public Guid InvitationId { get; private set; }
        public Board InvitedTo { get; private set; }

        
        
    }
}
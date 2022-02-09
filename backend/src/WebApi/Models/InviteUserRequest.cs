namespace WebApi.Models
{
    public class InviteUserToBoardRequest
    {
        public string BoardId { get; set; }
        public string InvitedUserUsername { get; set; }
    }
}
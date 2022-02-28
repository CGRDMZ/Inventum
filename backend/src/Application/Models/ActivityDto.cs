using System;

namespace Application.Models
{
    public class ActivityDto
    {
        public DateTime OccuredOn { get; init; }
        public string DoneByUser { get; init; }
        public string Message { get; init; }

    }
}

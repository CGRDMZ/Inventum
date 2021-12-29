
using System;

namespace Domain
{

    public class Activity
    {
        public Guid ActivityId { get; private set; }
        public DateTime OccuredOn { get; private set; }
        public User DoneBy { get; private set; }
        public string Message { get; private set; }
        public Board BelongsTo { get; private set; }

        private Activity(){ }

        public Activity(User doneBy, string message, Board belongsTo)
        {
            ActivityId = Guid.NewGuid();
            OccuredOn = DateTime.Now;

            Message = message ?? "Something happened.";
            BelongsTo = belongsTo ?? throw new ArgumentNullException(nameof(Board));
            DoneBy = doneBy ?? throw new ArgumentNullException(nameof(User));

        }

        public static Activity New(User doneBy, string message, Board belongsTo)
        {
            return new Activity(doneBy, message, belongsTo);
        }




    }



}
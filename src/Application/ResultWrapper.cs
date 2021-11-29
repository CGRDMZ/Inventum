using System.Collections.Generic;

namespace Application
{
    public class ResultWrapper<TData>
    {
        public bool Success => Errors.Count == 0;

        public TData Data { get; init; }

        public List<string> Errors { get; init; }

    }
}
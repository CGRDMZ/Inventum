using System.Collections.Generic;

namespace Application
{
    public class ResultWrapper<TData>
    {
        public bool Success => Errors.Count == 0;

        public TData Data { get; init; }

        private List<string> errors = new List<string>();
        public IReadOnlyCollection<string> Errors => errors.AsReadOnly();

        public ResultWrapper<TData> AddError(string errorMessage)
        {
            errors.Add(errorMessage);
            return this;
        }

    }
}
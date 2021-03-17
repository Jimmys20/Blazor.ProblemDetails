using System;

namespace Blazor.ProblemDetails.Client
{
    public class ProblemDetailsException : Exception
    {
        public ProblemDetailsException(ProblemDetails problem)
        {
            Problem = problem;
        }

        public ProblemDetails Problem { get; }
    }
}

using System;
namespace TBGraph.Graphing.Exceptions
{
    public class DuplicateNodeException : Exception
    {
        public DuplicateNodeException() : base() { }

        public DuplicateNodeException(string message) : base(message) { }
    }
}

namespace EvaluationAPI.BLL.Exceptions
{
    public class NonExistingResultException : EvaluationException
    {
        public NonExistingResultException()
            : base()
        {
        }

        public NonExistingResultException(string message)
            : base(message)
        {
        }
    }
}

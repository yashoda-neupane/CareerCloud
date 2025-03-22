namespace CareerCloud.BusinessLogicLayer
{
    public class ValidationException :Exception
    {
        public int Code { get; }
        public ValidationException(int code, string message) : base(message)
        {
            {
                Code = code;
            }
        }
    }
}

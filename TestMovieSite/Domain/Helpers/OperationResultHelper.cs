namespace TestMovieSite.Domain.Helpers
{
    public static class OperationResultHelper
    {
        public static string GetMessage(bool isSuccess)
        {
            return isSuccess ? "The operation completed successfully" : "Something went wrong";
        }
    }
}
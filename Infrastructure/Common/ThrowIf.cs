using System;

namespace Infrastructure.Common
{
    public static class ThrowIf
    {
        public static void IsNull(object argument, string argumentName, string message)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName, message);
            }
        }

        public static Exception GetInnerMostException(Exception ex)
        {
            IsNull(ex, "Exception", "Error");

            while (ex.InnerException != null) ex = ex.InnerException;

            return ex;
        }
    }
}

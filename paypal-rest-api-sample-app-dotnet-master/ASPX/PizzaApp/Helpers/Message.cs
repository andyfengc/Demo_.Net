using System;

namespace PizzaApp
{
    public static class Message
    {
        private static Exception exceptionLast;

        public static Exception LastException
        {
            get
            {
                return exceptionLast;
            }
            set
            {
                if (value != exceptionLast)
                {
                    exceptionLast = value;
                }
            }
        }
    }
}
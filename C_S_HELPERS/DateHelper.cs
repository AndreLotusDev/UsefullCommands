using System;

namespace Helpers
{
    public static class DateHelper
    {
        public static string ShortDateAndHour()
        {
            return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }
    }
}

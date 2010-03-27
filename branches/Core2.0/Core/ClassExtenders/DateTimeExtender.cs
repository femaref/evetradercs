using System;

namespace Core.ClassExtenders
{
    public static class DateTimeExtender
    {
        public static DateTime LocalizeEveTime(this DateTime value)
        {
            return value.ToLocalTime();
        }
    }
}

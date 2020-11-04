using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime StartOfWeek(this DateTime date)
        {
            return date.AddDays(DayOfWeek.Monday - date.DayOfWeek).Date;
        }

        public static DateTime EndOfWeek(this DateTime date)
        {
            return date.StartOfWeek().AddDays(6);
        }
    }
}

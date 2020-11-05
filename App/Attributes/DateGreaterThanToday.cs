using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Attributes
{
    public class DateGreaterThanTodayAttribute : ValidationAttribute
    {
        public DateGreaterThanTodayAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            return dt.Date > DateTime.Today;
        }
    }
}

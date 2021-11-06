using System;

namespace TopCourseWorkBl.BusinessLayer.Extensions
{
    public static class DayFromDateTimeExtension
    {
        public static int CalculateDayFromDateTime(this DateTime date, DateTime? refDate = null)
        {
            refDate ??= new DateTime(0001, 01, 01);
            return (date - refDate).Value.Days + 1;
        }
    }
}
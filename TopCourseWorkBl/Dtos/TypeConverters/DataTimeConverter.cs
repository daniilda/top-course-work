using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TopCourseWorkBl.Dtos.TypeConverters
{
    public class DateTimeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var newText = text.Split(' ');
            var newDateTime = new DateTime(0001, 01, 01).AddDays(Convert.ToDouble(newText[0])-1);
            return newDateTime.Add(TimeSpan.Parse(newText[1]));
        }
    }
}
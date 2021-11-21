using System;
using System.Globalization;
using System.Threading;
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
            DateTime newDateTime;
            newDateTime = newText[0] == "0"
                ? new DateTime(0001, 01, 01)
                : new DateTime(0001, 01, 01).AddDays(Convert.ToInt32(newText[0]));
            newText[1] = newText[1].Replace(":60", ":59");
            return newDateTime.Add(TimeSpan.Parse(newText[1]));
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var dateTime = (DateTime)value;
            //var day = dateTime.CalculateDayFromDateTime().ToString(); //TODO: Will it be needed?
            return dateTime.ToLongTimeString();
        }
    }
}
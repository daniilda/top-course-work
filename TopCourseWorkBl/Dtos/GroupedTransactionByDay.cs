using System;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using TopCourseWorkBl.BusinessLayer.Extensions;

namespace TopCourseWorkBl.Dtos
{
    public class GroupedTransactionByDay
    {
        private decimal _averageAmount;
        [Name("day")] public int Day => DateTime.CalculateDayFromDateTime();
        [Name("mcc_code")] public int MccCode { get; set; }
        [Name("avg_amount")]
        public decimal AverageAmount
        {
            get => _averageAmount;
            set => _averageAmount = Math.Round(value,2);
        }
        [JsonIgnore] [Ignore] public DateTime DateTime { get; set; }
    }
}
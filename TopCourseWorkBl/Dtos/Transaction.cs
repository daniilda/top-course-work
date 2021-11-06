using System;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using TopCourseWorkBl.Dtos.TypeConverters;

namespace TopCourseWorkBl.Dtos
{
    public class Transaction
    {
        [Name("customer_id")]
        public long CustomerId { get; set; }
        [Name("tr_datetime")]
        [TypeConverter(typeof(DateTimeConverter))]
        [JsonIgnore]
        public DateTime DateTime { get; set; }
        
        [Name("mcc_code")]
        public int TransactionMccCode { get; set; }
        [Name("tr_type")]
        public int TransactionType { get; set; }
        [Name("amount")]
        public int Amount { get; set; }
        [Name("term_id")]
        public long TerminalId { get; set; }
    }
}
using System.Collections.Generic;
using TopCourseWorkBl.BusinessLayer.CheckStrategy.Abstractions;
using TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Abstractions;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.BusinessLayer.CsvParseStrategy
{
    public class CsvParserResponse : IParseData, ICheckResponse
    {
        public string Path { get; set; } = null!;
        public List<Customer>? Customers { get; set; }
        public List<MccCode>? MccCodes { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public List<Type>? Types { get; set; }
    }
}
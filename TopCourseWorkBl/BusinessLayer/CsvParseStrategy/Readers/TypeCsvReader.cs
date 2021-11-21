using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Abstractions;
using TopCourseWorkBl.BusinessLayer.Extensions;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Readers
{
    public class TypeCsvReader : BaseCsvParser
    {
        public override (CsvParserResponse?, IParseStrategy) Parse(IParseData? parseData)
        {
            var data = parseData!.ThrowIfIncorrectType<CsvParserResponse>();
            var (returnData, parser) = ParseNext(parseData!);
            parser = returnData is null 
                ? this 
                : parser;
            returnData ??= data;
            using var reader = new StreamReader(data.Path);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                DetectDelimiter = true,
            };
            using var csv = new CsvReader(reader,config);
            csv.Read();
            csv.ReadHeader();
            if (csv.HeaderRecord[0].Equals("tr_type"))
                returnData.Types = csv.GetRecords<Type>().ToList();
            return (returnData, parser);
        }
    }
}
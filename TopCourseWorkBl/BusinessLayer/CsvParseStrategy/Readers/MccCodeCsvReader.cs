﻿using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Abstractions;
using TopCourseWorkBl.BusinessLayer.Extensions;
using TopCourseWorkBl.Dtos;

namespace TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Readers
{
    public class MccCodeCsvReader : BaseCsvParser
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
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Read();
            csv.ReadHeader();
            if (csv.HeaderRecord[0].Equals("mcc_code"))
                returnData.MccCodes = csv.GetRecords<MccCode>().ToList();
            return (returnData, parser);
        }
    }
}
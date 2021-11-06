namespace TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Abstractions
{
    public interface IParseStrategy
    {
        IParseStrategy SetNext(IParseStrategy parseStrategy);
        (CsvParserResponse?, IParseStrategy) Parse(IParseData parseData);
    }
}
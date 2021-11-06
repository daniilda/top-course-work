namespace TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Abstractions
{
    public abstract class BaseCsvParser : IParseStrategy
    {
        private IParseStrategy? _nextStrategy;

        public IParseStrategy SetNext(IParseStrategy nextStrategy)
            => _nextStrategy = nextStrategy;

        public abstract (CsvParserResponse?, IParseStrategy) Parse(IParseData parseData);

        protected (CsvParserResponse?, IParseStrategy) ParseNext(IParseData parseData)
            => _nextStrategy?.Parse(parseData) ?? (null, this);
    }
}
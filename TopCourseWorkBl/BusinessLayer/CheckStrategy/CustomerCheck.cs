using TopCourseWorkBl.BusinessLayer.CheckStrategy.Abstractions;
using TopCourseWorkBl.BusinessLayer.CsvParseStrategy;
using TopCourseWorkBl.BusinessLayer.Extensions;

namespace TopCourseWorkBl.BusinessLayer.CheckStrategy
{
    public class CustomerCheck : BaseCheck
    {
        public override (bool, ICheckStrategy) Check(ICheckResponse checkResponse)
        {
            var data = checkResponse.ThrowIfIncorrectType<CsvParserResponse>();
            var (isOk, check) = CheckNext(checkResponse);
            return !isOk
                ? (false, check)
                : (data.Customers != null, this);

        }
    }
}
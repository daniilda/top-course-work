using CsvHelper.Configuration.Attributes;

namespace TopCourseWorkBl.Dtos
{
    public class MccCode
    {
        [Name("mcc_code")] public int MccCodeId { get; set; }
        [Name("mcc_description")] public string? Description { get; set; }
    }
}
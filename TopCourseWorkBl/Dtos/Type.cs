using CsvHelper.Configuration.Attributes;

namespace TopCourseWorkBl.Dtos
{
    public class Type
    {
        [Name("tr_type")]
        public int TypeId { get; set; }
        [Name("tr_description")]
        public string? Description { get; set; }
    }
}
using CsvHelper.Configuration.Attributes;

namespace TopCourseWorkBl.Dtos
{
    public class Customer
    {
        [Name("customer_id")] public long CustomerId { get; set; }
        [Name("gender")] public bool? Gender { get; set; }
    }
}
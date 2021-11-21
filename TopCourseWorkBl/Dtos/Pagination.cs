namespace TopCourseWorkBl.Dtos
{
    public record PaginationRequest(int Count, int Offset);

    public record PaginationResponse(int Total);
}
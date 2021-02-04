namespace SeoulAir.Command.Domain.Dtos
{
    public class Paginator
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string OrderBy { get; set; } = "Id";
        public bool IsDescending { get; set; } = false;
    }
}
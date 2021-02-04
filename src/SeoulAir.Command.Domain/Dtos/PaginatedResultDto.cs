using System.Collections.Generic;

namespace SeoulAir.Command.Domain.Dtos
{
    public class PaginatedResultDto<TDto>
    {
        public List<TDto> Result { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}

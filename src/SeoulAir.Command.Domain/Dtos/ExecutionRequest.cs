using System.Collections.Generic;

namespace SeoulAir.Command.Domain.Dtos
{
    public class ExecutionRequest
    {
        public string CommandName { get; set; }
        public IEnumerable<string> Parameters { get; set; }
    }
}
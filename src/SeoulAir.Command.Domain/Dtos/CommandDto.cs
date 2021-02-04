namespace SeoulAir.Command.Domain.Dtos
{
    public class CommandDto : BaseDtoWithId
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Controller { get; set; }
        public string Endpoint { get; set; }
        public int NumOfParameters { get; set; }
        public string HttpMethod { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeoulAir.Command.Repositories.Entities
{
    public class Command : BaseEntityWithId
    {
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }
        
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }
        
        [Range(1024, 65535)]
        public int Port { get; set; }
        
        [Column(TypeName = "nvarchar(30)")]
        public string Controller { get; set; }
        
        [Column(TypeName = "nvarchar(30)")]
        public string Endpoint { get; set; }
        
        [Range(0, 30)]
        public int NumOfParameters { get; set; }
        
        [Column(TypeName = "nvarchar(20)")]
        public string HttpMethod { get; set; }
    }
}
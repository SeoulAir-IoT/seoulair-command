using System;
using System.ComponentModel.DataAnnotations;

namespace SeoulAir.Command.Repositories.Entities
{
    public class BaseEntityWithId
    {
        [Key]
        public Guid Id { get; set; }
    }
}
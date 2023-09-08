using System.ComponentModel.DataAnnotations;

namespace MinimalApiDotNetCore.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
    }
}

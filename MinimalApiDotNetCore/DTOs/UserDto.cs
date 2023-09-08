using MinimalApiDotNetCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace MinimalApiDotNetCore.DTOs
{
    public class UserDto
    {
        public UserDto()
        {
        }
        public UserDto(User user) => (Id,Name,IsComplete) = (user.Id,user.Name,user.IsComplete);
        
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }


    }
}

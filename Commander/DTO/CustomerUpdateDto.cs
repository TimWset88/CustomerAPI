using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.DTO
{
    public class CustomerUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string CustomerType { get; set; }
    }
}
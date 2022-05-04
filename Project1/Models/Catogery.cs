using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Catogery
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Display order must be greater than Zero")]
        public int displayOrder  { get; set; }
    }
}

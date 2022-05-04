using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class ApplicationType
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Models
{
    public class Product
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        public string shourtDescription { set; get; }
        public string Description { set; get; }
        [Required]
        [Range(1, int.MaxValue)]
        public double Price { set; get; }
        public string Image { set; get; }
        [Display(Name ="Catogery Type")]
        public int CatogeryId { set; get; }
        [ForeignKey("CatogeryId")]
        public virtual Catogery Catogery { get; set; }
        [Display(Name = "Application Type")]
        public int ApplicationTypeId { set; get; }
        [ForeignKey("ApplicationTypeId")]
        public virtual ApplicationType applicationType { get; set; }

    }
}

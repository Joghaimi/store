using System.Collections.Generic;

namespace Project1.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<Catogery> catogeries { get; set; }
    }
}

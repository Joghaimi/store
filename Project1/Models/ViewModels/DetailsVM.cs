namespace Project1.Models.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            product = new Product();

        }
        public Product product { get; set; }
        public bool ExistInCart { get; set; }
    }
}

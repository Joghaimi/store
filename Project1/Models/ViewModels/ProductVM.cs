using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Project1.Models.ViewModels
{
    public class ProductVM
    {
        public Product prodct{ set; get; }
        public IEnumerable<SelectListItem> CategoryDropDown { set; get; }
        public IEnumerable<SelectListItem> ApplicationTyptDropDown { set; get; }
    }
}

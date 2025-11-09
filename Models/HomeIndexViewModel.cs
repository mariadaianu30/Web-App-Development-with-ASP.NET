using Laborator5.Models;
using System.Collections.Generic;

namespace Laborator5.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Article> Articles { get; set; }
        public List<Category> Categories { get; set; }
    }
}
